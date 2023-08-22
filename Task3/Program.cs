/*
 * Должна быть реализована CRUD функциональность:
 *  Должен уметь принимать от пользователя номер и имя телефона.
 *  Сохранять номер в файле phonebook.txt. (При завершении программы либо при добавлении).
 *  Вычитывать из файла сохранённые номера. (При старте программы).
 *  Удалять номера.
 *  Получать абонента по номеру телефона.
 *  Получать номер телефона по имени абонента.
 * Обращение к Phonebook должно быть как к классу-одиночке.
 * Внутри должна быть коллекция с абонентами.
 * Для обращения с абонентами нужно завести класс Abonent. С полями «номер телефона», «имя».
 * Не дать заносить уже записанного абонента.
 */

namespace Task3
{
  internal class Program
  {
    static void Main()
    {
      var phoneBook = Phonebook.GetInstance();
      phoneBook.AddAbonent("Test", "123456");
      phoneBook.AddAbonent("Test2", "1234567");
      phoneBook.AddAbonent("Test3", "12345678");
      foreach (var abonent in phoneBook)
      {
        Console.WriteLine(abonent.ToString());
      }
      Console.WriteLine();
      phoneBook.RemoveAbonent("123456");
      foreach (var abonent in phoneBook)
      {
        Console.WriteLine(abonent.ToString());
      }
      Abonent varish = new Abonent("Varish", "123456");
      phoneBook[0] = varish;
    }
  }
}