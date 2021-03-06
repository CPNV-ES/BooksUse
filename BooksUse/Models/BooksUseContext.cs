﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BooksUse.Models
{
    public partial class BooksUseContext : DbContext
    {
        public BooksUseContext()
        {
        }

        public BooksUseContext(DbContextOptions<BooksUseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SchoolClasses> SchoolClasses { get; set; }
        public virtual DbSet<SchoolClassesRequests> SchoolClassesRequests { get; set; }
        public virtual DbSet<SupplierSupplyBook> SupplierSupplyBook { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Years> Years { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=BooksUse;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasIndex(e => e.Isbn)
                    .HasName("UQ__Books__447D36EAD6FB3567")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UnitsInStock).HasColumnName("unitsInStock");
            });

            modelBuilder.Entity<Requests>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Approved).HasColumnName("approved");

                entity.Property(e => e.FkBooks).HasColumnName("FK_Books");

                entity.Property(e => e.FkUsers).HasColumnName("FK_Users");

                entity.Property(e => e.FkYears).HasColumnName("FK_Years");

                entity.HasOne(d => d.FkBooksNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.FkBooks)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books");

                entity.HasOne(d => d.FkUsersNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.FkUsers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users");

                entity.HasOne(d => d.FkYearsNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.FkYears)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Years");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SchoolClasses>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Studentsnumber).HasColumnName("studentsnumber");
            });

            modelBuilder.Entity<SchoolClassesRequests>(entity =>
            {
                entity.ToTable("SchoolClasses_Requests");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FkRequests).HasColumnName("FK_Requests");

                entity.Property(e => e.FkSchoolClasses).HasColumnName("FK_SchoolClasses");

                entity.HasOne(d => d.FkRequestsNavigation)
                    .WithMany(p => p.SchoolClassesRequests)
                    .HasForeignKey(d => d.FkRequests)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requests");

                entity.HasOne(d => d.FkSchoolClassesNavigation)
                    .WithMany(p => p.SchoolClassesRequests)
                    .HasForeignKey(d => d.FkSchoolClasses)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolClasses");
            });

            modelBuilder.Entity<SupplierSupplyBook>(entity =>
            {
                entity.ToTable("supplier_supply_book");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Deldelay).HasColumnName("deldelay");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.SupplierSupplyBook)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("fk_sup_book");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierSupplyBook)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("fk_sup_sup");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.ToTable("suppliers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Suppliername)
                    .HasColumnName("suppliername")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Users__AB6E6164EFEDC157")
                    .IsUnique();

                entity.HasIndex(e => e.Initials)
                    .HasName("UQ__Users__696DB02C176341C3")
                    .IsUnique();

                entity.HasIndex(e => e.IntranetUserId)
                    .HasName("UQ__Users__C5DF48D809148643")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("UQ__Users__B43B145FA4BAD1C9")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FkRoles).HasColumnName("FK_Roles");

                entity.Property(e => e.Initials)
                    .IsRequired()
                    .HasColumnName("initials")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.IntranetUserId).HasColumnName("intranetUserID");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkRolesNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FkRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Roles");
            });

            modelBuilder.Entity<Years>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Open).HasColumnName("open");

                entity.Property(e => e.Title).HasColumnName("title");
            });
        }
    }
}
