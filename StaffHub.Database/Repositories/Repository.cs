using NHibernate;
using NHibernate.Linq;
using StaffHub.Core.Entities;
using StaffHub.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffHub.Database.Repositories;

/// <summary>
/// Универсальный репозиторий для выполнения операций с сущностями базы данных через NHibernate.
/// </summary>
/// <typeparam name="T">Тип сущности, наследуемый от <see cref="EntityBase"/>.</typeparam>
public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly ISession _session;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Repository{T}"/>.
    /// </summary>
    /// <param name="session">Сессия NHibernate.</param>
    public Repository(ISession session)
    {
        _session = session;
    }

    /// <summary>
    /// Добавляет новую сущность в базу данных.
    /// </summary>
    public void Add(T entity)
    {
        using var transaction = _session.BeginTransaction();
        _session.Save(entity);
        transaction.Commit();
    }

    /// <summary>
    /// Помечает сущность как удалённую (мягкое удаление через <see cref="EntityBase.IsDeleted"/>).
    /// </summary>
    public void Delete(T entity)
    {
        using var transaction = _session.BeginTransaction();
        entity.IsDeleted = true;
        _session.Update(entity);
        transaction.Commit();
    }

    /// <summary>
    /// Возвращает все сущности данного типа, у которых не установлен признак удаления.
    /// </summary>
    public IEnumerable<T> GetAll()
    {
        return _session.Query<T>().Where(e => !e.IsDeleted).ToList();
    }

    /// <summary>
    /// Асинхронно возвращает все неудалённые сущности данного типа.
    /// </summary>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _session.Query<T>()
            .Where(e => !e.IsDeleted)
            .ToListAsync();
    }

    /// <summary>
    /// Получает сущность по идентификатору; удалённые записи не возвращает.
    /// </summary>
    public T? GetById(int id)
    {
        var entity = _session.Get<T>(id);
        return entity != null && !entity.IsDeleted ? entity : null;
    }

    /// <summary>
    /// Обновляет существующую сущность в базе данных.
    /// </summary>
    public void Update(T entity)
    {
        using var transaction = _session.BeginTransaction();
        _session.Update(entity);
        transaction.Commit();
    }
}
