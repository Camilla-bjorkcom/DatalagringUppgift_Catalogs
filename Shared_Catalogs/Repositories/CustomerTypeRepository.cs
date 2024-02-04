using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class CustomerTypeRepository(CustomerDbContext context) : Repo<CustomerTypeEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

    //public override async Task<IEnumerable<CustomerTypeEntity>> GetAllAsync()
    //{
    //    try
    //    {
    //        var entities = await _context.CustomerType
    //            .Include(x => x.Customers) //Eventuellt mer .ThenInclude??
    //            .ToListAsync();
    //        if (entities.Count != 0)
    //        {
    //            return entities;
    //        }
    //    }
    //    catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

    //    return null!;
    //}

    //public override async Task<CustomerTypeEntity> GetOneAsync(Expression<Func<CustomerTypeEntity, bool>> predicate)
    //{
    //    try
    //    {
    //        var entity = await _context.CustomerType
    //            .Include(x => x.Customers) //Eventuellt mer .ThenInclude??
    //            .FirstOrDefaultAsync(predicate);
    //        if (entity != null)
    //        {
    //            return entity;
    //        }
    //    }
    //    catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

    //    return null!;
    //}
}
