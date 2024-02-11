using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class ContactInformationRepository(CustomerDbContext context) : Repo<ContactInformationEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

    public override ContactInformationEntity Update(ContactInformationEntity entity)
    {
        try
        {
            var entityToUpdate = _context.ContactInformation.Find(entity.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate = entity;
                _context.ContactInformation.Update(entityToUpdate);
                _context.SaveChanges();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }
}
