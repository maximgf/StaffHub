using StaffHub.Application.ViewModels;
using System.Windows;

namespace StaffHub.Application.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

        // Wire up dialog service for Employees
        viewModel.EmployeesVM.ShowDialogRequest = (vm) =>
        {
            var dialog = new EmployeeFormWindow
            {
                DataContext = vm,
                Owner = this
            };
            return dialog.ShowDialog();
        };

        // Wire up dialog service for Counterparties
        viewModel.CounterpartiesVM.ShowDialogRequest = (vm) =>
        {
            var dialog = new CounterpartyFormWindow
            {
                DataContext = vm,
                Owner = this
            };
            return dialog.ShowDialog();
        };

        // Wire up dialog service for Orders
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
