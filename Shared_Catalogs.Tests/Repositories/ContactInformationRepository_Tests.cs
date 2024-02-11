using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Repositories;

namespace Shared_Catalogs.Tests.Repositories;

public class ContactInformationRepository_Tests
{
    private readonly CustomerDbContext _context =
     new(new DbContextOptionsBuilder<CustomerDbContext>()
     .UseInMemoryDatabase($"{Guid.NewGuid()}")
     .Options);


    [Fact]
    public void CreateShould_AddOneTo_ContactInformationEntity_AndReturnEntity()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
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
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });


            var contactInformationEntity = new ContactInformationEntity
            {
                Id = 1,
                Email = "Epost-address",
                CustomerId = customerEntity.Id
            };

            // Act
            var result = contactInformationRepository.Create(contactInformationEntity);


            // Assert
            Assert.NotNull(result);
            Assert.IsType<ContactInformationEntity>(result);
        }
    }

    [Fact]
    public void CreateShouldNot_AddOneTo_ContactInformationEntity_IfAlreadyExists_AndReturnNull()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
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
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });


            var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
            {
                Email = "Epost-address",
                CustomerId = customerEntity.Id
            });

            // Act
            var result = contactInformationRepository.Create(contactInformationEntity);


            // Assert
            Assert.Null(result);
        }
    }

    [Fact]
    public void GetAllShouldGetAllRecords_ReturnIEnumerableofTypeContactInformation()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
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
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });


            var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
            {
                Email = "Epost-address",
                CustomerId = customerEntity.Id
            });

            // Act
            var result = contactInformationRepository.GetAll();


            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ContactInformationEntity>>(result);
        }
    }

    [Fact]
    public void GetAllShouldNotGetAllRecords_ReturnNull()
    {
        // Arrange
        var contactInformationRepository = new ContactInformationRepository(_context);


        // Act
        var result = contactInformationRepository.GetAll();


        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void GetOne_ShouldGetOneContactInformation_ReturnOneContactInformation()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
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
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });


            var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
            {
                Email = "Epost-address",
                CustomerId = customerEntity.Id
            });


            // Act
            var result = contactInformationRepository.GetOne(x => x.Id == contactInformationEntity.Id);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(contactInformationEntity.Id, result.Id);
        }
    }


    [Fact]
    public void GetOne_ShouldNotGetOneCustomer_ReturnNull()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
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
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });


            var contactInformationEntity = new ContactInformationEntity
            {
                Email = "Epost-address",
                CustomerId = customerEntity.Id
            };


            // Act
            var result = contactInformationRepository.GetOne(x => x.Id == contactInformationEntity.Id);


            // Assert
            Assert.Null(result);


        }
    }


    [Fact]
    public void Update_ShouldUpdateCustomer_ReturnUpdatedCustomer()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
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
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });


            var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
            {
                Email = "Epost-address",
                CustomerId = customerEntity.Id
            });


            // Act
            var existingContactInformation = contactInformationRepository.GetOne(x => x.Id == contactInformationEntity.Id);
            existingContactInformation.Email = "Ny Epost";
            var result = contactInformationRepository.Update(existingContactInformation);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingContactInformation.Email, result.Email);
        }
    }

    [Fact]
    public void Delete_ShouldDeleteOneContactInformation_ReturnTrue()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
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
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });


            var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
            {
                Email = "Epost-address",
                CustomerId = customerEntity.Id
            });

            // Act
            var result = contactInformationRepository.Delete(x => x.Id == contactInformationEntity.Id);


            // Assert
            Assert.True(result);
        }
    }

    [Fact]
    public void Delete_ShouldNotDeleteOneCustomer_ReturnFalse()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
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
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });


            var contactInformationEntity = new ContactInformationEntity
            {
                Email = "Epost-address",
                CustomerId = customerEntity.Id
            };

            // Act
            var result = contactInformationRepository.Delete(x => x.Id == contactInformationEntity.Id);


            // Assert
            Assert.False(result);
        }
    }

    [Fact]
    public void Exists_ShouldReturnOneContactInformation_ReturnTrue()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
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
            var customerEntity = customerRepository.Create(new CustomersEntity
            {
                AddressesId = addressEntity.Id,
                CustomerTypeId = customerTypeEntity.Id
            });


            var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
            {
                Email = "Epost-address",
                CustomerId = customerEntity.Id
            });
            // Act
            var result = contactInformationRepository.Exists(x => x.Id == 1);


            // Assert 
            Assert.True(result);
        }
    }

    [Fact]
    public void Exists_ShouldNotReturnOneCustomer_ReturnFalse()
    {
        // Arrange
        var contactInformationRepository = new ContactInformationRepository(_context);


        // Act
        var result = contactInformationRepository.Exists(x => x.Id == 1);


        // Assert 
        Assert.False(result);
    }
}
