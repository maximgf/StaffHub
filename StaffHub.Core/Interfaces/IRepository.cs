using StaffHub.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffHub.Core.Interfaces;

/// <summary>
/// Обобщённый интерфейс репозитория: создание, чтение, обновление, удаление и выборка сущностей.
/// </summary>
/// <typeparam name="T">Тип сущности, наследуемый от <see cref="EntityBase"/>.</typeparam>
public interface IRepository<T> where T : EntityBase
{
    /// <summary>
    /// Получает сущность по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор.</param>
    /// <returns>Найденная сущность или null.</returns>
    T? GetById(int id);

    /// <summary>
    /// Получает все неудаленные сущности данного типа синхронно.
    /// </summary>
    /// <returns>Коллекция сущностей.</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Получает все неудаленные сущности данного типа асинхронно.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию, содержащая коллекцию сущностей.</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Добавляет новую сущность в базу данных.
    /// </summary>
    /// <param name="entity">Сущность для добавления.</param>
    void Add(T entity);

    /// <summary>
    /// Обновляет существующую сущность в базе данных.
    /// </summary>
    /// <param name="entity">Сущность для обновления.</param>
    void Update(T entity);

    /// <summary>
    /// Удаляет сущность (мягкое удаление путем установки флага IsDeleted = true).
    /// </summary>
    /// <param name="entity">Сущность для удаления.</param>
    void Delete(T entity);
}
