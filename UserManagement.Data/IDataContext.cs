using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Data;

public interface IDataContext
{
    /// <summary>
    /// Get a list of items
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

    ValueTask<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class;

    ValueTask<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;

    ValueTask<bool> DeleteAsync<TEntity>(TEntity entity) where TEntity : class;

    ValueTask<TEntity?> GetByIdAsync<TEntity>(long id) where TEntity : class;
}
