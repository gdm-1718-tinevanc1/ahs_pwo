using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OpenIddict;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;
using Microsoft.Extensions.Logging;

// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using OpenIddict;
// using Npgsql.EntityFrameworkCore.PostgreSQL;
// using Models.Security;

namespace Database
{
    public class ApplicationDbContextSeeder
    {
       public static void Initialize(IServiceProvider serviceProvider)
        {
            // context.Database.EnsureCreated();
           using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
           {
                var random = new Random();
                var lorem = new Bogus.DataSets.Lorem(locale: "nl");

                if(!context.Financingforms.Any())
                {
                  context.Financingforms.AddRange(new List<Financingform>()
                    {
                      new Financingform { Name = "PWO"},
                      new Financingform { Name = "Extern"}
                    });
                  context.SaveChangesAsync().Wait();
                }
      
                if(!context.States.Any())
                {
                  context.States.AddRange(new List<Status>()
                    {
                      new Status { Name = "Feedback"},
                      new Status { Name = "In progress"},
                      new Status { Name = "Validated"},
                      new Status { Name = "Published"}
                    });
                  context.SaveChangesAsync().Wait();
                }

                if(!context.Participants.Any())
                {
                  context.Participants.AddRange(new List<Participant>()
                    {
                      new Participant { Name = "Test", TypeParticipant = TypeParticipant.ODC},
                      new Participant { Name = "Test2", TypeParticipant = TypeParticipant.Projectmedewerker},
                      new Participant { Name = "Test3", TypeParticipant = TypeParticipant.Opleiding}
                    });
                  context.SaveChangesAsync().Wait();
                }

                if(!context.Publications.Any())
                {
                  context.Publications.AddRange(new List<Publication>()
                    {
                      new Publication { Name = "Test"},
                      new Publication { Name = "Test2"}
                    });
                  context.SaveChangesAsync().Wait();
                }


               /* if(!context.Spearheads.Any())
                {
                  context.Spearheads.AddRange(new List<Spearhead>()
                    {
                      new Spearhead { Name = "ondernemingschap", Description = "ondernemerschap, organisatie en competenties"},
                      new Spearhead { Name = "zorg", Description = "zorg, welzijn en zelfredzaamheid"}
                    });
                  context.SaveChangesAsync().Wait();
                } */


                if(!context.Budgets.Any()) 
                {
                    var budgetSkeleton = new Faker<Models.Budget>()
                        .RuleFor(c => c.TotalBudget, f => f.Random.Number(500, 2000))
                        .RuleFor(c => c.ArteveldeBudget, f => f.Random.Number(500, 2000))
                        .RuleFor(c => c.OperatingBudget, f => f.Random.Number(500, 2000))
                        .RuleFor(c => c.InvestmentBudget, f => f.Random.Number(500, 2000))
                        .RuleFor(c => c.StaffBudget, f => f.Random.Number(500, 2000))
                        .FinishWith((f, u) =>
                        {
                            Console.WriteLine("Budget created with Bogus: {0}!", u.Id);
                        });
                   
                    var budgets = new List<Budget>();

                    for(var i = 0; i < 50; i++)
                    {
                        var budget = budgetSkeleton.Generate();

                        budgets.Add(budget);
                    }

                    context.Budgets.AddRange(budgets);
                    context.SaveChangesAsync().Wait();
                }

                if(!context.Projects.Any()) 
                {
                    var projectSkeleton = new Faker<Models.Project>()
                        .RuleFor(c => c.Startdate, f => f.Date.Soon())
                        .RuleFor(c => c.Enddate, f => f.Date.Future())
                        .RuleFor(c => c._Title, f => objectValue(lorem.Sentence()))
                        .RuleFor(c => c._Shorttitle, f => objectValue(lorem.Slug()))
                        .RuleFor(c => c._Subtitle, f => objectValue(lorem.Sentence()))
                        .RuleFor(c => c._Description, f => objectValue(lorem.Sentence(3)))
                        .RuleFor(c => c._Abstract, f => objectValue(lorem.Paragraphs(random.Next(1, 3))))

                        .RuleFor(c => c._PartnerValidate, f => objectValidate())
                        .RuleFor(c => c._ParticipantValidate, f => objectValidate())
                        .RuleFor(c => c._FinancingformValidate, f => objectValidate())
                        .RuleFor(c => c._LinkValidate, f => objectValidate())
                        .RuleFor(c => c._BudgetValidate, f => objectValidate())
                        
                        .FinishWith((f, u) =>
                        {
                            Console.WriteLine("Projects created with Bogus: {0}!", u.Id);
                        });
                   
                    var projects = new List<Project>();
                    var budgets = context.Budgets.ToList();
                    var states = context.States.ToList();
                    // var profiles = context.Profiles.ToList();

                    for(var i = 0; i < 50; i++)
                    {
                        var project = projectSkeleton.Generate();
                        // project.ProfileId = profiles[random.Next(profiles.Count - 1)].Id;
                        project.BudgetId = budgets[i].Id;
                        project.StatusId = states[random.Next(states.Count - 1)].Id;

                        context.Projects.Add(project);
                    }

                    context.Projects.AddRange(projects);
                    context.SaveChangesAsync().Wait();
                }


                if(!context.Profiles.Any()) 
                {
                    var profileSkeleton = new Faker<Models.Profile>()
                        .RuleFor(c => c.LastName, f => f.Name.LastName())
                        .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                        .RuleFor(c => c.UserName, (f, c) => f.Internet.UserName(c.FirstName, c.LastName))
                        .RuleFor(c => c.Image, f => f.Image.Image())
                        .RuleFor(c => c.Email, f => f.Internet.Email())
                        .RuleFor(c => c.Employeenumber, f => f.Random.Number(500, 2000))
                        .FinishWith((f, u) =>
                        {
                            Console.WriteLine("Profiles created with Bogus: {0}!", u.Id);
                        });
                   
                    var profiles = new List<Profile>();

                    for(var i = 0; i < 50; i++)
                    {
                        var profile = profileSkeleton.Generate();

                        profiles.Add(profile);
                    }

                    context.Profiles.AddRange(profiles);
                    context.SaveChangesAsync().Wait();
                }



                /*if(!context.Mediums.Any()) 
                {
                    var mediaSkeleton = new Faker<Models.Media>()
                        .RuleFor(c => c.Image, f => f.Image.Image())
                        .RuleFor(c => c.TypeMedia, TypeMedia.PrimaryImage)
                        .FinishWith((f, u) =>
                        {
                            Console.WriteLine("Mediums created with Bogus: {0}!", u.Id);
                        });
                   
                    var mediums = new List<Media>();
                    var projects = context.Projects.ToList();

                    for(var i = 0; i < 50; i++)
                    {
                        var media = mediaSkeleton.Generate();
                        media.ProjectId = projects[i].Id;

                        mediums.Add(media);
                    }

                    context.Mediums.AddRange(mediums);
                    context.SaveChangesAsync().Wait();
                } */

                if(!context.Tags.Any())
                {
                  context.Tags.AddRange(new List<Tag>()
                    {
                      new Tag { Name = "Projectmedewerker", ProjectId = 1},
                      new Tag { Name = "1e graad onderwijs", ProjectId = 2}
                    });
                  context.SaveChangesAsync().Wait();
                }

                if(!context.Links.Any())
                {
                  context.Links.AddRange(new List<Link>()
                    {
                      new Link { Name = "www.test.be", ProjectId = 1},
                      new Link { Name = "www.test2.be", ProjectId =2}
                    });
                  context.SaveChangesAsync().Wait();
                }  

                if(!context.Tasks.Any()) 
                {
                    var taskSkeleton = new Faker<Models.Task>()
                        .RuleFor(c => c.Name, f => lorem.Sentence())
                        .FinishWith((f, u) =>
                        {
                            Console.WriteLine("Tasks created with Bogus: {0}!", u.Id);
                        });
                   
                    var tasks = new List<Task>();
                    var profiles = context.Profiles.ToList();
                    var projects = context.Projects.ToList();

                    for(var i = 0; i < 25; i++)
                    {
                        var task = taskSkeleton.Generate();
                        task.ProfileId = profiles[i].Id;
                        task.ProjectId = profiles[random.Next(projects.Count - 1)].Id;

                        tasks.Add(task);
                    }

                    context.Tasks.AddRange(tasks);
                    context.SaveChangesAsync().Wait();
                }

                if(!context.Partners.Any()) 
                {
                    var partnerSkeleton = new Faker<Models.Partner>()
                        .RuleFor(c => c.Name, f => f.Name.LastName())
                        .FinishWith((f, u) =>
                        {
                            Console.WriteLine("Partners created with Bogus: {0}!", u.Id);
                        });
                   
                    var partners = new List<Partner>();
                    var projects = context.Projects.ToList();

                    for(var i = 0; i < 50; i++)
                    {
                        var partner = partnerSkeleton.Generate();
                        partner.ProjectId = projects[i].Id;

                        partners.Add(partner);
                    }

                    context.Partners.AddRange(partners);
                    context.SaveChangesAsync().Wait();
                }
            }
        }

         private static string objectValue(string value)
        {
            // var result ="\"{\\\"nl\\\": { \\\"value\\\": \\\" " + value + " \\\", \\\"validate\\\": {\\\"allow_changes\\\":  false, \\\"required\\\": false, \\\"publication_ok\\\": false, \\\"validated_by\\\": null, \\\"feedback\\\": null, \\\"visible_to\\\": [] }}}\"";
            var result ="\"{\\\"nl\\\": { \\\"value\\\": \\\" " + value + " \\\", \\\"validate\\\": {\\\"allow_changes\\\":  false, \\\"required\\\": false, \\\"publication_ok\\\": false, \\\"validated_by\\\": null,  \\\"editable_by\\\": [], \\\"feedback\\\": null, \\\"visible_to\\\": null }}, \\\"en\\\": { \\\"value\\\": \\\" " + value + " \\\", \\\"validate\\\": {\\\"allow_changes\\\":  false, \\\"required\\\": false, \\\"publication_ok\\\": false, \\\"validated_by\\\": null,  \\\"editable_by\\\": null, \\\"feedback\\\": null, \\\"visible_to\\\": null }}}\"";

            // eng
            return result;
        }

        private static string objectValidate()
        {
            var result ="\"{ \\\"validate\\\": {\\\"allow_changes\\\":  false, \\\"required\\\": false, \\\"publication_ok\\\": false, \\\"validated_by\\\": null, \\\"editable_by\\\": [], \\\"feedback\\\": null, \\\"visible_to\\\": null }}\"";
            // eng
            return result;
        }
    }
}
