using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DuoEditor.Media.App.Config
{
  public static class DIConfig
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddMediatR(Assembly.GetExecutingAssembly());

      return services;
    }
  }
}