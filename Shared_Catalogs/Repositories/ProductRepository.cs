using Catalog_App.Contexts;
using Catalog_App.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class ProductRepository(ProductsDbContext context) : Repo<Product, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Products
                .Include(x => x.Category).ThenInclude(x => x.CategoryName)
                .Include(x => x.StockQuantity).ThenInclude(x => x.Quantity)
                .Include(x => x.ManufactureName).ThenInclude(x => x.ManufactureName)
                .ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public override async Task<Product> GetOneAsync(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            //söker fram enititen beroende på vad man hämtat, t.ex. mail, id och returnerar den sen.
            var entity = await _context.Products
                .Include(x => x.Category).ThenInclude(x => x.CategoryName)
                .Include(x => x.StockQuantity).ThenInclude(x => x.Quantity)
                .Include(x => x.ManufactureName).ThenInclude(x => x.ManufactureName)
                .FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
    {
        return _context.Products.Where(x => x.CategoryId == categoryId).ToList();
    }

}
