namespace Shared_Catalogs.Dtos;

public class UpdateProductDto
{
    public string ArticleNumber { get; set; } = null!;
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Category { get; set; } = null!;

    public int Quantity { get; set; }
}

