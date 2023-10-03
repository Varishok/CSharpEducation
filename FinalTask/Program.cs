using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Telegram.Bot.Types.ReplyMarkups;

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

          if (message.Text.ToLower() == "/start")
          {
            inlineKeyboard = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Да", callbackData: "/newUserCreate"),
                    InlineKeyboardButton.WithCallbackData(text: "Нет", callbackData: "/userUpdateName"),
                },
            });
            users.Add(new User(message.Chat.Id));
            await botClient.SendTextMessageAsync(
              message.Chat,
              $"Добро пожаловать в библиотеку! Вас зовут {message.Chat.Username}?",
              replyMarkup: inlineKeyboard,
              cancellationToken: cancellationToken);
            return;
          }
        }

        if(currentUser.Mark == User.Status.OnCreatedName) // Изменение имени пользователя - конец
        {
          currentUser.Name = message.Text;
          await botClient.SendTextMessageAsync(
            message.Chat,
            $"Пользователь - {currentUser.Name}, добавлен в библиотеку.",
            cancellationToken: cancellationToken);
          currentUser.Mark = User.Status.OnStart;
          return;
        }

        // Меню пользователя.
        inlineKeyboard = new(new[]
        {
          new[]
          {
            InlineKeyboardButton.WithCallbackData(text: "Общая библиотека", callbackData: "/library"),
            InlineKeyboardButton.WithCallbackData(text: "Моя библиотека", callbackData: "/myLibrary"),
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
        await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!! Чего ты хочешь?"); 
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
            currentUser.Name = message.Chat.Username;
            currentUser.Mark = User.Status.OnStart;
            await botClient.SendTextMessageAsync(
              message.Chat,
              $"Пользователь - {currentUser.Name}, добавлен в библиотеку.",
              cancellationToken: cancellationToken);
            return;
          }
        }

        if(codeOfButton == "/userUpdateName") // Изменение имени пользователя - начало
        {
          if (currentUser != null)
          {
            currentUser.Mark = User.Status.OnCreatedName;
            await botClient.SendTextMessageAsync(
              message.Chat,
              $"Введите ваше имя.",
              cancellationToken: cancellationToken);
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