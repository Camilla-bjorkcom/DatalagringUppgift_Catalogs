using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class CustomerProfileRepository(CustomerDbContext context) : Repo<CustomerProfilesEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;


    public override async Task<IEnumerable<CustomerProfilesEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.CustomerProfiles
                .Include(x => x.Customer) //Eventuellt mer .ThenInclude??
                .ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public override async Task<CustomerProfilesEntity> GetOneAsync(Expression<Func<CustomerProfilesEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.CustomerProfiles
                .Include(x => x.Customer) //Eventuellt mer .ThenInclude??
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
