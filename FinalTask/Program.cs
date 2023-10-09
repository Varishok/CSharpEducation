using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinalTask
{
  internal class Program
  {
    static ITelegramBotClient bot;
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
      // Некоторые действия
      Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
      if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
      {
        InlineKeyboardMarkup inlineKeyboard;
        var message = update.Message;
        var currentUser = DBInteraction.FindUser(message.Chat.Id);

        if (currentUser == null) // Новый пользователь
        {
          TelegramBotInteraction.NewUserCreateStart(botClient, message, cancellationToken);
          return;
        }

        if(currentUser.Mark == User.Status.OnCreatedName) // Изменение имени пользователя - конец
        {
          TelegramBotInteraction.UpdateUserNameEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        if (currentUser.Mark == User.Status.OnCreateBookTitle) // Создание книги - конец
        {
          TelegramBotInteraction.CreateNewBookEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        TelegramBotInteraction.MainMenu(botClient, message, cancellationToken);
      }

      if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
      {
        var callbackData = update.CallbackQuery.Data.Split();
        var codeOfButton = callbackData[0];
        var message = update.CallbackQuery.Message;
        var currentUser = DBInteraction.FindUser(message.Chat.Id);
        
        if(currentUser == null) { return; }

        TelegramBotInteraction.DeleteMessage(botClient, message, cancellationToken);

        if(codeOfButton == "/mainMenu") // Возврат в главное меню
        {
          TelegramBotInteraction.MainMenu(botClient, message, cancellationToken);
          return;
        }

        if (codeOfButton == "/newUserCreate") // Создание пользователя с существующим именем.
        {
          TelegramBotInteraction.NewUserCreateEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        if(codeOfButton == "/userUpdateName") // Изменение имени пользователя - начало
        {
          TelegramBotInteraction.UpdateUserNameStart(botClient, message, cancellationToken, currentUser);
          return;
        }

        if (codeOfButton == "/library") // Вывод общей библиотеки
        {
          TelegramBotInteraction.Library(botClient, message, cancellationToken, currentUser);
          return;
        }

        if (codeOfButton == "/addBook") // Создание книги - начало
        {
          TelegramBotInteraction.CreateNewBookStart(botClient, message, cancellationToken, currentUser);
          return;
        }

        if(codeOfButton == "/libraryBook") // Вывод книги из общей библиотеки
        {
          var bookId = callbackData[1];
          TelegramBotInteraction.LibraryBook(botClient, message, cancellationToken, bookId);
          return;
        }

        if(codeOfButton == "/myLibraryAdd") // Добавление книжки пользователю
        {
          var bookId = callbackData[1];
          TelegramBotInteraction.LibraryAddBookToUser(botClient, message, cancellationToken, currentUser, bookId);
          return;
        }
      }
    }

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