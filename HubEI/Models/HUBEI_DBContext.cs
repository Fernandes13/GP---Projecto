using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HubEI.Models
{
    /// <summary>
    /// Database Context
    /// </summary>
    /// <remarks></remarks>
    public partial class HUBEI_DBContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<BusinessArea> BusinessArea { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectAdvisor> ProjectAdvisor { get; set; }
        public virtual DbSet<ProjectDocument> ProjectDocument { get; set; }
        public virtual DbSet<ProjectTechnology> ProjectTechnology { get; set; }
        public virtual DbSet<ProjectType> ProjectType { get; set; }
        public virtual DbSet<SchoolMentor> SchoolMentor { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentBranch> StudentBranch { get; set; }
        public virtual DbSet<Technology> Technology { get; set; }
        public virtual DbSet<RgpdInfo> RgpdInfo { get; set; }
        

        public HUBEI_DBContext(DbContextOptions<HUBEI_DBContext> options)
        : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=tcp:hubei.database.windows.net,1433;Database=HUBEI_DB;Integrated Security=False;User ID=master; Password=k,74MAh123;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.IdAddress);

                entity.Property(e => e.IdAddress)
                    .HasColumnName("id_address");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(99);

                entity.Property(e => e.Door)
                    .IsRequired()
                    .HasColumnName("door")
                    .HasMaxLength(20);

                entity.Property(e => e.IdDistrict).HasColumnName("id_district");

                entity.Property(e => e.Locality)
                    .IsRequired()
                    .HasColumnName("locality")
                    .HasMaxLength(99);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(8);

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.IdDistrict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_A_district");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin);

                entity.Property(e => e.IdAdmin)
                    .HasColumnName("id_admin");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(99);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<BusinessArea>(entity =>
            {
                entity.HasKey(e => e.IdBusinessArea);

                entity.Property(e => e.IdBusinessArea)
                    .HasColumnName("id_business_area");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(99);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.IdCompany);

                entity.Property(e => e.IdCompany)
                    .HasColumnName("id_company");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(99);

                entity.Property(e => e.IdDistrict).HasColumnName("id_district");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(99);

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.IdDistrict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_C_district");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.IdDistrict);

                entity.Property(e => e.IdDistrict)
                    .HasColumnName("id_district");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProject);

                entity.Property(e => e.IdProject)
                    .HasColumnName("id_project");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.Downloads).HasColumnName("downloads");

                entity.Property(e => e.Grade).HasColumnName("grade");

                entity.Property(e => e.IdCompany).HasColumnName("id_company");

                entity.Property(e => e.IdBusinessArea).HasColumnName("id_business_area");

                entity.Property(e => e.IdProjectType).HasColumnName("id_project_type");

                entity.Property(e => e.IdStudent).HasColumnName("id_student");

                entity.Property(e => e.IsVisible).HasColumnName("is_visible");

                entity.Property(e => e.Video).HasColumnName("video");

                entity.Property(e => e.ProjectDate)
                    .HasColumnName("project_date")
                    .HasColumnType("date");

                entity.Property(e => e.Report)
                    .IsRequired()
                    .HasColumnName("report");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(99);

                entity.Property(e => e.Views).HasColumnName("views");

                entity.HasOne(d => d.IdCompanyNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.IdCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_P_company");

                entity.HasOne(d => d.IdProjectTypeNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.IdProjectType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_P_project_type");

                entity.HasOne(d => d.IdBusinessAreaNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.IdBusinessArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_P_businessArea");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_P_student");
            });

            modelBuilder.Entity<ProjectAdvisor>(entity =>
            {
                entity.HasKey(e => new { e.IdProject, e.IdSchoolMentor });

                entity.ToTable("Project_advisor");

                entity.Property(e => e.IdProject).HasColumnName("id_project");

                entity.Property(e => e.IdSchoolMentor).HasColumnName("id_school_advisor");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.ProjectAdvisor)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PA_project");

                entity.HasOne(d => d.IdSchoolMentorNavigation)
                    .WithMany(p => p.ProjectAdvisor)
                    .HasForeignKey(d => d.IdSchoolMentor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PA_advisor");
            });

            modelBuilder.Entity<ProjectDocument>(entity =>
            {
                entity.HasKey(e => e.IdProjectDocument);

                entity.ToTable("Project_document");

                entity.Property(e => e.IdProjectDocument)
                    .HasColumnName("id_project_document");

                entity.Property(e => e.Document)
                    .IsRequired()
                    .HasColumnName("document");

                entity.Property(e => e.IdProject).HasColumnName("id_project");

                entity.Property(e => e.FileName).HasColumnName("file_name");

                entity.Property(e => e.FileSize).HasColumnName("file_size");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.ProjectDocument)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PD_project");
            });

            modelBuilder.Entity<ProjectTechnology>(entity =>
            {
                entity.HasKey(e => new { e.IdProject, e.IdTechnology });

                entity.ToTable("Project_technology");

                entity.Property(e => e.IdProject).HasColumnName("id_project");

                entity.Property(e => e.IdTechnology).HasColumnName("id_technology");

                //entity.HasOne(d => d.IdProjectNavigation)
                //    .WithMany(p => p.ProjectTechnology)
                //    .HasForeignKey(d => d.IdProject)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("fk_PT_project");

                entity.HasOne(d => d.IdTechnologyNavigation)
                    .WithMany(p => p.ProjectTechnology)
                    .HasForeignKey(d => d.IdTechnology)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PT_technology");
            });

            modelBuilder.Entity<ProjectType>(entity =>
            {
                entity.HasKey(e => e.IdProjectType);

                entity.ToTable("Project_type");

                entity.Property(e => e.IdProjectType)
                    .HasColumnName("id_project_type");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<SchoolMentor>(entity =>
            {
                entity.HasKey(e => e.IdSchoolMentor);

                entity.ToTable("School_mentor");

                entity.Property(e => e.IdSchoolMentor)
                    .HasColumnName("id_school_mentor");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(99);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(99);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent);

                entity.Property(e => e.IdStudent)
                    .HasColumnName("id_student");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(99);

                entity.Property(e => e.IdAddress).HasColumnName("id_address");

                entity.Property(e => e.IdStudentBranch).HasColumnName("id_student_branch");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(99);

                entity.Property(e => e.StudentNumber).HasColumnName("student_number");

                entity.Property(e => e.Telephone).HasColumnName("telephone");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_S_address");

                entity.HasOne(d => d.IdStudentBranchNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdStudentBranch)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_S_student_branch");
            });

            modelBuilder.Entity<StudentBranch>(entity =>
            {
                entity.HasKey(e => e.IdStudentBranch);

                entity.ToTable("Student_branch");

                entity.Property(e => e.IdStudentBranch)
                    .HasColumnName("id_student_branch");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Technology>(entity =>
            {
                entity.HasKey(e => e.IdTechnology);

                entity.Property(e => e.IdTechnology)
                    .HasColumnName("id_technology");


                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(99);
            });

            

            modelBuilder.Entity<RgpdInfo>(entity =>
            {
                entity.Property(e => e.IdTerm)
                    .HasColumnName("id_term");

                entity.ToTable("rgpd_info");

                entity.HasKey(e => e.IdTerm);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(3999);
            });
        }
    }
}
