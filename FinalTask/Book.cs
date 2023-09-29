namespace FinalTask
{
  /// <summary>
  /// Книга.
  /// </summary>
  internal class Book
  {
    #region Поля и свойства

    /// <summary>
    /// Ид.
    /// </summary>
    private Guid Id { get; set; }

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
    /// Путь к файлу.
    /// </summary>
    public string FilePath { get; set; }

    #endregion

    #region Конструктор

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="title">Название книги.</param>
    public Book(string title) : this(title, string.Empty, string.Empty, string.Empty) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="title">Название книги.</param>
    /// <param name="author">Автор книги.</param>
    public Book(string title, string author) : this(title, author, string.Empty, string.Empty) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="title">Название книги.</param>
    /// <param name="author">Автор книги.</param>
    /// <param name="description">Описание книги.</param>
    public Book(string title, string author, string description) : this(title, author, description, string.Empty) { }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="title">Название книги.</param>
    /// <param name="author">Автор книги.</param>
    /// <param name="description">Описание книги</param>
    /// <param name="filePath">Путь к файлу.</param>
    public Book(string title, string author, string description, string filePath)
    {
      this.Id = Guid.NewGuid();
      this.Title = title;
      this.Author = author;
      this.Description = description;
      this.FilePath = filePath;
    }

    #endregion
  }
}
