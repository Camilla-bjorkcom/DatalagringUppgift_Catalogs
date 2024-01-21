using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities;

namespace Shared_Catalogs.Repositories;

public class CustomerTypeRepository(CustomerDbContext context) : Repo<CustomerTypeEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;
}
