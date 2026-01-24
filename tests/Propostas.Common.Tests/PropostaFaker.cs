using Bogus;
using Propostas.Domain.Entidade;
using Propostas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Common.Tests
{
    public static class PropostaFaker
    {
        public static Faker<Proposta> GerarFake()
        {
            return new Faker<Proposta>()
                .RuleFor(p => p.Id, f => 0) // Id será gerado pelo DB
                //.RuleFor(p => p.Status, f => f.PickRandom(new[] { "Ativa", "Inativa", "Pendente" }))
                .RuleFor(p => p.DataCriacao, f => f.Date.Recent(30))
                .RuleFor(p => p.DataValidade, (f, p) => p.DataCriacao.AddDays(f.Random.Int(30, 365)))
                .RuleFor(p => p.Premio, f => f.Finance.Amount(100, 10000))
                .RuleFor(p => p.ValorCobertura, f => f.Finance.Amount(1000, 100000))
                .RuleFor(p => p.FormaPagamento, f => f.PickRandom(new[] { "Boleto", "Cartão de Crédito", "Débito Automático" }))
                .RuleFor(p => p.QuantidadeParcelas, f => f.Random.Int(1, 12))
                .RuleFor(p => p.CanalVenda, f => f.PickRandom(new[] { "Online", "Corretor", "Agência" }))
                .RuleFor(p => p.Observacoes, f => f.Lorem.Paragraph())
                .RuleFor(p => p.NumeroProposta, (f, p) => $"PROP-{f.Date.Between(p.DataCriacao, p.DataValidade).ToString("yyyyMMdd")}-{f.Random.Number(1000, 9999)}")
                .RuleFor(p => p.TipoSeguro, f => f.PickRandom(new[] { "Vida", "Automóvel", "Residencial", "Saúde" }))
                .RuleFor(p => p.Status, f => f.PickRandom<EnumStatusProposta>());
        }

        public static Faker<Proposta> DadosPersistidosFake()
        {
            return new Faker<Proposta>()
                .RuleFor(p => p.Id, f => 1) // Id será gerado pelo DB
                                            //.RuleFor(p => p.Status, f => f.PickRandom(new[] { "Ativa", "Inativa", "Pendente" }))
                .RuleFor(p => p.DataCriacao, f => f.Date.Recent(30))
                .RuleFor(p => p.DataValidade, (f, p) => p.DataCriacao.AddDays(f.Random.Int(30, 365)))
                .RuleFor(p => p.Premio, f => f.Finance.Amount(100, 10000))
                .RuleFor(p => p.ValorCobertura, f => f.Finance.Amount(1000, 100000))
                .RuleFor(p => p.FormaPagamento, f => f.PickRandom(new[] { "Boleto", "Cartão de Crédito", "Débito Automático" }))
                .RuleFor(p => p.QuantidadeParcelas, f => f.Random.Int(1, 12))
                .RuleFor(p => p.CanalVenda, f => f.PickRandom(new[] { "Online", "Corretor", "Agência" }))
                .RuleFor(p => p.Observacoes, f => f.Lorem.Paragraph())
                .RuleFor(p => p.NumeroProposta, (f, p) => $"PROP-{f.Date.Between(p.DataCriacao, p.DataValidade).ToString("yyyyMMdd")}-{f.Random.Number(1000, 9999)}")
                .RuleFor(p => p.TipoSeguro, f => f.PickRandom(new[] { "Vida", "Automóvel", "Residencial", "Saúde" }))
                .RuleFor(p => p.Status, f => f.PickRandom<EnumStatusProposta>());
        }
    }
}
