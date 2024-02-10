
using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class ProductRepository(ProductsDbContext context) : Repo<Product, ProductsDbContext>(context)
{
  
        private readonly ProductsDbContext _context = context;

    public override IEnumerable<Product> GetAll()
    {
        try
        {
            var entities = _context.Products
                .Include(x => x.Manufacturer)
                .Include(x => x.Category)
                .ToList();

            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public override Product GetOne(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            var entity = _context.Products
                .Include(x => x.Manufacturer)
                .Include(x => x.Category)
                .FirstOrDefault(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public override Product Update(Product entity)
    {
        try
        {
            var entityToUpdate = _context.Products.Find(entity.ArticleNumber);
            if (entityToUpdate != null)
            {
                entityToUpdate = entity;
                _context.Products.Update(entityToUpdate);
                _context.SaveChanges();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }
}

