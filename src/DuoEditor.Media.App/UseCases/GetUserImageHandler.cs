using DuoEditor.Media.App.Interfaces;
using DuoEditor.Media.Domain.Entities;
using MediatR;

namespace DuoEditor.Media.App.UseCases
{
  public class GetUserImageHandler : IRequestHandler<GetUserImage, UserImage?>
  {
    IUserImageRepository _repository;
    public GetUserImageHandler(IUserImageRepository repository)
    {
      _repository = repository;
    }

    public async Task<UserImage?> Handle(GetUserImage argument, CancellationToken cancellationToken)
    {
      try
      {
        return await _repository.Get(argument.UserId);
      }
      catch
      {
        return null;
      }
    }
  }
}