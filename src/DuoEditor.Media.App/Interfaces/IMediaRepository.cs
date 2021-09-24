using DuoEditor.Media.Domain.Entities;

namespace DuoEditor.Media.App.Interfaces
{
  public interface IMediaRepository
  {
    Task<string?> Set(string fileName, Stream fileStream);
    Task<bool> Remove(string fileName);
  }
}