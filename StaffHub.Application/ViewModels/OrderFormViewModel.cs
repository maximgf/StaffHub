using CommunityToolkit.Mvvm.ComponentModel;
using StaffHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StaffHub.Application.ViewModels;

/// <summary>
/// Модель представления для формы добавления и редактирования заказа.
/// Поддерживает валидацию введенных данных.
/// </summary>
public partial class OrderFormViewModel : ObservableValidator
{
    /// <summary>
    /// Внутренняя модель заказа.
    /// </summary>
    public Order Order { get; }

    /// <summary>
    /// Коллекция доступных сотрудников для выбора.
    /// </summary>
    public IEnumerable<Employee> Employees { get; }

    /// <summary>
    /// Коллекция доступных контрагентов для выбора.
    /// </summary>
    public IEnumerable<Counterparty> Counterparties { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="OrderFormViewModel"/>.
    /// </summary>
    /// <param name="order">Редактируемый заказ.</param>
    /// <param name="employees">Список сотрудников.</param>
    /// <param name="counterparties">Список контрагентов.</param>
    public OrderFormViewModel(Order order, IEnumerable<Employee> employees, IEnumerable<Counterparty> counterparties)
    {
        Order = order;
        Employees = employees;
        Counterparties = counterparties;
        ValidateAllProperties();
    }

    /// <summary>
    /// Дата оформления заказа в форме.
    /// </summary>
    public DateTime Date
    {
        get => Order.Date;
        set
        {
            Order.Date = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Сумма заказа в форме (с валидацией диапазона).
    /// </summary>
    [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше нуля.")]
    public decimal Amount
    {
        get => Order.Amount;
        set
        {
            Order.Amount = value;
            OnPropertyChanged();
            ValidateProperty(value, nameof(Amount));
        }
    }

    /// <summary>
    /// Ответственный сотрудник по заказу.
    /// </summary>
    [Required(ErrorMessage = "Сотрудник обязателен для заполнения.")]
    public Employee? Employee
    {
        get => Order.Employee;
        set
        {
            Order.Employee = value;
            OnPropertyChanged();
            ValidateProperty(value, nameof(Employee));
        }
    }

    /// <summary>
    /// Контрагент заказа.
    /// </summary>
    [Required(ErrorMessage = "Контрагент обязателен для заполнения.")]
    public Counterparty? Counterparty
    {
        get => Order.Counterparty;
        set
        {
            Order.Counterparty = value;
            OnPropertyChanged();
            ValidateProperty(value, nameof(Counterparty));
        }
    }
}
