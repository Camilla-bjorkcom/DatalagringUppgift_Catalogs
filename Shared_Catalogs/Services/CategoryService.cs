using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Services;

public class CategoryService(CategoryRepository categoryRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public Category CreateCategory(string categoryName)
    {
        try
        {
            var categoryEntity = _categoryRepository.GetOne(x => x.CategoryName == categoryName);
            if (categoryEntity == null)
            {
                categoryEntity = _categoryRepository.Create(new Category { CategoryName = categoryName });
                if (categoryEntity != null)
                {
                    return categoryEntity;
                }
            }
            else return categoryEntity;
        }

        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    //public CategoryDto GetCategory(Expression<Func<Category, bool>> predicate)
    //{
    //    try
    //    {
    //        var categoryEntity = _categoryRepository.GetOne(predicate);
    //        if (categoryEntity != null)
    //        {
    //            var categoryDto = new CategoryDto(categoryEntity.Id, categoryEntity.CategoryName);

    //            return categoryDto;
    //        }

    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
    //    return null!;
    //}

    //public IEnumerable<CategoryDto> GetAllCategories()
    //{
    //    try
    //    {
    //        var categoryEntities = _categoryRepository.GetAll();
    //        if (categoryEntities != null)
    //        {
    //            var list = new List<CategoryDto>();
    //            foreach (var categoryEntity in categoryEntities)
    //                list.Add(new CategoryDto(categoryEntity.Id, categoryEntity.CategoryName));

    //            return list;
    //        }

    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
    //    return null!;
    //}


    //public CategoryDto UpdateCategory(CategoryDto updatedCategory)
    //{
    //    try
    //    {
    //        var categoryEntity = _categoryRepository.GetOne(x => x.Id == updatedCategory.Id);
    //        if (categoryEntity != null)
    //        {
    //            var updatedCategoryEntity = _categoryRepository.Update(categoryEntity);
    //            if (updatedCategoryEntity != null)
    //            {

    //                var categoryDto = new CategoryDto(updatedCategoryEntity.Id, updatedCategoryEntity.CategoryName);
    //                return categoryDto;
    //            }
    //        }

    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
    //    return null!;
    //}

    public bool DeleteCategory(Category category)
    {
        try
        {
            var result = _categoryRepository.Delete(x => x.CategoryName == category.CategoryName);
            return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}

