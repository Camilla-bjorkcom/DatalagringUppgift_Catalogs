using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared_Catalogs.Entities.Products;

public partial class ProductReview
{
    [Key]
    public int Id { get; set; }

    public string Reviews { get; set; } = null!;

    [StringLength(50)]
    public string ArticleNumber { get; set; } = null!;

    [ForeignKey("ArticleNumber")]
    [InverseProperty("ProductReviews")]
    public virtual Product ArticleNumberNavigation { get; set; } = null!;
}
