using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Batch.Queries.GetBatchDetail
{
    public class BatchDetailHandler : IRequestHandler<BatchDetailQuery, BatchDetailModel>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BatchDetailHandler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<BatchDetailModel> Handle(BatchDetailQuery request, CancellationToken cancellationToken)
        {
            var entityBatch = await _context.Batches.Where(b => b.Id == request.BatchId)
            .Include(b => b.BatchAttributes)
            .SingleOrDefaultAsync(cancellationToken);
            var entity = _mapper.Map<Domain.Entities.Batch, BatchDetailModel>(entityBatch);

            if (entity != null)
            {
                entity.BusinessUnit = await _context.BusinessUnities.Where(a => a.Id == entityBatch.BusinessUnitId).Select(b => b.BusinessUnitName).FirstOrDefaultAsync();
                entity.Status = await _context.BatchStatus.Where(a => a.Id == entityBatch.BatchStatusId).Select(b => b.Status).FirstOrDefaultAsync();
                entity.Attributes = _mapper.Map<ICollection<Domain.Entities.BatchAttribute>, List<BatchAttributeDto>>(entityBatch.BatchAttributes);
                var acl = await _context.Acls.Where(b => b.BatchId == request.BatchId).SingleOrDefaultAsync(cancellationToken);
                entity.Acl = _mapper.Map<Domain.Entities.Acl, BatchAclDto>(acl);
                var readGrops = _context.ReadGroups.Where(b => b.Acl.Id == acl.Id);
                entity.Acl.ReadGroups =  _mapper.Map<IQueryable<Domain.Entities.ReadGroup>, List<BatchReadGroupDto>>(readGrops);
                var readUsers = _context.ReadUsers.Where(b => b.Acl.Id == acl.Id);
                entity.Acl.ReadUsers =  _mapper.Map<IQueryable<Domain.Entities.ReadUser>, List<BatchReadUserDto>>(readUsers);
            }

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Batch), request.BatchId);
            }
            return entity;

        }
    }
}