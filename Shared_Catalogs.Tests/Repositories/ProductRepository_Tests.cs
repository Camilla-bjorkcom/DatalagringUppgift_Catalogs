using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;

namespace Shared_Catalogs.Tests.Repositories;

public class ProductRepository_Tests
{
    private readonly ProductsDbContext _context =
        new(new DbContextOptionsBuilder<ProductsDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public void Create_Should_Add_One_To_ProductEntity_And_Return_Entity()
    {
        // Arrange
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


        // Act
        var result = productRepository.Create(productEntity);


        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Create_Should_Not_Add_One_To_ProductEntity_And_Return_Null()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productEntity = new Product();


        // Act
        var result = productRepository.Create(productEntity);


        // Assert
        Assert.Null(result);
    }

   

    [Fact]
    public void GetAll_ShouldGetAllRecords_ReturnIEnumerableofTypeProduct()
    {
        // Arrange
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


        // Act
        var result = productRepository.GetAll();


        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
    }

    [Fact]
    public void GetAll_ShouldNotGetAllRecords_ReturnNull()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);


        // Act
        var result = productRepository.GetAll();


        // Assert
        Assert.Null(result);
        
    }

    [Fact]
    public void GetOne_ShouldGetOneProduct_ReturnOneProduct()
    {
        // Arrange
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


        // Act
        var result = productRepository.GetOne(x => x.Title == productEntity.Title);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(productEntity.Title, result.Title);

    }

    [Fact]
    public void GetOne_ShouldNotGetOneProduct_ReturnNull()
    {
        // Arrange
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


        // Act
        var result = productRepository.GetOne(x => x.Title == productEntity.Title);


        // Assert
        Assert.Null(result);

    }



    [Fact]
    public void Update_ShouldUpdateProduct_ReturnUpdatedProduct()
    {
        // Arrange
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

        productEntity = productRepository.Create(productEntity);



        // Act
        var existingProduct = productRepository.GetOne(x => x.ArticleNumber == productEntity.ArticleNumber);
        existingProduct.Title = "Ny titel";
        var result = productRepository.Update(existingProduct);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingProduct.Title, result.Title);

    }


    [Fact]
    public void Delete_ShouldDeleteOneProduct_ReturnTrue()
    {
        // Arrange
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

        // Act
        var result = productRepository.Delete(x => x.Title == productEntity.Title);


        // Assert
        Assert.True(result);

    }

    [Fact]
    public void Delete_ShouldNotDeleteOneProduct_ReturnFalse()
    {
        // Arrange
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

        // Act
        var result = productRepository.Delete(x => x.Title == productEntity.Title);


        // Assert
        Assert.False(result);

    }
}
