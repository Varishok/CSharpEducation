namespace Task4.Entities
{
  /// <summary>
  /// Сущность в памяти.
  /// </summary>
  public class MemoryEntity : IEntity
  {
    public Guid Id { get; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public MemoryEntity() 
    { 
      this.Id = Guid.NewGuid();
    }
  }
}
