using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Repositories;

namespace Shared_Catalogs.Tests.Repositories;

public class AddressRepository_Tests
{
    private readonly CustomerDbContext _context =
     new(new DbContextOptionsBuilder<CustomerDbContext>()
     .UseInMemoryDatabase($"{Guid.NewGuid()}")
     .Options);

    [Fact]
    public void CreateShould_AddOneTo_AddressEntity_AndReturnEntity()
    {
        // Arrange
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = new AddressesEntity
        {

            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        };



        // Act
        var result = addressRepository.Create(addressEntity);


        // Assert
        Assert.NotNull(result);
        Assert.IsType<AddressesEntity>(result);
    }

    [Fact]
    public void CreateShouldNot_AddOneTo_AddressEntity_IfAlreadyExists_AndReturnNull()
    {
        // Arrange
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = addressRepository.Create(new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        });


        // Act
        var result = addressRepository.Create(addressEntity);


        // Assert
        Assert.Null(result);
    }



    [Fact]
    public void GetAllShouldGetAllRecords_ReturnIEnumerableofTypeAddress()
    {
        // Arrange
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = addressRepository.Create(new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        });


        // Act
        var result = addressRepository.GetAll();


        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AddressesEntity>>(result);
    }


    [Fact]
    public void GetAllShouldNotGetAllRecords_ReturnNull()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);


        // Act
        var result = customerRepository.GetAll();


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void GetOne_ShouldGetOneAddress_ReturnOneAddress()
    {
        // Arrange
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = addressRepository.Create(new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        });



        // Act
        var result = addressRepository.GetOne(x => x.Id == addressEntity.Id);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(addressEntity.Id, result.Id);
    }




    [Fact]
    public void GetOne_ShouldNotGetOneAddress_ReturnNull()
    {
        // Arrange
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        };

        // Act
        var result = addressRepository.GetOne(x => x.Id == addressEntity.Id);


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void Update_ShouldUpdateAddress_ReturnUpdatedAddress()
    {

        // Arrange
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = addressRepository.Create(new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        });

        // Act
        var existingAddress = addressRepository.GetOne(x => x.Id == addressEntity.Id);
        existingAddress.StreetName = "Nytt gatunamn";
        var result = addressRepository.Update(existingAddress);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingAddress.StreetName, result.StreetName);
    }

    [Fact]
    public void Delete_ShouldDeleteOneAddress_ReturnTrue()
    {
        // Arrange
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = addressRepository.Create(new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        });
        // Act
        var result = addressRepository.Delete(x => x.Id == addressEntity.Id);


        // Assert
        Assert.True(result);

    }

    [Fact]
    public void Delete_ShouldNotDeleteOneCustomer_ReturnFalse()
    {
        // Arrange
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        };

        // Act
        var result = addressRepository.Delete(x => x.Id == addressEntity.Id);


        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Exists_ShouldReturnOneAddress_ReturnTrue()
    {
        // Arrange
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = addressRepository.Create(new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        });


        // Act
        var result = addressRepository.Exists(x => x.Id == 1);


        // Assert 
        Assert.True(result);
    }

    [Fact]
    public void Exists_ShouldNotReturnOneCustomer_ReturnFalse()
    {
        var addressRepository = new AddressesRepository(_context);

        var addressEntity = new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        };


        // Act
        var result = addressRepository.Exists(x => x.Id == 1);


        // Assert 
        Assert.False(result);
    }

}

