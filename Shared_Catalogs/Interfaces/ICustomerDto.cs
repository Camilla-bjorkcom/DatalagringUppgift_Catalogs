namespace Shared_Catalogs.Interfaces
{
    public interface ICustomerDto
    {
        Guid CustomerId { get; set; }
        string CustomerType { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}