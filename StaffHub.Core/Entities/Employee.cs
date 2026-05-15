namespace StaffHub.Core.Entities;

public class Employee : EntityBase
{
    public virtual string LastName { get; set; } = string.Empty; // Фамилия
    public virtual string FirstName { get; set; } = string.Empty; // Имя
    public virtual string MiddleName { get; set; } = string.Empty; // Отчество
    public virtual Position Position { get; set; } // Должность
    public virtual DateTime BirthDate { get; set; } // Дата рождения
}
