using AutoMapper;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;
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
                .ForMember(x => x.Cliente, opt => opt.Ignore());
        }
    }
}
