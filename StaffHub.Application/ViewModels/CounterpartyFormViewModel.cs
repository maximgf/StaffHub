using CommunityToolkit.Mvvm.ComponentModel;
using StaffHub.Application.Validation;
using StaffHub.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StaffHub.Application.ViewModels;

public partial class CounterpartyFormViewModel : ObservableValidator
{
    public Counterparty Counterparty { get; }
    public IEnumerable<Employee> Employees { get; }

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
