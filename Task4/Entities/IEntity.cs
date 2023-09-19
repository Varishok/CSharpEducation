namespace Task4.Entities
{
  /// <summary>
  /// Сущность.
  /// </summary>
  public interface IEntity
  {
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public Guid Id { get; }
  }
}