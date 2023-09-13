using System.IO;

namespace Task4
{
  /// <summary>
  /// Репозиторий файлов.
  /// </summary>
  /// <typeparam name="T">Файловые сущности.</typeparam>
  internal class FileRepository<T> : IRepository<T> where T : FileEntity
  {
    /// <summary>
    /// Список файлов репозитория.
    /// </summary>
    public List<T> Files { get; set; }

    public void CreateEntity(T entity)
    {
      using (FileStream fs = File.Create(entity.FileInfo.FullName))
      {
        this.Files.Add(entity);
      }
    }

    public IEnumerable<T> GetAll()
    {
      return Files;
    }

    public T GetEntity(int id)
    {
      return this.Files[id];
    }

    public void UpdateEntity(T entity)
    {
      T updatingEntity = Files.Where(x => x.Id == entity.Id).FirstOrDefault();
      updatingEntity = entity;
    }

    public void DeleteEntity(int id)
    {
      T entity = this.Files[id];
      Files.Remove(entity);
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="path">Путь к папке.</param>
    public FileRepository(string path) 
    {
      var directory = new DirectoryInfo(path);
      Files = new List<T>();

      foreach(FileInfo file in directory.GetFiles())
      {
        T entity = (T)new FileEntity(file);
        Files.Add(entity);
      }
    }
  }
}
