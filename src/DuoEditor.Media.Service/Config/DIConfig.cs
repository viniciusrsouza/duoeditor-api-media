using DuoEditor.Media.Service.Auth;
using DuoEditor.Media.Service.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace DuoEditor.Media.Service.Config
{
  public static class DIConfig
  {
    public static IServiceCollection AddAuth<T>(this IServiceCollection services)
      where T : class, ICacheRepository
    {
      services.AddScoped<ICacheRepository, T>();
      services.AddAuthentication("Jwt").AddScheme<AuthOptions, AuthHandler>("Jwt", opt => { });

      return services;
    }
  }
}