using Azure.Storage.Blobs;
using DuoEditor.Media.App.Interfaces;
using DuoEditor.Media.Infra.Config;
using Microsoft.Extensions.Options;

namespace DuoEditor.Media.Infra.Repositories
{
  public class MediaRepository : IMediaRepository
  {
    private readonly BlobServiceClient _client;
    private readonly BlobContainerClient _userImagesClient;
    public MediaRepository(IOptions<MediaConfig> options)
    {
      var config = options.Value;
      var connectionString = Environment.GetEnvironmentVariable(config.ConnectionString);
      _client = new BlobServiceClient(connectionString);
      _userImagesClient = _client.GetBlobContainerClient(config.ProfileImageContainer);
    }

    public async Task<bool> Remove(string fileName)
    {
      try
      {
        await _userImagesClient.DeleteBlobIfExistsAsync(fileName);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public Task<bool> Set(string fileName, Stream fileStream)
    {
      try
      {
        Task.Run(async () =>
        {
          await _userImagesClient.DeleteBlobIfExistsAsync(fileName);
          await _userImagesClient.UploadBlobAsync(fileName, fileStream);
        });
        return Task.FromResult(true);
      }
      catch
      {
        return Task.FromResult(false);
      }
    }
  }
}