using AutoMapper;
using Propostas.Application.Interfaces;
using Propostas.Application.ViewModels;
using Propostas.Domain.Entidade;
using Propostas.Domain.Interfaces;

namespace Propostas.Application
{
    public class PropostaApp : AppBase<Proposta, PropostaViewModel>, IPropostaApp
    {
        public PropostaApp(IRepositorioBase<Proposta> repositorio, 
                           IMapper mapper) : base(repositorio, mapper)
        {
        }
    }
}
