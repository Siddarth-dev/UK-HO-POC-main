using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Batch.Commands.CreateBatch
{
    public class CreateBatchContainer : INotification
    {
        public string BatchId { get; set; }

        public class CreateBatchContainerHandler : INotificationHandler<CreateBatchContainer>
        {
            private readonly IAzureBlobStorageService _blobStorageService;
            public CreateBatchContainerHandler(IAzureBlobStorageService blobStorageService)
            {
                _blobStorageService = blobStorageService;
            }

            public async Task Handle(CreateBatchContainer notification, CancellationToken cancellationToken)
            {
                await _blobStorageService.CreateContainer(notification.BatchId);
            }
        }
    }
}