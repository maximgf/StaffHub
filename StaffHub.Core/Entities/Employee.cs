namespace StaffHub.Core.Entities;

/// <summary>
/// Сущность, представляющая сотрудника компании.
/// </summary>
public class Employee : EntityBase
{
    /// <summary>
    /// Фамилия сотрудника.
    /// </summary>
    public virtual string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Имя сотрудника.
    /// </summary>
    public virtual string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Отчество сотрудника.
    /// </summary>
    public virtual string MiddleName { get; set; } = string.Empty;

    /// <summary>
    /// Должность сотрудника (Работник или Руководитель).
    /// </summary>
    public virtual Position Position { get; set; }

    /// <summary>
    /// Дата рождения сотрудника.
    /// </summary>
    public virtual DateTime BirthDate { get; set; }
}
