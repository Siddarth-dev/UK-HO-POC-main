using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAzureBlobStorageService
    {
        Task<bool> CreateContainer(string containerName);

        Task UploadFileBlobAsync(string containerName, string content, string fileName, string mimeType);

        Task UploadFileBlobUsingPathAsync(string containerName, string filePath, string fileName, string mimeType);
    }
}