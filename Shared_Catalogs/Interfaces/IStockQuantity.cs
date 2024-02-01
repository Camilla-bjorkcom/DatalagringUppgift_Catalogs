using Shared_Catalogs.Entities.Products;

namespace Shared_Catalogs.Interfaces
{
    public interface IStockQuantity
    {
        int Id { get; set; }
        ICollection<Product> Products { get; set; }
        int Quantity { get; set; }
    }
}