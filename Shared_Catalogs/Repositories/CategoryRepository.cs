using Catalog_App.Contexts;
using Catalog_App.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class CategoryRepository(ProductsDbContext context) : Repo<Category, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;

    
}
