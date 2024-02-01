namespace Shared_Catalogs.Interfaces
{
    public interface IUpdateProductDto
    {
        string ArticleNumber { get; set; }
        string Category { get; set; }
        string? Description { get; set; }
        int Quantity { get; set; }
        string Title { get; set; }
    }
}