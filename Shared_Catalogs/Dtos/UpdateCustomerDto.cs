using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Dtos;

public class UpdateCustomerDto : IUpdateCustomerDto
{
    public Guid Id { get; } 
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? PhoneNumber2 { get; set; }
    public string? LinkedIn { get; set; }
    public string CustomerType { get; set; } = null!;
    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public string? ProfileImg { get; set; }


}
