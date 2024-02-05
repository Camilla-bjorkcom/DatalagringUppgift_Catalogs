using Shared_Catalogs.Dtos;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Models;
using Shared_Catalogs.Services;
using System;

namespace Catalog_ConsoleApp;

public class ConsoleUI(CustomerService customerService, ProductService productService)
{
    private readonly CustomerService _customerService = customerService;
    private readonly ProductService _productService = productService;

    public void CreateCustomer_UI()
    {
        CustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto();

        Console.Clear();
        Console.WriteLine("-------- SKAPA EN KUND --------");

        Console.Write("Förnamn: ");
        customerRegistrationDto.FirstName = Console.ReadLine()!;

        Console.Write("Efternamn: ");
       customerRegistrationDto.LastName = Console.ReadLine()!;

        Console.Write("E-post: ");
        customerRegistrationDto.Email = Console.ReadLine()!;

        Console.Write("Gatuadress: ");
        customerRegistrationDto.StreetName = Console.ReadLine()!;

        Console.Write("Postnummer: ");
        customerRegistrationDto.PostalCode = Console.ReadLine()!;

        Console.Write("Stad: ");
        customerRegistrationDto.City = Console.ReadLine()!;

        Console.Write("Kund typ: ");
        customerRegistrationDto.CustomerType = Console.ReadLine()!;


        var result = _customerService.CreateCustomer(customerRegistrationDto);
        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("Ny kund skapades.");
            Console.ReadKey();
        }


    }


    public void GetCustomers_UI()
    {
        Console.Clear();
        Console.WriteLine("--------HÄMTAR ALLA KUNDER--------");
        var customers = _customerService.GetAllCustomers();
        if (customers != null)
        {
            foreach (var customer in customers ?? null!)
            {
                Console.WriteLine();
                if (customer.CustomerProfiles != null && customer.ContactInformation != null) 
                {
                    Console.WriteLine($"Kund {customer.Id}, {customer.CustomerProfiles.FirstName} {customer.CustomerProfiles.LastName}, {customer.ContactInformation.Email} (Kundtyp: {customer.CustomerType.CustomerType})");
                    Console.WriteLine($"{customer.Addresses.StreetName}, {customer.Addresses.PostalCode} {customer.Addresses.City}");
                }
                else Console.WriteLine($"------ Kund {customer.Id} har ingen profil ------");
                
                Console.WriteLine();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Det finns inga kunder i databasen. ");
        }
        
    }

    public void UpdateCustomer_UI()
    {
        Console.Clear();
        Console.WriteLine("--------UPPDATERA KUND--------");
        Console.Write("Skriv in kund email: ");
        var email = Console.ReadLine()!;

        var customer = _customerService.GetCustomerByEmail(email);
        if (customer != null)
        {
            Console.WriteLine();
            Console.WriteLine($"Kund {customer.Id}, {customer.CustomerProfiles.FirstName} {customer.CustomerProfiles.LastName}, {customer.ContactInformation.Email} (Kundtyp: {customer.CustomerType.CustomerType})");
            Console.WriteLine($"{customer.Addresses.StreetName}, {customer.Addresses.PostalCode} {customer.Addresses.City}");
            Console.WriteLine();

            Console.Write("Uppdatera ditt efternamn: ");
            customer.CustomerProfiles.LastName = Console.ReadLine()!;

            _customerService.UpdateCustomer(customer);
            Console.WriteLine();
            Console.WriteLine($"Kund {customer.Id}, {customer.CustomerProfiles.FirstName} {customer.CustomerProfiles.LastName}, {customer.ContactInformation.Email} (Kundtyp: {customer.CustomerType.CustomerType})");
            Console.WriteLine($"{customer.Addresses.StreetName}, {customer.Addresses.PostalCode} {customer.Addresses.City}");
            Console.WriteLine();
        }

        else
        {
            Console.WriteLine("Ingen kund hittades.");
        }
        Console.ReadKey();
    }


    public void DeleteCustomer_UI()
    {
        Console.Clear();
        Console.WriteLine("--------RADERA KUND--------");
        Console.Write("Skriv in kund-id: ");
        var id = int.Parse(Console.ReadLine()!);

        var customer = _customerService.GetCustomerById(id);
        if (customer != null)
        {
            _customerService.DeleteCustomer(customer);
            Console.WriteLine("Kunden blev borttagen");
        }
        else
        {
            Console.WriteLine("Ingen kund hittades.");
        }
        Console.ReadKey();
    }


    //PRODUCTS

    public void CreateProduct_UI()
    {
        CreateProductDto createProductDto = new CreateProductDto();

        Console.Clear();
        Console.WriteLine("--- Skapa En Produkt ----");

        Console.Write("Titel: ");
        createProductDto.Title = Console.ReadLine()!;

        Console.Write("Produkt beskrivning: ");
        createProductDto.Description = Console.ReadLine()!;

        Console.Write("Kategori: ");
        createProductDto.Category = Console.ReadLine()!;

        Console.Write("Tillverkare: ");
        createProductDto.Manufacturer = Console.ReadLine()!;

        Console.Write("Kvantitet i lager: ");
        createProductDto.Quantity = int.Parse(Console.ReadLine()!);

        var result = _productService.CreateProduct(createProductDto);
        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("Produkten skapades.");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Något gick fel.");
        }
    }
}
