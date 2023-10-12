using Microsoft.Data.Sqlite;
using Telegram.Bot.Types;

namespace FinalTask
{
  /// <summary>
  /// Взаимодействие с базой данных.
  /// </summary>
  internal static class DBInteraction
  {
    /// <summary>
    /// Параметры базы данных.
    /// </summary>
    const string Config = "Data Source=library.db";

    /// <summary>
    /// Существование пользователя в базе.
    /// </summary>
    /// <param name="userId">Ид пользователя.</param>
    /// <returns>Пользователя, если он найден, null - в противном случае.</returns>
    public static User? FindUser(long userId)
    {
      using(var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT Id, Name, User_status From Users WHERE Id = $id";
        command.Parameters.AddWithValue("$id", userId);

        using(var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var id = (long)reader.GetValue(0);
            var name = reader.GetValue(1) != DBNull.Value ? (string)reader.GetValue(1) : String.Empty;
            var mark = reader.GetValue(2) != DBNull.Value ? (User.Status)Enum.GetValues(typeof(User.Status)).GetValue((long)reader.GetValue(2)) : User.Status.OnStart;
            return new User(id: id, name: name, mark: mark);
          }
        }
        connection.Close();
      }
      return null;
    }

    /// <summary>
    /// Создание пользователя.
    /// </summary>
    /// <param name="userId">Ид пользователя.</param>
    public static void CreateUser(long userId)
    {
      using(var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO Users ('Id') VALUES ($id)";
        command.Parameters.AddWithValue("$id", userId);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    /// <summary>
    /// Обновления состояния пользователя.
    /// </summary>
    /// <param name="userId">Ид пользователя.</param>
    /// <param name="mark">Новое состояние.</param>
    public static void UpdateUserMark(long userId, User.Status mark)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE Users SET User_status=$mark WHERE Id=$id";
        command.Parameters.AddWithValue("$id", userId);
        command.Parameters.AddWithValue("$mark", (long)mark);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    /// <summary>
    /// Обновление имени пользователя.
    /// </summary>
    /// <param name="userId">Ид пользователя.</param>
    /// <param name="userName">Новое Name.</param>
    public static void UpdateUserName(long userId, string userName)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE Users SET Name=$name WHERE Id=$id";
        command.Parameters.AddWithValue("$id", userId);
        command.Parameters.AddWithValue("$name", userName);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    /// <summary>
    /// Добавление связи между пользователем и книгой.
    /// </summary>
    /// <param name="userId">Ид пользователя.</param>
    /// <param name="bookId">Ид книги.</param>
    public static void AddBookToUser(long userId, string bookId)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO BooksUsers (Id_book, Id_user) VALUES ($idBook, $idUser)";
        command.Parameters.AddWithValue("$idBook", bookId.ToUpper());
        command.Parameters.AddWithValue("$idUser", userId);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    /// <summary>
    /// Получение книг, не привязанных к текущему пользователю.
    /// </summary>
    /// <param name="userId">Ид пользователя.</param>
    /// <returns>Список книг.</returns>
    public static List<Book> GetAllBooks(long userId)
    {
      var books = new List<Book>();
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT Id, Title FROM Books 
          WHERE Id NOT IN (SELECT Id_book From BooksUsers 
          WHERE Id_user=$id)";
        command.Parameters.AddWithValue("$id", userId);

        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var book = new Book(title: (string)reader.GetValue(1));
            book.Id = new Guid((string)reader.GetValue(0));
            books.Add(book);
          }
        }
        connection.Close();
      }
      return books;
    }

    /// <summary>
    /// Получение книг связанных с пользователем.
    /// </summary>
    /// <param name="userId">Ид пользователя.</param>
    /// <returns>Список книг связанных с пользователем.</returns>
    public static List<Book> GetUserBooks(long userId)
    {
      var books = new List<Book>();
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT Books.Id, Books.Title, BooksUsers.Book_status FROM Books
          JOIN BooksUsers ON Books.Id = BooksUsers.Id_book
          WHERE Books.Id IN (SELECT Id_book From BooksUsers WHERE Id_user=$id)";
        command.Parameters.AddWithValue("$id", userId);

        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var book = new Book(title: (string)reader.GetValue(1));
            book.Id = new Guid((string)reader.GetValue(0));
            book.Mark = (Book.Status)Enum.GetValues(typeof(Book.Status)).GetValue((long)reader.GetValue(2));
            books.Add(book);
          }
        }
        connection.Close();
      }
      return books;
    }

    /// <summary>
    /// Сохранение данных о книге в бд.
    /// </summary>
    /// <param name="book">Книга.</param>
    public static void CreateBook(Book book, User user)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO Books (Id, Title, AddedBy) VALUES ($idBook, $name, $idUser)";
        command.Parameters.AddWithValue("$idBook", book.Id);
        command.Parameters.AddWithValue("$name", book.Title);
        command.Parameters.AddWithValue("$idUser", user.Id);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    /// <summary>
    /// Получение книги по Id.
    /// </summary>
    /// <param name="bookId">Id книги.</param>
    /// <returns>Найденная книга, в противном случае null.</returns>
    public static Book GetBook(string bookId)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT Books.Id, Books.Title, Books.Author, Books.Description, Books.AddedBy, Users.Name 
          FROM Books JOIN Users ON Books.AddedBy = Users.Id WHERE Books.Id=$id";
        command.Parameters.AddWithValue("$id", bookId.ToUpper());

        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var id = (string)reader.GetValue(0);
            var title = (string)reader.GetValue(1);
            var author = reader.GetValue(2) != DBNull.Value ? (string)reader.GetValue(2) : String.Empty;
            var description = reader.GetValue(3) != DBNull.Value ? (string)reader.GetValue(3) : String.Empty;
            var addedBy = reader.GetValue(4) != DBNull.Value ? new User((long)reader.GetValue(4), (string)reader.GetValue(5)) : null;
            var book = new Book(title: title, author: author, description: description, addedBy: addedBy);
            book.Id = new Guid(id);
            return book;
          }
        }
        connection.Close();
      }
      return null;
    }

    /// <summary>
    /// Обновление названия книги.
    /// </summary>
    /// <param name="bookId">Id книги.</param>
    /// <param name="bookTitle">Новое название книги.</param>
    public static void UpdateBookTitle(string bookId, string bookTitle)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE Books SET Title=$title WHERE Id=$id";
        command.Parameters.AddWithValue("$id", bookId);
        command.Parameters.AddWithValue("$title", bookTitle);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    /// <summary>
    /// Обновление автора книги.
    /// </summary>
    /// <param name="bookId">Id книги.</param>
    /// <param name="bookAuthor">Новый автор книги.</param>
    public static void UpdateBookAuthor(string bookId, string bookAuthor)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE Books SET Author=$author WHERE Id=$id";
        command.Parameters.AddWithValue("$id", bookId);
        command.Parameters.AddWithValue("$author", bookAuthor);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    /// <summary>
    /// Обновление описания книги.
    /// </summary>
    /// <param name="bookId">Id книги.</param>
    /// <param name="bookDescription">Новое описание книги.</param>
    public static void UpdateBookDescription(string bookId, string bookDescription)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE Books SET Description=$description WHERE Id=$id";
        command.Parameters.AddWithValue("$id", bookId);
        command.Parameters.AddWithValue("$description", bookDescription);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    /// <summary>
    /// Обновление файла книги.
    /// </summary>
    /// <param name="bookId">Id книги.</param>
    /// <param name="bookFile">Новый файл книги.</param>
    public static void UpdateBookFile(string bookId, byte[] bookFile)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE Books SET File=$file WHERE Id=$id";
        command.Parameters.AddWithValue("$id", bookId);
        command.Parameters.AddWithValue("$file", bookFile);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    /// <summary>
    /// Обновления состояния пользователя.
    /// </summary>
    /// <param name="userId">Ид пользователя.</param>
    /// <param name="mark">Новое состояние.</param>
    public static void UpdateBookMark(string bookId, long userId, Book.Status mark)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE BooksUsers SET Book_status=$mark WHERE Id_book=$idBook AND Id_user=$idUser";
        command.Parameters.AddWithValue("$idBook", bookId);
        command.Parameters.AddWithValue("$idUser", userId);
        command.Parameters.AddWithValue("$mark", (long)mark);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }
  }
}
