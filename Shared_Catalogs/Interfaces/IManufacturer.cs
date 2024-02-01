using Shared_Catalogs.Entities.Products;

namespace Shared_Catalogs.Interfaces
{
    public interface IManufacturer
    {
        int Id { get; set; }
        string ManufactureName { get; set; }
        ICollection<Product> Products { get; set; }
    }
}