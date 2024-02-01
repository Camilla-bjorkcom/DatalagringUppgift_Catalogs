using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Dtos;

public class CustomerRegistrationDto : ICustomerRegistrationDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string StreetName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CustomerType { get; set; } = null!;

}
