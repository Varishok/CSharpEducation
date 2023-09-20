using Microsoft.EntityFrameworkCore;
using Task4.Entities;

namespace Task4.Repositories
{
  /// <summary>
  /// Работа с базой данных.
  /// </summary>
  /// <typeparam name="T">Тип сущности.</typeparam>
  internal class DBRepository<T> : IRepository<T> where T : DBEntity
  {
    #region Поля и свойства

    /// <summary>
    /// Соединение с базой данных.
    /// </summary>
    DbSet<T> dbSet;

    #endregion

    #region IRepository

    public void CreateEntity(T entity)
    {
      dbSet.Add(entity);
    }

    public IEnumerable<T> GetAll()
    {
      return dbSet;
    }

    public T GetEntity(int id)
    {
      return dbSet.Find(id);
    }

    public void UpdateEntity(T entity)
    {
      dbSet.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteEntity(int id)
    {
      T entity = GetEntity(id);
      dbSet.Remove(entity);
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="db">Соединение с базой данных.</param>
    public DBRepository(DbSet<T> db)
    {
      this.dbSet = db;
    }

    #endregion
  }
}
