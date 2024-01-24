namespace Shared_Catalogs.Models;

public class CreateProductDto
{
    public string ArticleNumber { get; set; } = null!;
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Category { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public int Quantity { get; set; }

}

