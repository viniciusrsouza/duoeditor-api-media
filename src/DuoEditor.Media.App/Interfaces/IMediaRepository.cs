using DuoEditor.Media.Domain.Entities;

namespace DuoEditor.Media.App.Interfaces
{
  public interface IMediaRepository
  {
    Task<bool> Set(string fileName, Stream fileStream);
    Task<bool> Remove(string fileName);
  }
}