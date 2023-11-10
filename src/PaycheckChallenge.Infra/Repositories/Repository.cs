using Microsoft.EntityFrameworkCore;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces.Repositories;

namespace PaycheckChallenge.Infra.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _set;

    public Repository(DbContext context)
    {
        _context = context;
        _set = _context.Set<TEntity>();
    }

    public async Task<TEntity> GetById(long id)
    {
        return await _set.FindAsync(id);
    }

    public async Task Add(TEntity entity)
    {
        await _set.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}
