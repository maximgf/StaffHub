using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StaffHub.Core.Entities;
using StaffHub.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace StaffHub.Application.ViewModels;

public partial class EmployeesViewModel : ObservableObject
{
    private readonly IRepository<Employee> _repository;

    [ObservableProperty]
    private ObservableCollection<Employee> employees = new();

    [ObservableProperty]
    private Employee? selectedEmployee;

    public EmployeesViewModel(IRepository<Employee> repository)
    {
        _repository = repository;
        LoadData();
    }

    private void LoadData()
    {
        Employees.Clear();
        foreach (var emp in _repository.GetAll())
        {
            Employees.Add(emp);
        }
    }

    [RelayCommand]
    private void Add()
    {
        var newEmployee = new Employee { BirthDate = System.DateTime.Today };
        var vm = new EmployeeFormViewModel(newEmployee);
        if (ShowDialog(vm) == true)
        {
            _repository.Add(newEmployee);
            LoadData();
        }
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedEmployee == null) return;

        // Clone for editing
        var clone = new Employee
        {
            Id = SelectedEmployee.Id,
            FirstName = SelectedEmployee.FirstName,
            LastName = SelectedEmployee.LastName,
            MiddleName = SelectedEmployee.MiddleName,
            Position = SelectedEmployee.Position,
            BirthDate = SelectedEmployee.BirthDate,
            IsDeleted = SelectedEmployee.IsDeleted
        };

        var vm = new EmployeeFormViewModel(clone);
        if (ShowDialog(vm) == true)
        {
            // Update original
            SelectedEmployee.FirstName = clone.FirstName;
            SelectedEmployee.LastName = clone.LastName;
            SelectedEmployee.MiddleName = clone.MiddleName;
            SelectedEmployee.Position = clone.Position;
            SelectedEmployee.BirthDate = clone.BirthDate;
            
            _repository.Update(SelectedEmployee);
            LoadData();
        }
    }

    [RelayCommand]
    private void Delete()
    {
        if (SelectedEmployee == null) return;
        
        var result = MessageBox.Show($"Удалить сотрудника {SelectedEmployee.LastName} {SelectedEmployee.FirstName}?", 
            "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
        if (result == MessageBoxResult.Yes)
        {
            _repository.Delete(SelectedEmployee);
            LoadData();
        }
    }

    // In a pure MVVM, this should be in an IDialogService. 
    // Using a quick callback to View for simplicity.
    public System.Func<EmployeeFormViewModel, bool?>? ShowDialogRequest { get; set; }

    private bool? ShowDialog(EmployeeFormViewModel vm)
    {
        return ShowDialogRequest?.Invoke(vm);
    }
}
