using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StaffHub.Core.Entities;

namespace StaffHub.Database.Mappings;

public class EmployeeMap : ClassMapping<Employee>
{
    public EmployeeMap()
    {
        Table("Employees");
        Id(x => x.Id, m => m.Generator(Generators.Identity));
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
