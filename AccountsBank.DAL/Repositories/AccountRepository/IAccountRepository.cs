using AccountsBank.DAL.Entities;
using AccountsBank.DAL.Repositories.GenericRepository;

namespace AccountsBank.DAL.Repositories.AccountRepository;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<IEnumerable<Account>> GetAccountsByUserIdAsync(Guid userId);

    // Task TransferMoney(Transaction model);
}