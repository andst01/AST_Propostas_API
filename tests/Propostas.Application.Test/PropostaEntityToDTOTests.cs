using AutoFixture;
using NUnit.Framework;
using Propostas.Application.Map;
using Propostas.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Application.Test
{
    [TestFixture]
    public class PropostaEntityToDTOTests
    {
        private PropostaEntityToDto _mapper;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _mapper = new PropostaEntityToDto();
            _fixture = new Fixture();
        }

        [Test]
        public void Map_QuandoObjetoOrigemForNulo_DeveRetornarNulo()
        {
            // Act
            var resultado = _mapper.Map(null);

            // Assert
            Assert.That(resultado, Is.Null);
        }

        [Test]
        public void Map_QuandoObjetoOrigemForValido_DeveMapearPropriedadesCorretamente()
        {
            // Arrange
            var proposta = _fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .Without(p => p.Apolice)
                .Create();
            // Act
            var resultado = _mapper.Map(proposta);

            Assert.NotNull(resultado);
            // Assert
            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.Id, Is.EqualTo(proposta.Id));
            Assert.That(resultado.CodigoStatus, Is.EqualTo((int)proposta.Status));
            Assert.That(resultado.IdCliente, Is.EqualTo(proposta.IdCliente));
            Assert.That(resultado.DataCriacao, Is.EqualTo(proposta.DataCriacao));
            Assert.That(resultado.DataValidade, Is.EqualTo(proposta.DataValidade));
            Assert.That(resultado.FormaPagamento, Is.EqualTo(proposta.FormaPagamento));
            Assert.That(resultado.ValorCobertura, Is.EqualTo(proposta.ValorCobertura));
            Assert.That(resultado.Premio, Is.EqualTo(proposta.Premio));
            Assert.That(resultado.QuantidadeParcelas, Is.EqualTo(proposta.QuantidadeParcelas));
            Assert.That(resultado.CanalVenda, Is.EqualTo(proposta.CanalVenda));
            Assert.That(resultado.Observacoes, Is.EqualTo(proposta.Observacoes));
            
        }

    }
}
