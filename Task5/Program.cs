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
    /// <returns>Скачанные файлы, если таковые есть.</returns>
    static async Task Main()
    {
      string uriToFile;
      string saveFilePath;
      char downloadMore = 'y';

      using (var client = new HttpClient())
      {
        while (downloadMore.Equals('y'))
        {
          Console.Write("Введите uri:");
          uriToFile = Console.ReadLine();

          Console.Write("Введите путь к файлу сохранения:");
          saveFilePath = Console.ReadLine();

          await client.DownloadFileTaskAsync(uriToFile, saveFilePath);

          Console.Write("Скачать еще один файл? (y - да, другой символ - выход):");
          downloadMore = Console.ReadKey().KeyChar;

          Console.WriteLine("\n");
        }

        Task.WaitAll();
      }
    }
  }
}