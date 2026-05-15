using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StaffHub.Core.Entities;
using StaffHub.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace StaffHub.Application.ViewModels;

public partial class OrdersViewModel : ObservableObject
{
    private readonly IRepository<Order> _repository;
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Counterparty> _counterpartyRepository;

    [ObservableProperty]
    private ObservableCollection<Order> orders = new();

    [ObservableProperty]
    private Order? selectedOrder;

    public OrdersViewModel(IRepository<Order> repository, IRepository<Employee> employeeRepository, IRepository<Counterparty> counterpartyRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
        _counterpartyRepository = counterpartyRepository;
        LoadData();
    }

    public void LoadData()
    {
        Orders.Clear();
        foreach (var order in _repository.GetAll())
        {
            Orders.Add(order);
        }
    }

    [RelayCommand]
    private void Add()
    {
        var newOrder = new Order { Date = System.DateTime.Now };
        var vm = new OrderFormViewModel(newOrder, _employeeRepository.GetAll(), _counterpartyRepository.GetAll());
        if (ShowDialog(vm) == true)
        {
            _repository.Add(newOrder);
            LoadData();
        }
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedOrder == null) return;

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
            LoadData();
        }
    }

    [RelayCommand]
    private void Delete()
    {
        if (SelectedOrder == null) return;
        
        var result = MessageBox.Show($"Удалить заказ ID: {SelectedOrder.Id} от {SelectedOrder.Date:dd.MM.yyyy}?", 
            "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
        if (result == MessageBoxResult.Yes)
        {
            _repository.Delete(SelectedOrder);
            LoadData();
        }
    }

    public System.Func<OrderFormViewModel, bool?>? ShowDialogRequest { get; set; }

    private bool? ShowDialog(OrderFormViewModel vm)
    {
        return ShowDialogRequest?.Invoke(vm);
    }
}
