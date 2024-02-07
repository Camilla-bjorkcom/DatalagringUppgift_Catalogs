
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;

namespace Shared_Catalogs.Repositories;

public class CategoryRepository (ProductsDbContext context) : Repo<Category, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;
}