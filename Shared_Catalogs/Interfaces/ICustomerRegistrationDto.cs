namespace Shared_Catalogs.Interfaces
{
    public interface ICustomerRegistrationDto
    {
        string City { get; set; }
        string CustomerType { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        Guid Id { get; set; }
        string LastName { get; set; }
        string PostalCode { get; set; }
        string StreetName { get; set; }
    }
}