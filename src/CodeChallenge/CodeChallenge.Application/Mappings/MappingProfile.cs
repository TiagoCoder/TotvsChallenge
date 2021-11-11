using AutoMapper;
using CodeChallenge.Application.Entities.Transaction;
using CodeChallenge.Application.Entities.TransactionDetails;
using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Application.Mappings
{
    public class MappingProfile : Profile
    {
        #region MappingProfile
        /// <summary>
        /// Cria um perfil de Mapeamento
        /// </summary>
        public MappingProfile()
        {
            // Cria um novo mapeamento entre as propriedades Transaction e TransactionDTO
            CreateMap<Transaction, TransactionDTO>()
              .ReverseMap()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Cria um novo mapeamento entre as propriedades TransactionDetail e TransactionDetailDTO
            CreateMap<TransactionDetail, TransactionDetailDTO>()
              .ReverseMap()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
        #endregion
    }
}
