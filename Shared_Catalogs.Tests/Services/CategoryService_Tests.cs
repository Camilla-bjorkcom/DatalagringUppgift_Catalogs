using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;
using Shared_Catalogs.Services;

namespace Shared_Catalogs.Tests.Services;

public class CategoryService_Tests
{
    private readonly ProductsDbContext _context =
      new(new DbContextOptionsBuilder<ProductsDbContext>()
      .UseInMemoryDatabase($"{Guid.NewGuid()}")
      .Options);

    [Fact]
    public void CreateCategory_ShouldCreateCategoryIfNotExists_ElseReturnCategoryEntity()
    {

        //Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var manufacturerRepository = new ManufacturerRepository(_context);
        var productReviewsRepository = new ProductReviewsRepository(_context);
        var productService = new ProductService(productRepository, productReviewsRepository, manufacturerRepository, categoryService);

        var createProductDto = new CreateProductDto
        {
            Title = "Ny",
            Description = "Beskrivning",
            Manufacturer = "Tillverkare",
            Category = "Kategori"
        };
        var product = productService.CreateProduct(createProductDto);

        // Act
        var result = categoryService.CreateCategory(product.Category.CategoryName);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Category>(result);
    }

    [Fact]
    public void CreateCategory_ShouldNotCreateCategoryIfExists_ReturnCategoryEntity()
    {

        //Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var manufacturerRepository = new ManufacturerRepository(_context);
        var productReviewsRepository = new ProductReviewsRepository(_context);
        var productService = new ProductService(productRepository, productReviewsRepository, manufacturerRepository, categoryService);

        var createProductDto = new CreateProductDto
        {
            Title = "Ny",
            Description = "Beskrivning",
            Manufacturer = "Tillverkare",
            Category = "Kategori"
        };
       productService.CreateProduct(createProductDto);

        var createProductDto2 = new CreateProductDto
        {
            Title = "Nyare titel",
            Description = "Beskrivning",
            Manufacturer = "Tillverkare",
            Category = "Kategori"
        };
        var product2 = productService.CreateProduct(createProductDto2);

        // Act
        var result = categoryService.CreateCategory(product2.Category.CategoryName);

        // Assert
        Assert.Equal(product2.Category.CategoryName, result.CategoryName);
    }


    [Fact]
    public void CreateCategory_ShouldNotCreateCategoryIfCategoryNameIsOver50Lenght_ReturnNull()
    {

        //Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);

        // Act
        var result = categoryService.CreateCategory("femtiobokstäverfemtiobokstäverfemtiobokstäverfemtiobokstäver");

        // Assert
        Assert.Null(result);
    }
}
