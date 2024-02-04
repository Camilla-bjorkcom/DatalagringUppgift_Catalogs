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

public class CustomerService(AddressesRepository addressesRepository, CustomerTypeRepository customerTypeRepository, CustomerProfileRepository customerProfileRepository, ContactInformationRepository contactInformationRepository, CustomersRepository customersRepository)
{
    private readonly AddressesRepository _addressesRepository = addressesRepository;
    private readonly CustomerTypeRepository _customerTypeRepository = customerTypeRepository;
    private readonly CustomerProfileRepository _customerProfileRepository = customerProfileRepository;
    private readonly ContactInformationRepository _contactInformationRepository = contactInformationRepository;
    private readonly CustomersRepository _customersRepository = customersRepository;


    public IUpdateCustomerDto CurrentCustomer { get; set; } = null!;

    public bool CreateCustomer(ICustomerRegistrationDto customerRegistrationDto)
    {
        try
        {
            var customerEmail =  _contactInformationRepository.GetOne(x => x.Email == customerRegistrationDto.Email);
            if (customerEmail == null)
            {

                var addressEntity =  _addressesRepository.Create(new AddressesEntity
                {
                    StreetName = customerRegistrationDto.StreetName,
                    PostalCode = customerRegistrationDto.PostalCode,
                    City = customerRegistrationDto.City,
                });

                var customerTypeEntity =  _customerTypeRepository.GetOne(x => x.CustomerType == customerRegistrationDto.CustomerType);
                if (customerTypeEntity == null)
                {
                    customerTypeEntity = _customerTypeRepository.Create(new CustomerTypeEntity
                    {
                        CustomerType = customerRegistrationDto.CustomerType
                    });
                }
                var customerProfileEntity = _customerProfileRepository.Create(new CustomerProfilesEntity
                {
                    FirstName = customerRegistrationDto.FirstName,
                    LastName = customerRegistrationDto.LastName,

                });

                var contactInformationEntity =  _contactInformationRepository.Create(new ContactInformationEntity
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

                    customerEntity =  _customersRepository.Create(customerEntity);

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

    public async Task<ICustomerDto> GetCustomer(ICustomersEntity customer)
    {
        try
        {
            var customerEntity = _customersRepository.GetOne(x => x.Id == customer.Id);

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

    public IEnumerable<ICustomerDto> GetAllCustomers()
    {
        var customers = new List<ICustomerDto>();
        try
        {
            var result =  _customersRepository.GetAll();

            if (result != null)
            {
                foreach (var customer in result)
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

            return customers;


        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return null!;
    }


    //UPDATE
    public bool UpdateCustomer(IUpdateCustomerDto updateCustomer)
    {
        var exisitingCustomer = _customersRepository.GetOne(x => x.Id == updateCustomer.Id);

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

                _customersRepository.Update(exisitingCustomer);
                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return false!;
    }



    //DELETE
    public bool DeleteCustomer(ICustomersEntity customer)
    {
        try
        {
            if ( _customersRepository.Exists(x => x.Id == customer.Id))
            {
                _customersRepository.Delete(x => x.Id == customer.Id);
                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return false;
    }

}

