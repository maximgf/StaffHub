using CommunityToolkit.Mvvm.ComponentModel;
using StaffHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StaffHub.Application.ViewModels;

/// <summary>
/// Модель представления для формы добавления и редактирования сотрудника.
/// Поддерживает валидацию введенных данных.
/// </summary>
public partial class EmployeeFormViewModel : ObservableValidator
{
    /// <summary>
    /// Внутренняя модель сотрудника.
    /// </summary>
    public Employee Employee { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EmployeeFormViewModel"/>.
    /// </summary>
    /// <param name="employee">Редактируемый сотрудник.</param>
    public EmployeeFormViewModel(Employee employee)
    {
        Employee = employee;
        ValidateAllProperties();
    }

    [Required(ErrorMessage = "Фамилия обязательна для заполнения.")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Фамилия не должна содержать числа.")]
    public string LastName
    {
        get => Employee.LastName;
        set
        {
            Employee.LastName = value;
            OnPropertyChanged();
            ValidateProperty(value, nameof(LastName));
        }
    }

    [Required(ErrorMessage = "Имя обязательно для заполнения.")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Имя не должно содержать числа.")]
    public string FirstName
    {
        get => Employee.FirstName;
        set
        {
            Employee.FirstName = value;
            OnPropertyChanged();
            ValidateProperty(value, nameof(FirstName));
        }
    }

    [RegularExpression(@"^[^\d]*$", ErrorMessage = "Отчество не должно содержать числа.")]
    public string MiddleName
    {
        get => Employee.MiddleName;
        set
        {
            Employee.MiddleName = value;
            OnPropertyChanged();
            ValidateProperty(value, nameof(MiddleName));
        }
    }

    /// <summary>
    /// Должность сотрудника в форме редактирования.
    /// </summary>
    public Position Position
    {
        get => Employee.Position;
        set
        {
            Employee.Position = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Дата рождения в форме редактирования.
    /// </summary>
    public DateTime BirthDate
    {
        get => Employee.BirthDate;
        set
        {
            Employee.BirthDate = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Значения перечисления должностей для привязки к списку выбора.
    /// </summary>
    public IEnumerable<Position> Positions => Enum.GetValues(typeof(Position)).Cast<Position>();
}
