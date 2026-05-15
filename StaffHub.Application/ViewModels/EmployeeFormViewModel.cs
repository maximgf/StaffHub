using CommunityToolkit.Mvvm.ComponentModel;
using StaffHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StaffHub.Application.ViewModels;

public partial class EmployeeFormViewModel : ObservableObject
{
    public Employee Employee { get; }

    public EmployeeFormViewModel(Employee employee)
    {
        Employee = employee;
    }

    public string LastName
    {
        get => Employee.LastName;
        set { Employee.LastName = value; OnPropertyChanged(); }
    }

    public string FirstName
    {
        get => Employee.FirstName;
        set { Employee.FirstName = value; OnPropertyChanged(); }
    }

    public string MiddleName
    {
        get => Employee.MiddleName;
        set { Employee.MiddleName = value; OnPropertyChanged(); }
    }

    public Position Position
    {
        get => Employee.Position;
        set { Employee.Position = value; OnPropertyChanged(); }
    }

    public DateTime BirthDate
    {
        get => Employee.BirthDate;
        set { Employee.BirthDate = value; OnPropertyChanged(); }
    }

    public IEnumerable<Position> Positions => Enum.GetValues(typeof(Position)).Cast<Position>();
}
