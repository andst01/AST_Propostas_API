using Propostas.Domain.Entidade;

namespace Propostas.Domain.Interfaces
{
    public interface IPropostaRepositorio : IRepositorioBase<Proposta>
    {
        Task<List<Proposta>> ObterPropostaClienteAsync();

        Task<List<Proposta>> ObterPropostaAprovadaSemApoliceAsync();

        Task<Proposta> ObterPropostaClientePorIdAsync(int id);

        Task<List<Proposta>> ObterTodosComFiltroAsync(DateTime? dataCriacao, string? numeroProposta, int status);



    }
}
