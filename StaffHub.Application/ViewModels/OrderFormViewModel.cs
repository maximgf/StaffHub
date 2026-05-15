using CommunityToolkit.Mvvm.ComponentModel;
using StaffHub.Core.Entities;
using System;
using System.Collections.Generic;

namespace StaffHub.Application.ViewModels;

public partial class OrderFormViewModel : ObservableObject
{
    public Order Order { get; }
    public IEnumerable<Employee> Employees { get; }
    public IEnumerable<Counterparty> Counterparties { get; }

    public OrderFormViewModel(Order order, IEnumerable<Employee> employees, IEnumerable<Counterparty> counterparties)
    {
        Order = order;
        Employees = employees;
        Counterparties = counterparties;
    }

    public DateTime Date
    {
        get => Order.Date;
        set { Order.Date = value; OnPropertyChanged(); }
    }

    public decimal Amount
    {
        get => Order.Amount;
        set { Order.Amount = value; OnPropertyChanged(); }
    }

    public Employee? Employee
    {
        get => Order.Employee;
        set { Order.Employee = value; OnPropertyChanged(); }
    }

    public Counterparty? Counterparty
    {
        get => Order.Counterparty;
        set { Order.Counterparty = value; OnPropertyChanged(); }
    }
}
