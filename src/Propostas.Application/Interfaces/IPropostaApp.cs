using Propostas.Application.DTO;
using Propostas.Domain.Entidade;

namespace Propostas.Application.Interfaces
{
    public interface IPropostaApp : IAppBase<Proposta, PropostaDTO>
    {
        Task<List<PropostaDTO>> ObterDadosPropostaClienteAsync();

        Task<List<PropostaDTO>> ObterPropostaAprovadaSemApoliceAsync();
    }
}
