using CommunityToolkit.Mvvm.ComponentModel;

namespace StaffHub.Application.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public EmployeesViewModel EmployeesVM { get; }
    public CounterpartiesViewModel CounterpartiesVM { get; }
    public OrdersViewModel OrdersVM { get; }

    public MainViewModel(EmployeesViewModel employeesVM, CounterpartiesViewModel counterpartiesVM, OrdersViewModel ordersVM)
    {
        EmployeesVM = employeesVM;
        CounterpartiesVM = counterpartiesVM;
        OrdersVM = ordersVM;
    }
}
