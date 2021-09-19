using DuoEditor.Media.Domain.Entities;
using MediatR;

namespace DuoEditor.Media.App.UseCases
{
  public class DeleteUserImage : IRequest<bool>
  {
    public int UserId { get; set; }

    public DeleteUserImage(int userId)
    {
      UserId = userId;
    }
  }
}