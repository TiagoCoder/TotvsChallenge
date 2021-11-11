using AutoMapper;
using CodeChallenge.Application.Entities.Transaction;
using CodeChallenge.Application.Entities.TransactionDetails;
using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDTO>()
              .ReverseMap()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TransactionDetail, TransactionDetailDTO>()
              .ReverseMap()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
