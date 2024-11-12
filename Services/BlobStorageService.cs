using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace WebApplication1.Services;

public class BlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(string connectionString)
    {
        _blobServiceClient = new BlobServiceClient(connectionString);
    }
    // The UploadFileAsync method uploads a file to the specified container
    public async Task UploadFileAsync(string containerName, string fileName, Stream content)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(content, true);
    }
    // The DownloadFileAsync method downloads a file from the specified container
    public async Task<Stream> DownloadFileAsync(string containerName, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        var response = await blobClient.DownloadAsync();
        return response.Value.Content;
    }
    // The ListBlobsAsync method lists all the blobs in the specified container
    public async Task<List<string>> ListBlobsAsync(string containerName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobs = new List<string>();

        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
        {
            blobs.Add(blobItem.Name);
        }

        return blobs;
    }
    // The DeleteFileAsync method deletes a file from the specified container
    public async Task DeleteFileAsync(string containerName, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.DeleteIfExistsAsync();
    }
}