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
    public void CreateProductShould_CreateNewProduct_ReturnNewProduct()
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
            Title = "Ny",
            Description = "Beskrivning",
            Manufacturer = "Tillverkare",
            Category = "Kategori"
        };


        // Act
        var result = productService.CreateProduct(createProductDto);


        // Assert
        Assert.NotNull(result);

    }


    [Fact]
    public void CreateProductShouldNot_CreateNewProductIfTitleAlreadyExists_ReturnNull()
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
        productService.CreateProduct(createProductDto);


        // Act
        var newCreateProductDto = new CreateProductDto
        {
            Title = "Ny titel",
            Description = "Beskrivning",
            Manufacturer = "Tillverkare",
            Category = "Kategori"
        };
         var result = productService.CreateProduct(newCreateProductDto);

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void GetProductByTitle_ShouldReturnProductByTitle()
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
        productService.CreateProduct(createProductDto);


        // Act
        var result = productService.GetProductByTitle(createProductDto.Title);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetProductByTitle_ShouldNotReturnProduct_IfNotExists()
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
      


        // Act
        var result = productService.GetProductByTitle(createProductDto.Title);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllProducts_ShouldReturnAllProducts_IfExists()
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
        productService.CreateProduct(createProductDto);



        // Act
        var result = productService.GetAllProducts();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
    }

    [Fact]
    public void GetAllProducts_ShouldNotReturnAllProducts_IfNotExists()
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
        

        // Act
        var result = productService.GetAllProducts();

        // Assert
        Assert.Null(result);   
    }


    [Fact]
    public void UpdateProducts_ShouldReturnUpdatedProduct_IfExists()
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
        productService.CreateProduct(createProductDto);

        // Act
        var existingProduct = productRepository.GetOne(x => x.Title == createProductDto.Title);
        if (existingProduct != null)
        {
            existingProduct.Title = "Nyare Titel";
        }
        var result = productService.UpdateProduct(existingProduct);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void UpdateProducts_ShouldNotReturnUpdatedProduct_IfNotExists()
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
        var newProduct = productService.CreateProduct(createProductDto);

        // Act
        var existingProduct = productRepository.GetOne(x => x.Title == "titel");
        if (existingProduct != null)
        {
            existingProduct.Title = "Nyare Titel";
        }
        var result = productService.UpdateProduct(existingProduct);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteProduct_ShouldDeleteProduct_ReturnTrue()
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
        var newProduct = productService.CreateProduct(createProductDto);

        // Act
        var result = productService.DeleteProduct(newProduct);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void DeleteProduct_ShouldNotDeleteProduct_IfNotExists_ReturnFalse()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var manufacturerRepository = new ManufacturerRepository(_context);
        var productReviewsRepository = new ProductReviewsRepository(_context);
        var productService = new ProductService(productRepository, productReviewsRepository, manufacturerRepository, categoryService);

        var product = new Product
        {
            ArticleNumber = "1",
            Title = "Title",

        };

        // Act
        var result = productService.DeleteProduct(product);

        // Assert
        Assert.False(result);
    }



}










