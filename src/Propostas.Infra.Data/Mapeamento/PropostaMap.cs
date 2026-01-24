using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Propostas.Domain.Entidade;

namespace Propostas.Infra.Data.Mapeamento
{
    public class PropostaMap : IEntityTypeConfiguration<Proposta>
    {
        public void Configure(EntityTypeBuilder<Proposta> builder)
        {
            builder.ToTable("Propostas");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NumeroProposta).IsRequired().HasMaxLength(50);
            builder.Property(p => p.TipoSeguro).IsRequired().HasMaxLength(100);

            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.DataCriacao).IsRequired();
            builder.Property(p => p.DataValidade);
            builder.Property(p => p.Premio).HasColumnType("decimal(18,2)");
            builder.Property(p => p.ValorCobertura).HasColumnType("decimal(18,2)");
            builder.Property(p => p.FormaPagamento).IsRequired().HasMaxLength(100);
            builder.Property(p => p.QuantidadeParcelas);
            builder.Property(p => p.CanalVenda).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Observacoes).HasMaxLength(500);

        }
    }
}
