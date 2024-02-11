using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories
{
    public class CustomerPhoneNumbersRepository(CustomerDbContext context) : Repo<CustomerPhoneNumbersEntity, CustomerDbContext>(context)
    {
        private readonly CustomerDbContext _context = context;

        public override CustomerPhoneNumbersEntity Update(CustomerPhoneNumbersEntity entity)
        {
            try
            {
                var entityToUpdate = _context.CustomerPhoneNumbers.Find(entity.PhoneNumber, entity.ContactInformationId);
                if (entityToUpdate != null)
                {
                     entityToUpdate = entity;
                    _context.CustomerPhoneNumbers.Update(entityToUpdate);
                    _context.SaveChanges();

                    return entityToUpdate;
                }
            }
            catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

            return null!;
        }

    }
}
