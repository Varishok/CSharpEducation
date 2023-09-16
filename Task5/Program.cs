using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Task5
{
  internal class Program
  {
    /// <summary>
    /// Асинхронная загрузка файлов.
    /// </summary>
    /// <returns></returns>
    static async Task Main()
    {
      Console.Write("Введите uri:");
      string uri = Console.ReadLine();

      Console.Write("Введите путь к файлу сохранения:");
      string saveFilePath = Console.ReadLine();

      using (var client = new HttpClient())
      {
        await client.DownloadFileTaskAsync(uri, saveFilePath);
      }
    }
  }
}