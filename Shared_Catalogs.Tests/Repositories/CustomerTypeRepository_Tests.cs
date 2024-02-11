using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Repositories;

namespace Shared_Catalogs.Tests.Repositories;

public class CustomerTypeRepository_Tests
{

    private readonly CustomerDbContext _context =
      new(new DbContextOptionsBuilder<CustomerDbContext>()
      .UseInMemoryDatabase($"{Guid.NewGuid()}")
      .Options);

    [Fact]
    public void CreateShould_AddOneTo_CustomerTypeEntity_AndReturnEntity()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);

        var customerTypeEntity = new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        };


        // Act
        var result = customerTypeRepository.Create(customerTypeEntity);


        // Assert
        Assert.NotNull(result);
        Assert.IsType<CustomerTypeEntity>(result);
    }


    [Fact]
    public void CreateShouldNot_AddOneTo_CustomerTypeEntity_IfAlreadyExists_AndReturnNull()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);

        var customerTypeEntity = customerTypeRepository.Create(new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        });


        // Act
        var result = customerTypeRepository.Create(customerTypeEntity);


        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllShouldGetAllRecords_ReturnIEnumerableofTypeCustomerType()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);

        customerTypeRepository.Create(new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        });

        // Act
        var result = customerTypeRepository.GetAll();


        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerTypeEntity>>(result);
    }

    [Fact]
    public void GetAllShouldNotGetAllRecords_ReturnNull()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);



        // Act
        var result = customerTypeRepository.GetAll();


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void GetOne_ShouldGetOneCustomerType_ReturnOneCustomerType()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);


        var customerTypeEntity = customerTypeRepository.Create(new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        });


        // Act
        var result = customerTypeRepository.GetOne(x => x.Id == customerTypeEntity.Id);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerTypeEntity.Id, result.Id);
    }


    [Fact]
    public void GetOne_ShouldNotGetOneCustomerType_ReturnNull()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);

        var customerTypeEntity = new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        };


        // Act
        var result = customerTypeRepository.GetOne(x => x.Id == customerTypeEntity.Id);


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void Update_ShouldUpdateCustomerType_ReturnUpdatedCustomerType()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);


        var customerTypeEntity = customerTypeRepository.Create(new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        });


        // Act
        var existingCustomeType = customerTypeRepository.GetOne(x => x.Id == customerTypeEntity.Id);
        customerTypeEntity.CustomerType = "Ny kundtyp";
        var result = customerTypeRepository.Update(existingCustomeType);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingCustomeType.CustomerType, result.CustomerType);
    }

    [Fact]
    public void Delete_ShouldDeleteOneCustomerType_ReturnTrue()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);

        var customerTypeEntity = customerTypeRepository.Create(new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        });

        // Act
        var result = customerTypeRepository.Delete(x => x.Id == customerTypeEntity.Id);


        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldNotDeleteOneCustomerType_ReturnFalse()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);

        var customerTypeEntity = new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        };

        // Act
        var result = customerTypeRepository.Delete(x => x.Id == customerTypeEntity.Id);


        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Exists_ShouldReturnOneCustomerType_ReturnTrue()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);

        var customerTypeEntity = customerTypeRepository.Create(new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        });


        // Act
        var result = customerTypeRepository.Exists(x => x.Id == 1);


        // Assert 
        Assert.True(result);
    }

    [Fact]
    public void Exists_ShouldNotReturnOneCustomer_ReturnFalse()
    {
        // Arrange
        var customerTypeRepository = new CustomerTypeRepository(_context);



        // Act
        var result = customerTypeRepository.Exists(x => x.Id == 1);


        // Assert 
        Assert.False(result);
    }
}
