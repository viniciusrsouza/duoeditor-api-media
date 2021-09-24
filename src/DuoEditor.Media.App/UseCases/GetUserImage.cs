using DuoEditor.Media.Domain.Entities;
using MediatR;

namespace DuoEditor.Media.App.UseCases
{
  public class GetUserImage : IRequest<UserImage?>
  {
    public int UserId { get; set; }

    public GetUserImage(int userId)
    {
      UserId = userId;
    }
  }
}