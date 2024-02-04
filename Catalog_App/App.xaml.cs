using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Shared_Catalogs.Contexts;
using Microsoft.EntityFrameworkCore;
using Catalog_App.Mvvm.ViewModels;
using Catalog_App.Mvvm.Views;
using Shared_Catalogs.Services;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Models;
using Microsoft.Extensions.Logging;
using Shared_Catalogs.Repositories;

namespace Catalog_App;

public partial class App : Application
{
    private static IHost? builder;

    public App()
    {
        builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IT_kurser\Kurser\Webbutveckling-dotnet\Datalagring\Catalogs\Shared_Catalogs\Data\CustomersCatalog.mdf;Integrated Security=True;Connect Timeout=30"));
           
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            services.AddTransient<CustomerService>();
            services.AddTransient<ProductService>();
            services.AddTransient<CustomerModel>();

            services.AddScoped<AddressesRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<ContactInformationRepository>();
            services.AddScoped<CustomerPhoneNumbersRepository>();
            services.AddScoped<CustomerProfileRepository>();
            services.AddScoped<CustomersRepository>();
            services.AddScoped<CustomerTypeRepository>();
            

            services.AddTransient<StartCatalogPageViewModel>();
            services.AddTransient<StartCatalogPageView>();

            services.AddTransient<AddCustomerView>();
            services.AddTransient<AddCustomerViewModel>();

            services.AddTransient<CustomerListViewModel>();
            services.AddTransient<CustomerListView>();

            services.AddTransient<UpdateCustomerViewModel>();
            services.AddTransient<UpdateCustomerView>();

            services.AddTransient<CustomerProfileViewModel>();
            services.AddTransient<CustomerProfileView>();

        }).Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
         builder!.StartAsync();
         var mainWindow = builder!.Services.GetRequiredService<MainWindow>();
         mainWindow.Show();
        

    }
}

