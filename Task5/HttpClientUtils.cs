using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
  /// <summary>
  /// Расширение класса HttpClient.
  /// </summary>
  public static class HttpClientUtils
  {
    /// <summary>
    /// Асинхронное скачивание файла.
    /// </summary>
    /// <param name="client">Ссылка на экземпляр HttpClient.</param>
    /// <param name="uri">Ссылка на файл для скачивания.</param>
    /// <param name="saveFilePath">Путь сохранения файла.</param>
    /// <returns></returns>
    public static async Task DownloadFileTaskAsync(this HttpClient client, string uri, string saveFilePath)
    {
      using (var stream = await client.GetStreamAsync(uri))
      {
        using (var saveFile = new FileStream(saveFilePath, FileMode.Create))
        {
          await stream.CopyToAsync(saveFile);
        }
      }
    }
  }
}
