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
    }
}
