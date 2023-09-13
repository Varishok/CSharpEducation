namespace Task4
{
  /// <summary>
  /// Файловая сущность.
  /// </summary>
  internal class FileEntity : IEntity
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
