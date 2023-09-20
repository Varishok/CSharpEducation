using Task4.Entities;

namespace Task4.Repositories
{
  /// <summary>
  /// Репозиторий для работы с сущностями в памяти.
  /// </summary>
  /// <typeparam name="T">Сущности в пямяти.</typeparam>
  internal class MemoryRepository<T> : IRepository<T> where T : MemoryEntity
  {
    #region Поля и свойства

    /// <summary>
    /// Список сущностей.
    /// </summary>
    private List<T> Entities { get; set; }

    #endregion

    #region IRepository

    public void CreateEntity(T entity) 
    { 
      this.Entities.Add(entity);
    }

    public IEnumerable<T> GetAll() 
    { 
      return Entities;
    }

    public T GetEntity(int id)
    {
      return this.Entities[id];
    }

    public void UpdateEntity(T entity) 
    { 
      T updatingEntity = this.Entities.Find(x => x.Id == entity.Id);
      updatingEntity = entity;
    }

    public void DeleteEntity(int id) 
    {
      T entity = GetEntity(id);
      this.Entities.Remove(entity);
    }

    #endregion

    #region Конструкторы

    public MemoryRepository(IEnumerable<T> entities)
    {
      this.Entities = new List<T>(entities);
    }

    #endregion
  }
}
