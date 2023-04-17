using Microsoft.EntityFrameworkCore;
using Tracker.WebAPI.DbContexts;
using Tracker.WebAPI.Repositories.Interfaces;

namespace Tracker.WebAPI.Repositories
{
    public class SQLServerRepository<T> : IRepositoryBase<T> where T : class
    {
        private readonly TrackContext _context;
        private readonly DbSet<T> _dbSet;
        public SQLServerRepository()
        {
            _context = new TrackContext();
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public List<T> Load()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public async Task<List<T>> LoadAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
