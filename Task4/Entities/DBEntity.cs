namespace Task4
{
  /// <summary>
  /// Сущность для работы с базой данных.
  /// </summary>
  internal class DBEntity : IEntity
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
