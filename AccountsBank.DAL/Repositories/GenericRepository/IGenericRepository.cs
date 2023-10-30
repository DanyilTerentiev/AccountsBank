using AccountsBank.DAL.Entities;

namespace AccountsBank.DAL.Repositories.GenericRepository;

public interface IGenericRepository<T> where T: BaseEntity
{
    Task<T> GetByIdAsync(Guid guid);

    Task<IEnumerable<T>> GetAllAsync();

    Task DeleteAsync(Guid guid);

    Task<T> UpdateAsync(T entity);

    Task<T> InsertAsync(T entity);
}