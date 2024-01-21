using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities;

namespace Shared_Catalogs.Repositories;

public class ContactInformationRepository(CustomerDbContext context) : Repo<ContactInformationEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;
}
