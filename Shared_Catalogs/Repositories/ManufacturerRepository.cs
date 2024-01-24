using Catalog_App.Contexts;
using Catalog_App.Entities;

namespace Shared_Catalogs.Repositories;

public class ManufacturerRepository(ProductsDbContext context) : Repo<Manufacturer, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;
}
