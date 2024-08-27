using CarRent.Views.Models.BaseEntities;

namespace CarRent.Repositories.Interfaces;

public interface IRepository<T>where T : BaseEntity
{
    Task<bool> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entites);
    
    bool Update(T entity);
    void UpdateRange(IEnumerable<T> entites);
    bool Remove(T entity);
    void RemoveRange(IEnumerable<T> entites);
    Task<bool> RemoveAsync(int id);
    Task<T> GetAsync(int id);
    IQueryable<T>GetAll();
    int Save();
    Task<int> SaveAsync();

}