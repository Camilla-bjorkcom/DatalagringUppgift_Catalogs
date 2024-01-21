using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities;

namespace Shared_Catalogs.Repositories;

public class AddressesRepository(CustomerDbContext context) : Repo<AddressesEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;
}
