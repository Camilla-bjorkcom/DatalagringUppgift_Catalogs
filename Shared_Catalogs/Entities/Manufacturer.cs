using System;
using System.Collections.Generic;

namespace Catalog_App.Entities;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string ManufactureName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
