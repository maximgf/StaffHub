using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StaffHub.Core.Entities;

namespace StaffHub.Database.Mappings;

/// <summary>
/// Конфигурация маппинга NHibernate для сущности <see cref="Employee"/>.
/// </summary>
public class EmployeeMap : ClassMapping<Employee>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EmployeeMap"/> и настраивает сопоставление с таблицей Employees.
    /// </summary>
    public EmployeeMap()
    {
        Table("Employees");
        Id(x => x.Id, m => m.Generator(Generators.Identity));
        // ФИО, должность, дата рождения и признак удаления.
        Property(x => x.LastName, m => {
            m.Length(100);
            m.NotNullable(true);
        });
        Property(x => x.FirstName, m => {
            m.Length(100);
            m.NotNullable(true);
        });
        Property(x => x.MiddleName, m => {
            m.Length(100);
            m.NotNullable(false);
        });
        Property(x => x.Position, m => {
            m.NotNullable(true);
        });
        Property(x => x.BirthDate, m => {
            m.NotNullable(true);
        });
        Property(x => x.IsDeleted, m => {
            m.NotNullable(true);
        });
    }
}
