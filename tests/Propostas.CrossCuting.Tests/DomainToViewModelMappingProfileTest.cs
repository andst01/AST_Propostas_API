using AutoMapper;
using NUnit.Framework;
using Propostas.Application.ViewModels;
using Propostas.Domain.Entidade;
using Propostas.Infra.CrossCuting.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Propostas.CrossCuting.Tests
{
    [TestFixture]
    public class DomainToViewModelMappingProfileTest
    {
        private readonly IMapper _mapper;

        public DomainToViewModelMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
            });

            // Valida a configuração do AutoMapper durante a criação do fixture do teste
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        [Test] 
        public void Proposta_To_PropostaViewModel_DeveMapearTodasPropriedades()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var entidade = new Proposta
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
            var vm = _mapper.Map<PropostaViewModel>(entidade);

            // Assert
            Assert.NotNull(vm);
           
        }
    }
}
