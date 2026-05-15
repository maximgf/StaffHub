using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StaffHub.Core.Entities;

namespace StaffHub.Database.Mappings;

/// <summary>
/// Конфигурация маппинга NHibernate для сущности <see cref="Order"/>.
/// </summary>
public class OrderMap : ClassMapping<Order>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="OrderMap"/> и настраивает сопоставление с таблицей Orders.
    /// </summary>
    public OrderMap()
    {
        Table("Orders");
        Id(x => x.Id, m => m.Generator(Generators.Identity));
        // Дата и сумма заказа.
        Property(x => x.Date, m => {
            m.NotNullable(true);
        });
        Property(x => x.Amount, m => {
            m.NotNullable(true);
            m.Precision(18);
            m.Scale(2);
        });
        // Исполнитель и заказчик.
        ManyToOne(x => x.Employee, m => {
            m.Column("EmployeeId");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
            m.Lazy(LazyRelation.NoLazy);
            m.Fetch(FetchKind.Join);
        });
        ManyToOne(x => x.Counterparty, m => {
            m.Column("CounterpartyId");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
            m.Lazy(LazyRelation.NoLazy);
            m.Fetch(FetchKind.Join);
        });
        Property(x => x.IsDeleted, m => {
            m.NotNullable(true);
        });
    }
}
