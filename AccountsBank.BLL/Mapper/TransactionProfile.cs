using AccountsBank.BLL.DTOs;
using AccountsBank.DAL.Entities;
using AutoMapper;

namespace AccountsBank.BLL.Mapper;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionDTO>().ReverseMap();
    }
}