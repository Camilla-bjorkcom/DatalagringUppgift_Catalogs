using Catalog_App.Contexts;
using Catalog_App.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class CategoryRepository(ProductsDbContext context) : Repo<Category, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Categories
                .Include(x => x.Products).ThenInclude(x => x.ManufactureName).ThenInclude(x => x.ManufactureName)
                .Include(x => x.Products).ThenInclude(x => x.StockQuantity).ThenInclude(x => x.Quantity)
                .ToListAsync();

            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public override async Task<Category> GetOneAsync(Expression<Func<Category, bool>> predicate)
    {
        try
        {
            var entity = await _context.Categories
                .Include(x => x.Products).ThenInclude(x => x.ManufactureName).ThenInclude(x => x.ManufactureName)
                .Include(x => x.Products).ThenInclude(x => x.StockQuantity).ThenInclude(x => x.Quantity)
                .FirstOrDefaultAsync(predicate);

            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }


}
