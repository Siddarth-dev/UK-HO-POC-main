using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Batch.Commands.UploadBatchFile
{
    public class UploadBatchFileToContainer : INotification
    {
        public string ContainerName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Content { get; set; }
        public long FileSize { get; set; }
        public string MimeType { get; set; }

        public class UploadBatchFileToContainerHandler : INotificationHandler<UploadBatchFileToContainer>
        {
            private readonly IAzureBlobStorageService _blobStorageService;
            public UploadBatchFileToContainerHandler(IAzureBlobStorageService blobStorageService)
            {
                _blobStorageService = blobStorageService;
            }

            public async Task Handle(UploadBatchFileToContainer notification, CancellationToken cancellationToken)
            {
                if (!string.IsNullOrEmpty(notification.Content))
                {
                    await _blobStorageService.UploadFileBlobAsync(notification.ContainerName, notification.Content, notification.FileName, notification.MimeType);
                }
                else
                {
                    await _blobStorageService.UploadFileBlobUsingPathAsync(notification.ContainerName, notification.FilePath, notification.FileName, notification.MimeType);
                }
            }
        }
    }
}