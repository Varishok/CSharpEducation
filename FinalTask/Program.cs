using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types.ReplyMarkups;
using System;

namespace FinalTask
{
  internal class Program
  {
    static ITelegramBotClient bot;
    static List<User> users = new List<User>();
    static List<Book> books = new List<Book>();
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
      // Некоторые действия
      Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
      if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
      {
        InlineKeyboardMarkup inlineKeyboard;
        var currentUser = users.Where(x => x.Id == update.Message.Chat.Id).FirstOrDefault();
        var message = update.Message;

        if (currentUser == null) // Новый пользователь
        {
          TelegramBotInteraction.NewUserCreateStart(botClient, message, cancellationToken, users);
          return;
        }

        if(currentUser.Mark == User.Status.OnCreatedName) // Изменение имени пользователя - конец
        {
          TelegramBotInteraction.UpdateUserNameEnd(botClient, message, cancellationToken, currentUser);
          return;
        }

        TelegramBotInteraction.MainMenu(botClient, message, cancellationToken);
      }

      if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
      {
        var currentUser = users.Where(x => x.Id == update.CallbackQuery.Message.Chat.Id).FirstOrDefault();
        var codeOfButton = update.CallbackQuery.Data;
        var message = update.CallbackQuery.Message;

        if (codeOfButton == "/newUserCreate") // Создание пользователя с существующим именем.
        {
          if (currentUser != null)
          {
            TelegramBotInteraction.NewUserCreateEnd(botClient, message, cancellationToken, currentUser);
            return;
          }
        }

        if(codeOfButton == "/userUpdateName") // Изменение имени пользователя - начало
        {
          if (currentUser != null)
          {
            TelegramBotInteraction.UpdateUserNameStart(botClient, message, cancellationToken, currentUser);
            return;
          }
        }
      }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
      // Некоторые действия
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