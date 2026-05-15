using System.Windows;

namespace StaffHub.Application.Views;

/// <summary>
/// Окно формы добавления и редактирования контрагента.
/// </summary>
public partial class CounterpartyFormWindow : Window
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CounterpartyFormWindow"/>.
    /// </summary>
    public CounterpartyFormWindow()
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
