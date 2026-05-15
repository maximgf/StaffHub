using StaffHub.Application.ViewModels;
using System.Windows;

namespace StaffHub.Application.Views;

/// <summary>
/// Главное окно приложения.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="MainWindow"/>.
    /// </summary>
    /// <param name="viewModel">Главная модель представления (<see cref="MainViewModel"/>).</param>
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

        // Подключение модальных окон для операций со списком сотрудников.
        viewModel.EmployeesVM.ShowDialogRequest = (vm) =>
        {
            var dialog = new EmployeeFormWindow
            {
                DataContext = vm,
                Owner = this
            };
            return dialog.ShowDialog();
        };

        // Подключение модальных окон для операций со списком контрагентов.
        viewModel.CounterpartiesVM.ShowDialogRequest = (vm) =>
        {
            var dialog = new CounterpartyFormWindow
            {
                DataContext = vm,
                Owner = this
            };
            return dialog.ShowDialog();
        };

        // Подключение модальных окон для операций со списком заказов.
        viewModel.OrdersVM.ShowDialogRequest = (vm) =>
        {
            var dialog = new OrderFormWindow
            {
                DataContext = vm,
                Owner = this
            };
            return dialog.ShowDialog();
        };
    }
}
