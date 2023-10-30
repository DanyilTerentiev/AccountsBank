using System.Data;
using System.Data.SqlClient;
using AccountsBank.DAL.Entities;
using AccountsBank.DAL.Repositories.GenericRepository;
using Dapper;

namespace AccountsBank.DAL.Repositories.TransactionRepository;

public sealed class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Transactions")
    {
    }

    public async Task<IEnumerable<Transaction>> GetBySenderIdAsync(Guid guid)
    {
        var sql = $"SELECT * FROM {TableName} WHERE SenderId = @SenderId";

        return await SqlConnection.QueryAsync<Transaction>(sql, param: new { SenderId = guid }, DbTransaction);
    }

    public async Task<IEnumerable<Transaction>> GetByReceiverIdAsync(Guid guid)
    {
        var sql = $"SELECT * FROM {TableName} WHERE ReceiverId = @ReceiverId";

        return await SqlConnection.QueryAsync<Transaction>(sql, param: new { ReceiverId = guid }, DbTransaction);
    }

    public async Task<IEnumerable<Transaction>> GetTopFiveAsync(int limit)
    {
        var sql = $"SELECT TOP({limit}) * FROM {TableName}";

        return await SqlConnection.QueryAsync<Transaction>(sql, transaction: DbTransaction);
    }
}