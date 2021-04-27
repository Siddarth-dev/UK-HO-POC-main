using System;
using Persistence;

namespace Application.Test.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly DataContext _context;
        public CommandTestBase()
        {
            _context = DataContextFactory.Create();
        }

        public void Dispose()
        {
            DataContextFactory.Destroy(_context);
        }
    }
}