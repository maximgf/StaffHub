using CommunityToolkit.Mvvm.ComponentModel;
using StaffHub.Application.Validation;
using StaffHub.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StaffHub.Application.ViewModels;

/// <summary>
/// Модель представления для формы добавления и редактирования контрагента.
/// Поддерживает валидацию введенных данных.
/// </summary>
public partial class CounterpartyFormViewModel : ObservableValidator
{
    /// <summary>
    /// Внутренняя модель контрагента.
    /// </summary>
    public Counterparty Counterparty { get; }

    /// <summary>
    /// Коллекция доступных сотрудников для выбора куратора.
    /// </summary>
    public IEnumerable<Employee> Employees { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CounterpartyFormViewModel"/>.
    /// </summary>
    /// <param name="counterparty">Редактируемый контрагент.</param>
    /// <param name="employees">Список сотрудников.</param>
    public CounterpartyFormViewModel(Counterparty counterparty, IEnumerable<Employee> employees)
    {
        Counterparty = counterparty;
        Employees = employees;
        ValidateAllProperties();
    }

    [Required(ErrorMessage = "Наименование обязательно для заполнения.")]
    public string Name
    {
        get => Counterparty.Name;
        set
        {
            Counterparty.Name = value;
            OnPropertyChanged();
            ValidateProperty(value, nameof(Name));
        }
    }

    [Required(ErrorMessage = "ИНН обязателен для заполнения.")]
    [InnValidation]
    public string INN
    {
        get => Counterparty.INN;
        set
        {
            Counterparty.INN = value;
            OnPropertyChanged();
            ValidateProperty(value, nameof(INN));
        }
    }

    /// <summary>
    /// Выбранный куратор контрагента.
    /// </summary>
    public Employee? Curator
    {
        get => Counterparty.Curator;
        set
        {
            Counterparty.Curator = value;
            OnPropertyChanged();
        }
    }
}
