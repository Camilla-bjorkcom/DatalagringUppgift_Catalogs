using System;
using System.Collections.Generic;

namespace Shared_Catalogs.Entities.Products;

public partial class ProductReview
{
    public int Id { get; set; }

    public string Reviews { get; set; } = null!;

    public string ArticleNumber { get; set; } = null!;

    public virtual Product ArticleNumberNavigation { get; set; } = null!;
}
