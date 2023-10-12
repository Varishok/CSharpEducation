namespace FinalTask
{
  /// <summary>
  /// Книга.
  /// </summary>
  internal class Book
  {
    #region Вложенные типы

    /// <summary>
    /// Статусы книги.
    /// </summary>
    public enum Status
    {
      Available,
      Added,
      Read,
      Done,
    }

    #endregion

    #region Поля и свойства

    /// <summary>
    /// Ид.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Автор.
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Пользователь добавивший книгу.
    /// </summary>
    public User AddedBy { get; set; }

    /// <summary>
    /// Путь к файлу.
    /// </summary>
    public byte[] File { get; set; }

    /// <summary>
    /// Статус.
    /// </summary>
    public Status Mark { get; set; }

    #endregion

    #region Конструктор

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="title">Название книги.</param>
    public Book(string title) : this(title, string.Empty, string.Empty, null, null) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="title">Название книги.</param>
    /// <param name="author">Автор книги.</param>
    public Book(string title, string author) : this(title, author, string.Empty, null, null) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="title">Название книги.</param>
    /// <param name="author">Автор книги.</param>
    /// <param name="description">Описание книги.</param>
    public Book(string title, string author, string description) : this(title, author, description, null, null) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="title">Название книги.</param>
    /// <param name="author">Автор книги.</param>
    /// <param name="description">Описание книги</param>
    /// <param name="filePath">Путь к файлу.</param>
    public Book(string title, string author, string description, User addedBy) : this(title, author, description, addedBy, null) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="title">Название книги.</param>
    /// <param name="author">Автор книги.</param>
    /// <param name="description">Описание книги</param>
    /// <param name="filePath">Путь к файлу.</param>
    /// <param name="addedBy">Пользователь добавивший книгу.</param>
    public Book(string title, string author, string description, User addedBy, byte[] file)
    {
      this.Id = Guid.NewGuid();
      this.Title = title;
      this.Author = author;
      this.Description = description;
      this.AddedBy = addedBy;
      this.File = file;
      this.Mark = Status.Available;
    }

    #endregion
  }
}
