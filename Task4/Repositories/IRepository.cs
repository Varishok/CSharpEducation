using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
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
    public void CreateRepository(T entity) { }

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
    public T GetRepository(int id);

    /// <summary>
    /// Обновление сущности.
    /// </summary>
    /// <param name="entity">Сущность</param>
    public void UpdateRepository(T entity) { }

    /// <summary>
    /// Удаление сущности.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    public void DeleteRepository(int id) { }
  }
}
