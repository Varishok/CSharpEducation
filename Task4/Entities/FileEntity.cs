namespace Task4.Entities
{
  /// <summary>
  /// Файловая сущность.
  /// </summary>
  public class FileEntity : IEntity
  {
    public Guid Id { get; }

    /// <summary>
    /// Информация о файле.
    /// </summary>
    public FileInfo FileInfo { get; set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public FileEntity(FileInfo fileInfo)
    {
      Id = Guid.NewGuid();
      FileInfo = fileInfo;
    }
  }
}
