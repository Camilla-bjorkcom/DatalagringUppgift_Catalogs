using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Models;
using Shared_Catalogs.Repositories;
using System.Diagnostics;

namespace Shared_Catalogs.Services;

public class CustomerService(AddressesRepository addressesRepository, CustomerTypeRepository customerTypeRepository, CustomerProfileRepository customerProfileRepository, ContactInformationRepository contactInformationRepository, CustomersRepository customersRepository) : ICustomerService
{
    private readonly AddressesRepository _addressesRepository = addressesRepository;
    private readonly CustomerTypeRepository _customerTypeRepository = customerTypeRepository;
    private readonly CustomerProfileRepository _customerProfileRepository = customerProfileRepository;
    private readonly ContactInformationRepository _contactInformationRepository = contactInformationRepository;
    private readonly CustomersRepository _customersRepository = customersRepository;


    public async Task<bool> CreateCustomerAsync(ICustomerRegistrationDto customerRegistrationDto)
    {
        try
        {
            if (!await _customersRepository.ExistsAsync(x => x.ContactInformation.Email == customerRegistrationDto.Email))
            {

                var addressEntity = await _addressesRepository.CreateAsync(new AddressesEntity
                {
                    StreetName = customerRegistrationDto.StreetName,
                    PostalCode = customerRegistrationDto.PostalCode,
                    City = customerRegistrationDto.City,
                });

                var customerTypeEntity = await _customerTypeRepository.GetOneAsync(x => x.CustomerType == customerRegistrationDto.CustomerType);
                if (customerTypeEntity == null)
                {
                    customerTypeEntity = await _customerTypeRepository.CreateAsync(new CustomerTypeEntity
                    {
                        CustomerType = customerRegistrationDto.CustomerType
                    });
                }
                var customerProfileEntity = await _customerProfileRepository.CreateAsync(new CustomerProfilesEntity
                {
                    FirstName = customerRegistrationDto.FirstName,
                    LastName = customerRegistrationDto.LastName,

                });

                var contactInformationEntity = await _contactInformationRepository.CreateAsync(new ContactInformationEntity
                {
                    Email = customerRegistrationDto.Email
                });

                if (contactInformationEntity != null)
                {
                    var customerEntity = new CustomersEntity
                    {
                        Id = Guid.NewGuid(),
                        AddressesId = addressEntity.Id,
                        CustomerTypeId = customerTypeEntity.Id
                    };

                    customerEntity = await _customersRepository.CreateAsync(customerEntity);

                    if (customerEntity != null)
                    {
                        var customerDto = new CustomerDto
                        {
                            CustomerId = customerEntity.Id,
                            FirstName = customerProfileEntity.FirstName,
                            LastName = customerProfileEntity.LastName,
                            Email = contactInformationEntity.Email,
                            CustomerType = customerTypeEntity.CustomerType,

                        };

                    }
                    return true;
                }
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }


    //GET

    public async Task<ICustomerDto> GetCustomerAsync(ICustomersEntity customer)
    {
        try
        {
            var customerEntity = await _customersRepository.GetOneAsync(x => x.Id == customer.Id);

            if (customerEntity != null)
            {
                var customerDto = new CustomerDto
                {
                    CustomerId = customerEntity.Id,
                    FirstName = customerEntity.CustomerProfiles.FirstName,
                    LastName = customerEntity.CustomerProfiles.LastName,
                    Email = customerEntity.ContactInformation.Email,
                    CustomerType = customerEntity.CustomerType.CustomerType

                };
                return customerDto;

            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<ICustomerDto>> GetAllCustomersAsync()
    {
        var customers = new List<ICustomerDto>();
        try
        {
            var result = _customersRepository.GetAllAsync();

            foreach (var customer in await result)
            {
                customers.Add(new CustomerDto
                {
                    CustomerId = customer.Id,
                    FirstName = customer.CustomerProfiles.FirstName,
                    LastName = customer.CustomerProfiles.LastName,
                    Email = customer.ContactInformation.Email,
                    CustomerType = customer.CustomerType.CustomerType,
                });
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return customers;
    }


    //UPDATE
    public async Task<bool> UpdateCustomerAsync(IUpdateCustomerDto updateCustomer)
    {
        var exisitingCustomer = await _customersRepository.GetOneAsync(x => x.Id == updateCustomer.Id);

        try
        {
            if (exisitingCustomer != null)
            {
                exisitingCustomer.CustomerProfiles.FirstName = updateCustomer.FirstName;
                exisitingCustomer.CustomerProfiles.LastName = updateCustomer.LastName;
                exisitingCustomer.ContactInformation.Email = updateCustomer.Email;
                exisitingCustomer.CustomerType.CustomerType = updateCustomer.CustomerType;
                exisitingCustomer.Addresses.StreetName = updateCustomer.StreetName;
                exisitingCustomer.Addresses.PostalCode = updateCustomer.PostalCode;
                exisitingCustomer.Addresses.City = updateCustomer.City;
                if (updateCustomer.PhoneNumber != null)
                {
                    var newPhoneNumber = new CustomerPhoneNumbersEntity
                    {
                        PhoneNumber = updateCustomer.PhoneNumber,
                    };
                    if (newPhoneNumber != null)
                    {
                        exisitingCustomer.ContactInformation.PhoneNumbers.Add(newPhoneNumber);
                    }
                }
                if (updateCustomer.PhoneNumber2 != null)
                {
                    var newPhoneNumber2 = new CustomerPhoneNumbersEntity
                    {
                        PhoneNumber = updateCustomer.PhoneNumber2,
                    };
                    if (newPhoneNumber2 != null)
                    {
                        exisitingCustomer.ContactInformation.PhoneNumbers.Add(newPhoneNumber2);
                    }
                }
                if (updateCustomer.LinkedIn != null)
                {
                    exisitingCustomer.ContactInformation.LinkedIn = updateCustomer.LinkedIn;
                }
                if (updateCustomer.ProfileImg != null)
                {
                    exisitingCustomer.CustomerProfiles.ProfileImg = updateCustomer.ProfileImg;
                }

                await _customersRepository.UpdateAsync(exisitingCustomer);
                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return false!;
    }



    //DELETE
    public async Task<bool> DeleteCustomerAsync(ICustomersEntity customer)
    {
        try
        {
            if (await _customersRepository.ExistsAsync(x => x.Id == customer.Id))
            {
                await _customersRepository.DeleteAsync(x => x.Id == customer.Id);
                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return false;
    }

}

