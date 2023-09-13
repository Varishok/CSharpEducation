using Task4.Entities;
using Task4.Repositories;

namespace Task4
{
  internal class Program
  {
    static void Main(string[] args)
    {
      MemoryEntity entity1 = new MemoryEntity();
      MemoryEntity entity2 = new MemoryEntity(2);
      MemoryEntity entity3 = new MemoryEntity(3);
      MemoryEntity entity4 = new MemoryEntity(6);

      List<MemoryEntity> memoryEntities = new List<MemoryEntity>();
      memoryEntities.Add(entity1);
      memoryEntities.Add(entity2);

      MemoryRepository<MemoryEntity> memoryRepository = new MemoryRepository<MemoryEntity>(memoryEntities);

      memoryRepository.CreateEntity(entity3);

      foreach (MemoryEntity entity in memoryRepository.GetAll()) 
      {
        Console.WriteLine($"Id: {entity.Id}, Value: {entity.Value}");
      }
      Console.WriteLine();

      memoryRepository.DeleteEntity(1);
      foreach (MemoryEntity entity in memoryRepository.GetAll())
      {
        Console.WriteLine($"Id: {entity.Id}, Value: {entity.Value}");
      }
      Console.WriteLine();

      entity1.Value = 5;
      memoryRepository.UpdateEntity(entity1);
      foreach (MemoryEntity entity in memoryRepository.GetAll())
      {
        Console.WriteLine($"Id: {entity.Id}, Value: {entity.Value}");
      }
      Console.WriteLine();

      entity4 = memoryRepository.GetEntity(1);
      Console.WriteLine($"Id: {entity4.Id}, Value: {entity4.Value}");

      string directory = "E:\\Education\\Test";
      string pathToFile1 = "E:\\Education\\Test\\test1.txt";
      string pathToFile2 = "E:\\Education\\Test\\test2.txt";
      FileInfo fileInfo = new FileInfo(pathToFile1);
      FileEntity file1 = new FileEntity(fileInfo);
      fileInfo = new FileInfo(pathToFile2);
      FileEntity file2 = new FileEntity(fileInfo);

      FileRepository<FileEntity> fileRepository = new FileRepository<FileEntity>(directory);
      
      FileEntity file3 = fileRepository.GetEntity(0);
      file3.FileInfo = fileInfo;

      fileRepository.UpdateEntity(file3);
    }
  }
}