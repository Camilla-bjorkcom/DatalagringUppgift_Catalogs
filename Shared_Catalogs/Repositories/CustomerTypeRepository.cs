using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class CustomerTypeRepository(CustomerDbContext context) : Repo<CustomerTypeEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

}
