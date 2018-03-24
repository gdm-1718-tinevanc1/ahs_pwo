using System;
using System.IO; 

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;

using Database;
using Models;
using Models.Security;
using System.Web;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ProjectfileController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get PDF from the API";
        private const string FAILGETENTITYBYID = "Failed to get PDF from the API by Id: {0}";
        private IHostingEnvironment _environment;

    
        public ProjectfileController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, IHostingEnvironment environment):base(applicationDbContext, userManager) 
        {
            _environment = environment;

        }


        //        public async Task<IActionResult> GetProjectById(Int16 projectId)

        [HttpGet("{projectId:int}", Name = "GetProjectfileController")]
        public async Task<IActionResult> GetProjectfileController(Int16 projectId)
        {
            var model = await ApplicationDbContext.Projects.Include(f => f.Financingforms).ThenInclude(f => f.Financingform)
            .Include(m => m.Mediums).Include(m => m.Links).Include(p => p.Partners).Include(m => m.Profiles).ThenInclude(p => p.Profile)
            .Include(p => p.Budget).Include(t => t.Tags).Include(p => p.Publications).Include(p => p.Participants).ThenInclude(p => p.Participant)
            .FirstOrDefaultAsync(o => o.Id == projectId);

            /* JsonConvert.DeserializeObject(model.Title);
            JsonConvert.DeserializeObject(model.Subtitle);
            JsonConvert.DeserializeObject(model.Description);
            JsonConvert.DeserializeObject(model.Abstract);
            JsonConvert.DeserializeObject(model.Shorttitle); */


            /* model.Title = JSON.parse(model.Title);
            model.Subtitle = JSON.parse(model.Subtitle);
            model.Description = JSON.parse(model.Description);
            model.Abstract = JSON.parse(model.Abstract);
            model.Shorttitle = JSON.parse(model.Shorttitle);
 */

            var uploadPath = Path.Combine(_environment.WebRootPath, "pdf");
            Directory.CreateDirectory(Path.Combine(uploadPath, projectId.ToString()));

            System.IO.FileStream fs = new FileStream(Path.Combine(uploadPath, projectId.ToString(), "Productiedossier_" +  projectId.ToString() +".pdf"), FileMode.Create);
            // MemoryStream memory = new MemoryStream();

            // Create an instance of the document class which represents the PDF document itself.
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            // PdfWriter.GetInstance(document,memory);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            // Add meta information to the document
            /*document.AddAuthor("Ahs-PWO");
            document.AddKeywords("PDF PWO");
            document.AddSubject("Document subject - Describing the steps creating a PDF document");
            document.AddTitle("The document title - PDF creation using iTextSharp");
*/
            // Open the document to enable you to write to the document
            document.Open();

            /* Frontpage **/
            document.NewPage();
            
            var Title = JsonConvert.DeserializeObject<Validate>(model.Title);
            var Subtitle =  JsonConvert.DeserializeObject<Validate>(model.Subtitle);
            var Description =  JsonConvert.DeserializeObject<Validate>(model.Description);
            var Abstract =  JsonConvert.DeserializeObject<Validate>(model.Abstract);
            var Shorttitle =  JsonConvert.DeserializeObject<Validate>(model.Shorttitle);

                //icoon
            Paragraph title = new Paragraph(Title.nl.value);
            Paragraph subtitle = new Paragraph(Subtitle.nl.value);
            Paragraph duration = new Paragraph(model.Startdate.ToString() + " - " + model.Enddate.ToString());

            title.Alignment = Element.ALIGN_CENTER;
            subtitle.Alignment = Element.ALIGN_CENTER;
            duration.Alignment = Element.ALIGN_CENTER;

            title.Font.SetStyle(Font.BOLD);
            title.Font.Size = 20;

            document.Add(title);
            document.Add(subtitle);
            document.Add(duration);

            /* second page **/
            document.NewPage();
            document.Add(new Paragraph(Title.nl.value));
            document.Add(new Paragraph(Subtitle.nl.value));
            document.Add(new Paragraph(model.Startdate.ToString() + " - " + model.Enddate.ToString()));
            document.Add(new Paragraph(Description.nl.value));
            document.Add(new Paragraph(Abstract.nl.value));

 
            /* third page **/
            document.NewPage();
            document.Add(new Paragraph("PROJECTMEDEWERKERS"));
            List participantList = new List(List.UNORDERED); 
            foreach (var participant in model.Participants.ToList()){
                participantList.Add(new ListItem(participant.Participant.Name)); 
            }
            document.Add(participantList);
            document.Add(new Paragraph(" "));


            document.Add(new Paragraph("PARTNERS"));
            List partnerList = new List(List.UNORDERED); 
            foreach (var partner in model.Partners.ToList()){
                partnerList.Add(new ListItem(partner.Name)); 
            }
            document.Add(partnerList);
            document.Add(new Paragraph(" "));

            
            document.Add(new Paragraph("FINANCIERINGSVORMEN"));
            List financingformList = new List(List.UNORDERED); 
            foreach (var financingform in model.Financingforms.ToList()){
                financingformList.Add(new ListItem(financingform.Financingform.Name)); 
            }
            document.Add(participantList);
            document.Add(new Paragraph(" "));


            document.Add(new Paragraph("TAGS"));
            List tagList = new List(List.UNORDERED); 
            foreach (var tag in model.Tags.ToList()){
                tagList.Add(new ListItem(tag.Name)); 
            }
            document.Add(tagList);
            document.Add(new Paragraph(" "));


            document.Add(new Paragraph("LINKS"));
            List linkList = new List(List.UNORDERED); 
            foreach (var link in model.Links.ToList()){
                linkList.Add(new ListItem(link.Name)); 
            }
            document.Add(linkList);
            document.Add(new Paragraph(" "));


            /* fourth page **/
            document.NewPage();
            document.Add(new Paragraph("Totaal budget"));
            document.Add(new Paragraph("€ " + model.Budget.TotalBudget.ToString()));
            document.Add(new Paragraph("Budget Artevelde hogeschool"));
            document.Add(new Paragraph("€ " + model.Budget.ArteveldeBudget.ToString()));
            document.Add(new Paragraph("Investeringsbudget"));
            document.Add(new Paragraph("€ " + model.Budget.InvestmentBudget.ToString()));
            document.Add(new Paragraph("Werkingsbudget"));
            document.Add(new Paragraph("€ " + model.Budget.OperatingBudget.ToString()));
            document.Add(new Paragraph("Personeelbudget"));
            document.Add(new Paragraph("€ " + model.Budget.StaffBudget.ToString()));
            
            // var json = JsonConvert.SerializeObject(model.Budget);
             // document.Add(new Paragraph(json));

            /* fifth page **/
            document.NewPage();
            document.Add(new Paragraph("MEDIA"));
             // memory.Position = 0;


            // Close the document, close the writer instance and always close open filehandles explicity
            document.Close();
            writer.Close();
            fs.Close(); 


       

/*
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID);
                return NotFound(msg);
            } */

            return File(System.IO.File.OpenRead(uploadPath + "/" + projectId.ToString() + "/Productiedossier_" +  projectId.ToString() +".pdf"), contentType: "application/pdf");
        }
    }
}

//Export/Print/Output the PDF File directly to the Client without saving it to the Disk:

/*
We can create PDF File in memory by creatig System.IO.MemorySystem's object. Lets see:

Hide   Copy Code
using (MemoryStream ms = new MemoryStream())
using(Document document = new Document(PageSize.A4, 25, 25, 30, 30))
using(PdfWriter writer = PdfWriter.GetInstance(document, ms))
{
    document.Open();
    document.Add(new Paragraph("Hello World"));
    document.Close();
    writer.Close();
    ms.Close();
    Response.ContentType = "pdf/application";
    Response.AddHeader("content-disposition", "attachment;filename=First_PDF_document.pdf");
    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
} */