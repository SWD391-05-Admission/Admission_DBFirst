using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Admission.Data.Models;

#nullable disable

namespace Admission.Data.Models.Context
{
    public partial class AdmissionsDBContext : DbContext
    {
        public AdmissionsDBContext()
        {
        }

        public AdmissionsDBContext(DbContextOptions<AdmissionsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdmissionForm> AdmissionForms { get; set; }
        public virtual DbSet<Counselor> Counselors { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<OldSchool> OldSchools { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Talkshow> Talkshows { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<UniAddress> UniAddresses { get; set; }
        public virtual DbSet<UniAdmission> UniAdmissions { get; set; }
        public virtual DbSet<UniImage> UniImages { get; set; }
        public virtual DbSet<UniMajor> UniMajors { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ANHNPP\\ANHNPP;Initial Catalog=AdmissionsDB;User id = sa; pwd=207121; MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdmissionForm>(entity =>
            {
                entity.ToTable("AdmissionForm");
            });

            modelBuilder.Entity<Counselor>(entity =>
            {
                entity.ToTable("Counselor");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Counselor)
                    .HasForeignKey<Counselor>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Counselor_User");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");
            });

            modelBuilder.Entity<OldSchool>(entity =>
            {
                entity.ToTable("OldSchool");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate");

                entity.Property(e => e.Rate1).HasColumnName("Rate");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Rate_Student1");

                entity.HasOne(d => d.Talkshow)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.TalkshowId)
                    .HasConstraintName("FK__Rate__Talkshow__5441852A");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName).IsUnicode(false);
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("Slot");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Slot_Student");

                entity.HasOne(d => d.Talkshow)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.TalkshowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Slot_Talkshow");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_User");

                entity.HasOne(d => d.OldSchool)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.OldSchoolId)
                    .HasConstraintName("FK_Student_OldSchool");
            });

            modelBuilder.Entity<Talkshow>(entity =>
            {
                entity.ToTable("Talkshow");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.IsApprove).HasColumnName("isApprove");

                entity.Property(e => e.IsBanner).HasColumnName("isBanner");

                entity.Property(e => e.IsCancel).HasColumnName("isCancel");

                entity.Property(e => e.IsFinish).HasColumnName("isFinish");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UrlMeet)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("urlMeet");

                entity.HasOne(d => d.Counselor)
                    .WithMany(p => p.Talkshows)
                    .HasForeignKey(d => d.CounselorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Talkshow_Counselor");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Talkshows)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Talkshow_Major");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Talkshows)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_Talkshow_University");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Desciption).IsRequired();

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WalletId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Wallet");
            });

            modelBuilder.Entity<UniAddress>(entity =>
            {
                entity.ToTable("UniAddress");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.UniAddresses)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__UniAddres__Distr__5070F446");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.UniAddresses)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK__UniAddres__Unive__5165187F");
            });

            modelBuilder.Entity<UniAdmission>(entity =>
            {
                entity.ToTable("UniAdmission");

                entity.HasOne(d => d.Admission)
                    .WithMany(p => p.UniAdmissions)
                    .HasForeignKey(d => d.AdmissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UniAdmission_AdmissionForm");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.UniAdmissions)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UniAdmission_University");
            });

            modelBuilder.Entity<UniImage>(entity =>
            {
                entity.ToTable("UniImage");

                entity.Property(e => e.IsLogo).HasColumnName("isLogo");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.UniImages)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK__UniImage__Univer__4BAC3F29");
            });

            modelBuilder.Entity<UniMajor>(entity =>
            {
                entity.ToTable("UniMajor");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.UniMajors)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK__UniMajor__MajorI__5535A963");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.UniMajors)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK__UniMajor__Univer__5629CD9C");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.ToTable("University");

                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Facebook).IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.LastYearBenchmark).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MaxFee).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MinFee).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Website).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User__RoleId__4F7CD00D");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wallet_Student");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
