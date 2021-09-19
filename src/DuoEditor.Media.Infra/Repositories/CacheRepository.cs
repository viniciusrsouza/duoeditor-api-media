using System.Text.Json;
using DuoEditor.Media.Service.Cache;
using DuoEditor.Media.Service.Models;
using ServiceStack.Redis;

namespace DuoEditor.Media.Infra.Repositories
{
  public class CacheRepository : ICacheRepository
  {
    private readonly IRedisClientsManagerAsync _manager;

    public CacheRepository(IRedisClientsManagerAsync manager)
    {
      _manager = manager;
    }
    public async Task<UserModel?> GetUser(string token)
    {
      await using var redis = await _manager.GetClientAsync();
      try
      {
        var serializedUser = await redis.GetAsync<string>(token);
        var user = JsonSerializer.Deserialize<UserModel>(serializedUser);
        return user;
      }
      catch
      {
        return null;
      }
    }

    public async Task<UserModel?> SetUser(string token, UserModel user, long expiration)
    {
      var expirationDate = DateTimeOffset.UnixEpoch.AddSeconds(expiration).DateTime;
      var serializedUser = JsonSerializer.Serialize(user);
      await using var redis = await _manager.GetClientAsync();
      await redis.AddAsync(token, serializedUser, expirationDate);
      return user;
    }
  }
}