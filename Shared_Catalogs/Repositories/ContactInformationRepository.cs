using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class ContactInformationRepository(CustomerDbContext context) : Repo<ContactInformationEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;
}
