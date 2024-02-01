using System;
using System.Collections.Generic;

namespace Shared_Catalogs.Entities.Products;

public partial class StockQuantity : IStockQuantity
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
