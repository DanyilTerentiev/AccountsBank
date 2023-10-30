using AccountsBank.BLL.DTOs;
using AccountsBank.DAL.Entities;
using AutoMapper;

namespace AccountsBank.BLL.Mapper;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDTO>().ReverseMap();
    }
}