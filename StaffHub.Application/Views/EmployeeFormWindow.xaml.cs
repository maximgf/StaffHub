using System.Windows;

namespace StaffHub.Application.Views;

public partial class EmployeeFormWindow : Window
{
    public EmployeeFormWindow()
    {
        InitializeComponent();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
