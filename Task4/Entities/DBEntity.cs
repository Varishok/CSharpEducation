namespace Task4.Entities
{
  /// <summary>
  /// Сущность для работы с базой данных.
  /// </summary>
  public class DBEntity : IEntity
  {
    public Guid Id { get; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public DBEntity()
    { 
      this.Id = Guid.NewGuid();
    }
  }
}
