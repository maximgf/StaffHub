namespace StaffHub.Core.Entities;

/// <summary>
/// Сущность, представляющая контрагента (клиента или партнера).
/// </summary>
public class Counterparty : EntityBase
{
    /// <summary>
    /// Наименование контрагента.
    /// </summary>
    public virtual string Name { get; set; } = string.Empty;

    /// <summary>
    /// Индивидуальный номер налогоплательщика (ИНН).
    /// </summary>
    public virtual string INN { get; set; } = string.Empty;

    /// <summary>
    /// Сотрудник, являющийся куратором (ответственным) за данного контрагента.
    /// </summary>
    public virtual Employee? Curator { get; set; }
}
