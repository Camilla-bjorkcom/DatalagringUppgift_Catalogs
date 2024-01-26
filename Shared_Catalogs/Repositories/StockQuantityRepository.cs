.using Catalog_App.Contexts;
using Catalog_App.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class StockQuantityRepository(ProductsDbContext context) : Repo<StockQuantity, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;

    public override async Task<IEnumerable<StockQuantity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.StockQuantities
                .Include(x => x.Products) //Eventuellt mer .ThenInclude??
                .ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public override async Task<StockQuantity> GetOneAsync(Expression<Func<StockQuantity, bool>> predicate)
    {
        try
        {
            var entity = await _context.StockQuantities
                .Include(x => x.Products) //Eventuellt mer .ThenInclude??
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
