using Propostas.Application.DTO;
using Propostas.Application.Interfaces.Map;
using Propostas.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Application.Map
{
    public class PropostaEntityToDto : IMapBase<PropostaDTO, Proposta>
    {
        public PropostaDTO Map(Proposta source)
        {
            if (source == null) return null;

            var propostaDto = new PropostaDTO
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
                CodigoStatus = (int)source.Status,
                NomeClienteCpf = source.Cliente == null 
                                    ? null 
                                    :  $"{source.Cliente.Nome} - {source.Cliente.CpfCnpj}",
                NumeroApolice = source.Apolice == null 
                                    ? null 
                                    : source.Apolice.NumeroApolice

            };

            return propostaDto;
        }
    }
}
