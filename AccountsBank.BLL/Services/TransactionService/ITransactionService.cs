using AccountsBank.BLL.DTOs;

namespace AccountsBank.BLL.Services.TransactionService;

public interface ITransactionService
{
    Task<TransactionDTO> GetByIdAsync(Guid guid);

    Task<IEnumerable<TransactionDTO>> GetAllAsync();

    Task DeleteAsync(Guid guid);

    Task<TransactionDTO> UpdateAsync(TransactionDTO model);

    Task<TransactionDTO> InsertAsync(TransactionDTO model);

    Task<IEnumerable<TransactionDTO>> GetBySenderIdAsync(Guid guid);

    Task<IEnumerable<TransactionDTO>> GetByReceiverIdAsync(Guid guid);

    Task<IEnumerable<TransactionDTO>> GetTopFiveAsync(int limit);
}