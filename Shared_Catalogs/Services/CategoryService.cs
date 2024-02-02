using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Services;

public class CategoryService(CategoryRepository categoryRepository) : ICategoryService
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public async Task<bool> CreateCategoryAsync(string categoryName)
    {
        try
        {
            if (!await _categoryRepository.ExistsAsync(x => x.CategoryName == categoryName))
            {
                var categoryEntity = await _categoryRepository.CreateAsync(new Category { CategoryName = categoryName });
                if (categoryEntity != null)
                {
                    return true;
                }
            }
        }

        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<CategoryDto> GetCategoryAsync(Expression<Func<Category, bool>> predicate)
    {
        try
        {
            var categoryEntity = await _categoryRepository.GetOneAsync(predicate);
            if (categoryEntity != null)
            {
                var categoryDto = new CategoryDto(categoryEntity.Id, categoryEntity.CategoryName);

                return categoryDto;
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        try
        {
            var categoryEntities = await _categoryRepository.GetAllAsync();
            if (categoryEntities != null)
            {
                var list = new List<CategoryDto>();
                foreach (var categoryEntity in categoryEntities)
                    list.Add(new CategoryDto(categoryEntity.Id, categoryEntity.CategoryName));

                return list;
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }


    public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto updatedCategory)
    {
        try
        {
            var categoryEntity = await _categoryRepository.GetOneAsync(x => x.Id == updatedCategory.Id);
            if (categoryEntity != null)
            {
                var updatedCategoryEntity = await _categoryRepository.UpdateAsync(categoryEntity);
                if (updatedCategoryEntity != null)
                {

                    var categoryDto = new CategoryDto(updatedCategoryEntity.Id, updatedCategoryEntity.CategoryName);
                    return categoryDto;
                }
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteCategoryAsync(Category category)
    {
        try
        {
            var result = await _categoryRepository.DeleteAsync(x => x.CategoryName == category.CategoryName);
            return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}

