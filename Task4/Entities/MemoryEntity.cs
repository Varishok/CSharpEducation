namespace Task4.Entities
{
  /// <summary>
  /// Сущность в памяти.
  /// </summary>
  public class MemoryEntity : IEntity
  {
    public Guid Id { get; }

    /// <summary>
    /// Значение сущности.
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// Конструктор без параметров.
    /// </summary>
    public MemoryEntity() : this(0) { }

    /// <summary>
    /// Конструктор с одним параметром.
    /// </summary>
    /// <param name="value">Значение сущности.</param>
    public MemoryEntity(int value)
    {
      this.Id = Guid.NewGuid();
      this.Value = value;
    }
  }
}
