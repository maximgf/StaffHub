using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StaffHub.Core.Entities;

namespace StaffHub.Database.Mappings;

public class OrderMap : ClassMapping<Order>
{
    public OrderMap()
    {
        Table("Orders");
        Id(x => x.Id, m => m.Generator(Generators.Identity));
        Property(x => x.Date, m => {
            m.NotNullable(true);
        });
        Property(x => x.Amount, m => {
            m.NotNullable(true);
            m.Precision(18);
            m.Scale(2);
        });
        ManyToOne(x => x.Employee, m => {
            m.Column("EmployeeId");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
        });
        ManyToOne(x => x.Counterparty, m => {
            m.Column("CounterpartyId");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
        });
        Property(x => x.IsDeleted, m => {
            m.NotNullable(true);
        });
    }
}
