using AutoMapper;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;
using Propostas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Infra.CrossCuting.AutoMapper
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<PropostaDTO, Proposta>()
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => (EnumStatusProposta)src.CodigoStatus))
                .ForMember(x => x.Cliente, opt => opt.Ignore())
                .ForMember(x => x.Apolice, opt => opt.Ignore());
        }
    }
}
