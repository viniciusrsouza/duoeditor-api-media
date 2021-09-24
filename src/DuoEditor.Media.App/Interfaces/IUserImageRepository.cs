using DuoEditor.Media.Domain.Entities;

namespace DuoEditor.Media.App.Interfaces
{
  public interface IUserImageRepository
  {
    Task<UserImage?> Set(string fileName, Stream fileStream, int userId);
    Task<bool> Remove(int userId);
    Task<UserImage?> Get(int userId);
  }
}