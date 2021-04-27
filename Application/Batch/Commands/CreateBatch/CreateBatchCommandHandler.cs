using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Batch.Commands.CreateBatch
{
    public class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, Guid>
    {
        private readonly DataContext _context;
        private readonly IMediator _mediator;
        public CreateBatchCommandHandler(DataContext context, IMediator mediator)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<Guid> Handle(CreateBatchCommand request, CancellationToken cancellationToken)
        {
            var buEntity = _context.BusinessUnities.Where(a => a.BusinessUnitName.Contains(request.BusinessUnit) && a.IsActive).FirstOrDefault();
            var statusEntity = _context.BatchStatus.Where(a => a.Status == "Incomplete" && a.IsActive).FirstOrDefault();

            var entity = new Domain.Entities.Batch
            {
                BatchStatus = statusEntity,
                BusinessUnit = buEntity,
                CreatedBy = string.Empty,
                Created = DateTime.Now,
                LastModifiedBy = string.Empty,
                LastModified = DateTime.Now,
                ExpiryDate = request.ExpiryDate,
                IsActive = true,
                BatchPublishedDate = DateTime.Now
            };

            await _context.Batches.AddAsync(entity);

            if (request.Attributes != null && request.Attributes.Any())
            {
                var attEntity = new List<BatchAttribute>();
                foreach (var item in request.Attributes)
                {
                    attEntity.Add(new BatchAttribute
                    {
                        Key = item.Key,
                        Value = item.Value,
                        IsActive = true,
                        Batch = entity
                    });
                }
                await _context.BatchAttributes.AddRangeAsync(attEntity);
            }

            var aclEntities = new Acl();
            aclEntities.Batch = entity;
            aclEntities.IsActive = true;
            aclEntities.BatchId = entity.Id;
            entity.Acl = aclEntities;

            if (request.Acl != null && request.Acl.ReadGroups != null && request.Acl.ReadGroups.Any())
            {
                foreach (var item in request.Acl.ReadGroups)
                {
                    aclEntities.ReadGroups.Add(new ReadGroup() { GroupName = item, Acl = aclEntities });
                }
            }
            await _context.ReadGroups.AddRangeAsync(aclEntities.ReadGroups);

            if (request.Acl != null && request.Acl.ReadUsers != null && request.Acl.ReadUsers.Any())
            {
                foreach (var item in request.Acl.ReadUsers)
                {
                    aclEntities.ReadUsers.Add(new ReadUser() { UserName = item, Acl = aclEntities });
                }
            }
            await _context.ReadUsers.AddRangeAsync(aclEntities.ReadUsers);
            await _context.Acls.AddAsync(aclEntities);

            await _mediator.Publish(new CreateBatchContainer { BatchId = entity.Id.ToString().ToLower() }, cancellationToken);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}