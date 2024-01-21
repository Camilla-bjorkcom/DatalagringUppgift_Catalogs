﻿using System;
using System.Collections.Generic;

namespace Catalog_App.Entities;

public partial class Product
{
    public string ArticleNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    public int StockQuantity { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual StockQuantity StockQuantityNavigation { get; set; } = null!;
}