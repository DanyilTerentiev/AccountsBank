using AccountsBank.BLL.DTOs;
using AccountsBank.DAL.Entities;

namespace AccountsBank.BLL.Services.AccountService;

public interface IAccountService
{
    Task<AccountDTO> GetByIdAsync(Guid guid);

    Task<IEnumerable<AccountDTO>> GetAllAsync();

    Task DeleteAsync(Guid guid);

    Task<AccountDTO> UpdateAsync(AccountDTO entity);

    Task<AccountDTO> InsertAsync(AccountDTO model);
    
    Task<IEnumerable<AccountDTO>> GetAccountsByUserIdAsync(Guid userId);
}