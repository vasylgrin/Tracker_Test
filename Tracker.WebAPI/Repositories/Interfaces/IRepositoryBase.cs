namespace Tracker.WebAPI.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public List<T> Load();

        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<List<T>> LoadAsync();
    }
}
