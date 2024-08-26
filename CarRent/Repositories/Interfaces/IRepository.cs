using CarRent.Views.Models.BaseEntities;

namespace CarRent.Repositories.Interfaces;

public interface IRepository<T>where T : BaseEntity
{
    Task<bool> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entites);
    
    bool Update(T entity);
    bool Remove(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entites);
    Task<bool> RemoveAsync(int id);
    Task<T> Get(int id);
    Task<IQueryable<T>> GetAllAsync();
    int Save();
    Task<int> SaveAsync();

}