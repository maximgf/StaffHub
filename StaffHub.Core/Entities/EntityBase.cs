namespace StaffHub.Core.Entities;

/// <summary>
/// Базовый класс для всех сущностей в системе.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public virtual int Id { get; set; }

    /// <summary>
    /// Флаг мягкого удаления. Если установлен, сущность считается удалённой.
    /// </summary>
    public virtual bool IsDeleted { get; set; }
}
