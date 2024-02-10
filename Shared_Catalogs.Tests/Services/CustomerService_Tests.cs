using Microsoft.EntityFrameworkCore;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Dtos;
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

}
