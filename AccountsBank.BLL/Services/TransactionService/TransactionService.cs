using System.Data;
using AccountsBank.BLL.DTOs;
using AccountsBank.DAL.Entities;
using AccountsBank.DAL.Repositories.UnitOfWork;
using AutoMapper;

namespace AccountsBank.BLL.Services.TransactionService;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TransactionDTO> GetByIdAsync(Guid guid)
    {
        var result = await _unitOfWork.TransactionRepository.GetByIdAsync(guid);
        _unitOfWork.Commit();

        return _mapper.Map<TransactionDTO>(result);
    }

    public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
    {
        var result = await _unitOfWork.TransactionRepository.GetAllAsync();
        _unitOfWork.Commit();

        return _mapper.Map<IEnumerable<TransactionDTO>>(result);
    }

    public async Task DeleteAsync(Guid guid)
    {
        await _unitOfWork.TransactionRepository.DeleteAsync(guid);
        _unitOfWork.Commit();
    }

    public async Task<TransactionDTO> UpdateAsync(TransactionDTO model)
    {
        var entity = _mapper.Map<Transaction>(model);
        entity.UpdatedAt = DateTime.Now;

        var result = await _unitOfWork.TransactionRepository.UpdateAsync(entity);
        _unitOfWork.Commit();
        
        return _mapper.Map<TransactionDTO>(result);
    }

    public async Task<TransactionDTO> InsertAsync(TransactionDTO model)
    {
        var entity = _mapper.Map<Transaction>(model);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.Now;

        var sender = await _unitOfWork.AccountRepository.GetByIdAsync(model.SenderId);
        var receiver = await _unitOfWork.AccountRepository.GetByIdAsync(model.ReceiverId);

        if (sender.Balance < model.Sum)
        {
            throw new EvaluateException("The receiver's balance is lower than the amount you are trying to transfer");
        }

        sender.Balance -= model.Sum;
        receiver.Balance += model.Sum;

        var t1 = await _unitOfWork.AccountRepository.UpdateAsync(sender);
        var t2 = await _unitOfWork.AccountRepository.UpdateAsync(receiver);
        var result = await _unitOfWork.TransactionRepository.InsertAsync(entity); 
        // Task.WaitAll(t1, t2);
        _unitOfWork.Commit();
        var x = _mapper.Map<TransactionDTO>(result);
        return _mapper.Map<TransactionDTO>(result);
    }

    public async Task<IEnumerable<TransactionDTO>> GetBySenderIdAsync(Guid guid)
    {
        var result = await _unitOfWork.TransactionRepository.GetBySenderIdAsync(guid);

        return _mapper.Map<IEnumerable<TransactionDTO>>(result);
    }

    public async Task<IEnumerable<TransactionDTO>> GetByReceiverIdAsync(Guid guid)
    {
        var result = await _unitOfWork.TransactionRepository.GetByReceiverIdAsync(guid);

        return _mapper.Map<IEnumerable<TransactionDTO>>(result);
    }

    public async Task<IEnumerable<TransactionDTO>> GetTopFiveAsync(int limit)
    {
        if (limit > 5) limit = 5;
        var result = await _unitOfWork.TransactionRepository.GetTopFiveAsync(limit);

        return _mapper.Map<IEnumerable<TransactionDTO>>(result);
    }
}