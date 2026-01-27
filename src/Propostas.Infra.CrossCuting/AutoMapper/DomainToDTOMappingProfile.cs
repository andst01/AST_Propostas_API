using AutoMapper;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;
using Propostas.Domain.Enums;

namespace Propostas.Infra.CrossCuting.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Proposta, PropostaDTO>()
                .ForMember(x => x.Mensagem, opt => opt.Ignore())
                .ForMember(x => x.NomeCliente, 
                                opt => opt.MapFrom(src => src.Cliente.Nome))
                .ForMember(x => x.CodigoStatus, opt => opt.MapFrom(src => (int)src.Status))
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));
        }
    }
}
