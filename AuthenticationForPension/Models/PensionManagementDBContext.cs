using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AuthenticationForPension.Models
{
    public partial class PensionManagementDBContext : DbContext
    {
        public PensionManagementDBContext()
        {
        }

        public PensionManagementDBContext(DbContextOptions<PensionManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PensionTransaction> PensionTransactions { get; set; }
        public virtual DbSet<PensionerDetail> PensionerDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=KANINI-LTP-329;Database=PensionManagementDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<PensionTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionNum)
                    .HasName("PK__PensionT__829367DAEB1C2BC7");

                entity.ToTable("PensionTransaction");

                entity.Property(e => e.AadhaarNo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pan)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PAN");

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.HasOne(d => d.AadhaarNoNavigation)
                    .WithMany(p => p.PensionTransactions)
                    .HasForeignKey(d => d.AadhaarNo)
                    .HasConstraintName("FK__PensionTr__Aadha__398D8EEE");
            });

            modelBuilder.Entity<PensionerDetail>(entity =>
            {
                entity.HasKey(e => e.AadhaarNo)
                    .HasName("PK__Pensione__6F3B29F7579D94A2");

                entity.ToTable("PensionerDetail");

                entity.Property(e => e.AadhaarNo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pan)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PAN");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
