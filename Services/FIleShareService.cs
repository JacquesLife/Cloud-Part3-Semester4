using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;

namespace WebApplication1.Services
{
    public class FileShareService
    {
        private readonly ShareClient _shareClient;

        public FileShareService(string connectionString, string shareName)
        {
            _shareClient = new ShareClient(connectionString, shareName);
        }

        // The CreateDirectoryAsync method creates a directory in the specified share
        public async Task CreateDirectoryAsync(string directoryName)
        {
            var directoryClient = _shareClient.GetDirectoryClient(directoryName);
            await directoryClient.CreateIfNotExistsAsync();
        }

        // UploadFileAsync method uploads a file to the specified directory
        public async Task UploadFileAsync(string directoryName, string fileName, Stream content)
        {
            var directoryClient = _shareClient.GetDirectoryClient(directoryName);
            var fileClient = directoryClient.GetFileClient(fileName);
            await fileClient.CreateAsync(content.Length);
            await fileClient.UploadAsync(content);
        }

        // DownloadFileAsync method downloads a file from the specified directory
        public async Task<Stream> DownloadFileAsync(string directoryName, string fileName)
        {
            var directoryClient = _shareClient.GetDirectoryClient(directoryName);
            var fileClient = directoryClient.GetFileClient(fileName);
            var response = await fileClient.DownloadAsync();
            return response.Value.Content;
        }

        // ListFilesAsync method lists all the files in the specified directory
        public async Task<List<string>> ListFilesAsync(string directoryName)
        {
            var directoryClient = _shareClient.GetDirectoryClient(directoryName);
            var files = new List<string>();

            await foreach (ShareFileItem fileItem in directoryClient.GetFilesAndDirectoriesAsync())
            {
                files.Add(fileItem.Name);
            }

            return files;
        }

        // DeleteFileAsync method deletes a file from the specified directory
        public async Task DeleteFileAsync(string directoryName, string fileName)
        {
            var directoryClient = _shareClient.GetDirectoryClient(directoryName);
            var fileClient = directoryClient.GetFileClient(fileName);
            await fileClient.DeleteIfExistsAsync();
        }
    }
}