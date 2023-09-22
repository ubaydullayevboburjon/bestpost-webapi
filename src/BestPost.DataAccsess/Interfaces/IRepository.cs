namespace BestPost.DataAccsess.Interfaces;

public interface IRepository<TEntity, TViewModel>
{
    public Task<long> CreateAsync(TEntity entity);

    public Task<long> UpdateAsync(long id, TEntity entity);

    public Task<long> DeleteAsync(long id);

    public Task<TEntity?> GetByIdAsync(long id);

    public Task<long> CountAsync();
}
