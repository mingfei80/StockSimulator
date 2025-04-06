using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace StockSimulator.Data.Repositories.Generic;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task AddManyAsync(IEnumerable<TEntity> entities)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await FindAsync(predicate);
        _dbContext.Set<TEntity>().RemoveRange(entities); 
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate,
                                            bool asNoTracking = true,
                                            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        return await Get(asNoTracking, include).FirstOrDefaultAsync(predicate);
    }


    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
                                                bool asNoTracking = true,
                                                Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        return await Get(asNoTracking, include).Where(predicate).ToListAsync();
    }

    public async Task<List<TEntity>> GetAllAsync(bool asNoTracking = true,
                                                Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        return await Get(asNoTracking, include).ToListAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity); await _dbContext.SaveChangesAsync();
    }


    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(predicate);
    }


    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().CountAsync(predicate);
    }
    private IQueryable<TEntity> Get(bool asNoTracking = true,
                                    Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        IQueryable<TEntity> entity = _dbContext.Set<TEntity>();

        if (asNoTracking)
        {
            entity = entity.AsNoTracking();
        }

        entity = include?.Invoke(entity) ?? entity; // Apply includes if provided

        return entity;
    }
}