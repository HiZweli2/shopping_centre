using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using istore.Models;

namespace istore.Data;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // # warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=postgres;Username=postgres;Password=password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("discounts_pkey");

            entity.ToTable("discounts");

            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnName("created");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Updated)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnName("created");
            entity.Property(e => e.DiscountName)
                .HasMaxLength(80)
                .HasColumnName("discount_name");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(180)
                .HasColumnName("name");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Updated)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated");

            entity.HasOne(d => d.DiscountNameNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.DiscountName)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_discounts");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(150)
                .HasColumnName("city");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnName("created");
            entity.Property(e => e.DiscountName)
                .HasMaxLength(80)
                .HasColumnName("discount_name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.Updated)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated");

            entity.HasOne(d => d.DiscountNameNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.DiscountName)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_discounts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
