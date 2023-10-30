using System.Data;
using AccountsBank.DAL.Repositories.AccountRepository;
using AccountsBank.DAL.Repositories.TransactionRepository;

namespace AccountsBank.DAL.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IDbTransaction dbTransaction)
    {
        AccountRepository = accountRepository;
        TransactionRepository = transactionRepository;
        _dbTransaction = dbTransaction;
    }

    private readonly IDbTransaction _dbTransaction;
    
    public IAccountRepository AccountRepository { get; }
    
    public ITransactionRepository TransactionRepository { get; }
    
    public void Commit()
    {
        try
        {
            _dbTransaction.Commit();
        }
        catch (Exception exception)
        {
            _dbTransaction.Rollback();
        }
    }
    
    public void Dispose()
    {
        _dbTransaction.Connection?.Close();
        _dbTransaction.Connection?.Dispose();
        _dbTransaction.Dispose();
    }

}