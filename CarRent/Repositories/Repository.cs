using CarRent.Context;
using CarRent.Exceptions;
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
        EntityEntry entityEntry = _dbSet.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }

    public void UpdateRange(IEnumerable<T> entites)
        => _dbSet.UpdateRange(entites);

    public bool Remove(T entity)
    {
        EntityEntry entityEntry = _dbSet.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public void RemoveRange(IEnumerable<T> entites)
        => _dbSet.RemoveRange(entites);

    public async Task<bool> RemoveAsync(int id)
    {
        var entity = await GetAsync(id);
        EntityEntry entityEntry= _dbSet.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<T> GetAsync(int id)
    {
        var entity= await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            throw new EntityNotFoundException();
        return entity;
    }

    public IQueryable<T>GetAll()
    {
        return _dbSet;
    }

    public int Save()
        => _context.SaveChanges();

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
}
