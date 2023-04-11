using FinancialManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.DbManagement
{
    public partial class FinancialManagementContext : DbContext
    {
        public FinancialManagementContext()
        {
        }

        public FinancialManagementContext(DbContextOptions<FinancialManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bank> Banks { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=desktop-ceskqgb;Database=FinancialManagement;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("BANKS");

                entity.HasIndex(e => e.BankPhone, "UQ__BANKS__6A1297CC1117188B")
                    .IsUnique();

                entity.HasIndex(e => e.BankName, "UQ__BANKS__AEC7A8EF835E3680")
                    .IsUnique();

                entity.Property(e => e.BankId)
                    .ValueGeneratedNever()
                    .HasColumnName("BANK_ID");

                entity.Property(e => e.BankAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BANK_ADDRESS");

                entity.Property(e => e.BankName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BANK_NAME");

                entity.Property(e => e.BankPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BANK_PHONE");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORIES");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CategoryDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_DESCRIPTION");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("LOCATIONS");

                entity.Property(e => e.LocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("LOCATION_ID");

                entity.Property(e => e.LocationAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION_ADDRESS");

                entity.Property(e => e.LocationLatitude).HasColumnName("LOCATION_LATITUDE");

                entity.Property(e => e.LocationLongtude).HasColumnName("LOCATION_LONGTUDE");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION_NAME");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("PEOPLE");

                entity.HasIndex(e => e.Email, "UQ__PEOPLE__161CF724051A7E76")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__PEOPLE__D4FA0A26DFF8D175")
                    .IsUnique();

                entity.HasIndex(e => e.AccountNumber, "UQ__PEOPLE__ECCCDF0550539D15")
                    .IsUnique();

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("PERSON_ID");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT_NUMBER");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("TRANSACTIONS");

                entity.Property(e => e.TransactionId)
                    .ValueGeneratedNever()
                    .HasColumnName("TRANSACTION_ID");

                entity.Property(e => e.BankId).HasColumnName("BANK_ID");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.LocationId).HasColumnName("LOCATION_ID");

                entity.Property(e => e.PersonId).HasColumnName("PERSON_ID");

                entity.Property(e => e.TransactionAmount).HasColumnName("TRANSACTION_AMOUNT");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TRANSACTION_DATE");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("TRANSACTION_TYPE");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TRANSACTI__BANK___30F848ED");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TRANSACTI__CATEG__33D4B598");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TRANSACTI__LOCAT__31EC6D26");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TRANSACTI__PERSO__32E0915F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
