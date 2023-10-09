﻿using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

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
    public static async void MainMenu(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
      InlineKeyboardMarkup inlineKeyboard = new(new[]
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

      await botClient.SendTextMessageAsync(
        message.Chat,
        "Привет-привет!! Чего ты хочешь?",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Добавление нового пользователя - начало.
    /// </summary>
    /// <param name="botClient">Клиент телеграмм бота.</param>
    /// <param name="message">Сообщение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="users">Список пользователей.</param>
    public static async void NewUserCreateStart(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
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
    /// <param name="currentUser">Текущий пользователь.</param>
    public static async void NewUserCreateEnd(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User newUser)
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
    public static async void UpdateUserNameStart(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
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
    public static async void UpdateUserNameEnd(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
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
    /// <param name="books">Список книг.</param>
    public static async void Library(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
    {
      var books = DBInteraction.GetAllBookNames(currentUser.Id);
      var list = new List<List<InlineKeyboardButton>>();
      var lines = string.Empty;
      for (int i = 0; i < books.Count; i++)
      {
        lines += $"{i+1}. {books[i].Title} \n";
        list.Add(new List<InlineKeyboardButton>(new[]
          {
            InlineKeyboardButton.WithCallbackData(text: $"{i+1}", callbackData: $"/libraryBook {books[i].Id}")
          }));
      }

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
    public static async void CreateNewBookStart(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
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
    /// <param name="books">Библиотека книжек.</param>
    public static async void CreateNewBookEnd(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken, User currentUser)
    {
      DBInteraction.CreateBook(currentUser.Id, new Book(message.Text));
      DBInteraction.UpdateUserMark(currentUser.Id, User.Status.OnStart);
      await botClient.SendTextMessageAsync(
        message.Chat,
        $"Книга с названием '{message.Text}' создана.",
        cancellationToken: cancellationToken);
    }
  }
}
