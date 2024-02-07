

namespace Shared_Catalogs.Dtos;

public class CreateProductDto
{
    public string? ArticleNumber { get; set; }
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Category { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    

}

