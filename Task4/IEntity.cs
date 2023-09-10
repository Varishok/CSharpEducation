namespace Task4
{
  /// <summary>
  /// Сущность.
  /// </summary>
  /// <typeparam name="T">Тип сущности.</typeparam>
  internal class IEntity<T>
  {
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public T Id { get; set; }
  }
}