using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Propostas.Domain.Entidade;
using Propostas.Infra.Data.Mapeamento;

namespace Propostas.Infra.Data.Contexto
{
    public class PropostaDbContext : DbContext
    {
        public DbSet<Proposta> Propostas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Apolice> Apolices { get; set; }

        public PropostaDbContext(DbContextOptions<PropostaDbContext> options)
           : base(options)
        {
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.ApplyConfiguration(new PropostaMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ApoliceMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
