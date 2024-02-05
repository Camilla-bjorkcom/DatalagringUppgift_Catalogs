
using Catalog_App.Contexts;
using Catalog_ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Models;
using Shared_Catalogs.Repositories;
using Shared_Catalogs.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IT_kurser\Kurser\Webbutveckling-dotnet\Datalagring\Catalogs\Shared_Catalogs\Data\CustomersCatalog.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddDbContext<ProductsDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IT_kurser\Kurser\Webbutveckling-dotnet\Datalagring\Catalogs\Shared_Catalogs\Data\ProductsCatalog.mdf;Integrated Security=True"));

    services.AddTransient<ProductService>();
    services.AddTransient<CustomerService>();
    services.AddTransient<CategoryService>();

    services.AddTransient<AddressesRepository>();
    services.AddTransient<ContactInformationRepository>();
    services.AddTransient<CustomerPhoneNumbersRepository>();
    services.AddTransient<CustomerProfileRepository>();
    services.AddTransient<CustomersRepository>();
    services.AddTransient<CustomerTypeRepository>();


    services.AddTransient<CategoryRepository>();
    services.AddTransient<ManufacturerRepository>();
    services.AddTransient<ProductRepository>();
    services.AddTransient<StockQuantityRepository>();

    services.AddSingleton<ConsoleUI>();


}).Build();


var consoleUI = builder.Services.GetRequiredService<ConsoleUI>();
//consoleUI.CreateCustomer_UI();
//consoleUI.GetCustomers_UI();
//consoleUI.UpdateCustomer_UI();
//consoleUI.DeleteCustomer_UI();

consoleUI.CreateProduct_UI();
