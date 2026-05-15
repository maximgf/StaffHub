using System.Windows;

namespace StaffHub.Application.Views;

/// <summary>
/// Окно формы добавления и редактирования сотрудника.
/// </summary>
public partial class EmployeeFormWindow : Window
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EmployeeFormWindow"/>.
    /// </summary>
    public EmployeeFormWindow()
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
