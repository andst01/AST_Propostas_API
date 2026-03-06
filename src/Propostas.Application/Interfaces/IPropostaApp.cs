using Propostas.Application.DTO;
using Propostas.Application.Request;
using Propostas.Domain.Entidade;

namespace Propostas.Application.Interfaces
{
    public interface IPropostaApp : IAppBase<Proposta, PropostaRequest, PropostaDTO>
    {
        Task<List<PropostaDTO>> ObterDadosPropostaClienteAsync();

        Task<List<PropostaDTO>> ObterPropostaAprovadaSemApoliceAsync();

        Task<PropostaDTO> ObterPropostaClientePorIdAsync(int id);

        Task<List<PropostaDTO>> ObterTodosComFiltroAsync(DateTime? dataCriacao, string? numeroProposta, int status);
    }
}
