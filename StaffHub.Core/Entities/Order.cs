namespace StaffHub.Core.Entities;

/// <summary>
/// Сущность, представляющая заказ, оформленный контрагентом.
/// </summary>
public class Order : EntityBase
{
    /// <summary>
    /// Дата оформления заказа.
    /// </summary>
    public virtual DateTime Date { get; set; }

    /// <summary>
    /// Сумма заказа в рублях.
    /// </summary>
    public virtual decimal Amount { get; set; }

    /// <summary>
    /// Сотрудник, оформивший заказ.
    /// </summary>
    public virtual Employee? Employee { get; set; }

    /// <summary>
    /// Контрагент, для которого оформлен заказ.
    /// </summary>
    public virtual Counterparty? Counterparty { get; set; }
}
