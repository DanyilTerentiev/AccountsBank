using System.Data;
using System.Data.SqlClient;
using AccountsBank.DAL.Entities;
using AccountsBank.DAL.Repositories.GenericRepository;
using Dapper;

namespace AccountsBank.DAL.Repositories.AccountRepository;

public sealed class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Accounts")
    {
    }

    public async Task<IEnumerable<Account>> GetAccountsByUserIdAsync(Guid userId)
    {
        string sql = $"SELECT * FROM {TableName} WHERE UserId = @UserId";

        await using SqlCommand command = new SqlCommand(sql, SqlConnection);
        command.Transaction = (SqlTransaction)DbTransaction;
        command.Parameters.AddWithValue("@UserId", userId);

        List<Account> accounts = new List<Account>();

        await using SqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            Account account = new Account
            {
                Id = (Guid)reader["Id"],
                Balance = (double)reader["Balance"],
                CardType = reader["CardType"].ToString()!,
                UserId = (Guid)reader["UserId"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? (DateTime?)null : (DateTime)reader["UpdatedAt"]
            };
            accounts.Add(account);
        }

        return accounts;
    }
}