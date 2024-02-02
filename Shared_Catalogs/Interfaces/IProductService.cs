namespace Shared_Catalogs.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ICreateProductDto product);
        Task<bool> DeleteProductAsync(IProduct product);
        Task<IEnumerable<IProductDto>> GetAllProductsAsync();
        Task<IProductDto> GetProductByTitleAsync(string title);
        Task<IEnumerable<IProductDto>> GetProductsByCategoryNameAsync(string categoryName);
        Task<bool> UpdateProductAsync(IUpdateProductDto productToBeUpdated);
    }
}