using Shared_Catalogs.Entities.Products;

namespace Shared_Catalogs.Interfaces
{
    public interface IProduct
    {
        string ArticleNumber { get; set; }
        Category Category { get; set; }
        int CategoryId { get; set; }
        string? Description { get; set; }
        Manufacturer ManufactureName { get; set; }
        int ManufacturerId { get; set; }
        StockQuantity StockQuantity { get; set; }
        int StockQuantityId { get; set; }
        string Title { get; set; }
    }
}