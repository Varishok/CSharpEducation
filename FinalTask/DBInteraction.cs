using Microsoft.Data.Sqlite;

namespace FinalTask
{
  /// <summary>
  /// Взаимодействие с базой данных.
  /// </summary>
  internal static class DBInteraction
  {
    const string Config = "Data Source=library.db";

    /// <summary>
    /// Существование пользователя в базе.
    /// </summary>
    /// <param name="userId">Ид пользователя.</param>
    /// <returns>True - пользователь найден, иначе - false.</returns>
    public static User FindUser(long userId)
    {
      using(var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT Id, Имя, Id_состояния From 'Пользователи' WHERE Id = $id";
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
        command.CommandText = @"INSERT INTO 'Пользователи' ('Id') VALUES ($id)";
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
        command.CommandText = @"UPDATE 'Пользователи' SET Id_состояния=$mark WHERE Id=$id";
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
    /// <param name="userName">Новое имя.</param>
    public static void UpdateUserName(long userId, string userName)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE 'Пользователи' SET Имя=$name WHERE Id=$id";
        command.Parameters.AddWithValue("$id", userId);
        command.Parameters.AddWithValue("$name", userName);

        var reader = command.ExecuteNonQuery();
        connection.Close();
      }
    }

    public static void AddBookToUser(long userId, string bookId)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO 'КнигиПользователи' ('Id_книги', 'Id_пользователя') VALUES ($idBook, $idUser)";
        command.Parameters.AddWithValue("$idBook", bookId);
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
    public static List<Book> GetAllBookNames(long userId)
    {
      var books = new List<Book>();
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT Id, Название FROM 'Книги' 
          WHERE Id NOT IN (SELECT Id_книги From 'КнигиПользователи' 
          WHERE Id_пользователя=$id)";
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
    /// Сохранение данных о книге в бд.
    /// </summary>
    /// <param name="book">Книга.</param>
    public static void CreateBook(Book book)
    {
      using (var connection = new SqliteConnection(Config))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO 'Книги' ('Id', 'Название') VALUES ($idBook, $name)";
        command.Parameters.AddWithValue("$idBook", book.Id);
        command.Parameters.AddWithValue("$name", book.Title);

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
        command.CommandText = @"SELECT Id, Название, Автор, Описание FROM 'Книги' WHERE Id=$id";
        command.Parameters.AddWithValue("$id", bookId.ToUpper());

        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var id = (string)reader.GetValue(0);
            var title = (string)reader.GetValue(1);
            var author = reader.GetValue(2) != DBNull.Value ? (string)reader.GetValue(2) : String.Empty;
            var description = reader.GetValue(3) != DBNull.Value ? (string)reader.GetValue(3) : String.Empty;
            var book = new Book(title: title, author: author, description: description);
            book.Id = new Guid(id);
            return book;
          }
        }
        connection.Close();
      }
      return null;
    }
  }
}
