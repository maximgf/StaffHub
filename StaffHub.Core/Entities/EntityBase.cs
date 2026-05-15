namespace StaffHub.Core.Entities;

public abstract class EntityBase
{
    public virtual int Id { get; set; }
    public virtual bool IsDeleted { get; set; }
}
