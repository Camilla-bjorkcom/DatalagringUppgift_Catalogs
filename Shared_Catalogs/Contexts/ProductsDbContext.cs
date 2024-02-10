using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Entities.Products;

namespace Shared_Catalogs.Contexts;

public partial class ProductsDbContext : DbContext
{
    public ProductsDbContext()
    {
    }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options): base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
      // optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\IT_kurser\\Kurser\\Webbutveckling-dotnet\\Datalagring\\Catalogs\\Shared_Catalogs\\Data\\ProductsCatalog.mdf;Integrated Security=True"); 
    }
  
    
      

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC079A7885A2");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC07CACA649A");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Products__3C991143A03F9CB1");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__3B0BC30C");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Manufa__3A179ED3");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductR__3214EC078E183EA7");

            entity.HasOne(d => d.ArticleNumberNavigation).WithMany(p => p.ProductReviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductRe__Artic__3DE82FB7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
