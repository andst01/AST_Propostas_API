using AutoMapper;
using Propostas.Application.ViewModels;
using Propostas.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Infra.CrossCuting.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PropostaViewModel, Proposta>();
        }
    }
}
