using LitigationPlanner.Data.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LitigationPlanner.Data.DbContexts
{
    public partial class LitigationPlannerDBContext : IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>> 
    {
        public LitigationPlannerDBContext()
        {
        }

        public LitigationPlannerDBContext(DbContextOptions<LitigationPlannerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Litigation> Litigation { get; set; }
        public virtual DbSet<LitigationUser> LitigationUser { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<ProcessType> ProcessType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasIndex(e => e.CompanyId);

                entity.Property(e => e.Id);

                entity.Property(e => e.Address)
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Profession).HasMaxLength(50);

                entity.Property(e => e.TelephoneNumber1).HasMaxLength(50);

                entity.Property(e => e.TelephoneNumber2).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Contact_Company");
            });

            modelBuilder.Entity<Litigation>(entity =>
            {
                entity.HasIndex(e => e.Defendant);

                entity.HasIndex(e => e.Judge);

                entity.HasIndex(e => e.LocationId);

                entity.HasIndex(e => e.ProcessType);

                entity.HasIndex(e => e.Prosecutor);

                entity.Property(e => e.Id);

                entity.Property(e => e.ProcessIdentifier)
                    .HasMaxLength(20);

                entity.HasOne(d => d.DefendantNavigation)
                    .WithMany(p => p.LitigationDefendantNavigation)
                    .HasForeignKey(d => d.Defendant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Litigation_Contact2");

                entity.HasOne(d => d.JudgeNavigation)
                    .WithMany(p => p.LitigationJudgeNavigation)
                    .HasForeignKey(d => d.Judge)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Litigation_Contact");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Litigation)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Litigation_Location");

                entity.HasOne(d => d.ProcessTypeNavigation)
                    .WithMany(p => p.Litigation)
                    .HasForeignKey(d => d.ProcessType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Litigation_ProcessType");

                entity.HasOne(d => d.ProsecutorNavigation)
                    .WithMany(p => p.LitigationProsecutorNavigation)
                    .HasForeignKey(d => d.Prosecutor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Litigation_Contact1");
            });

            modelBuilder.Entity<LitigationUser>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Litigation)
                    .WithMany(p => p.LitigationUsers)
                    .HasForeignKey(d => d.LitigationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LitigationUser_Litigation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LitigationUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LitigationUser_AspNetUsers");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ProcessType>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
