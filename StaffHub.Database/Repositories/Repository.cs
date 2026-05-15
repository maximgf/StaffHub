using NHibernate;
using StaffHub.Core.Entities;
using StaffHub.Core.Interfaces;

namespace StaffHub.Database.Repositories;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly ISession _session;

    public Repository(ISession session)
    {
        _session = session;
    }

    public void Add(T entity)
    {
        using var transaction = _session.BeginTransaction();
        _session.Save(entity);
        transaction.Commit();
    }

    public void Delete(T entity)
    {
        using var transaction = _session.BeginTransaction();
        entity.IsDeleted = true;
        _session.Update(entity);
        transaction.Commit();
    }

    public IEnumerable<T> GetAll()
    {
        return _session.Query<T>().Where(e => !e.IsDeleted).ToList();
    }

    public T GetById(int id)
    {
        var entity = _session.Get<T>(id);
        return entity != null && !entity.IsDeleted ? entity : null;
    }

    public void Update(T entity)
    {
        using var transaction = _session.BeginTransaction();
        _session.Update(entity);
        transaction.Commit();
    }
}
