using System.Windows;

namespace StaffHub.Application.Views;

public partial class OrderFormWindow : Window
{
    public OrderFormWindow()
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