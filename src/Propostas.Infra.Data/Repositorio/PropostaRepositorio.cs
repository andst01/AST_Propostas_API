using Microsoft.EntityFrameworkCore;
using Propostas.Domain.Entidade;
using Propostas.Domain.Interfaces;
using Propostas.Infra.Data.Contexto;

namespace Propostas.Infra.Data.Repositorio
{
    public class PropostaRepositorio : RepositorioBase<Proposta>, IPropostaRepositorio
    {
        public PropostaRepositorio(PropostaDbContext context) : base(context)
        {
        }

        public async Task<List<Proposta>> ObterDadosPropostaClienteAsync()
        {
            var proposta = await _context.Propostas
                                         .Include(p => p.Cliente)
                                         .ToListAsync();
            return proposta;
        }

        public async Task<List<Proposta>> ObterPropostaAprovadaSemApoliceAsync()
        {
            var proposta = await _context.Propostas
                                         .AsNoTracking()
                                         .Include(p => p.Apolice)
                                         .Where(p => p.Status == Domain.Enums.EnumStatusProposta.Aprovada &&
                                                    p.Apolice == null)
                                         .ToListAsync();

            return proposta;
        }

    }
}
