using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;
using Shared_Catalogs.Services;

namespace Shared_Catalogs.Tests.Services;

public class ProductReviewService_Tests
{
    private readonly ProductsDbContext _context =
      new(new DbContextOptionsBuilder<ProductsDbContext>()
      .UseInMemoryDatabase($"{Guid.NewGuid()}")
      .Options);

    [Fact]
    public void CreateProductReview_ShouldCreateProductReview_IfProductExists_ReturnProductReviewEntity()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var manufacturerRepository = new ManufacturerRepository(_context);
        var productReviewService = new ProductReviewsService(productReviewRepository, productRepository);

        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };

        categoryRepository.Create(categoryEntity);

        var manufacturerEntity = new Manufacturer
        {
            Id = 1,
            ManufactureName = "Tillverkarens namn"
        };

        manufacturerRepository.Create(manufacturerEntity);

        var productEntity = new Product
        {
            ArticleNumber = Guid.NewGuid().ToString(),
            Title = "Title",
            Description = "Beskrivning",
            CategoryId = categoryEntity.Id,
            ManufacturerId = manufacturerEntity.Id,
        };
        productRepository.Create(productEntity);

        var productReviewDto = new ProductReviewsDto
        {
            ArticleNumber = productEntity.ArticleNumber,
            Reviews = "Omdöme"
        };


        // Act
        var result = productReviewService.CreateProductReview(productReviewDto);


        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductReview>(result);
    }


    [Fact]
    public void CreateProductReview_ShouldNotCreateProductReview_IfProductNotExists_ReturnNull()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);
        var productRepository = new ProductRepository(_context);
        var productReviewService = new ProductReviewsService(productReviewRepository, productRepository);


        var productReviewDto = new ProductReviewsDto
        {
            ArticleNumber = Guid.NewGuid().ToString(),
            Reviews = "Omdöme"
        };


        // Act
        var result = productReviewService.CreateProductReview(productReviewDto);


        // Assert
        Assert.Null(result);
    }
}
