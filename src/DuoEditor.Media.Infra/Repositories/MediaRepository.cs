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
    private readonly MediaConfig _config;
    public MediaRepository(IOptions<MediaConfig> options)
    {
      _config = options.Value;
      var connectionString = Environment.GetEnvironmentVariable(_config.ConnectionString);
      _client = new BlobServiceClient(connectionString);
      _userImagesClient = _client.GetBlobContainerClient(_config.ProfileImageContainer);
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

    public async Task<string?> Set(string fileName, Stream fileStream)
    {
      try
      {
        await _userImagesClient.DeleteBlobIfExistsAsync(fileName);
        await _userImagesClient.UploadBlobAsync(fileName, fileStream);

        return $"{_config.StorageUri}{_config.ProfileImageContainer}/{fileName}";
      }
      catch
      {
        return null;
      }
    }
  }
}