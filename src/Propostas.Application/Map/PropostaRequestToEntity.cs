using Propostas.Application.Interfaces.Map;
using Propostas.Application.Request;
using Propostas.Domain.Entidade;
using Propostas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Application.Map
{
    public class PropostaRequestToEntity : IMapBase<Proposta, PropostaRequest>
    {
        public Proposta Map(PropostaRequest source)
        {
            if (source == null) return null;

            var proposta = new Proposta
            {
                Id = source.Id,
                NumeroProposta = source.NumeroProposta,
                TipoSeguro = source.TipoSeguro,
                DataCriacao = source.DataCriacao,
                DataValidade = source.DataValidade,
                Premio = source.Premio,
                ValorCobertura = source.ValorCobertura,
                FormaPagamento = source.FormaPagamento,
                QuantidadeParcelas = source.QuantidadeParcelas,
                CanalVenda = source.CanalVenda,
                Observacoes = source.Observacoes,
                IdCliente = source.IdCliente,
                Status = (EnumStatusProposta)source.CodigoStatus
            };

            return proposta;
        }
    }
}
