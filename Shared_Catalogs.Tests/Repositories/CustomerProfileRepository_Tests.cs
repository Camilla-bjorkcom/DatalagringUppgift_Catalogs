using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Repositories;

namespace Shared_Catalogs.Tests.Repositories;

public class CustomerProfileRepository_Tests
{

    private readonly CustomerDbContext _context =
     new(new DbContextOptionsBuilder<CustomerDbContext>()
     .UseInMemoryDatabase($"{Guid.NewGuid()}")
     .Options);


    [Fact]
    public void CreateShould_AddOneTo_CustomerProfileEntity_AndReturnEntity()
    {
        // Arrange
        var customerProfileRepository = new CustomerProfileRepository(_context);

        var customerProfileEntity = new CustomerProfilesEntity
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            CustomerId = 1
        };

        // Act
        var result = customerProfileRepository.Create(customerProfileEntity);


        // Assert
        Assert.NotNull(result);
        Assert.IsType<CustomerProfilesEntity>(result);
    }


    [Fact]
    public void CreateShouldNot_AddOneTo_CustomerProfileEntity_IfAlreadyExists_AndReturnNull()
    {
        // Arrange
        var customerProfileRepository = new CustomerProfileRepository(_context);


        var customerProfileEntity = customerProfileRepository.Create(new CustomerProfilesEntity
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            CustomerId = 1
        });

        // Act
        var result = customerProfileRepository.Create(customerProfileEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllShouldGetAllRecords_ReturnIEnumerableofTypeCustomerProfile()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var addressRepository = new AddressesRepository(_context);


        var addressEntity = addressRepository.Create(new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        });

        var customerTypeEntity = customerTypeRepository.Create(new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        });

        if (customerTypeEntity != null && addressEntity != null)
        {
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });

            customerProfileRepository.Create(new CustomerProfilesEntity
            {
                FirstName = "Förnamn",
                LastName = "Efternamn",
                CustomerId = customerEntity.Id
            });

            // Act
            var result = customerProfileRepository.GetAll();


            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<CustomerProfilesEntity>>(result);
        }
    }

    [Fact]
    public void GetAllShouldNotGetAllRecords_ReturnNull()
    {
        // Arrange
        var customerProfileRepository = new CustomerProfileRepository(_context);


        // Act
        var result = customerProfileRepository.GetAll();


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void GetOne_ShouldGetOneContactInformation_ReturnOneCustomerProfile()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var addressRepository = new AddressesRepository(_context);


        var addressEntity = addressRepository.Create(new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        });

        var customerTypeEntity = customerTypeRepository.Create(new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        });

        if (customerTypeEntity != null && addressEntity != null)
        {
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });

            var customerProfileEntity = customerProfileRepository.Create(new CustomerProfilesEntity
            {
                FirstName = "Förnamn",
                LastName = "Efternamn",
                CustomerId = customerEntity.Id
            });


            // Act
            var result = customerProfileRepository.GetOne(x => x.Id == customerProfileEntity.Id);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerProfileEntity.Id, result.Id);
        }
    }


    [Fact]
    public void GetOne_ShouldNotGetOnCustomerProfile_ReturnNull()
    {
        // Arrange
        var customerProfileRepository = new CustomerProfileRepository(_context);

        var customerProfileEntity = new CustomerProfilesEntity
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            CustomerId = 1
        };

        // Act
        var result = customerProfileRepository.GetOne(x => x.Id == customerProfileEntity.Id);


        // Assert
        Assert.Null(result);
    }



    [Fact]
    public void Update_ShouldUpdateCustomerProfile_ReturnUpdatedCustomerProfile()
    {
        // Arrange
        var customerProfileRepository = new CustomerProfileRepository(_context);


        var customerProfileEntity = customerProfileRepository.Create(new CustomerProfilesEntity
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            CustomerId = 1
        });

        // Act
        var existingCustomerProfile = customerProfileRepository.GetOne(x => x.Id == customerProfileEntity.Id);
        existingCustomerProfile.FirstName = "Nytt förnamn";
        var result = customerProfileRepository.Update(existingCustomerProfile);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingCustomerProfile.FirstName, result.FirstName);

    }

    [Fact]
    public void Delete_ShouldDeleteOneCustomerProfile_ReturnTrue()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var addressRepository = new AddressesRepository(_context);


        var addressEntity = addressRepository.Create(new AddressesEntity
        {
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
        });

        var customerTypeEntity = customerTypeRepository.Create(new CustomerTypeEntity
        {
            CustomerType = "kundtyp"
        });

        if (customerTypeEntity != null && addressEntity != null)
        {
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });

            var customerProfileEntity = customerProfileRepository.Create(new CustomerProfilesEntity
            {
                FirstName = "Förnamn",
                LastName = "Efternamn",
                CustomerId = customerEntity.Id
            });

            // Act
            var result = customerProfileRepository.Delete(x => x.Id == customerProfileEntity.Id);


            // Assert
            Assert.True(result);
        }
    }

    [Fact]
    public void Delete_ShouldNotDeleteOneCustomer_ReturnFalse()
    {
        // Arrange
        var customerProfileRepository = new CustomerProfileRepository(_context);

        var customerProfileEntity = new CustomerProfilesEntity
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            CustomerId = 1
        };

        // Act
        var result = customerProfileRepository.Delete(x => x.Id == customerProfileEntity.Id);


        // Assert
        Assert.False(result);

    }

    [Fact]
    public void Exists_ShouldReturnOneCustomerProfile_ReturnTrue()
    {
        // Arrange
        var customerProfileRepository = new CustomerProfileRepository(_context);
        

            var customerProfileEntity = customerProfileRepository.Create(new CustomerProfilesEntity
            {
                FirstName = "Förnamn",
                LastName = "Efternamn",
                CustomerId = 1
            });


            // Act
            var result = customerProfileRepository.Exists(x => x.Id == customerProfileEntity.Id);


            // Assert 
            Assert.True(result);
        
    }

    [Fact]
    public void Exists_ShouldNotReturnOneCustomer_ReturnFalse()
    {
        // Arrange
        var customerProfileRepository = new CustomerProfileRepository(_context);


        // Act
        var result = customerProfileRepository.Exists(x => x.Id == 1);


        // Assert 
        Assert.False(result);
    }
}
