using System.Reflection;
using AccountsBank.BLL.Mapper;
using AccountsBank.BLL.Services.AccountService;
using AccountsBank.BLL.Services.TransactionService;
using Microsoft.Extensions.DependencyInjection;

namespace AccountsBank.BLL.ServiceCollectionExtension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(Assembly.GetAssembly(typeof(AccountProfile)));

        serviceCollection.AddScoped<IAccountService, AccountService>();
        serviceCollection.AddScoped<ITransactionService, TransactionService>();

        return serviceCollection;
    }
}