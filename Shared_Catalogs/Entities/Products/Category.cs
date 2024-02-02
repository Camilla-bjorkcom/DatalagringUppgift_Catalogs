using Shared_Catalogs.Interfaces;
using System;
using System.Collections.Generic;

namespace Shared_Catalogs.Entities.Products;

public partial class Category : ICategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
