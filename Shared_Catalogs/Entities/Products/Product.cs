using System;
using System.Collections.Generic;

namespace Shared_Catalogs.Entities.Products;

public partial class Product : IProduct
{
    public string ArticleNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    public int StockQuantityId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer ManufactureName { get; set; } = null!;

    public virtual StockQuantity StockQuantity { get; set; } = null!;
}
