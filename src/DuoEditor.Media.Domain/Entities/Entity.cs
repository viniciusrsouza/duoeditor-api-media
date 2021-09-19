namespace DuoEditor.Media.Domain.Entities
{
  public abstract class Entity
  {
    public int Id { get; set; }

    protected Entity(int id)
    {
      Id = id;
    }
  }
}