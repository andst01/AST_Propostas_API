using Microsoft.EntityFrameworkCore;
using Propostas.Domain.Entidade;
using Propostas.Domain.Enums;
using Propostas.Domain.Interfaces;
using Propostas.Infra.Data.Contexto;

namespace Propostas.Infra.Data.Repositorio
{
    public class PropostaRepositorio : RepositorioBase<Proposta>, IPropostaRepositorio
    {
        public PropostaRepositorio(PropostaDbContext context) : base(context)
        {
        }

        public async Task<List<Proposta>> ObterPropostaClienteAsync()
        {
            var proposta = await _context.Propostas
                                         .Include(p => p.Cliente)
                                         .ToListAsync();
            return proposta;
        }

        public async Task<Proposta> ObterPropostaClientePorIdAsync(int id)
        {
            var proposta = await _context.Propostas
                                         .Include(p => p.Cliente)
                                         .Where(p => p.Id == id)
                                         .FirstOrDefaultAsync();
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

        public async Task<List<Proposta>> ObterTodosComFiltroAsync(DateTime? dataCriacao, string? numeroProposta, int status)
        {
            var retorno = _context.Propostas
                                         .AsNoTracking()
                                         .Include(p => p.Apolice)
                                         .Include(p => p.Cliente)
                                        
                                         .AsQueryable();


            if (dataCriacao.HasValue && dataCriacao.Value != DateTime.MinValue)
                retorno = retorno.Where(x => x.DataCriacao.Date >= dataCriacao);
            if (status >= 0)
                retorno = retorno.Where(x => x.Status == (EnumStatusProposta)status);
            if (!string.IsNullOrEmpty(numeroProposta))
                retorno = retorno.Where(x => x.NumeroProposta.Contains(numeroProposta));

            return await retorno.ToListAsync();

        }
    }
}
