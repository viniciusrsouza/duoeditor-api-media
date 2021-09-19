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
        return await redis.GetAsync<UserModel>(token);
      }
      catch
      {
        return null;
      }
    }

    public async Task<UserModel?> SetUser(string token, UserModel user, long expiration)
    {
      var expirationDate = DateTimeOffset.UnixEpoch.AddMilliseconds(expiration).DateTime;
      await using var redis = await _manager.GetClientAsync();
      await redis.AddAsync(token, user, expirationDate);
      return user;
    }
  }
}