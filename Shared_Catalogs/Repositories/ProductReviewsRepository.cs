
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;

namespace Shared_Catalogs.Repositories;

public class ProductReviewsRepository(ProductsDbContext context) : Repo<ProductReview, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;
}
