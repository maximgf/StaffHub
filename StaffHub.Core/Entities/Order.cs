namespace StaffHub.Core.Entities;

public class Order : EntityBase
{
    public virtual DateTime Date { get; set; } // Дата
    public virtual decimal Amount { get; set; } // Сумма
    public virtual Employee? Employee { get; set; } // Сотрудник
    public virtual Counterparty? Counterparty { get; set; } // Контрагент
}
