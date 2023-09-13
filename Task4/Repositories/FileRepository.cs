using Task4.Entities;

namespace Task4.Repositories
{
  /// <summary>
  /// Репозиторий файлов.
  /// </summary>
  /// <typeparam name="T">Файловые сущности.</typeparam>
  internal class FileRepository<T> : IRepository<T> where T : FileEntity
  {
    #region Поля и свойства

    /// <summary>
    /// Список файлов репозитория.
    /// </summary>
    public List<T> Files { get; set; }

    #endregion

    #region IRepository

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
      T updatingEntity = Files.Where(x => x.Id == entity.Id);
      File.Move(updatingEntity.FileInfo.FullName, entity.FileInfo.FullName);
      updatingEntity = entity;
    }

    public void DeleteEntity(int id)
    {
      T entity = this.Files[id];

      File.Delete(entity.FileInfo.FullName);
      this.Files.Remove(entity);
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="path">Путь к папке.</param>
    public FileRepository(string path) 
    {
      var directory = new DirectoryInfo(path);
      this.Files = new List<T>();

      foreach(FileInfo file in directory.GetFiles())
      {
        T entity = (T)new FileEntity(file);
        this.Files.Add(entity);
      }

      #endregion
    }
  }
}
