using DuoEditor.Media.App.Interfaces;
using DuoEditor.Media.Domain.Entities;
using DuoEditor.Media.Infra.Persistence;

namespace DuoEditor.Media.Infra.Repositories
{
  public class UserImageRepository : IUserImageRepository
  {
    private readonly ApiDbContext _context;
    private readonly IMediaRepository _mediaRepository;

    public UserImageRepository(ApiDbContext context, IMediaRepository mediaRepository)
    {
      _context = context;
      _mediaRepository = mediaRepository;
    }

    public async Task<bool> Remove(int userId)
    {
      try
      {
        var entry = await _context.UserImages.FindAsync(userId);
        await _mediaRepository.Remove(entry.FileName);
        _context.UserImages.Remove(entry);
        await _context.SaveChangesAsync();
        return true;
      }
      catch
      {
        return false;
      }
    }

    public async Task<UserImage?> Set(string fileName, Stream fileStream, int userId)
    {
      var entry =
        await _context.UserImages.FindAsync(userId) ??
        (await _context.UserImages.AddAsync(new UserImage(userId, fileName))).Entity;
      if (entry != null)
      {
        var added = await _mediaRepository.Set(fileName, fileStream);
        if (added)
        {
          await _context.SaveChangesAsync();
          return entry;
        }
      }

      return null;
    }
  }
}