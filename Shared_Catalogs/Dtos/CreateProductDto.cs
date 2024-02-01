using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Models;

public class CreateProductDto : ICreateProductDto
{
    public string ArticleNumber { get; set; } = null!;
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Category { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public int Quantity { get; set; }

}

