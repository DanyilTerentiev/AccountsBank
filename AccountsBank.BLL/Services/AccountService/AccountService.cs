using System.ComponentModel;
using AccountsBank.BLL.DTOs;
using AccountsBank.DAL.Entities;
using AccountsBank.DAL.Enums;
using AccountsBank.DAL.Repositories.UnitOfWork;
using AutoMapper;

namespace AccountsBank.BLL.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AccountDTO> GetByIdAsync(Guid guid)
    {
        var result = await _unitOfWork.AccountRepository.GetByIdAsync(guid);
        _unitOfWork.Commit();

        return _mapper.Map<AccountDTO>(result);
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAsync()
    {
        var result = await _unitOfWork.AccountRepository.GetAllAsync();
        _unitOfWork.Commit();

        return _mapper.Map<IEnumerable<AccountDTO>>(result);
    }

    public async Task DeleteAsync(Guid guid)
    {
        await _unitOfWork.AccountRepository.DeleteAsync(guid);
        _unitOfWork.Commit();
    }

    public async Task<AccountDTO> UpdateAsync(AccountDTO model)
    {
        var entity = _mapper.Map<Account>(model);
        entity.UpdatedAt = DateTime.Now;
        
        if (!IsValidCardType(model.CardType))
        {
            throw new InvalidEnumArgumentException("Invalid card type was entered");
        }
        
        var result = await _unitOfWork.AccountRepository.UpdateAsync(entity);
        _unitOfWork.Commit();
        
        return _mapper.Map<AccountDTO>(result);
    }

    public async Task<AccountDTO> InsertAsync(AccountDTO model)
    {
        var entity = _mapper.Map<Account>(model);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.Now;
        
        if (!IsValidCardType(model.CardType))
        {
            throw new InvalidEnumArgumentException("Invalid card type was entered");
        }
        
        var result = await _unitOfWork.AccountRepository.InsertAsync(entity);
        _unitOfWork.Commit();

        return _mapper.Map<AccountDTO>(result);
    }

    public async Task<IEnumerable<AccountDTO>> GetAccountsByUserIdAsync(Guid userId)
    {
        var result = await _unitOfWork.AccountRepository.GetAccountsByUserIdAsync(userId);
        _unitOfWork.Commit();

        return _mapper.Map<IEnumerable<AccountDTO>>(result);
    }
    
    private bool IsValidCardType(string cardType)
    {
        return Enum.TryParse(cardType, out CardType result);
    }
}