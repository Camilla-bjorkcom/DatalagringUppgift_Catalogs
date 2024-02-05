﻿using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class CustomersRepository(CustomerDbContext context) : Repo<CustomersEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

    public override IEnumerable<CustomersEntity> GetAll()
    {
        try
        {
            var entities = _context.Customers
                .Include(x => x.Addresses)
                .Include(x => x.CustomerType)
                 .Include(x => x.CustomerProfiles)
                  .Include(x => x.ContactInformation)
                .ToList();
            
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public override CustomersEntity GetOne(Expression<Func<CustomersEntity, bool>> predicate)
    {
        try
        {
            var entity = _context.Customers
                .Include(x => x.Addresses)
                .Include(x => x.CustomerType)
                 .Include(x => x.CustomerProfiles)
                  .Include(x => x.ContactInformation)
                .FirstOrDefault(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }
}




