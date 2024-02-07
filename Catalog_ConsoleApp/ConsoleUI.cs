using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Models;
using Shared_Catalogs.Services;
using System;

namespace Catalog_ConsoleApp;

public class ConsoleUI(CustomerService customerService, ProductService productService, ProductReviewsService productReviewsService)
{
    private readonly CustomerService _customerService = customerService;
    private readonly ProductService _productService = productService;
    private readonly ProductReviewsService _productReviewsService = productReviewsService;

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

    public void GetProducts_UI()
    {
        Console.Clear();
        Console.WriteLine("--------HÄMTAR ALLA PRODUKTER--------");
        var products = _productService.GetAllProducts();
        if (products != null)
        {
            foreach (var product in products ?? null!)
            {
                Console.WriteLine();
                {
                    Console.WriteLine($"Produkt: {product.Title} ({product.ArticleNumber})");
                    Console.WriteLine($"Beskrivning: {product.Description}");
                    Console.WriteLine($"Tillverkare:  {product.Manufacturer.ManufactureName}");
                    Console.WriteLine($"Kategori: {product.Category.CategoryName}");
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Det finns inga produkter i databasen. ");
        }

    }

    public void UpdateProduct_UI()
    {
        Console.Clear();
        GetProducts_UI();
        Console.WriteLine("--------UPPDATERA PRODUKT--------");
        Console.Write("Skriv in produkt titel: ");
        var title = Console.ReadLine()!;

        var product = _productService.GetProductByTitle(title);
        if (product != null)
        {
            Console.WriteLine();
            Console.WriteLine($"Produkt: {product.Title} ({product.ArticleNumber})");
            Console.WriteLine($"Beskrivning: {product.Description}");
            Console.WriteLine($"Tillverkare:  {product.Manufacturer.ManufactureName}");
            Console.WriteLine($"Kategori: {product.Category.CategoryName}");
            Console.WriteLine();



            Console.Write("Uppdatera produkt beskrivning: ");
            product.Description = Console.ReadLine()!;

            _productService.UpdateProduct(product);
            Console.WriteLine();
            Console.WriteLine("UPPDATERAD BESKRIVNING");
            Console.WriteLine($"Produkt: {product.Title} ({product.ArticleNumber})");
            Console.WriteLine($"Beskrivning: {product.Description}");
            Console.WriteLine($"Tillverkare:  {product.Manufacturer.ManufactureName}");
            Console.WriteLine($"Kategori: {product.Category.CategoryName}");
            Console.WriteLine();
        }

        else
        {
            Console.WriteLine("Ingen produkt hittades.");
        }
        Console.ReadKey();
    }

    public void DeleteProduct_UI()
    {
        Console.Clear();
        GetProducts_UI();
        Console.WriteLine("--------RADERA PRODUKT--------");
        Console.Write("Skriv in produkt titel: ");
        var title = Console.ReadLine()!;

        var product = _productService.GetProductByTitle(title);
        if (product != null)
        {
           _productService.DeleteProduct(product);
            Console.WriteLine("Produkten har blivit borttagen");
        }
        else
        {
            Console.WriteLine("Ingen produkt hittades.");
        }
        Console.ReadKey();
    }

    public void CreateReviews_UI(string productTitle)
    {
        var productEntity = _productService.GetProductByTitle(productTitle);

        ProductReviewsDto reviewsDto = new ProductReviewsDto();

        Console.Clear();
        Console.WriteLine($"--- Skapa Ett Omdöme för {productEntity.Title} ----");

        Console.Write("Omdöme: ");
        reviewsDto.reviews = Console.ReadLine()!;
        reviewsDto.ArticleNumber = productEntity.ArticleNumber;

        var result = _productReviewsService.CreateProductReview(reviewsDto);
        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("Omdömet skapades.");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Något gick fel.");
        }
    }
}
