using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities;

namespace Shared_Catalogs.Repositories
{
    public class CustomerPhoneNumbersRepository(CustomerDbContext context) : Repo<CustomerPhoneNumbersEntity, CustomerDbContext>(context)
    {
        private readonly CustomerDbContext _context = context;
    }
}
