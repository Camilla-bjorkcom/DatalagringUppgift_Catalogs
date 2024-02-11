
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;
using System.Diagnostics;

namespace Shared_Catalogs.Repositories;

public class ProductReviewsRepository(ProductsDbContext context) : Repo<ProductReview, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;

    public override ProductReview Update(ProductReview entity)
    {
        try
        {
            var entityToUpdate = _context.ProductReviews.Find(entity.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate = entity;
                _context.ProductReviews.Update(entityToUpdate);
                _context.SaveChanges();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }
}
