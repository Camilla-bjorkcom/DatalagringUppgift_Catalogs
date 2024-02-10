using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Models;
using Shared_Catalogs.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Services;

public class CustomerService(AddressesRepository addressesRepository, CustomerTypeRepository customerTypeRepository, CustomerProfileRepository customerProfileRepository, ContactInformationRepository contactInformationRepository, CustomersRepository customersRepository, CustomerPhoneNumbersRepository customerPhoneNumbersRepository)
{
    private readonly AddressesRepository _addressesRepository = addressesRepository;
    private readonly CustomerTypeRepository _customerTypeRepository = customerTypeRepository;
    private readonly CustomerProfileRepository _customerProfileRepository = customerProfileRepository;
    private readonly ContactInformationRepository _contactInformationRepository = contactInformationRepository;
    private readonly CustomersRepository _customersRepository = customersRepository;
    private readonly CustomerPhoneNumbersRepository _customerPhoneNumbersRepository = customerPhoneNumbersRepository;


    public UpdateCustomerDto CurrentCustomer { get; set; } = null!;

    public CustomersEntity CreateCustomer(CustomerRegistrationDto customerRegistrationDto)
    {
        try
        {
            var customerEmail = _contactInformationRepository.GetOne(x => x.Email == customerRegistrationDto.Email);
            if (customerEmail == null)
            {

                var addressEntity = _addressesRepository.Create(new AddressesEntity
                {
                    StreetName = customerRegistrationDto.StreetName,
                    PostalCode = customerRegistrationDto.PostalCode,
                    City = customerRegistrationDto.City,
                });

                var customerTypeEntity = _customerTypeRepository.GetOne(x => x.CustomerType == customerRegistrationDto.CustomerType);
                if (customerTypeEntity == null)
                {
                    customerTypeEntity = _customerTypeRepository.Create(new CustomerTypeEntity
                    {
                        CustomerType = customerRegistrationDto.CustomerType
                    });
                }
                if (customerTypeEntity != null)
                {
                    var customerEntity = new CustomersEntity
                    {
                        AddressesId = addressEntity.Id,
                        CustomerTypeId = customerTypeEntity.Id
                    };
                    customerEntity = _customersRepository.Create(customerEntity);

                    var customerProfileEntity = _customerProfileRepository.Create(new CustomerProfilesEntity
                    {
                        FirstName = customerRegistrationDto.FirstName,
                        LastName = customerRegistrationDto.LastName,
                        CustomerId = customerEntity.Id,

                    });

                    var contactInformationEntity = _contactInformationRepository.Create(new ContactInformationEntity
                    {
                        Email = customerRegistrationDto.Email,
                        CustomerId = customerEntity.Id,
                    });


                    return customerEntity;
                }
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!; ;
    }


    //GET

    public ContactInformationEntity GetCustomerContactInformationById(int id)
    {
        try
        {
            var customerEntity = _contactInformationRepository.GetOne(x => x.CustomerId == id);
            return customerEntity;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return null!;

    }


    public CustomersEntity GetCustomerByEmail(string email)
    {
        try
        {
            var customerEntity = _customersRepository.GetOne(x => x.ContactInformation.Email == email);
            return customerEntity;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return null!;
    }


    public IEnumerable<CustomersEntity> GetAllCustomers()
    {
        try
        {
            var result = _customersRepository.GetAll();

            if (result != null)
            {
                return result;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return null!;
    }


    //UPDATE
    public CustomersEntity UpdateCustomer(CustomersEntity customerEntity)
    {
        var exisitingCustomer = _customersRepository.GetOne(x => x.Id == customerEntity.Id);

        try
        {
            if (exisitingCustomer != null)
            {
                var updatedCustomerEntity = _customersRepository.Update(exisitingCustomer);
                return updatedCustomerEntity;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return null!;
    }


    //UPDATE
    public ContactInformationEntity UpdateCustomerProfileAndContactInformation(UpdateCustomerDto updateCustomerDto)
    {
        try
        {
            var exisitingCustomer = _contactInformationRepository.GetOne(x => x.Id == updateCustomerDto.Id);
            if (exisitingCustomer != null)
            {
                if (updateCustomerDto.PhoneNumber != null)
                {
                    var customerPhoneNumberEntity = _customerPhoneNumbersRepository.Create(new CustomerPhoneNumbersEntity
                    {
                        PhoneNumber = updateCustomerDto.PhoneNumber,
                        ContactId = exisitingCustomer.Id
                    });
                }

                var updatedCustomerEntity = _contactInformationRepository.Update(exisitingCustomer);
                return updatedCustomerEntity;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return null!;
    }


    //DELETE
    public bool DeleteCustomer(CustomersEntity customer)
    {
        try
        {
            if (_customersRepository.Exists(x => x.Id == customer.Id))
            {
                _customersRepository.Delete(x => x.Id == customer.Id);
                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return false;
    }

}

