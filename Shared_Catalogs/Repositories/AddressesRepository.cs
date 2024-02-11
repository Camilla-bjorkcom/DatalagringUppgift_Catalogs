using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class AddressesRepository(CustomerDbContext context) : Repo<AddressesEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

    public override AddressesEntity Update(AddressesEntity entity)
    {
        try
        {
            var entityToUpdate = _context.Addresses.Find(entity.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate = entity;
                _context.Addresses.Update(entityToUpdate);
                _context.SaveChanges();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }
}

