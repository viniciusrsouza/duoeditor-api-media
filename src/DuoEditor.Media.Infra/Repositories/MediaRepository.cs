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

    public async Task<bool> Set(string fileName, Stream fileStream)
    {
      try
      {
        await _userImagesClient.DeleteBlobIfExistsAsync(fileName);
        await _userImagesClient.UploadBlobAsync(fileName, fileStream);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}