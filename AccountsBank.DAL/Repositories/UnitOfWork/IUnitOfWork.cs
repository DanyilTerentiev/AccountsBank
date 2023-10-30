using AccountsBank.DAL.Repositories.AccountRepository;
using AccountsBank.DAL.Repositories.TransactionRepository;

namespace AccountsBank.DAL.Repositories.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository AccountRepository { get; }
    
    ITransactionRepository TransactionRepository { get; }

    void Commit();
}