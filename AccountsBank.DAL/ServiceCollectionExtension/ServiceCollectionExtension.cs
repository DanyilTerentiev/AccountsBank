using System.Data;
using System.Data.SqlClient;
using AccountsBank.DAL.Repositories;
using AccountsBank.DAL.Repositories.AccountRepository;
using AccountsBank.DAL.Repositories.TransactionRepository;
using AccountsBank.DAL.Repositories.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountsBank.DAL.ServiceCollectionExtension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped(s => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

        serviceCollection.AddScoped<IDbTransaction>(s =>
        {
            SqlConnection cnn = s.GetRequiredService<SqlConnection>();
            cnn.Open();

            return cnn.BeginTransaction();
        });

        serviceCollection.AddScoped<ITransactionRepository, TransactionRepository>();
        serviceCollection.AddScoped<IAccountRepository, AccountRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        return serviceCollection;
    }
}