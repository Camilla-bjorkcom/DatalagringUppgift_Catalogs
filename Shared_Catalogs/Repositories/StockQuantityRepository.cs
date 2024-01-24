.using Catalog_App.Contexts;
using Catalog_App.Entities;

namespace Shared_Catalogs.Repositories;

public class StockQuantityRepository(ProductsDbContext context) : Repo<StockQuantity, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;


    //Update StockQuantity?, expression-del?
}
