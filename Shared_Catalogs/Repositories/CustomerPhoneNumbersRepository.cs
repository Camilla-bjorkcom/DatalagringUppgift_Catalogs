using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories
{
    public class CustomerPhoneNumbersRepository(CustomerDbContext context) : Repo<CustomerPhoneNumbersEntity, CustomerDbContext>(context)
    {
        private readonly CustomerDbContext _context = context;
        public override async Task<IEnumerable<CustomerPhoneNumbersEntity>> GetAllAsync()
        {
            try
            {
                var entities = await _context.CustomerPhoneNumbers
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

        public override async Task<CustomerPhoneNumbersEntity> GetOneAsync(Expression<Func<CustomerPhoneNumbersEntity, bool>> predicate)
        {
            try
            {
                var entity = await _context.CustomerPhoneNumbers
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
