using Microsoft.EntityFrameworkCore;

namespace Task4
{
  /// <summary>
  /// Работа с базой данных.
  /// </summary>
  /// <typeparam name="T">Тип сущности.</typeparam>
  internal class DBRepository<T> : IRepository<T> where T : DBEntity
  {
    /// <summary>
    /// Соединение с базой данных.
    /// </summary>
    DbSet<T> dbSet;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="db">Соединение с базой данных.</param>
    public DBRepository(DbSet<T> db) 
    {
      this.dbSet = db;
    }

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
  }
}
