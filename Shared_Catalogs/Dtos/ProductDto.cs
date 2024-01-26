﻿namespace Shared_Catalogs.Models;

public class ProductDto
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Category { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

}
