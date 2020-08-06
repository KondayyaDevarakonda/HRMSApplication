using System;
using CompanyApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompanyApi.DBContexts
{
    public partial class HRMSDBContext : DbContext
    {
        public HRMSDBContext()
        {
        }

        public HRMSDBContext(DbContextOptions<HRMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=LAP1119\\KD1;Initial Catalog=HRMSDB;User ID=sa;Password=DockerDB2020");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.ComanyCode);

                entity.Property(e => e.ComanyCode)
                    .HasColumnName("Comany_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ComanyName)
                    .IsRequired()
                    .HasColumnName("Comany_Name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("Company_Id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("Created_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("Is_Active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(7)
                    .IsUnicode(false);
            });
        }
    }
}
