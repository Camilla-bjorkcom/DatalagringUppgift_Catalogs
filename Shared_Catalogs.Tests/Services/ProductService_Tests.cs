using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;
using Shared_Catalogs.Services;

namespace Shared_Catalogs.Tests.Services;

public class ProductService_Tests
{
    private readonly ProductsDbContext _context =
       new(new DbContextOptionsBuilder<ProductsDbContext>()
       .UseInMemoryDatabase($"{Guid.NewGuid()}")
       .Options);

    [Fact]
    public void CreateProductShouid_CreateNewProduct_ReturnNewProduct()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var manufacturerRepository = new ManufacturerRepository(_context);
        var productReviewsRepository = new ProductReviewsRepository(_context);
        var productService = new ProductService(productRepository, productReviewsRepository, manufacturerRepository, categoryService);

        var createProductDto = new CreateProductDto
        {
            Title = "Ny titel",
            Description = "Beskrivning",
            Manufacturer = "Tillverkare",
            Category = "Kategori"
        };

        if (!productRepository.Exists(x => x.Title == createProductDto.Title))
        {
            var categoryEntity = categoryService.CreateCategory(createProductDto.Category);


            var manufacturerEntity = manufacturerRepository.GetOne(x => x.ManufactureName == createProductDto.Manufacturer);
            if (manufacturerEntity == null)
            {
                manufacturerEntity = manufacturerRepository.Create(new Manufacturer
                {
                    ManufactureName = createProductDto.Manufacturer
                });
            }



            // Act
            var productEntity = productRepository.Create(new Product
            {
                ArticleNumber = Guid.NewGuid().ToString(),
                Title = createProductDto.Title,
                Description = createProductDto.Description,
                CategoryId = categoryEntity.Id,
                ManufacturerId = manufacturerEntity.Id,

            });


            // Assert
            Assert.NotNull(productEntity);

        }

    }

}
