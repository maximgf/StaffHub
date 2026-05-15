namespace StaffHub.Core.Entities;

public class Counterparty : EntityBase
{
    public virtual string Name { get; set; } = string.Empty; // Наименование
    public virtual string INN { get; set; } = string.Empty; // ИНН
    public virtual Employee? Curator { get; set; } // Куратор
}
