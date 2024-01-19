using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Shared_Catalogs.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IHost? builder;

    public App()
    {
        var builder = Host.CreateDefaultBuilder();

        builder.ConfigureServices(services =>
        {
            services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\IT_kurser\Kurser\Webbutveckling-dotnet\Datalagring\Catalogs\Shared_Catalogs\Data\CustomersCatalog_db.mdf;Integrated Security=True"));
        });
        builder.Build();
    }
}
