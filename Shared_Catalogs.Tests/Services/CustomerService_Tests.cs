using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Repositories;
using Shared_Catalogs.Services;

namespace Shared_Catalogs.Tests.Services;

public class CustomerService_Tests
{

    private readonly CustomerDbContext _context =
       new(new DbContextOptionsBuilder<CustomerDbContext>()
       .UseInMemoryDatabase($"{Guid.NewGuid()}")
       .Options);

    [Fact]
    public void CreateCustomerShould_CreateNewCustomer_ReturnNewCustomer()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var addressRepository = new AddressesRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
        var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var customerService = new CustomerService(addressRepository, customerTypeRepository, customerProfileRepository, contactInformationRepository, customerRepository, customerPhoneNumberRepository);

        CustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto
        {
            FirstName = "Förnamn", 
            LastName = "Efternamn", 
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
            Email = "Epost-address",
            CustomerType = "Kundtyp"
        };


        // Act
        var result = customerService.CreateCustomer(customerRegistrationDto);


        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void CreateCustomerShould_NotCreateNewCustomer_IfAlreadyEmailExists_ReturnNull()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var addressRepository = new AddressesRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
        var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var customerService = new CustomerService(addressRepository, customerTypeRepository, customerProfileRepository, contactInformationRepository, customerRepository, customerPhoneNumberRepository);

        CustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
            Email = "Epost-address",
            CustomerType = "Kundtyp"
        };
        var oldCustomer = customerService.CreateCustomer(customerRegistrationDto);

        // Act
        CustomerRegistrationDto newCustomerRegistrationDto = new CustomerRegistrationDto
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
            Email = "Epost-address",
            CustomerType = "Kundtyp"
        };
        var result = customerService.CreateCustomer(newCustomerRegistrationDto);


        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetCustomerContactInformationById_ShouldReturnContactInformationEntity()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var addressRepository = new AddressesRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
        var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var customerService = new CustomerService(addressRepository, customerTypeRepository, customerProfileRepository, contactInformationRepository, customerRepository, customerPhoneNumberRepository);

        CustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
            Email = "Epost-address",
            CustomerType = "Kundtyp"
        };
        customerService.CreateCustomer(customerRegistrationDto);

        // Act

        var result = customerService.GetCustomerContactInformationById(1);


        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContactInformationEntity>(result);
    }



    [Fact]
    public void GetCustomerByEmail_ShouldReturnCustomerEntity()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var addressRepository = new AddressesRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
        var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var customerService = new CustomerService(addressRepository, customerTypeRepository, customerProfileRepository, contactInformationRepository, customerRepository, customerPhoneNumberRepository);

        CustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
            Email = "Epost-address",
            CustomerType = "Kundtyp"
        };
        customerService.CreateCustomer(customerRegistrationDto);

        // Act

        var result = customerService.GetCustomerByEmail("Epost-address");


        // Assert
        Assert.NotNull(result);
        Assert.IsType<CustomersEntity>(result);
    }

    [Fact]
    public void GeAllCustomers_ShouldReturnIEnumerableOfTypeCustomer()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var addressRepository = new AddressesRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
        var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var customerService = new CustomerService(addressRepository, customerTypeRepository, customerProfileRepository, contactInformationRepository, customerRepository, customerPhoneNumberRepository);

        CustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
            Email = "Epost-address",
            CustomerType = "Kundtyp"
        };
        customerService.CreateCustomer(customerRegistrationDto);

        // Act

        var result = customerService.GetAllCustomers();


        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomersEntity>>(result);
    }

    [Fact]
    public void UpdatCustomer_ShouldReturnUpdatedCustomer_IfExists()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var addressRepository = new AddressesRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
        var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var customerService = new CustomerService(addressRepository, customerTypeRepository, customerProfileRepository, contactInformationRepository, customerRepository, customerPhoneNumberRepository);
       
       CustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
            Email = "Epost-address",
            CustomerType = "Kundtyp"
        };
        customerService.CreateCustomer(customerRegistrationDto);

        // Act
        var existingCustomer = customerRepository.GetOne(x => x.CustomerProfiles.LastName == customerRegistrationDto.LastName);
        if (existingCustomer != null)
        {
            existingCustomer.CustomerProfiles.LastName = "Nytt Efternamn";
        }
        var result = customerService.UpdateCustomer(existingCustomer!);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingCustomer!.CustomerProfiles.LastName, result.CustomerProfiles.LastName);
    }

    [Fact]
    public void UpdateCustomerProfileAndContactInformation_ShouldReturnUpdatedContactInformationEntity()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var addressRepository = new AddressesRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
        var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var customerService = new CustomerService(addressRepository, customerTypeRepository, customerProfileRepository, contactInformationRepository, customerRepository, customerPhoneNumberRepository);

        CustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
            Email = "Epost-address",
            CustomerType = "Kundtyp"
        };
        customerService.CreateCustomer(customerRegistrationDto);

        // Act
        UpdateCustomerDto updateCustomerDto = new UpdateCustomerDto
        {
            Id = 1,
            PhoneNumber = "0704589747"
        };

        var result = customerService.UpdateCustomerProfileAndContactInformation(updateCustomerDto);

        // Assert
        Assert.NotNull(result);
    }


    [Fact]
    public void DeleteCustomer_ShouldDeleteCustomer_ReturnTrue()
    {
        // Arrange
        var customerRepository = new CustomersRepository(_context);
        var addressRepository = new AddressesRepository(_context);
        var customerTypeRepository = new CustomerTypeRepository(_context);
        var contactInformationRepository = new ContactInformationRepository(_context);
        var customerPhoneNumberRepository = new CustomerPhoneNumbersRepository(_context);
        var customerProfileRepository = new CustomerProfileRepository(_context);
        var customerService = new CustomerService(addressRepository, customerTypeRepository, customerProfileRepository, contactInformationRepository, customerRepository, customerPhoneNumberRepository);

        CustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto
        {
            FirstName = "Förnamn",
            LastName = "Efternamn",
            StreetName = "Gatunamn",
            PostalCode = "77777",
            City = "Stad",
            Email = "Epost-address",
            CustomerType = "Kundtyp"
        };
        var customerEntity = customerService.CreateCustomer(customerRegistrationDto);

        // Act
  

        var result = customerService.DeleteCustomer(customerEntity);

        // Assert
        Assert.True(result);
    }


}
