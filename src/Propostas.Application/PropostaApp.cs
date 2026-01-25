using AutoMapper;
using Propostas.Application.Interfaces;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;
using Propostas.Domain.Interfaces;

namespace Propostas.Application
{
    public class PropostaApp : AppBase<Proposta, PropostaDTO>, IPropostaApp
    {
        public PropostaApp(IRepositorioBase<Proposta> repositorio, 
                           IMapper mapper) : base(repositorio, mapper)
        {
        }
    }
}
