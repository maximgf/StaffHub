using CommunityToolkit.Mvvm.ComponentModel;
using StaffHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StaffHub.Application.ViewModels;

public partial class OrderFormViewModel : ObservableValidator
{
    public Order Order { get; }
    public IEnumerable<Employee> Employees { get; }
    public IEnumerable<Counterparty> Counterparties { get; }

    public OrderFormViewModel(Order order, IEnumerable<Employee> employees, IEnumerable<Counterparty> counterparties)
    {
        Order = order;
        Employees = employees;
        Counterparties = counterparties;
        ValidateAllProperties();
    }

    public DateTime Date
    {
        get => Order.Date;
        set
        {
            Order.Date = value;
            OnPropertyChanged();
        }
    }

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
