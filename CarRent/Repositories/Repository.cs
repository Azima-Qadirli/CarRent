using CarRent.Context;
using CarRent.Repositories.Interfaces;
using CarRent.Views.Models.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarRent.Repositories;

public class Repository<T>:IRepository<T> where T :BaseEntity
{
    private readonly CarRentDbContext _context;
    private readonly DbSet<T> _dbSet;
    public Repository(CarRentDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<bool> AddAsync(T entity)
    {
        EntityEntry entityEntry = await _dbSet.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }

    public async Task AddRangeAsync(IEnumerable<T> entites)
        => await _dbSet.AddRangeAsync(entites);

    public bool Update(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(IEnumerable<T> entites)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public int Save()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }
}
