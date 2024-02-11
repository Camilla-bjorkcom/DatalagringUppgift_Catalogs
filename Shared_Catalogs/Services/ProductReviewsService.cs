using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;
using System.Diagnostics;

namespace Shared_Catalogs.Services;

public class ProductReviewsService(ProductReviewsRepository productReviewsRepository, ProductRepository productRepository)
{
    private readonly ProductReviewsRepository _productReviewsRepository = productReviewsRepository;
    private readonly ProductRepository _productRepository = productRepository;

    public ProductReview CreateProductReview(ProductReviewsDto productReviews)
    {
        try
        {
            var exisitingProductArticleNumber = _productRepository.Exists(x => x.ArticleNumber == productReviews.ArticleNumber);
            if (exisitingProductArticleNumber == true) 
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
           
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
