using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using System.Linq.Expressions;

namespace Shared_Catalogs.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryAsync(string categoryName);
        Task<bool> DeleteCategoryAsync(Category category);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryAsync(Expression<Func<Category, bool>> predicate);
        Task<CategoryDto> UpdateCategoryAsync(CategoryDto updatedCategory);
    }
}