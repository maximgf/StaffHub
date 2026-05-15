using Microsoft.Extensions.DependencyInjection;
using StaffHub.Application.ViewModels;
using StaffHub.Application.Views;
using StaffHub.Database.Extensions;
using System.Windows;

namespace StaffHub.Application;

public partial class App : System.Windows.Application
{
    private ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddStaffHubDatabase();

        // ViewModels
        services.AddTransient<EmployeesViewModel>();
        services.AddTransient<CounterpartiesViewModel>();
        services.AddTransient<OrdersViewModel>();
        services.AddTransient<MainViewModel>();

        // Views
        services.AddTransient<MainWindow>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
