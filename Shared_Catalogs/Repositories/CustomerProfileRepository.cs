using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class CustomerProfileRepository(CustomerDbContext context) : Repo<CustomerProfilesEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

    public override CustomerProfilesEntity Update(CustomerProfilesEntity entity)
    {
        try
        {
            var entityToUpdate = _context.CustomerProfiles.Find(entity.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate = entity;
                _context.CustomerProfiles.Update(entityToUpdate);
                _context.SaveChanges();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }
}
