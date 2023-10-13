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
      OnCreateBookTitle,
      OnUpdateBook,
      OnUpdateBookTitle,
      OnUpdateBookAuthor,
      OnUpdateBookDescription,
      OnUpdateBookFile,
    }

    #endregion

    #region Поля и свойства

    /// <summary>
    /// Ид.
    /// </summary>
    public long Id { get; set; }

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
    public User(long id) : this(id, String.Empty, Status.OnStart) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Ид пользователя.</param>
    /// <param name="name">Имя пользователя.</param>
    public User(long id, string name) : this(id, name, Status.OnStart) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Ид пользователя.</param>
    /// <param name="name">Имя пользователя.</param>
    /// <param name="mark">Статус пользователя.</param>
    public User(long id, string name, Status mark)
    {
      this.Id = id;
      this.Name = name;
      this.Mark = mark;
    }

    #endregion
  }
}
