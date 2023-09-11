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
    DbSet<T> db;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="db">Соединение с базой данных.</param>
    public DBRepository(DbSet<T> db) 
    {
      this.db = db;
    }

    public void CreateRepository(T entity)
    {
      db.Add(entity);
    }

    public IEnumerable<T> GetAll()
    {
      return db;
    }

    public T GetRepository(int id)
    {
      return db.Find(id);
    }

    public void UpdateRepository(T entity)
    {
      db.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteRepository(int id)
    {
      T entity = GetRepository(id);
      db.Remove(entity);
    }
  }
}
