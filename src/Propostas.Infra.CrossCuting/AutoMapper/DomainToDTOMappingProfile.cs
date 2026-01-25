using AutoMapper;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;

namespace Propostas.Infra.CrossCuting.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Proposta, PropostaDTO>();
        }
    }
}
