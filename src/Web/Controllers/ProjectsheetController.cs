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

// using System.Net.Http.Headers;
// using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

using Database;
using Models;
using Models.Security;
using System.Web;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsheetController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get PDF from the API";
        private const string FAILGETENTITYBYID = "Failed to get PDF from the API by Id: {0}";
        private IHostingEnvironment _environment;

    
        public ProjectsheetController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, IHostingEnvironment environment):base(applicationDbContext, userManager) 
        {
            _environment = environment;

        }


        //        public async Task<IActionResult> GetProjectById(Int16 projectId)

        [HttpGet("{projectId:int}", Name = "GetProjectsheetController")]
        public async Task<IActionResult> GetProjectsheetController(Int16 projectId)
        {
            var model = await ApplicationDbContext.Projects.Include(f => f.Financingforms).ThenInclude(f => f.Financingform)
            .Include(m => m.Mediums).Include(m => m.Links).Include(p => p.Partners).Include(m => m.Profiles).ThenInclude(p => p.Profile)
            .Include(p => p.Budget).Include(t => t.Tags).Include(p => p.Publications).Include(p => p.Participants).ThenInclude(p => p.Participant)
            .FirstOrDefaultAsync(o => o.Id == projectId);

            var uploadPath = Path.Combine(_environment.WebRootPath, "pdf");
            Directory.CreateDirectory(Path.Combine(uploadPath, projectId.ToString()));

            System.IO.FileStream fs = new FileStream(Path.Combine(uploadPath, projectId.ToString(), "Projectfiche_" +  projectId.ToString() +".pdf"), FileMode.Create);

            // Create an instance of the document class which represents the PDF document itself.
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            // Create an instance to the PDF file by creating an instance of the PDF 
            // Writer class using the document and the filestrem in the constructor.
             PdfWriter writer = PdfWriter.GetInstance(document, fs);
 
            /*  MemoryStream memory = new MemoryStream();

            PdfWriter.GetInstance(document,memory);

              */

            // Add meta information to the document
            /*document.AddAuthor("Ahs-PWO");
            document.AddKeywords("PDF PWO");
            document.AddSubject("Document subject - Describing the steps creating a PDF document"); */
            // document.AddTitle("Projectfiche_" +  projectId.ToString());
            // Open the document to enable you to write to the document
            document.Open();

            var Title = JsonConvert.DeserializeObject<Validate>(model.Title);
            var Subtitle =  JsonConvert.DeserializeObject<Validate>(model.Subtitle);
            var Description =  JsonConvert.DeserializeObject<Validate>(model.Description);
            var Abstract =  JsonConvert.DeserializeObject<Validate>(model.Abstract);
            var Shorttitle =  JsonConvert.DeserializeObject<Validate>(model.Shorttitle);
            


            //icoon
            Paragraph title = new Paragraph(Title.nl.value);
            Paragraph subtitle = new Paragraph(Subtitle.nl.value);
            Paragraph duration = new Paragraph(model.Startdate.ToString() + " - " + model.Enddate.ToString());
            Paragraph description = new Paragraph(Description.nl.value);

            title.Alignment = Element.ALIGN_CENTER;
            subtitle.Alignment = Element.ALIGN_CENTER;
            duration.Alignment = Element.ALIGN_CENTER;
            description.Alignment = Element.ALIGN_CENTER;

            title.Font.SetStyle(Font.BOLD);
            title.Font.Size = 20;

            document.Add(title);
            document.Add(subtitle);
            document.Add(duration);
            document.Add(description);


            document.Close();
            writer.Close();
            fs.Close();

            return File(System.IO.File.OpenRead(uploadPath + "/" + projectId.ToString() + "/Projectfiche_" +  projectId.ToString() +".pdf"), contentType: "application/pdf");
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