
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;

namespace Shared_Catalogs.Repositories;

public class ManufacturerRepository(ProductsDbContext context) : Repo<Manufacturer, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;
}
