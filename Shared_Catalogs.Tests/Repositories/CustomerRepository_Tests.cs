﻿using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Repositories;

namespace Shared_Catalogs.Tests.Repositories;

public class CustomerRepository_Tests
{
    private readonly CustomerDbContext _context =
      new(new DbContextOptionsBuilder<CustomerDbContext>()
      .UseInMemoryDatabase($"{Guid.NewGuid()}")
      .Options);

    [Fact]
    public void CreateShould_AddOneTo_CustomerEntity_AndReturnEntity()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
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
            var customerEntity = new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            };


            // Act
            var result = customerRepository.Create(customerEntity);


            // Assert
            Assert.NotNull(result);
            Assert.IsType<CustomersEntity>(result);
        }
    }


    [Fact]
    public void CreateShouldNot_AddOneTo_CustomerEntity_IfAlreadyExists_AndReturnNull()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
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
            var customerEntity = new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            };
            customerRepository.Create(customerEntity);

            // Act
            var result = customerRepository.Create(customerEntity);


            // Assert
            Assert.Null(result);
        }

    }

    [Fact]
    public void GetAllShouldGetAllRecords_ReturnIEnumerableofTypeCustomer()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
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
            var customerEntity = new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            };
            customerRepository.Create(customerEntity);

            // Act
            var result = customerRepository.GetAll();


            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<CustomersEntity>>(result);
        }
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
    public void GetOne_ShouldGetOneCustomer_ReturnOneCustomer()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
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
            var customerEntity = new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            };
            customerRepository.Create(customerEntity);


            // Act
            var result = customerRepository.GetOne(x => x.Id == customerEntity.Id);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerEntity.Id, result.Id);

        }

    }


    [Fact]
    public void GetOne_ShouldNotGetOneCustomer_ReturnNull()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
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
            var customerEntity = new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            };

            // Act
            var result = customerRepository.GetOne(x => x.Id == customerEntity.Id);


            // Assert
            Assert.Null(result);


        }
    }


    [Fact]
    public void Update_ShouldUpdateCustomer_ReturnUpdatedCustomer()
    {

        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
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
            var customerEntity = new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            };
            customerRepository.Create(customerEntity);



            // Act
            var existingCustomer = customerRepository.GetOne(x => x.Id == customerEntity.Id);
            existingCustomer.Addresses.StreetName = "Nytt gatunamn";
            var result = customerRepository.Update(existingCustomer);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingCustomer.Addresses.StreetName, result.Addresses.StreetName);

        }
    }
    [Fact]
    public void Delete_ShouldDeleteOneCustomer_ReturnTrue()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
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
            var customerEntity = new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            };
            customerRepository.Create(customerEntity);



            // Act
            var result = customerRepository.Delete(x => x.Id == customerEntity.Id);


            // Assert
            Assert.True(result);
        }
    }

    [Fact]
    public void Delete_ShouldNotDeleteOneCustomer_ReturnFalse()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
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
            var customerEntity = new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            };



            // Act
            var result = customerRepository.Delete(x => x.Id == customerEntity.Id);


            // Assert
            Assert.False(result);
        }
    }

    [Fact]
    public void Exists_ShouldReturnOneCustomer_ReturnTrue()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
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
            var customerEntity = new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            };
            customerRepository.Create(customerEntity);


            // Act
            var result = customerRepository.Exists(x => x.Id == 1);


            // Assert 
            Assert.True(result);
        }
    }

    [Fact]
    public void Exists_ShouldNotReturnOneCustomer_ReturnFalse()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);


        // Act
        var result = customerRepository.Exists(x => x.Id == 1);


        // Assert 
        Assert.False(result);
    }
}





