using Propostas.Domain.Entidade;

namespace Propostas.Domain.Interfaces
{
    public interface IPropostaRepositorio : IRepositorioBase<Proposta>
    {
        Task<List<Proposta>> ObterDadosPropostaClienteAsync();

        Task<List<Proposta>> ObterPropostaAprovadaSemApoliceAsync();

    }
}
