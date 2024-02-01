using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Entities.Products;

namespace Catalog_App.Contexts;

public partial class ProductsDbContext : DbContext
{
    public ProductsDbContext()
    {
    }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<StockQuantity> StockQuantities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\IT_kurser\\Kurser\\Webbutveckling-dotnet\\Datalagring\\Catalogs\\Shared_Catalogs\\Data\\ProductsCatalog.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07E522B699");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0F167C0C1").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC07040F18B0");

            entity.HasIndex(e => e.ManufactureName, "UQ__Manufact__D194335AF2AFDB42").IsUnique();

            entity.Property(e => e.ManufactureName)
                .HasMaxLength(50)
                .HasColumnName("Manufacturer");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Products__3C99114358416759");

            entity.HasIndex(e => e.Title, "UQ__Products__2CB664DC5BAE6EFD").IsUnique();

            entity.Property(e => e.ArticleNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__6754599E");

            entity.HasOne(d => d.ManufactureName).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Manufa__66603565");

            entity.HasOne(d => d.StockQuantity).WithMany(p => p.Products)
                .HasForeignKey(d => d.StockQuantity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__StockQ__68487DD7");
        });

        modelBuilder.Entity<StockQuantity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockQua__3214EC07AC6FD2D8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
