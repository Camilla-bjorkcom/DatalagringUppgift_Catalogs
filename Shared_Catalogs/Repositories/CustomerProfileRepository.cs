using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities;

namespace Shared_Catalogs.Repositories;

public class CustomerProfileRepository(CustomerDbContext context) : Repo<CustomerProfilesEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;
}
