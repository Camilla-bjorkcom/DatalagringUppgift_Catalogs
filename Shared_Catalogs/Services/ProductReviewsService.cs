using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;
using System.Diagnostics;

namespace Shared_Catalogs.Services;

public class ProductReviewsService(ProductReviewsRepository productReviewsRepository)
{
    private readonly ProductReviewsRepository _productReviewsRepository = productReviewsRepository;

    public ProductReview CreateProductReview(ProductReviewsDto productReviews)
    {
        try
        {
            var productReviewEntity = _productReviewsRepository.Create(new ProductReview
            {
                ArticleNumber = productReviews.ArticleNumber,
                Reviews = productReviews.Reviews,
            });
            if (productReviewEntity != null)
            {
                return productReviewEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
