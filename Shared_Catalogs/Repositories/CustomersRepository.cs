using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities;

namespace Shared_Catalogs.Repositories
{
    public class CustomersRepository(CustomerDbContext context) : Repo<CustomersEntity, CustomerDbContext>(context)
    {
        private readonly CustomerDbContext _context = context;
    }
}
