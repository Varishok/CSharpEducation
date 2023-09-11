using Microsoft.EntityFrameworkCore;

namespace Task4
{
  internal class DBEntity : IEntity
  {
    public Guid Id { get; }

    public DBEntity()
    { 
      this.Id = Guid.NewGuid();
    }
  }
}
