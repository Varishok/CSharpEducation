namespace FinalTask
{
  /// <summary>
  /// Пользователь библиотеки.
  /// </summary>
  internal class User
  {
    #region Вложенные типы

    /// <summary>
    /// Статусы пользователя.
    /// </summary>
    public enum Status
    {
      OnStart,
      OnCreatedName,
      OnUpdatedName,
      OnCreateBookTitle,
      OnUpdateBookTitle,
      OnCreateBookAuthor,
      OnUpdateBookAuthor,
      OnCreateBookDescription,
      OnUpdateBookDescription,
      OnCreateBookFilePath,
      OnUpdateBookFilePath,
      OnUpdateBookMark,
    }

    #endregion

    #region Поля и свойства

    /// <summary>
    /// Ид.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Статус.
    /// </summary>
    public Status Mark { get; set; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    public User(int id) : this(id, String.Empty) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Ид пользователя.</param>
    /// <param name="name">Имя пользователя.</param>
    public User(int id, string name)
    {
      this.Id = id;
      this.Name = name;
    }

    #endregion
  }
}
