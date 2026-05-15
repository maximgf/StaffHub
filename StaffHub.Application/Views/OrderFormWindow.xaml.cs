using System.Windows;

namespace StaffHub.Application.Views;

/// <summary>
/// Окно формы добавления и редактирования заказа.
/// </summary>
public partial class OrderFormWindow : Window
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="OrderFormWindow"/>.
    /// </summary>
    public OrderFormWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Обрабатывает нажатие кнопки "Сохранить".
    /// </summary>
    private void Save_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    /// <summary>
    /// Обрабатывает нажатие кнопки "Отмена".
    /// </summary>
    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}