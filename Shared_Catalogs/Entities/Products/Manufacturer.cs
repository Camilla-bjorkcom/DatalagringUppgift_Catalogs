using System;
using System.Collections.Generic;

namespace Shared_Catalogs.Entities.Products;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string ManufactureName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
