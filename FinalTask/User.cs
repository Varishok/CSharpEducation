namespace FinalTask
{
  /// <summary>
  /// Пользователь библиотеки.
  /// </summary>
  internal class User
  {
    #region Поля и свойства

    /// <summary>
    /// Ид.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    public User(string name) : this(Guid.NewGuid(), name) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Ид пользователя.</param>
    /// <param name="name">Имя пользователя.</param>
    public User(Guid id, string name)
    {
      this.Id = id;
      this.Name = name;
    }

    #endregion
  }
}
