using System.Net.Http.Json;
using DuoEditor.Media.Service.Config;
using DuoEditor.Media.Service.Models;
using Microsoft.Extensions.Options;

namespace DuoEditor.Media.Service.Clients
{
  public class AuthClient : BaseClient<AuthClientConfig>
  {
    public AuthClient(HttpClient client, IOptions<AuthClientConfig> options) : base(client, options)
    {
    }

    public async Task<IntrospectionResponse?> Introspection(IntrospectionRequest request)
    {
      try
      {
        var response = await _client.PostAsJsonAsync(_config.IntrospectionEndpoint, request);
        return await response.Content.ReadFromJsonAsync<IntrospectionResponse>();
      }
      catch
      {
        return null;
      }
    }
  }
}