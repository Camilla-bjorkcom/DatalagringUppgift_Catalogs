using System;
using System.Collections.Generic;

namespace Catalog_App.Entities;

public partial class StockQuantity
{
    public int Id { get; set; }

    public bool Quantity { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
