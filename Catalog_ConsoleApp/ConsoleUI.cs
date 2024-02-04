using Shared_Catalogs.Dtos;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Services;

namespace Catalog_ConsoleApp;

public class ConsoleUI(CustomerService customerService)
{
    private readonly CustomerService _customerService = customerService;

    public void CreateCustomer_UI()
    {
        ICustomerRegistrationDto customerRegistrationDto = new CustomerRegistrationDto();

        Console.Clear();
        Console.WriteLine("--- SKAPA EN KUND ----");

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
}
