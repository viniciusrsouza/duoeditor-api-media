using DuoEditor.Media.Service.Models;

namespace DuoEditor.Media.Service.Cache
{
  public interface ICacheRepository
  {
    Task<UserModel?> GetUser(string token);
    Task<UserModel?> SetUser(string token, UserModel user, long expiration);
  }
}