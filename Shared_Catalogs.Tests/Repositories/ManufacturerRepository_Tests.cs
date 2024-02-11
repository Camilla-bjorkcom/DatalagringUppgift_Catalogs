using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;

namespace Shared_Catalogs.Tests.Repositories;

public class ManufacturerRepository_Tests
{
    private readonly ProductsDbContext _context =
    new(new DbContextOptionsBuilder<ProductsDbContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);


    [Fact]
    public void CreateShouldAddOne_ToManufacturerEntity_ReturnEntity()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);

        var manufacturerEntity = new Manufacturer
        {
            Id = 1,
            ManufactureName = "Tillverkarens namn"
        };


        // Act
        var result = manufacturerRepository.Create(manufacturerEntity);


        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void CreateShouldNotAddOne_ToManufcaturerEntity_ReturnNull()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);
        var manufacturerEntity = new Manufacturer();

        // Act
        var result = manufacturerRepository.Create(manufacturerEntity);


        // Assert
        Assert.Null(result);
    }



    [Fact]
    public void GetAll_ShouldGetAllRecords_ReturnIEnumerableofTypeManufacturer()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);

        var manufacturerEntity = new Manufacturer
        {
            Id = 1,
            ManufactureName = "Tillverkarens namn"
        };
        manufacturerRepository.Create(manufacturerEntity);


        // Act
        var result = manufacturerRepository.GetAll();


        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Manufacturer>>(result);
    }

    [Fact]
    public void GetAll_ShouldNotGetAllRecords_ReturnNull()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);


        // Act
        var result = manufacturerRepository.GetAll();


        // Assert
        Assert.Null(result);

    }

    [Fact]
    public void GetOne_ShouldGetOneManufacturer_ReturnOneManufacturer()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);

        var manufacturerEntity = new Manufacturer
        {
            Id = 1,
            ManufactureName = "Tillverkarens namn"
        };
        manufacturerRepository.Create(manufacturerEntity);



        // Act
        var result = manufacturerRepository.GetOne(x => x.ManufactureName == manufacturerEntity.ManufactureName);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(manufacturerEntity.ManufactureName, result.ManufactureName);

    }

    [Fact]
    public void GetOne_ShouldNotGetOneManufacturer_ReturnNull()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);

        var manufacturerEntity = new Manufacturer
        {
            Id = 1,
            ManufactureName = "Tillverkarens namn"
        };


        // Act
        var result = manufacturerRepository.GetOne(x => x.ManufactureName == manufacturerEntity.ManufactureName);


        // Assert
        Assert.Null(result);

    }



    [Fact]
    public void Update_ShouldUpdateManufacturer_ReturnUpdatedManufacturer()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);

        var manufacturerEntity = new Manufacturer
        {
            Id = 1,
            ManufactureName = "Tillverkarens namn"
        };
        manufacturerRepository.Create(manufacturerEntity);



        // Act
        var existingManufacturer = manufacturerRepository.GetOne(x => x.Id == manufacturerEntity.Id);
        existingManufacturer.ManufactureName = "Nytt namn";
        var result = manufacturerRepository.Update(existingManufacturer);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingManufacturer.ManufactureName, result.ManufactureName);

    }


    [Fact]
    public void Delete_ShouldDeleteOneManufacturer_ReturnTrue()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);

        var manufacturerEntity = new Manufacturer
        {
            Id = 1,
            ManufactureName = "Tillverkarens namn"
        };
        manufacturerRepository.Create(manufacturerEntity);


        // Act
        var result = manufacturerRepository.Delete(x => x.Id == manufacturerEntity.Id);


        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldNotDeleteOneManufacturer_ReturnFalse()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);


        var manufacturerEntity = new Manufacturer
        {
            Id = 1,
            ManufactureName = "Tillverkarens namn"
        };
  
        // Act
        var result = manufacturerRepository.Delete(x => x.Id == manufacturerEntity.Id);


        // Assert
        Assert.False(result);

    }


    [Fact]
    public void Exists_ShouldReturnOneManufacturer_ReturnTrue()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);
        var manufacturerEntity = new Manufacturer
        {
            Id = 1,
            ManufactureName = "Tillverkarens namn"
        };
        manufacturerRepository.Create(manufacturerEntity);


        // Act
        var result = manufacturerRepository.Exists(x => x.Id == manufacturerEntity.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Exists_ShouldNotReturnOneProduct_ReturnFalse()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);

        // Act
        bool result = manufacturerRepository.Exists(x => x.Id == 1);


        // Assert 
        Assert.False(result);
    }
}
