using DuoEditor.Media.App.Interfaces;
using DuoEditor.Media.Domain.Entities;
using MediatR;

namespace DuoEditor.Media.App.UseCases
{
  public class DeleteUserImageHandler : IRequestHandler<DeleteUserImage, bool>
  {
    IUserImageRepository _repository;
    public DeleteUserImageHandler(IUserImageRepository repository)
    {
      _repository = repository;
    }

    public async Task<bool> Handle(DeleteUserImage argument, CancellationToken cancellationToken)
    {
      try
      {
        return await _repository.Remove(argument.UserId);
      }
      catch
      {
        return false;
      }
    }
  }
}