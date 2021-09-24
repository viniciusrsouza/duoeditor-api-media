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
      var extension = argument.FileName.Split(".").LastOrDefault("");
      var fileNameArray = new string[] { $"profile-picture-{argument.UserId}", extension };
      var fileName = String.Join('.', fileNameArray);

      return await _repository.Set(fileName, argument.FileStream, argument.UserId);
    }
  }
}