using System.Data;
using System.Data.SqlClient;
using System.Text;
using AccountsBank.DAL.Entities;
using Dapper;

namespace AccountsBank.DAL.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T: BaseEntity
{
    protected readonly SqlConnection SqlConnection;

    protected readonly IDbTransaction DbTransaction;

    protected readonly string TableName;

    protected GenericRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName)
    {
        SqlConnection = sqlConnection;
        DbTransaction = dbTransaction;
        TableName = tableName;
    }


    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await SqlConnection.QueryAsync<T>($"SELECT * FROM {TableName}", 
            transaction: DbTransaction);
    }

    public async Task DeleteAsync(Guid guid)
    {
        var deleteQuery = $"DELETE FROM {TableName} WHERE Id=@Id";
        await SqlConnection.ExecuteAsync(sql: deleteQuery, param: new { Id = guid },
            transaction: DbTransaction);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var updateQuery = GetUpdateQuery(entity);
        
        await SqlConnection.ExecuteAsync(sql: updateQuery, param: entity, transaction: DbTransaction);

        return entity;
    }

    public async Task<T> InsertAsync(T entity)
    {
        var insertQuery = GetInsertQuery(entity);

        await SqlConnection.ExecuteAsync(insertQuery, param: entity, transaction: DbTransaction);

        return entity;
    }
    
    public async Task<T> GetByIdAsync(Guid guid)
    {
        var result = await SqlConnection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {TableName} WHERE Id = @Id",
            new { Id = guid },
            DbTransaction);
        // if (result == null)
        //     throw new KeyNotFoundException($"{TableName} with id [{guid}] could not be found.");

        return result;
    }

    private List<string> GetColumnNames(T entity)
    {
        return typeof(T).GetProperties().Select(x => x.Name).ToList();
    }
    
    private List<string> GetParameterNames(T entity)
    {
        return GetColumnNames(entity).Select(x => $"@{x}").ToList();
    }

    private string GetInsertQuery(T entity)
    {
        var sb = new StringBuilder($"INSERT INTO {TableName} (");
        var columnNames = GetColumnNames(entity);
        var parameterNames = GetParameterNames(entity);
        
        columnNames.ForEach(prop =>
        {
            sb.Append($"[{prop}], ");
        });

        sb
            .Remove(sb.Length - 2, 2)
            .Append(") VALUES (");
        
        parameterNames.ForEach(param =>
        {
            sb.Append($"{param}, ");
        });

        sb
            .Remove(sb.Length - 2, 2)
            .Append(")");

        return sb.ToString();
    }

    private string GetUpdateQuery(T entity)
    {
        var sb = new StringBuilder($"UPDATE {TableName} SET ");
        var columnNames = GetColumnNames(entity);
        
        columnNames.ForEach(prop =>
        {
            sb.Append($"[{prop}]=@{prop},");
        });
        
        sb.Remove(sb.Length - 1, 1);
        sb.Append(" WHERE Id=@Id");

        return sb.ToString();
    }
}