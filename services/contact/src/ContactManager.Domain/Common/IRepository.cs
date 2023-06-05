using System.Linq.Expressions;

namespace ContactManager.Domain.Common
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
