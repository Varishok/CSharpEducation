using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Configuration;

namespace FinalTask
{
  internal class Program
  {
    /// <summary>
    /// Телеграмм бот.
    /// </summary>
    static ITelegramBotClient bot;

    /// <summary>
    /// Обработчик обновлений телеграмм бота.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="update">Обновления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
      // Некоторые действия
      Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
      if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
      {
        var message = update.Message;
        var currentUser = DBInteraction.FindUser(message.Chat.Id);

        if (currentUser == null) // Новый пользователь
        {
          await TelegramBotInteraction.NewUserCreateStart(botClient, message, cancellationToken);
          return;
        }

        if(currentUser.Mark == User.Status.OnCreatedName) // Изменение имени пользователя - конец
        {
          await TelegramBotInteraction.UpdateUserNameEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        if (currentUser.Mark == User.Status.OnCreateBookTitle) // Создание книги - конец
        {
          await TelegramBotInteraction.CreateNewBookEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        if(currentUser.Mark == User.Status.OnUpdateBookTitle) // Изменение названия книги - конец
        {
          await TelegramBotInteraction.UpdateBookTitleEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        if (currentUser.Mark == User.Status.OnUpdateBookAuthor) // Изменение автора книги - конец
        {
          await TelegramBotInteraction.UpdateBookAuthorEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        if (currentUser.Mark == User.Status.OnUpdateBookDescription) // Изменение описания книги - конец
        {
          await TelegramBotInteraction.UpdateBookDescriptionEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        if (currentUser.Mark == User.Status.OnUpdateBookFile) // Изменение файла книги - конец
        {
          await TelegramBotInteraction.UpdateBookFileEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        await TelegramBotInteraction.MainMenu(botClient, message, cancellationToken);
      }

      if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
      {
        var callbackData = update.CallbackQuery.Data.Split();
        var codeOfButton = callbackData[0];
        var message = update.CallbackQuery.Message;
        var currentUser = DBInteraction.FindUser(message.Chat.Id);
        
        if(currentUser == null) { return; }

        await TelegramBotInteraction.DeleteMessage(botClient, message, cancellationToken);

        if(codeOfButton == "/mainMenu") // Возврат в главное меню
        {
          await TelegramBotInteraction.MainMenu(botClient, message, cancellationToken);
          return;
        }

        if(codeOfButton == "/newUserCreate") // Создание пользователя с существующим именем.
        {
          await TelegramBotInteraction.NewUserCreateEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        if(codeOfButton == "/userUpdateName") // Изменение имени пользователя - начало
        {
          await TelegramBotInteraction.UpdateUserNameStart(botClient, message, cancellationToken, currentUser);
          return;
        }

        if(codeOfButton == "/library") // Вывод общей библиотеки
        {
          await TelegramBotInteraction.Library(botClient, message, cancellationToken, currentUser);
          return;
        }

        if(codeOfButton == "/libraryBook") // Вывод книги из общей библиотеки
        {
          var bookId = callbackData[1];
          await TelegramBotInteraction.LibraryBook(botClient, message, cancellationToken, bookId);
          return;
        }

        if(codeOfButton == "/addBook") // Создание книги - начало
        {
          await TelegramBotInteraction.CreateNewBookStart(botClient, message, cancellationToken, currentUser);
          return;
        }

        if(codeOfButton == "/myLibrary") // Вывод библиотеки пользователя с фильтром или без
        {
          var filter = Convert.ToInt32(callbackData[1]);
          await TelegramBotInteraction.MyLibrary(botClient, message, cancellationToken, currentUser, filter);
          return;
        }

        if(codeOfButton == "/myLibraryBook") // Вывод книги из пользовательской библиотеки
        {
          var bookId = callbackData[1];
          await TelegramBotInteraction.MyLibraryBook(botClient, message, cancellationToken, currentUser, bookId);
          return;
        }

        if(codeOfButton == "/checkFile") // Вывод файла книги из пользовательской библиотеки
        {
          var bookId = callbackData[1];
          await TelegramBotInteraction.MyLibraryBookFile(botClient, message, cancellationToken, currentUser, bookId);
          return;
        }

        if(codeOfButton == "/myLibraryAdd") // Добавление книжки пользователю
        {
          var bookId = callbackData[1];
          await TelegramBotInteraction.LibraryAddBookToUser(botClient, message, cancellationToken, currentUser, bookId);
          return;
        }

        if(codeOfButton == "/bookChangeTitle") // Изменение названия книги
        {
          var bookId = callbackData[1];
          await TelegramBotInteraction.UpdateBookTitleStart(botClient, message, cancellationToken, currentUser, bookId);
          return;
        }

        if(codeOfButton == "/bookChangeAuthor") // Изменение автора книги
        {
          var bookId = callbackData[1];
          await TelegramBotInteraction.UpdateBookAuthorStart(botClient, message, cancellationToken, currentUser, bookId);
          return;
        }

        if(codeOfButton == "/bookChangeDescription") // Изменение описания книги
        {
          var bookId = callbackData[1];
          await TelegramBotInteraction.UpdateBookDescriptionStart(botClient, message, cancellationToken, currentUser, bookId);
          return;
        }

        if(codeOfButton == "/bookChangeFile") // Изменение файла книги
        {
          var bookId = callbackData[1];
          await TelegramBotInteraction.UpdateBookFileStart(botClient, message, cancellationToken, currentUser, bookId);
          return;
        }

        if(codeOfButton == "/bookChangeMark") // Изменение статуса книги
        {
          var bookId = callbackData[1];
          var mark = (Book.Status)Enum.GetValues(typeof(Book.Status)).GetValue(Convert.ToInt32(callbackData[2]));
          await TelegramBotInteraction.UpdateBookMark(botClient, message, cancellationToken, currentUser, bookId, mark);
        }
      }
    }

    /// <summary>
    /// Обработчик исключений телеграмм бота.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="exception">Исключение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
      // Вывод ошибки в консоль
      Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }


    static void Main(string[] args)
    {
      var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build(); 
      bot = new TelegramBotClient(config["token"]);
      Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

      var cts = new CancellationTokenSource();
      var cancellationToken = cts.Token;
      var receiverOptions = new ReceiverOptions
      {
        AllowedUpdates = { }, // receive all update types
      };
      bot.StartReceiving(
        HandleUpdateAsync,
        HandleErrorAsync,
        receiverOptions,
        cancellationToken
      );
      
      Console.ReadLine();
    }
  }
}