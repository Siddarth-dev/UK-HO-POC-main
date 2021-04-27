using System;
using Application.Common.Mappings;
using AutoMapper;
using Persistence;

namespace Application.Test.Common
{
    public class QueryTestFixture : IDisposable
    {        
        public DataContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
        public QueryTestFixture()
        {
            Context = DataContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            DataContextFactory.Destroy(Context);
        }

        // [CollectionDefinition("QueryCollection")]
        // public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
    }
}