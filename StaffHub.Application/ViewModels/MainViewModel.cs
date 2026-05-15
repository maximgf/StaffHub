using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace StaffHub.Application.ViewModels;

/// <summary>
/// Главная модель представления, объединяющая другие вкладки.
/// </summary>
public partial class MainViewModel : ObservableObject
{
    /// <summary>
    /// Модель представления вкладки сотрудников.
    /// </summary>
    public EmployeesViewModel EmployeesVM { get; }

    /// <summary>
    /// Модель представления вкладки контрагентов.
    /// </summary>
    public CounterpartiesViewModel CounterpartiesVM { get; }

    /// <summary>
    /// Модель представления вкладки заказов.
    /// </summary>
    public OrdersViewModel OrdersVM { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="MainViewModel"/>.
    /// </summary>
    public MainViewModel(EmployeesViewModel employeesVM, CounterpartiesViewModel counterpartiesVM, OrdersViewModel ordersVM)
    {
        EmployeesVM = employeesVM;
        CounterpartiesVM = counterpartiesVM;
        OrdersVM = ordersVM;
        
        _ = InitializeAsync();
    }

    /// <summary>
    /// Первичная загрузка данных для всех вкладок главного окна.
    /// </summary>
    private async Task InitializeAsync()
    {
        await EmployeesVM.LoadDataAsync();
        await CounterpartiesVM.LoadDataAsync();
        await OrdersVM.LoadDataAsync();
    }
}
