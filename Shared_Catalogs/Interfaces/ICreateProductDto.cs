namespace Shared_Catalogs.Interfaces
{
    public interface ICreateProductDto
    {
        string ArticleNumber { get; set; }
        string Category { get; set; }
        string? Description { get; set; }
        string Manufacturer { get; set; }
        int Quantity { get; set; }
        string Title { get; set; }
    }
}