using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StaffHub.Core.Entities;

namespace StaffHub.Database.Mappings;

/// <summary>
/// Конфигурация маппинга NHibernate для сущности <see cref="Counterparty"/>.
/// </summary>
public class CounterpartyMap : ClassMapping<Counterparty>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CounterpartyMap"/> и настраивает сопоставление с таблицей Counterparties.
    /// </summary>
    public CounterpartyMap()
    {
        Table("Counterparties");
        Id(x => x.Id, m => m.Generator(Generators.Identity));
        // Реквизиты контрагента.
        Property(x => x.Name, m => {
            m.Length(255);
            m.NotNullable(true);
        });
        Property(x => x.INN, m => {
            m.Length(12);
            m.NotNullable(true);
        });
        // Связь с ответственным сотрудником (может быть не задана).
        ManyToOne(x => x.Curator, m => {
            m.Column("CuratorId");
            m.NotNullable(false);
            m.Cascade(Cascade.None);
            m.Lazy(LazyRelation.NoLazy);
            m.Fetch(FetchKind.Join);
        });
        Property(x => x.IsDeleted, m => {
            m.NotNullable(true);
        });
    }
}
