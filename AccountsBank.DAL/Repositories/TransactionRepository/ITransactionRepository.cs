using AccountsBank.DAL.Entities;
using AccountsBank.DAL.Repositories.GenericRepository;

namespace AccountsBank.DAL.Repositories.TransactionRepository;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetBySenderIdAsync(Guid guid);

    Task<IEnumerable<Transaction>> GetByReceiverIdAsync(Guid guid);

    Task<IEnumerable<Transaction>> GetTopFiveAsync(int limit);
}