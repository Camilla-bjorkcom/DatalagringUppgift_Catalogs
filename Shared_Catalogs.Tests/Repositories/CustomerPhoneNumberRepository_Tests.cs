using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Catalogs.Tests.Repositories
{
    public class CustomerPhoneNumberRepository_Tests
    {

        private readonly CustomerDbContext _context =
          new(new DbContextOptionsBuilder<CustomerDbContext>()
          .UseInMemoryDatabase($"{Guid.NewGuid()}")
          .Options);

        [Fact]
        public void CreateShould_AddOneTo_CustomerPhoneNumbersEntity_AndReturnEntity()
        {
            // Arrange
            var customerRepository = new CustomersRepository(_context);
            var contactInformationRepository = new ContactInformationRepository(_context);
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
                var customerEntity = customerRepository.Create(new CustomersEntity
                {
                    AddressesId = addressEntity.Id,
                    CustomerTypeId = customerTypeEntity.Id
                });

                var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
                {
                    Id = 1,
                    Email = "Epost-address",
                    CustomerId = customerEntity.Id
                });

                var phoneNumberEntity = new CustomerPhoneNumbersEntity
                {
                    PhoneNumber = "070-000000",
                    ContactInformationId = contactInformationEntity.Id
                };

                // Act
                var result = customerPhoneNumberRepository.Create(phoneNumberEntity);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<CustomerPhoneNumbersEntity>(result);
            }
        }


        [Fact]
        public void CreateShouldNot_AddOneTo_CustomerPhoneNumbersEntity_IfAlreadyExists_AndReturnNull()
        {
            // Arrange
            var customerRepository = new CustomersRepository(_context);
            var contactInformationRepository = new ContactInformationRepository(_context);
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
                var customerEntity = customerRepository.Create(new CustomersEntity
                {
                    AddressesId = addressEntity.Id,
                    CustomerTypeId = customerTypeEntity.Id
                });

                var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
                {
                    Id = 1,
                    Email = "Epost-address",
                    CustomerId = customerEntity.Id
                });

                var phoneNumberEntity = customerPhoneNumberRepository.Create(new CustomerPhoneNumbersEntity 
                {
                    PhoneNumber = "070-000000",
                    ContactInformationId = contactInformationEntity.Id
                });
     

                // Act
                var result = customerPhoneNumberRepository.Create(phoneNumberEntity);


                // Assert
                Assert.Null(result);
              
            }

        }

        [Fact]
        public void GetAllShouldGetAllRecords_ReturnIEnumerableofTypeCustomerPhoneNumbers()
        {
            // Arrange
            var customerRepository = new CustomersRepository(_context);
            var contactInformationRepository = new ContactInformationRepository(_context);
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
                var customerEntity = customerRepository.Create(new CustomersEntity
                {
                    AddressesId = addressEntity.Id,
                    CustomerTypeId = customerTypeEntity.Id
                });

                var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
                {
                    Id = 1,
                    Email = "Epost-address",
                    CustomerId = customerEntity.Id
                });

                var phoneNumberEntity = customerPhoneNumberRepository.Create(new CustomerPhoneNumbersEntity
                {
                    PhoneNumber = "070-000000",
                    ContactInformationId = contactInformationEntity.Id
                });


                // Act
                var result = customerPhoneNumberRepository.GetAll();


                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<CustomerPhoneNumbersEntity>>(result);
            }
        }

        [Fact]
        public void GetAllShouldNotGetAllRecords_ReturnNull()
        {
            // Arrange
            var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);


            // Act
            var result = customerPhoneNumberRepository.GetAll();


            // Assert
            Assert.Null(result);

        }


        [Fact]
        public void GetOne_ShouldGetOneCustomerPhoneNumber_ReturnOneCustomerPhoneNumber()
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


                var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
                {
                    Id = 1,
                    Email = "Epost-address",
                    CustomerId = customerEntity.Id
                });

                var phoneNumberEntity = customerPhoneNumberRepository.Create(new CustomerPhoneNumbersEntity
                {
                    PhoneNumber = "070-000000",
                    ContactInformationId = contactInformationEntity.Id
                });

                // Act
                var result = customerPhoneNumberRepository.GetOne(x => x.PhoneNumber == phoneNumberEntity.PhoneNumber);


                // Assert
                Assert.NotNull(result);
                Assert.Equal(phoneNumberEntity.PhoneNumber, result.PhoneNumber);
            }

        }


        [Fact]
        public void GetOne_ShouldNotGetOneCustomerPhoneNumber_ReturnNull()
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


                var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
                {
                    Id = 1,
                    Email = "Epost-address",
                    CustomerId = customerEntity.Id
                });

                var phoneNumberEntity = new CustomerPhoneNumbersEntity
                {
                    PhoneNumber = "070-000000",
                    ContactInformationId = contactInformationEntity.Id
                };

                // Act
                var result = customerPhoneNumberRepository.GetOne(x => x.PhoneNumber == phoneNumberEntity.PhoneNumber);


                // Assert
                Assert.Null(result);
            }
        }


        [Fact]
        public void Update_ShouldNotUpdateCustomerPhoneNumber_Because_PK_ReturnNull()
        {

            // Arrange
            var customerRepository = new CustomersRepository(_context);
            var contactInformationRepository = new ContactInformationRepository(_context);
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

                var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
                {
                    Id = 1,
                    Email = "Epost-address",
                    CustomerId = customerEntity.Id
                });

                var phoneNumberEntity = customerPhoneNumberRepository.Create(new CustomerPhoneNumbersEntity
                {
                    PhoneNumber = "070-000000",
                    ContactInformationId = contactInformationEntity.Id
                });


                // Act
                var existingCustomerPhone = customerPhoneNumberRepository.GetOne(x => x.PhoneNumber == phoneNumberEntity.PhoneNumber);
                existingCustomerPhone.PhoneNumber = "070-1111111";
                var result = customerPhoneNumberRepository.Update(existingCustomerPhone);


                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void Delete_ShouldDeleteOneCustomerPhoneNumber_ReturnTrue()
        {
            // Arrange
            var customerRepository = new CustomersRepository(_context);
            var contactInformationRepository = new ContactInformationRepository(_context);
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

                var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
                {
                    Id = 1,
                    Email = "Epost-address",
                    CustomerId = customerEntity.Id
                });

                var phoneNumberEntity = customerPhoneNumberRepository.Create(new CustomerPhoneNumbersEntity
                {
                    PhoneNumber = "070-000000",
                    ContactInformationId = contactInformationEntity.Id
                });


                // Act
                var result = customerPhoneNumberRepository.Delete(x => x.PhoneNumber == phoneNumberEntity.PhoneNumber);


                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void Delete_ShouldNotDeleteOneCustomerPhoneNumber_ReturnFalse()
        {
            // Arrange
            var customerRepository = new CustomersRepository(_context);
            var contactInformationRepository = new ContactInformationRepository(_context);
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

                var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
                {
                    Id = 1,
                    Email = "Epost-address",
                    CustomerId = customerEntity.Id
                });

                var phoneNumberEntity = new CustomerPhoneNumbersEntity
                {
                    PhoneNumber = "070-000000",
                    ContactInformationId = contactInformationEntity.Id
                };


                // Act
                var result = customerPhoneNumberRepository.Delete(x => x.PhoneNumber == phoneNumberEntity.PhoneNumber);


                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void Exists_ShouldReturnOneCustomerPhoneNumber_ReturnTrue()
        {
            // Arrange
            var customerRepository = new CustomersRepository(_context);
            var contactInformationRepository = new ContactInformationRepository(_context);
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

                var contactInformationEntity = contactInformationRepository.Create(new ContactInformationEntity
                {
                    Id = 1,
                    Email = "Epost-address",
                    CustomerId = customerEntity.Id
                });

                var phoneNumberEntity = customerPhoneNumberRepository.Create(new CustomerPhoneNumbersEntity
                {
                    PhoneNumber = "070-000000",
                    ContactInformationId = contactInformationEntity.Id
                });


                // Act
                var result = customerPhoneNumberRepository.Exists(x => x.PhoneNumber == phoneNumberEntity.PhoneNumber);


                // Assert 
                Assert.True(result);
            }
        }

        [Fact]
        public void Exists_ShouldNotReturnOneCustomer_ReturnFalse()
        {
            // Arrange
            var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
            var phoneNumberEntity = new CustomerPhoneNumbersEntity
            {
                PhoneNumber = "070-000000",
                ContactInformationId = 1
            };

            // Act
            var result = customerPhoneNumberRepository.Exists(x => x.PhoneNumber == phoneNumberEntity.PhoneNumber);


            // Assert 
            Assert.False(result);
        }
    }
}
