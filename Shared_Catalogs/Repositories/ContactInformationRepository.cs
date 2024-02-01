using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class ContactInformationRepository(CustomerDbContext context) : Repo<ContactInformationEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

    public override async Task<IEnumerable<ContactInformationEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.ContactInformation
                .Include(x => x.Customer) //Eventuellt mer .ThenInclude??
                .Include(x => x.PhoneNumbers).ThenInclude(x => x.PhoneNumber)
                .ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public override async Task<ContactInformationEntity> GetOneAsync(Expression<Func<ContactInformationEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.ContactInformation
                .Include(x => x.Customer) //Eventuellt mer .ThenInclude??
                .Include(x => x.PhoneNumbers).ThenInclude(x => x.PhoneNumber)
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
