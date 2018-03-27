using System;
using Models;
using Models.Security;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenIddict;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Financingform> Financingforms { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Media> Mediums { get; set; }
        public DbSet<Status> States { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Partner> Partners { get; set; }

        public DbSet<ProjectProfile> ProjectProfile { get; set; }
        public DbSet<ProjectParticipant> ProjectParticipant { get; set; }
        public DbSet<ProjectPublication> ProjectPublication { get; set; }
        public DbSet<ProjectFinancingform> ProjectFinancingform { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // project
            builder.Entity<Project>()
              .HasKey(p => p.Id);

            builder.Entity<Project>()
              .Property(p => p.Startdate);

            builder.Entity<Project>()
              .Property(p => p.Enddate);

            builder.Entity<Project>()
              .Property(p => p._Title).HasColumnName("Title")
              .HasMaxLength(1000);
//              .Ignore(p => p.Title);

            builder.Entity<Project>()
              .Property(p => p._Shorttitle).HasColumnName("Shorttitle")
              .HasMaxLength(1000);

            builder.Entity<Project>()
              .Property(p => p._Subtitle).HasColumnName("Subtitle")
              .HasMaxLength(1000);

            builder.Entity<Project>()
              .Property(p => p._Description).HasColumnName("Description")
              .HasMaxLength(2000);

            builder.Entity<Project>()
              .Property(p => p._Abstract).HasColumnName("Abstract")
              .HasMaxLength(7500);

            builder.Entity<Project>()
              .Property(p => p._PartnerValidate).HasColumnName("PartnerValidate")
              .HasMaxLength(7500);

            builder.Entity<Project>()
              .Property(p => p._ParticipantValidate).HasColumnName("ParticipantValidate")
              .HasMaxLength(7500);

            builder.Entity<Project>()
              .Property(p => p._FinancingformValidate).HasColumnName("FinancingformValidate")
              .HasMaxLength(7500);

            builder.Entity<Project>()
              .Property(p => p._LinkValidate).HasColumnName("LinkValidate")
              .HasMaxLength(7500);

            builder.Entity<Project>()
              .Property(p => p._BudgetValidate).HasColumnName("BudgetValidate")
              .HasMaxLength(7500);
 
            builder.Entity<Project>()
                .HasOne(l => l.Budget)
                .WithOne()
                .HasForeignKey<Project>(l => l.BudgetId);

            builder.Entity<Project>()
                .HasOne(s => s.Status)
                .WithMany(p => p.Projects)
                .HasForeignKey(s => s.StatusId);

            /*
            builder.Entity<Project>()
                .HasOne(l => l.Profile)
                .WithMany(l => l.Projects)
                .HasForeignKey(l => l.ProfileId); */
            
            builder.Entity<Project>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Project>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

             //
            builder.Entity<ProjectParticipant>()
                .HasKey(a => new { a.ProjectId, a.ParticipantId });

            builder.Entity<ProjectParticipant>()
                .HasOne(da => da.Project)
                .WithMany(d => d.Participants)
                .HasForeignKey(da => da.ProjectId);
                
            builder.Entity<ProjectParticipant>()
                .HasOne(da => da.Participant)
                .WithMany(d => d.Projects)
                .HasForeignKey(da => da.ParticipantId);

            //
            builder.Entity<ProjectFinancingform>()
                .HasKey(a => new { a.ProjectId, a.FinancingformId });

            builder.Entity<ProjectFinancingform>()
                .HasOne(da => da.Project)
                .WithMany(d => d.Financingforms)
                .HasForeignKey(da => da.ProjectId);
                
            builder.Entity<ProjectFinancingform>()
                .HasOne(da => da.Financingform)
                .WithMany(d => d.Projects)
                .HasForeignKey(da => da.FinancingformId);



            // 
            builder.Entity<ProjectPublication>()
                .HasKey(a => new { a.ProjectId, a.PublicationId });

            builder.Entity<ProjectPublication>()
                .HasOne(da => da.Project)
                .WithMany(d => d.Publications)
                .HasForeignKey(da => da.ProjectId);
                
            builder.Entity<ProjectPublication>()
                .HasOne(da => da.Publication)
                .WithMany(d => d.Projects)
                .HasForeignKey(da => da.PublicationId);

            
            // 
            builder.Entity<ProjectProfile>()
                .HasKey(a => new { a.ProjectId, a.ProfileId });

            builder.Entity<ProjectProfile>()
                .HasOne(da => da.Project)
                .WithMany(d => d.Profiles)
                .HasForeignKey(da => da.ProjectId);
                
            builder.Entity<ProjectProfile>()
                .HasOne(da => da.Profile)
                .WithMany(d => d.Projects)
                .HasForeignKey(da => da.ProfileId);


            // financingform
            builder.Entity<Financingform>()
              .HasKey(f => f.Id);

            builder.Entity<Financingform>()
              .Property(f => f.Name)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Financingform>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Financingform>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

                
              // link
            builder.Entity<Link>()
              .HasKey(l => l.Id);

            builder.Entity<Link>()
              .Property(l => l.Name)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Link>()
                .HasOne(l => l.Project)
                .WithMany(l => l.Links)
                .HasForeignKey(l => l.ProjectId);

            builder.Entity<Link>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Link>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();


            // budget
            builder.Entity<Budget>()
              .HasKey(l => l.Id);

            builder.Entity<Budget>()
              .Property(l => l.TotalBudget)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Budget>()
              .Property(l => l.ArteveldeBudget)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Budget>()
              .Property(l => l.OperatingBudget)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Budget>()
              .Property(l => l.InvestmentBudget)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Budget>()
              .Property(l => l.StaffBudget)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Budget>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Budget>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();


              // participant
            builder.Entity<Participant>()
              .HasKey(p => p.Id);

            builder.Entity<Participant>()
              .Property(p => p.TypeParticipant)
              .HasMaxLength(255)
              .IsRequired();
            
            builder.Entity<Participant>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Participant>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();



              // profile
            builder.Entity<Profile>()
              .HasKey(p => p.Id);

            builder.Entity<Profile>()
              .Property(p => p.UserName)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Profile>()
              .Property(p => p.Email)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Profile>()
              .Property(p => p.LastName)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Profile>()
              .Property(p => p.FirstName)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Profile>()
              .Property(p => p.Image)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Profile>()
              .Property(p => p.Employeenumber)
              .IsRequired();

            builder.Entity<Profile>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Profile>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Entity<Profile>()
                .HasOne(l => l.Setting)
                .WithOne()
                .HasForeignKey<Profile>(l => l.SettingId);
              

              
               // setting
            builder.Entity<Setting>()
                .HasKey(p => p.Id);

            builder.Entity<Setting>()
                .Property(p => p.Language)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Setting>()
                .Property(p => p.Color)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Setting>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Setting>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

      
              // projectmedia
            builder.Entity<Media>()
              .HasKey(p => p.Id);

            builder.Entity<Media>()
              .Property(p => p.Image)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Media>()
              .Property(p => p.TypeMedia)
              .IsRequired();

            builder.Entity<Media>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Media>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Entity<Media>()
                .HasOne(l => l.Project)
                .WithMany(l => l.Mediums)
                .HasForeignKey(l => l.ProjectId);

            // partner
            builder.Entity<Partner>()
              .HasKey(p => p.Id);

            builder.Entity<Partner>()
              .Property(p => p.Name)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Partner>()
                .HasOne(l => l.Project)
                .WithMany(l => l.Partners)
                .HasForeignKey(l => l.ProjectId);

            builder.Entity<Partner>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Partner>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();


              // projectstatus
            builder.Entity<Status>()
              .HasKey(p => p.Id);

            builder.Entity<Status>()
              .Property(p => p.Name)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Status>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Status>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();


              // publication
            builder.Entity<Publication>()
              .HasKey(p => p.Id);

            builder.Entity<Publication>()
              .Property(p => p.Name)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<Publication>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Publication>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

              // tag
            builder.Entity<Tag>()
              .HasKey(t => t.Id);

            builder.Entity<Tag>()
              .Property(t => t.Name)
              .HasMaxLength(255)
              .IsRequired();
            
           /* builder.Entity<Tag>()
              .Property(t => t.Description)
              .HasMaxLength(255)
              .IsRequired();
*/
            builder.Entity<Tag>()
                .HasOne(l => l.Project)
                .WithMany(l => l.Tags)
                .HasForeignKey(l => l.ProjectId);

            builder.Entity<Tag>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Tag>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            
              // Task
            builder.Entity<Task>()
              .HasKey(t => t.Id);

            builder.Entity<Task>()
              .Property(t => t.Name)
              .HasMaxLength(255)
              .IsRequired();
            
            builder.Entity<Task>()
                .HasOne(l => l.Profile)
                .WithMany(l => l.Tasks)
                .HasForeignKey(l => l.ProfileId);

            builder.Entity<Task>()
                .HasOne(l => l.Project)
                .WithMany(l => l.Tasks)
                .HasForeignKey(l => l.ProjectId);

            builder.Entity<Task>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<Task>()
                .Property(t => t.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
