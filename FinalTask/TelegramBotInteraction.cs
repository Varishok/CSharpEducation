using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Reflection.PortableExecutable;

namespace FinalTask
{
  /// <summary>
  /// Взаимодействие с телеграмм ботом.
  /// </summary>
  internal static class TelegramBotInteraction
  {
    /// <summary>
    /// Пользовательское меню.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public static async Task MainMenu(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
      InlineKeyboardMarkup inlineKeyboard = new(new[]
      {
        new[]
        {
          InlineKeyboardButton.WithCallbackData(text: "Общая библиотека", callbackData: "/library"),
          InlineKeyboardButton.WithCallbackData(text: "Моя библиотека", callbackData: "/myLibrary 0"),
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(text: "Добавить книгу", callbackData: "/addBook"),
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(text: "Изменить имя", callbackData: "/userUpdateName"),
        },
      });

      await botClient.SendTextMessageAsync(
        message.Chat,
        "Привет-привет!! Чего ты хочешь?",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Удаление текущего сообщения.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public static async Task DeleteMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
      await botClient.DeleteMessageAsync(
        chatId: message.Chat.Id, 
        messageId: message.MessageId, 
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Добавление нового пользователя - начало.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public static async Task NewUserCreateStart(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
      InlineKeyboardMarkup inlineKeyboard = new(new[]
        {
          new[]
          {
            InlineKeyboardButton.WithCallbackData(text: "Да", callbackData: "/newUserCreate"),
            InlineKeyboardButton.WithCallbackData(text: "Нет", callbackData: "/userUpdateName"),
          },
      });

      DBInteraction.CreateUser(message.Chat.Id);
      await botClient.SendTextMessageAsync(
        message.Chat,
        $"Добро пожаловать в библиотеку! Вас зовут {message.Chat.Username}?",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Добавление нового пользователя - конец.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="newUser">Новый пользователь.</param>
    public static async Task NewUserCreateEnd(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User newUser)
    {
      DBInteraction.UpdateUserName(newUser.Id, message.Chat.Username);
      DBInteraction.UpdateUserMark(newUser.Id, User.Status.OnStart);
      await botClient.SendTextMessageAsync(
        message.Chat,
        $"Пользователь - {message.Chat.Username}, добавлен в библиотеку.",
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Изменения имени пользователя - начало.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    public static async Task UpdateUserNameStart(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
    {
      DBInteraction.UpdateUserMark(currentUser.Id, User.Status.OnCreatedName);
      await botClient.SendTextMessageAsync(
        message.Chat,
        $"Введите ваше имя.",
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Изменения имени пользователя - конец.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    public static async Task UpdateUserNameEnd(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
    {
      DBInteraction.UpdateUserName(currentUser.Id, message.Text);
      DBInteraction.UpdateUserMark(currentUser.Id, User.Status.OnStart);
      await botClient.SendTextMessageAsync(
        message.Chat,
        $"Пользователь - {currentUser.Name}, добавлен в библиотеку.",
        cancellationToken: cancellationToken);
      currentUser.Mark = User.Status.OnStart;
    }

    /// <summary>
    /// Вывод общей библиотеки.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    public static async Task Library(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
    {
      var books = DBInteraction.GetAllBooks(currentUser.Id);
      var list = new List<List<InlineKeyboardButton>>();
      var lines = string.Empty;

      list.Add(new List<InlineKeyboardButton>(new[]
          {
            InlineKeyboardButton.WithCallbackData(text: "Главное меню", callbackData: $"/mainMenu"),
          }));
      if (books.Count > 0)
      {
        for (int i = 0; i < books.Count; i++)
        {
          lines += $"{i + 1}. {books[i].Title} \n";
          list.Add(new List<InlineKeyboardButton>(new[]
            {
            InlineKeyboardButton.WithCallbackData(text: $"{i+1}", callbackData: $"/libraryBook {books[i].Id}"),
          }));
        }
      }
      else
      {
        lines = "По заданному фильтру книги не найдены.";
      }

      await botClient.SendTextMessageAsync(
        message.Chat,
        lines,
        replyMarkup: new InlineKeyboardMarkup(list),
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Данные о книге из общей библиотеки.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="bookId">Ид книги.</param>
    public static async Task LibraryBook(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, string bookId)
    {
      var book = DBInteraction.GetBook(bookId);
      InlineKeyboardMarkup inlineKeyboard = new(new[]
      {
        new[]
        {
          InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "/library"),
          InlineKeyboardButton.WithCallbackData(text: "Добавить в мою библиотеку", callbackData: $"/myLibraryAdd {bookId}"),
        },
      });
      var lines = $"Название: '{book.Title}'\nАвтор: {book.Author}\nОписание: {book.Description}";

      await botClient.SendTextMessageAsync(
        message.Chat,
        lines,
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Добавление книги в библиотеку пользователя.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <param name="bookId">Ид книги.</param>
    public static async Task LibraryAddBookToUser(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser, string bookId)
    {
      DBInteraction.AddBookToUser(currentUser.Id, bookId);

      await botClient.SendTextMessageAsync(
        message.Chat,
        "Книжка добавлена в вашу библиотеку.",
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Просмотр библиотеки пользователя.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    public static async Task MyLibrary(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser, int mark = 0)
    {
      var books = DBInteraction.GetUserBooks(currentUser.Id);
      var list = new List<List<InlineKeyboardButton>>();
      var lines = string.Empty;
      if(mark > 0)
      {
        var filter = (Book.Status)Enum.GetValues(typeof(Book.Status)).GetValue(mark);
        books = books.Where(x => x.Mark == filter).ToList();
      }

      list.Add(new List<InlineKeyboardButton>(new[]
        {
          InlineKeyboardButton.WithCallbackData(text: "Главное меню", callbackData: $"/mainMenu"),
          InlineKeyboardButton.WithCallbackData(text: "Без фильтра", callbackData: $"/myLibrary 0"),
        }));
      list.Add(new List<InlineKeyboardButton>(new[]
        {
          InlineKeyboardButton.WithCallbackData(text: "Добавлены", callbackData: $"/myLibrary 1"),
          InlineKeyboardButton.WithCallbackData(text: "На чтении", callbackData: $"/myLibrary 2"),
          InlineKeyboardButton.WithCallbackData(text: "Прочитаны", callbackData: $"/myLibrary 3"),
        }));

      if (books.Count > 0)
      {
        for (int i = 0; i < books.Count; i++)
        {
          lines += $"{i + 1}. {books[i].Title} \n";
          list.Add(new List<InlineKeyboardButton>(new[]
            {
              InlineKeyboardButton.WithCallbackData(text: $"{i+1}", callbackData: $"/myLibraryBook {books[i].Id}")
            }));
        }
      }
      else
      {
        lines = "По заданному фильтру книги не найдены.";
      }
      

      await botClient.SendTextMessageAsync(
        message.Chat,
        lines,
        replyMarkup: new InlineKeyboardMarkup(list),
        cancellationToken: cancellationToken);
    }

    public static async Task MyLibraryBook(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser, string bookId)
    {
      var book = DBInteraction.GetBook(bookId);
      var list = new List<List<InlineKeyboardButton>>();
      list.Add(new List<InlineKeyboardButton>(new[]
        {
          InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "/library"),
          InlineKeyboardButton.WithCallbackData(text: "Предоставить файл", callbackData: $"/checkFile {bookId}"),
        }));
      if(currentUser.Id == book.AddedBy.Id)
      {
        list.Add(new List<InlineKeyboardButton>(new[]
          {
            InlineKeyboardButton.WithCallbackData(text: "Изменить название", callbackData: $"/bookChangeTitle {bookId}"),
            InlineKeyboardButton.WithCallbackData(text: "Изменить автора", callbackData: $"/bookChangeAuthor {bookId}"),
          }));
        list.Add(new List<InlineKeyboardButton>(new[]
          {
            InlineKeyboardButton.WithCallbackData(text: "Изменить описание", callbackData: $"/bookChangeDescription {bookId}"),
            InlineKeyboardButton.WithCallbackData(text: "Изменить файл", callbackData: $"/bookChangeFile {bookId}"),
          }));
      }
      list.Add(new List<InlineKeyboardButton>(new[]
        {
          InlineKeyboardButton.WithCallbackData(text: "Читаю", callbackData: $"/bookChangeMark {bookId} 2"),
          InlineKeyboardButton.WithCallbackData(text: "Прочитал", callbackData: $"/bookChangeMark {bookId} 3"),
        }));
      var lines = $"Название: '{book.Title}'\nАвтор: {book.Author}\nОписание: {book.Description}\nДобавлено:{book.AddedBy.Name}";

      await botClient.SendTextMessageAsync(
        message.Chat,
        lines,
        replyMarkup: new InlineKeyboardMarkup(list),
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Создание новой книги - начало.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    public static async Task CreateNewBookStart(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
    {
      DBInteraction.UpdateUserMark(currentUser.Id, User.Status.OnCreateBookTitle);
      await botClient.SendTextMessageAsync(
        message.Chat,
        $"Введите название книги.",
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Создание новой книги - конец.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="currentUser">Текущий пользователь.</param>
    public static async Task CreateNewBookEnd(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
    {
      var book = new Book(title: message.Text);
      DBInteraction.CreateBook(book, currentUser);
      DBInteraction.AddBookToUser(currentUser.Id, book.Id.ToString());
      DBInteraction.UpdateUserMark(currentUser.Id, User.Status.OnStart);
      await botClient.SendTextMessageAsync(
        message.Chat,
        $"Книга с названием '{message.Text}' создана.",
        cancellationToken: cancellationToken);
    }
  }
}
