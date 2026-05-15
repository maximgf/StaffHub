using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StaffHub.Core.Entities;
using StaffHub.Core.Interfaces;
using StaffHub.Database.Extensions;

namespace StaffHub.Application
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Настройка DI и подключение к базе данных...");
            
            var services = new ServiceCollection();
            services.AddStaffHubDatabase();
            
            using var serviceProvider = services.BuildServiceProvider();
            
            // Создаем scope, чтобы получить сервисы
            using var scope = serviceProvider.CreateScope();
            var employeeRepo = scope.ServiceProvider.GetRequiredService<IRepository<Employee>>();
            var counterpartyRepo = scope.ServiceProvider.GetRequiredService<IRepository<Counterparty>>();
            var orderRepo = scope.ServiceProvider.GetRequiredService<IRepository<Order>>();

            Console.WriteLine("Создание тестовых данных...");
            
            // Создание сотрудника
            var employee = new Employee
            {
                LastName = "Иванов",
                FirstName = "Иван",
                MiddleName = "Иванович",
                Position = Position.Manager,
                BirthDate = new DateTime(1980, 5, 15)
            };
            employeeRepo.Add(employee);
            Console.WriteLine($"Добавлен сотрудник: {employee.LastName} {employee.FirstName}, ID: {employee.Id}");

            // Создание контрагента
            var counterparty = new Counterparty
            {
                Name = "ООО Ромашка",
                INN = "123456789012",
                Curator = employee
            };
            counterpartyRepo.Add(counterparty);
            Console.WriteLine($"Добавлен контрагент: {counterparty.Name}, ID: {counterparty.Id}");

            // Создание заказа
            var order = new Order
            {
                Date = DateTime.Now,
                Amount = 150000.50m,
                Employee = employee,
                Counterparty = counterparty
            };
            orderRepo.Add(order);
            Console.WriteLine($"Добавлен заказ на сумму: {order.Amount}, ID: {order.Id}");

            Console.WriteLine("\nОжидание 10 секунд...");
            await Task.Delay(10000);

            Console.WriteLine("\nЧтение данных из базы...");
            
            // Чтобы избежать чтения из кэша сессии, можно создать новый scope (новую сессию)
            using var readScope = serviceProvider.CreateScope();
            var readEmployeeRepo = readScope.ServiceProvider.GetRequiredService<IRepository<Employee>>();
            var readCounterpartyRepo = readScope.ServiceProvider.GetRequiredService<IRepository<Counterparty>>();
            var readOrderRepo = readScope.ServiceProvider.GetRequiredService<IRepository<Order>>();

            var employees = readEmployeeRepo.GetAll();
            Console.WriteLine("Сотрудники в базе:");
            foreach (var emp in employees)
            {
                Console.WriteLine($"- [{emp.Id}] {emp.LastName} {emp.FirstName} {emp.MiddleName}, Должность: {emp.Position}");
            }

            var counterparties = readCounterpartyRepo.GetAll();
            Console.WriteLine("\nКонтрагенты в базе:");
            foreach (var cp in counterparties)
            {
                Console.WriteLine($"- [{cp.Id}] {cp.Name} (ИНН: {cp.INN}), Куратор ID: {cp.Curator?.Id}");
            }

            var orders = readOrderRepo.GetAll();
            Console.WriteLine("\nЗаказы в базе:");
            foreach (var ord in orders)
            {
                Console.WriteLine($"- [{ord.Id}] Сумма: {ord.Amount}, Дата: {ord.Date}, Сотрудник ID: {ord.Employee?.Id}, Контрагент ID: {ord.Counterparty?.Id}");
            }
            
            Console.WriteLine("\nСкрипт успешно завершен.");
        }
    }
}
