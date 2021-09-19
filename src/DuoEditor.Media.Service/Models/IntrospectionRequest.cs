namespace DuoEditor.Media.Service.Models
{
  public class IntrospectionRequest
  {
    public string Token { get; set; }

    public IntrospectionRequest(string token)
    {
      Token = token;
    }
  }
}