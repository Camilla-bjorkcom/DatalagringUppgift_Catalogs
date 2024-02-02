namespace Shared_Catalogs.Interfaces
{
    public interface ICustomerModel
    {
        string City { get; set; }
        string CustomerType { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PostalCode { get; set; }
        string StreetName { get; set; }
    }
}