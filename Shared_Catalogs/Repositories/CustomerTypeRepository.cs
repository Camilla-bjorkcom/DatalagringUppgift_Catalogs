using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class CustomerTypeRepository(CustomerDbContext context) : Repo<CustomerTypeEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

    public override CustomerTypeEntity Update(CustomerTypeEntity entity)
    {
        try
        {
            var entityToUpdate = _context.CustomerType.Find(entity.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate = entity;
                _context.CustomerType.Update(entityToUpdate);
                _context.SaveChanges();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

}
