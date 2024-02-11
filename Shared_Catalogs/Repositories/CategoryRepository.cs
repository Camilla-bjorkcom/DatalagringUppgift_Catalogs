
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;
using System.Diagnostics;

namespace Shared_Catalogs.Repositories;

public class CategoryRepository (ProductsDbContext context) : Repo<Category, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;

    public override Category Update(Category entity)
    {
        try
        {
            var entityToUpdate = _context.Categories.Find(entity.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate = entity;
                _context.Categories.Update(entityToUpdate);
                _context.SaveChanges();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }
}