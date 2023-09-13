using Task4.Entities;

namespace Task4.Repositories
{
  /// <summary>
  /// Операции с сущностями.
  /// </summary>
  /// <typeparam name="T">Тип сущности.</typeparam>
  internal interface IRepository<T> 
    where T : IEntity
  {
    /// <summary>
    /// Создание сущности
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public void CreateEntity(T entity);

    /// <summary>
    /// Получение всех сущностей.
    /// </summary>
    /// <returns>Перечисление сущностей.</returns>
    public IEnumerable<T> GetAll();

    /// <summary>
    /// Получение сущности.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>Полученная сущности или ошибка, если таковой нет.</returns>
    public T GetEntity(int id);

    /// <summary>
    /// Обновление сущности.
    /// </summary>
    /// <param name="entity">Сущность</param>
    public void UpdateEntity(T entity);

    /// <summary>
    /// Удаление сущности.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    public void DeleteEntity(int id);
  }
}
