using System.IO;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Storage;
using Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Retry;
using System;

namespace Infrastructure
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private const int maxRetry = 2;
        private /*readonly*/ BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        private readonly AsyncRetryPolicy _retryPolicy;
        public AzureBlobStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _blobServiceClient = blobServiceClient;
            //_retryPolicy = Policy.Handle<StorageException>().RetryAsync(maxRetry);
            _retryPolicy = Policy.Handle<StorageException>().WaitAndRetryAsync(maxRetry, times => 
                TimeSpan.FromMilliseconds(times * 100));
        }

        public async Task<bool> CreateContainer(string containerName)
        {
            var blobContainerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
            return blobContainerClient != null && blobContainerClient.Value.Exists();
        }

        public async Task UploadFileBlobAsync(string containerName, string content, string fileName, string mimeType)
        {
            await _retryPolicy.ExecuteAsync(async () => {
                await CreateBatchStorageObject();
            });
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobContainerInfo = blobContainerClient.CreateIfNotExists();
            var blobClient = blobContainerClient.GetBlobClient(fileName);
            var bytes = Encoding.UTF8.GetBytes(content);
            await using var memoryStream = new MemoryStream(bytes);
            await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = mimeType ?? fileName.GetContentType() });
        }

        public async Task UploadFileBlobUsingPathAsync(string containerName, string filePath, string fileName, string mimeType)
        {
            await _retryPolicy.ExecuteAsync(async () => {
                await CreateBatchStorageObject();
            });
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobContainerInfo = blobContainerClient.CreateIfNotExists();
            var blobClient = blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = mimeType ?? fileName.GetContentType() });
        }

        private async Task CreateBatchStorageObject()
        {
            /*
            //var blobServiceClient = new BlobServiceClient(_configuration.GetValue<string>("AzureBlobStorageConnectionString"));
            var storageConnectionString = _configuration.GetValue<string>("AzureBlobStorageConnectionString");
            var retryPolly = Policy.Handle<StorageException>()
            .RetryAsync(2, async (ex, count, context) => {
                (_configuration as IConfigurationRoot).Reload();
                storageConnectionString = _configuration.GetValue<string>("AzureBlobStorageConnectionString");
                _blobServiceClient = new BlobServiceClient(_configuration.GetValue<string>("AzureBlobStorageConnectionString"));
            });
            await Task.CompletedTask;
            await retryPolly.ExecuteAsync(() => {});*/
            (_configuration as IConfigurationRoot).Reload();
            _blobServiceClient = new BlobServiceClient(_configuration.GetValue<string>("AzureBlobStorageConnectionString"));
            await Task.CompletedTask;
        }
    }
}