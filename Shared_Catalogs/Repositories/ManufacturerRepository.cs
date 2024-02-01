using Catalog_App.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Entities;
using Shared_Catalogs.Entities.Products;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class ManufacturerRepository(ProductsDbContext context) : Repo<Manufacturer, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;

    public override async Task<IEnumerable<Manufacturer>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Manufacturers
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

    public override async Task<Manufacturer> GetOneAsync(Expression<Func<Manufacturer, bool>> predicate)
    {
        try
        {
            var entity = await _context.Manufacturers
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
