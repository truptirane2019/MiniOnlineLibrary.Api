using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrar.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MiniLibraryDbContext _ctx;
        protected readonly DbSet<T> _dbset;
        public Repository(MiniLibraryDbContext ctx)
        {
            _ctx = ctx; _dbset = ctx.Set<T>();
        }
        public async Task AddAsync(T entity) => await _dbset.AddAsync(entity);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbset.ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _dbset.FindAsync(id);
        public void Remove(T entity) => _dbset.Remove(entity);
        public void Update(T entity) => _dbset.Update(entity);
    }
}
