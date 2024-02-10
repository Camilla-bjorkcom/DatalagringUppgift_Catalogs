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
}

