namespace Shared_Catalogs.Interfaces
{
    public interface IProductDto
    {
        string Category { get; set; }
        string? Description { get; set; }
        string Manufacturer { get; set; }
        string Title { get; set; }
    }
}