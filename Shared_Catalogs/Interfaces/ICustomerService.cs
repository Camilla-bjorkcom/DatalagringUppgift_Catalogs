using Shared_Catalogs.Dtos;

namespace Shared_Catalogs.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CustomerRegistrationDto customerRegistrationDto);
        Task<bool> DeleteCustomerAsync(ICustomersEntity customer);
        Task<IEnumerable<ICustomerDto>> GetAllCustomersAsync();
        Task<ICustomerDto> GetCustomerAsync(ICustomersEntity customer);
        Task<bool> UpdateCustomerAsync(IUpdateCustomerDto updateCustomer);
    }
}