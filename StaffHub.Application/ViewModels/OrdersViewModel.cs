using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StaffHub.Core.Entities;
using StaffHub.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace StaffHub.Application.ViewModels;

/// <summary>
/// Модель представления для списка заказов.
/// </summary>
public partial class OrdersViewModel : ObservableObject
{
    private readonly IRepository<Order> _repository;
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Counterparty> _counterpartyRepository;

    /// <summary>
    /// Коллекция загруженных заказов.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Order> orders = new();

    /// <summary>
    /// Выделенный в таблице заказ.
    /// </summary>
    [ObservableProperty]
    private Order? selectedOrder;

    /// <summary>
    /// Флаг состояния загрузки данных.
    /// </summary>
    [ObservableProperty]
    private bool isLoading;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="OrdersViewModel"/>.
    /// </summary>
    /// <param name="repository">Репозиторий заказов.</param>
    /// <param name="employeeRepository">Репозиторий сотрудников.</param>
    /// <param name="counterpartyRepository">Репозиторий контрагентов.</param>
    public OrdersViewModel(IRepository<Order> repository, IRepository<Employee> employeeRepository, IRepository<Counterparty> counterpartyRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
        _counterpartyRepository = counterpartyRepository;
    }

    /// <summary>
    /// Асинхронно загружает список заказов из базы данных.
    /// </summary>
    [RelayCommand]
    public async Task LoadDataAsync()
    {
        try
        {
            IsLoading = true;

            var items = await _repository.GetAllAsync();

            Orders.Clear();
            foreach (var order in items)
            {
                Orders.Add(order);
            }
        }
        catch (System.Exception ex)
        {
            MessageBox.Show("Ошибка загрузки заказов: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Команда добавления нового заказа.
    /// </summary>
    [RelayCommand]
    private async Task Add()
    {
        var newOrder = new Order { Date = System.DateTime.Now };
        var vm = new OrderFormViewModel(newOrder, _employeeRepository.GetAll(), _counterpartyRepository.GetAll());
        if (ShowDialog(vm) == true)
        {
            _repository.Add(newOrder);
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Команда редактирования выбранного заказа.
    /// </summary>
    [RelayCommand]
    private async Task Edit()
    {
        if (SelectedOrder == null) return;

        // Независимая копия для диалога: при отмене исходная сущность не меняется.
        var clone = new Order
        {
            Id = SelectedOrder.Id,
            Date = SelectedOrder.Date,
            Amount = SelectedOrder.Amount,
            Employee = SelectedOrder.Employee,
            Counterparty = SelectedOrder.Counterparty,
            IsDeleted = SelectedOrder.IsDeleted
        };

        var vm = new OrderFormViewModel(clone, _employeeRepository.GetAll(), _counterpartyRepository.GetAll());
        if (ShowDialog(vm) == true)
        {
            SelectedOrder.Date = clone.Date;
            SelectedOrder.Amount = clone.Amount;
            SelectedOrder.Employee = clone.Employee;
            SelectedOrder.Counterparty = clone.Counterparty;
            
            _repository.Update(SelectedOrder);
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Команда удаления выбранного заказа с подтверждением.
    /// </summary>
    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedOrder == null) return;
        
        var result = MessageBox.Show($"Удалить заказ ID: {SelectedOrder.Id} от {SelectedOrder.Date:dd.MM.yyyy}?", 
            "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
        if (result == MessageBoxResult.Yes)
        {
            _repository.Delete(SelectedOrder);
            await LoadDataAsync();
        }
    }

    /// <summary>
    /// Делегат открытия модального окна формы заказа (задаётся из представления главного окна).
    /// </summary>
    public System.Func<OrderFormViewModel, bool?>? ShowDialogRequest { get; set; }

    /// <summary>
    /// Открывает диалог через <see cref="ShowDialogRequest"/>, если делегат задан.
    /// </summary>
    private bool? ShowDialog(OrderFormViewModel vm)
    {
        return ShowDialogRequest?.Invoke(vm);
    }
}
