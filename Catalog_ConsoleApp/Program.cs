
using Catalog_ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared_Catalogs.Contexts;

using Shared_Catalogs.Repositories;
using Shared_Catalogs.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IT_kurser\Kurser\Webbutveckling-dotnet\Datalagring\Catalogs\Shared_Catalogs\Data\CustomersCatalog.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddDbContext<ProductsDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IT_kurser\Kurser\Webbutveckling-dotnet\Datalagring\Catalogs\Shared_Catalogs\Data\ProductsCatalog.mdf;Integrated Security=True"));


    services.AddTransient<ProductService>();
    services.AddTransient<CustomerService>();
    services.AddTransient<ProductReviewsService>();
    services.AddTransient<CategoryService>();

    services.AddTransient<CategoryRepository>();
    services.AddTransient<ProductRepository>();
    services.AddTransient<ManufacturerRepository>();
    services.AddTransient<ProductReviewsRepository>();




    services.AddTransient<AddressesRepository>();
    services.AddTransient<ContactInformationRepository>();
    services.AddTransient<CustomerPhoneNumbersRepository>();
    services.AddTransient<CustomerProfileRepository>();
    services.AddTransient<CustomersRepository>();
    services.AddTransient<CustomerTypeRepository>();


    //services.AddSingleton<ConsoleUI>();


}).Build();


//var consoleUI = builder.Services.GetRequiredService<ConsoleUI>();
////CUSTOMERSCATALOG
////consoleUI.CreateCustomer_UI();
////consoleUI.GetCustomers_UI();
////consoleUI.UpdateCustomer_UI();
//consoleUI.UpdateCustomerProfileAndContactInformation_UI();
////consoleUI.DeleteCustomer_UI();

////PRODUCTSCATALOG
////consoleUI.CreateProduct_UI();
////consoleUI.GetProducts_UI();
////consoleUI.UpdateProduct_UI();
////consoleUI.DeleteProduct_UI();

//////*****SKAPA OMDÖME FÖR PRODUKT*****
////consoleUI.GetProducts_UI();
////Console.WriteLine("Välj produkt med titel att skriva omdöme för");
////var productTitle = Console.ReadLine();
////if (productTitle != null)
////{
////    consoleUI.CreateReviews_UI(productTitle!);

////}
//////SKAPA OMDÖME SLUT


