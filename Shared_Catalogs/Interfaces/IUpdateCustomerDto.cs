namespace Shared_Catalogs.Interfaces
{
    public interface IUpdateCustomerDto
    {
        Guid Id { get; }
        string City { get; set; }
        string CustomerType { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string? LinkedIn { get; set; }
        string? PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }
        string PostalCode { get; set; }
        string? ProfileImg { get; set; }
        string StreetName { get; set; }
    }
}