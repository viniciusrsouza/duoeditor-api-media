using DuoEditor.Media.Domain.Entities;
using MediatR;

namespace DuoEditor.Media.App.UseCases
{
  public class SetUserImage : IRequest<UserImage?>
  {
    public string FileName { get; set; }
    public Stream FileStream { get; set; }
    public int UserId { get; set; }

    public SetUserImage(string fileName, Stream fileStream, int userId)
    {
      FileName = fileName;
      FileStream = fileStream;
      UserId = userId;
    }
  }
}