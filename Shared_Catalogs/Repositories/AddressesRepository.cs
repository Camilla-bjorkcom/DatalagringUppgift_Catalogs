using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class AddressesRepository(CustomerDbContext context) : Repo<AddressesEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;


    public override async Task<IEnumerable<AddressesEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Addresses
                .Include(x => x.Customers) //Eventuellt mer .ThenInclude??
                .ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public override async Task<AddressesEntity> GetOneAsync(Expression<Func<AddressesEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Addresses
                .Include(x => x.Customers) //Eventuellt mer .ThenInclude??
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
