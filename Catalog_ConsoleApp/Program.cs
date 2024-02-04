
using Catalog_ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Models;
using Shared_Catalogs.Repositories;
using Shared_Catalogs.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IT_kurser\Kurser\Webbutveckling-dotnet\Datalagring\Catalogs\Shared_Catalogs\Data\CustomersCatalog.mdf;Integrated Security=True;Connect Timeout=30"));

    services.AddTransient<CustomerService>();

    services.AddTransient<AddressesRepository>();
    services.AddTransient<ContactInformationRepository>();
    services.AddTransient<CustomerPhoneNumbersRepository>();
    services.AddTransient<CustomerProfileRepository>();
    services.AddTransient<CustomersRepository>();
    services.AddTransient<CustomerTypeRepository>();

    services.AddSingleton<ConsoleUI>();


}).Build();


var consoleUI = builder.Services.GetRequiredService<ConsoleUI>();
consoleUI.CreateCustomer_UI();