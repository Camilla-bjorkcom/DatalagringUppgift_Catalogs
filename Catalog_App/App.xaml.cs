using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Shared_Catalogs.Contexts;
using Microsoft.EntityFrameworkCore;
using Catalog_App.Mvvm.ViewModels;
using Catalog_App.Mvvm.Views;

namespace Catalog_App;

public partial class App : Application
{
    private static IHost? builder;

    public App()
    {
        var builder = Host.CreateDefaultBuilder();

        builder.ConfigureServices(services =>
        {
            services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IT_kurser\Kurser\Webbutveckling-dotnet\Datalagring\Catalogs\Shared_Catalogs\Data\CustomersCatalog.mdf;Integrated Security=True;Connect Timeout=30"));
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<StartCatalogPageViewModel>();
            services.AddTransient<StartCatalogPageView>();
            services.AddTransient<AddCustomerView>();
            services.AddTransient<AddCustomerViewModel>();
            services.AddTransient<CustomerListViewModel>();
            services.AddTransient<CustomerListView>();


        });
        builder.Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        builder!.Start();
        var mainWindow = builder!.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        
    }
}
