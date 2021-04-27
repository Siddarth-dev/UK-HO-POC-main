using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Batch.Queries.GetBatchDetail;
using Application.Test.Common;
using AutoMapper;
using NUnit.Framework;
using Persistence;

namespace Application.Test.Batch.Queries
{
    public class GetBatchDetailQueryHandlerTest
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetBatchDetailQueryHandlerTest()
        {
            var fixture = new QueryTestFixture();
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Test]
        public async Task GetBatchDetail()
        {
            var sut = new BatchDetailHandler(_context, _mapper);

            var result = await sut.Handle(new BatchDetailQuery { BatchId = new Guid("187E4DA7-12E2-49B1-C9ED-08D8F297BB6D") }, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BatchDetailModel>(result);
        }
    }
}