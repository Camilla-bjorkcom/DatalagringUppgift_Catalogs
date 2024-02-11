using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;

namespace Shared_Catalogs.Tests.Repositories;

public class ProductReviewsRepository_Tests
{
    private readonly ProductsDbContext _context =
      new(new DbContextOptionsBuilder<ProductsDbContext>()
      .UseInMemoryDatabase($"{Guid.NewGuid()}")
      .Options);

    [Fact]
    public void CreateShould_CreateNewProductReview_ReturnProductReview()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository( _context);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var manufacturerRepository = new ManufacturerRepository(_context);

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
            Reviews = "Fin och bra!"
        };

        var productReviewEntity = new ProductReview
        {
            ArticleNumber = productReviewDto.ArticleNumber,
            Reviews = productReviewDto.Reviews,
        };

        // Act
        var result = productReviewRepository.Create(productReviewEntity);


        // Assert
        Assert.NotNull(result);
    }


    [Fact]
    public void CreateShouldNotAddOne_ToProductReviewsEntity_ReturnNull()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);
        var productReviewEntity = new ProductReview();


        // Act
        var result = productReviewRepository.Create(productReviewEntity);


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void GetAll_ShouldGetAllRecords_ReturnIEnumerableofTypeProductReviews()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);

        var productReviewEntity = new ProductReview
        {
            ArticleNumber = Guid.NewGuid().ToString(),
            Reviews = "Omdöme",
        };
        productReviewRepository.Create(productReviewEntity);

        // Act
        var result = productReviewRepository.GetAll();


        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductReview>>(result);
    }

    [Fact]
    public void GetAll_ShouldNotGetAllRecords_ReturnNull()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);


        // Act
        var result = productReviewRepository.GetAll();


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void GetOne_ShouldGetOneProductReview_ReturnOneProductReview()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);
        

        var productReviewEntity = new ProductReview
        {
            ArticleNumber = Guid.NewGuid().ToString(),
            Reviews = "Omdöme",
        };
        productReviewRepository.Create(productReviewEntity);

        // Act
        var result = productReviewRepository.GetOne(x => x.ArticleNumber == productReviewEntity.ArticleNumber);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(productReviewEntity.ArticleNumber, result.ArticleNumber);

    }

    [Fact]
    public void GetOne_ShouldNotGetOneProductReview_ReturnNull()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);
        var productReviewEntity = new ProductReview
        {
            ArticleNumber = Guid.NewGuid().ToString(),
            Reviews = "Omdöme",
        };

        // Act
        var result = productReviewRepository.GetOne(x => x.ArticleNumber == productReviewEntity.ArticleNumber);


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void Update_ShouldUpdateProductReview_ReturnUpdatedProductReview()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var manufacturerRepository = new ManufacturerRepository(_context);

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
            Reviews = "Fin och bra!"
        };

        var productReviewEntity = new ProductReview
        {
            ArticleNumber = productReviewDto.ArticleNumber,
            Reviews = productReviewDto.Reviews,
        };
        productReviewRepository.Create(productReviewEntity);



        // Act
        var existingProductReview = productReviewRepository.GetOne(x => x.Id == productReviewEntity.Id);
        existingProductReview.Reviews = "Nytt omdöme";
        var result = productReviewRepository.Update(existingProductReview);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingProductReview.Reviews, result.Reviews);
    }


    [Fact]
    public void Delete_ShouldDeleteOneProductReview_ReturnTrue()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);
      
        var productReviewEntity = new ProductReview
        {
            ArticleNumber = Guid.NewGuid().ToString(),
            Reviews = "Omdöme",
        };
        productReviewRepository.Create(productReviewEntity);


        // Act
        var result = productReviewRepository.Delete(x => x.Id == productReviewEntity.Id);


        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldNotDeleteOneProductReview_ReturnFalse()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);

        var productReviewEntity = new ProductReview
        {
            ArticleNumber = Guid.NewGuid().ToString(),
            Reviews = "Omdöme",
        };

        // Act
        var result = productReviewRepository.Delete(x => x.Id == productReviewEntity.Id);


        // Assert
        Assert.False(result);
    }


    [Fact]
    public void Exists_ShouldReturnOneProductReview_ReturnTrue()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);
        var productReviewEntity = new ProductReview
        {
            ArticleNumber = Guid.NewGuid().ToString(),
            Reviews = "Omdöme",
        };
        productReviewRepository.Create(productReviewEntity);

        // Act
        var result = productReviewRepository.Exists(x => x.ArticleNumber == productReviewEntity.ArticleNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Exists_ShouldNotReturnOneProductReview_ReturnFalse()
    {
        // Arrange
        var productReviewRepository = new ProductReviewsRepository(_context);


        // Act
        bool result = productReviewRepository.Exists(x => x.ArticleNumber == $"{Guid.NewGuid()}");


        // Assert 
        Assert.False(result);
    }




}
