using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;

namespace Shared_Catalogs.Tests.Repositories;

public class CategoryRepository_Tests
{
    private readonly ProductsDbContext _context =
       new(new DbContextOptionsBuilder<ProductsDbContext>()
       .UseInMemoryDatabase($"{Guid.NewGuid()}")
       .Options);

    [Fact]
    public void CreateShouldAddOneTo_CategoryEntity_AndReturnEntity()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);


        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };


        // Act
        var result = categoryRepository.Create(categoryEntity);


        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void CreateShouldNotAddOne_ToCategoryEntity_AndReturnNull()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new Category();


        // Act
        var result = categoryRepository.Create(categoryEntity);


        // Assert
        Assert.Null(result);
    }



    [Fact]
    public void GetAll_ShouldGetAllRecords_ReturnIEnumerableofTypCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };
        categoryRepository.Create(categoryEntity);


        // Act
        var result = categoryRepository.GetAll();


        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Category>>(result);
    }


    [Fact]
    public void GetAll_ShouldNotGetAllRecords_ReturnNull()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);


        // Act
        var result = categoryRepository.GetAll();


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void GetOne_ShouldGetOneCategory_ReturnOneCategory()
    {
        // Arrange 
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };
        categoryRepository.Create(categoryEntity);



        // Act
        var result = categoryRepository.GetOne(x => x.Id == categoryEntity.Id);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.Id, result.Id);

    }

    [Fact]
    public void GetOne_ShouldNotGetOneCategory_ReturnNull()
    {
        // Arrange 
        var categoryRepository = new CategoryRepository(_context);
       
        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };

        // Act
        var result = categoryRepository.GetOne(x => x.Id == categoryEntity.Id);


        // Assert
        Assert.Null(result);

    }



    [Fact]
    public void Update_ShouldUpdateCategory_ReturnUpdatedCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };
        categoryRepository.Create(categoryEntity);


        // Act
        var existingCategory = categoryRepository.GetOne(x => x.Id == categoryEntity.Id);
        existingCategory.CategoryName = "Ny kategori";
        var result = categoryRepository.Update(existingCategory);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingCategory.CategoryName, result.CategoryName);
    }


    [Fact]
    public void Delete_ShouldDeleteOnCategory_ReturnTrue()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };
        categoryRepository.Create(categoryEntity);


        // Act
        var result = categoryRepository.Delete(x => x.Id == categoryEntity.Id);


        // Assert
        Assert.True(result);

    }

    [Fact]
    public void Delete_ShouldNotDeleteOneCategory_ReturnFalse()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);

        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };


        // Act
        var result = categoryRepository.Delete(x => x.Id == categoryEntity.Id);


        // Assert
        Assert.False(result);
    }


    [Fact]
    public void Exists_ShouldReturnOneCategory_ReturnTrue()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };

        categoryRepository.Create(categoryEntity);

        // Act
        var result = categoryRepository.Exists(x => x.Id == categoryEntity.Id);

        // Assert
        Assert.True(result);

    }

    [Fact]
    public void Exists_ShouldNotReturnOneProduct_ReturnFalse()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new Category
        {
            Id = 1,
            CategoryName = "Kategorinamn"
        };
        categoryRepository.Create(categoryEntity);


        // Act
        bool result = categoryRepository.Exists(x => x.Id == 2);


        // Assert 
        Assert.False(result);
    }
}
