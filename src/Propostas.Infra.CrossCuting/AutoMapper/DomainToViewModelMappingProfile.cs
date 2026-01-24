using AutoMapper;
using Propostas.Application.ViewModels;
using Propostas.Domain.Entidade;

namespace Propostas.Infra.CrossCuting.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Proposta, PropostaViewModel>();
        }
    }
}
