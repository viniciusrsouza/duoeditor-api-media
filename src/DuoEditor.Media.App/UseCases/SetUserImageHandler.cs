using DuoEditor.Media.App.Interfaces;
using DuoEditor.Media.Domain.Entities;
using MediatR;

namespace DuoEditor.Media.App.UseCases
{
  public class SetUserImageHandler : IRequestHandler<SetUserImage, UserImage?>
  {
    IUserImageRepository _repository;
    public SetUserImageHandler(IUserImageRepository repository)
    {
      _repository = repository;
    }

    public async Task<UserImage?> Handle(SetUserImage argument, CancellationToken cancellationToken)
    {
      return await _repository.Set(argument.FileName, argument.FileStream, argument.UserId);
    }
  }
}