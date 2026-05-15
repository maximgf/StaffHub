using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StaffHub.Core.Entities;
using StaffHub.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace StaffHub.Application.ViewModels;

/// <summary>
/// Модель представления для списка сотрудников.
/// </summary>
public partial class EmployeesViewModel : ObservableObject
{
    private readonly IRepository<Employee> _repository;

    /// <summary>
    /// Коллекция загруженных сотрудников.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Employee> employees = new();

    /// <summary>
    /// Выделенный в таблице сотрудник.
    /// </summary>
    [ObservableProperty]
    private Employee? selectedEmployee;

    /// <summary>
    /// Флаг состояния загрузки данных.
    /// </summary>
    [ObservableProperty]
    private bool isLoading;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EmployeesViewModel"/>.
    /// </summary>
    /// <param name="repository">Репозиторий сотрудников.</param>
    public EmployeesViewModel(IRepository<Employee> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Асинхронно загружает список сотрудников из базы данных.
    /// </summary>
    [RelayCommand]
    public async Task LoadDataAsync()
    {
        try
        {
            IsLoading = true;
            
            var items = await _repository.GetAllAsync();
            
            Employees.Clear();
            foreach (var item in items)
            {
                Employees.Add(item);
            }
        }
        catch (System.Exception ex)
        {
            MessageBox.Show("Ошибка загрузки сотрудников: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Команда добавления нового сотрудника.
    /// </summary>
    [RelayCommand]
    private async Task Add()
    {
        var newEmployee = new Employee { BirthDate = System.DateTime.Today };
        var vm = new EmployeeFormViewModel(newEmployee);
        if (ShowDialog(vm) == true)
        {
            _repository.Add(newEmployee);
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Команда редактирования выбранного сотрудника.
    /// </summary>
    [RelayCommand]
    private async Task Edit()
    {
        if (SelectedEmployee == null) return;

        // Копия для редактирования, чтобы изменения можно было отменить закрытием окна без записи в репозиторий.
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
            SelectedEmployee.FirstName = clone.FirstName;
            SelectedEmployee.LastName = clone.LastName;
            SelectedEmployee.MiddleName = clone.MiddleName;
            SelectedEmployee.Position = clone.Position;
            SelectedEmployee.BirthDate = clone.BirthDate;
            
            _repository.Update(SelectedEmployee);
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Команда удаления выбранного сотрудника с подтверждением.
    /// </summary>
    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedEmployee == null) return;
        
        var result = MessageBox.Show($"Удалить сотрудника {SelectedEmployee.LastName} {SelectedEmployee.FirstName}?", 
            "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
        if (result == MessageBoxResult.Yes)
        {
            _repository.Delete(SelectedEmployee);
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Делегат открытия модального окна формы сотрудника (задаётся из представления главного окна).
    /// </summary>
    public System.Func<EmployeeFormViewModel, bool?>? ShowDialogRequest { get; set; }

    /// <summary>
    /// Открывает диалог через <see cref="ShowDialogRequest"/>, если делегат задан.
    /// </summary>
    private bool? ShowDialog(EmployeeFormViewModel vm)
    {
        return ShowDialogRequest?.Invoke(vm);
    }
}
