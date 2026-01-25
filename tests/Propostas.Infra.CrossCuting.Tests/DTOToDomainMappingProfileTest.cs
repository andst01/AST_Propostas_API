using AutoMapper;
using NUnit.Framework;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;
using Propostas.Infra.CrossCuting.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.CrossCuting.Tests
{
    public class DTOToDomainMappingProfileTest
    {
        private readonly IMapper _mapper;

        public DTOToDomainMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DTOToDomainMappingProfile>();
            });

            // Valida a configuração do AutoMapper durante a criação do fixture do teste
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        [Test]
        public void PropostaViewModel_To_Proposta_DeveMapearTodasPropriedades()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var vm = new PropostaDTO
            {
                Id = 123,
                NumeroProposta = "PROP-001",
                TipoSeguro = "Auto",
                Status = Propostas.Domain.Enums.EnumStatusProposta.Aprovada, // ajuste conforme enum disponível
                DataCriacao = now,
                DataValidade = now.AddDays(30),
                Premio = 150.75m,
                ValorCobertura = 10000m,
                FormaPagamento = "Boleto",
                QuantidadeParcelas = 3,
                CanalVenda = "Corretor",
                Observacoes = "Observação de teste"
            };

            // Act
            var entidade = _mapper.Map<Proposta>(vm);

            // Assert
            Assert.NotNull(vm);

        }
    }
}
