using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StaffHub.Core.Entities;
using StaffHub.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace StaffHub.Application.ViewModels;

public partial class CounterpartiesViewModel : ObservableObject
{
    private readonly IRepository<Counterparty> _repository;
    private readonly IRepository<Employee> _employeeRepository;

    [ObservableProperty]
    private ObservableCollection<Counterparty> counterparties = new();

    [ObservableProperty]
    private Counterparty? selectedCounterparty;

    public CounterpartiesViewModel(IRepository<Counterparty> repository, IRepository<Employee> employeeRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
        LoadData();
    }

    public void LoadData()
    {
        Counterparties.Clear();
        foreach (var cp in _repository.GetAll())
        {
            Counterparties.Add(cp);
        }
    }

    [RelayCommand]
    private void Add()
    {
        var newCounterparty = new Counterparty();
        var vm = new CounterpartyFormViewModel(newCounterparty, _employeeRepository.GetAll());
        if (ShowDialog(vm) == true)
        {
            _repository.Add(newCounterparty);
            LoadData();
        }
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedCounterparty == null) return;

        var clone = new Counterparty
        {
            Id = SelectedCounterparty.Id,
            Name = SelectedCounterparty.Name,
            INN = SelectedCounterparty.INN,
            Curator = SelectedCounterparty.Curator,
            IsDeleted = SelectedCounterparty.IsDeleted
        };

        var vm = new CounterpartyFormViewModel(clone, _employeeRepository.GetAll());
        if (ShowDialog(vm) == true)
        {
            SelectedCounterparty.Name = clone.Name;
            SelectedCounterparty.INN = clone.INN;
            SelectedCounterparty.Curator = clone.Curator;
            
            _repository.Update(SelectedCounterparty);
            LoadData();
        }
    }

    [RelayCommand]
    private void Delete()
    {
        if (SelectedCounterparty == null) return;
        
        var result = MessageBox.Show($"Удалить контрагента {SelectedCounterparty.Name}?", 
            "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
        if (result == MessageBoxResult.Yes)
        {
            _repository.Delete(SelectedCounterparty);
            LoadData();
        }
    }

    public System.Func<CounterpartyFormViewModel, bool?>? ShowDialogRequest { get; set; }

    private bool? ShowDialog(CounterpartyFormViewModel vm)
    {
        return ShowDialogRequest?.Invoke(vm);
    }
}
