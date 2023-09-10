using Microsoft.EntityFrameworkCore;

namespace Task4
{
  internal class DBRepository<T> : IRepository<T> where T : IEntity<T>
  {
    DbContext db;

    public DBRepository(DbContext db) 
    {
      this.db = db;
    }

    public void CreateRepository(T entity)
    {
      db.Repository.Add(entity);
    }

    public IEnumerable<T> GetAll()
    {
      return db.Repository;
    }

    public T GetRepository(int id)
    {
      return db.Repository.Find(id);
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
