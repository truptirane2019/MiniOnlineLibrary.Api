using MiniOnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrar.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MiniLibraryDbContext _ctx;
        public IRepository<User> Users { get; }
        public IRepository<Book> Books { get; }
        public IRepository<BorrowTransaction> Transactions { get; }
        public IRepository<api_logs> APILogs { get; }

        public UnitOfWork(MiniLibraryDbContext ctx)
        {
            _ctx = ctx;
            Users = new Repository<User>(ctx);
            Books = new Repository<Book>(ctx);
            Transactions = new Repository<BorrowTransaction>(ctx);
            APILogs = new Repository<api_logs>(ctx);
        }


        public async Task<int> SaveChangesAsync() => await _ctx.SaveChangesAsync();
        public void Dispose() => _ctx.Dispose();
    }
}
