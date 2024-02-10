using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared_Catalogs.Entities.Products;

[Index("Title", Name = "UQ__Products__2CB664DC32BA402B", IsUnique = true)]
public partial class Product
{
    [Key]
    [StringLength(50)]
    public string ArticleNumber { get; set; } = null!;

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("ManufacturerId")]
    [InverseProperty("Products")]
    public virtual Manufacturer Manufacturer { get; set; } = null!;

    [InverseProperty("ArticleNumberNavigation")]
    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
}
