using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories
{
    public class CustomersRepository(CustomerDbContext context) : Repo<CustomersEntity, CustomerDbContext>(context)
    {
        private readonly CustomerDbContext _context = context;


        public override async Task<IEnumerable<CustomersEntity>> GetAllAsync()
        {
            try
            {
                var entities = await _context.Customers
                    .Include(x => x.Addresses)
                    .Include(x => x.CustomerType).ThenInclude(x => x.CustomerType)
                     .Include(x => x.CustomerProfiles)
                      .Include(x => x.ContactInformation)
                    .ToListAsync();
                if (entities.Count != 0)
                {
                    return entities;
                }
            }
            catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

            return null!;
        }

        public override async Task<CustomersEntity> GetOneAsync(Expression<Func<CustomersEntity, bool>> predicate)
        {
            try
            {
                var entity = await _context.Customers
                    .Include(x => x.Addresses)
                    .Include(x => x.CustomerType).ThenInclude(x => x.CustomerType)
                     .Include(x => x.CustomerProfiles)
                      .Include(x => x.ContactInformation)
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
}
