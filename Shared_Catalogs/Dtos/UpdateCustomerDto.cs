

namespace Shared_Catalogs.Dtos;

public class UpdateCustomerDto 
{
    public int Id { get; set; } 
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

}
