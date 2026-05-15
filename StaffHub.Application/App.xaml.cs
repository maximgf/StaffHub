using Microsoft.Extensions.DependencyInjection;
using StaffHub.Application.ViewModels;
using StaffHub.Application.Views;
using StaffHub.Database.Extensions;
using System.Windows;

namespace StaffHub.Application;

/// <summary>
/// Логика взаимодействия для App.xaml
/// Настраивает DI контейнер и запускает главное окно.
/// </summary>
public partial class App : System.Windows.Application
{
    private ServiceProvider _serviceProvider;

    /// <summary>
    /// Инициализирует новый экземпляр приложения.
    /// </summary>
    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    /// <summary>
    /// Регистрирует сервисы базы данных, моделей представления и окон в контейнере внедрения зависимостей.
    /// </summary>
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddStaffHubDatabase();

        // Модели представления вкладок и главного окна.
        services.AddTransient<EmployeesViewModel>();
        services.AddTransient<CounterpartiesViewModel>();
        services.AddTransient<OrdersViewModel>();
        services.AddTransient<MainViewModel>();

        // Главное окно интерфейса.
        services.AddTransient<MainWindow>();
    }

    /// <summary>
    /// Показывает главное окно при запуске приложения.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
