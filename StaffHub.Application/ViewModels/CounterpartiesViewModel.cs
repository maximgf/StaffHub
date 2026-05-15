using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StaffHub.Core.Entities;
using StaffHub.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace StaffHub.Application.ViewModels;

/// <summary>
/// Модель представления для списка контрагентов.
/// </summary>
public partial class CounterpartiesViewModel : ObservableObject
{
    private readonly IRepository<Counterparty> _repository;
    private readonly IRepository<Employee> _employeeRepository;

    /// <summary>
    /// Коллекция загруженных контрагентов.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Counterparty> counterparties = new();

    /// <summary>
    /// Выделенный в таблице контрагент.
    /// </summary>
    [ObservableProperty]
    private Counterparty? selectedCounterparty;

    /// <summary>
    /// Флаг состояния загрузки данных.
    /// </summary>
    [ObservableProperty]
    private bool isLoading;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CounterpartiesViewModel"/>.
    /// </summary>
    /// <param name="repository">Репозиторий контрагентов.</param>
    /// <param name="employeeRepository">Репозиторий сотрудников.</param>
    public CounterpartiesViewModel(IRepository<Counterparty> repository, IRepository<Employee> employeeRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Асинхронно загружает список контрагентов из базы данных.
    /// </summary>
    [RelayCommand]
    public async Task LoadDataAsync()
    {
        try
        {
            IsLoading = true;

            var items = await _repository.GetAllAsync();

            Counterparties.Clear();
            foreach (var cp in items)
            {
                Counterparties.Add(cp);
            }
        }
        catch (System.Exception ex)
        {
            MessageBox.Show("Ошибка загрузки контрагентов: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Команда добавления нового контрагента.
    /// </summary>
    [RelayCommand]
    private async Task Add()
    {
        var newCounterparty = new Counterparty();
        var vm = new CounterpartyFormViewModel(newCounterparty, _employeeRepository.GetAll());
        if (ShowDialog(vm) == true)
        {
            _repository.Add(newCounterparty);
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Команда редактирования выбранного контрагента.
    /// </summary>
    [RelayCommand]
    private async Task Edit()
    {
        if (SelectedCounterparty == null) return;

        // Независимая копия для диалога: при отмене исходная сущность не меняется.
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
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Команда удаления выбранного контрагента с подтверждением.
    /// </summary>
    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedCounterparty == null) return;
        
        var result = MessageBox.Show($"Удалить контрагента {SelectedCounterparty.Name}?", 
            "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
        if (result == MessageBoxResult.Yes)
        {
            _repository.Delete(SelectedCounterparty);
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Делегат открытия модального окна формы контрагента (задаётся из представления главного окна).
    /// </summary>
    public System.Func<CounterpartyFormViewModel, bool?>? ShowDialogRequest { get; set; }

    /// <summary>
    /// Открывает диалог через <see cref="ShowDialogRequest"/>, если делегат задан.
    /// </summary>
    private bool? ShowDialog(CounterpartyFormViewModel vm)
    {
        return ShowDialogRequest?.Invoke(vm);
    }
}
