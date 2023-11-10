namespace PaycheckChallenge.Domain.Interfaces.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> GetById(long id);
    Task Add(TEntity entity);
}
