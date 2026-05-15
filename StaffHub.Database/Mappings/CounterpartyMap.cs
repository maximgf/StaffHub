using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StaffHub.Core.Entities;

namespace StaffHub.Database.Mappings;

public class CounterpartyMap : ClassMapping<Counterparty>
{
    public CounterpartyMap()
    {
        Table("Counterparties");
        Id(x => x.Id, m => m.Generator(Generators.Identity));
        Property(x => x.Name, m => {
            m.Length(255);
            m.NotNullable(true);
        });
        Property(x => x.INN, m => {
            m.Length(12);
            m.NotNullable(true);
        });
        ManyToOne(x => x.Curator, m => {
            m.Column("CuratorId");
            m.NotNullable(false);
            m.Cascade(Cascade.None);
        });
        Property(x => x.IsDeleted, m => {
            m.NotNullable(true);
        });
    }
}
