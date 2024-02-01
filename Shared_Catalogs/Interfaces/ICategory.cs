using Shared_Catalogs.Entities.Products;

namespace Shared_Catalogs.Interfaces
{
    public interface ICategory
    {
        string CategoryName { get; set; }
        int Id { get; set; }
        ICollection<Product> Products { get; set; }
    }
}