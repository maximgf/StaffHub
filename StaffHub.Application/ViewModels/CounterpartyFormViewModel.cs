using CommunityToolkit.Mvvm.ComponentModel;
using StaffHub.Core.Entities;
using System.Collections.Generic;

namespace StaffHub.Application.ViewModels;

public partial class CounterpartyFormViewModel : ObservableObject
{
    public Counterparty Counterparty { get; }
    public IEnumerable<Employee> Employees { get; }

    public CounterpartyFormViewModel(Counterparty counterparty, IEnumerable<Employee> employees)
    {
        Counterparty = counterparty;
        Employees = employees;
    }

    public string Name
    {
        get => Counterparty.Name;
        set { Counterparty.Name = value; OnPropertyChanged(); }
    }

    public string INN
    {
        get => Counterparty.INN;
        set { Counterparty.INN = value; OnPropertyChanged(); }
    }

    public Employee? Curator
    {
        get => Counterparty.Curator;
        set { Counterparty.Curator = value; OnPropertyChanged(); }
    }
}
