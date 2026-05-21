using AutoFixture;
using NUnit.Framework;
using Propostas.Application.Map;
using Propostas.Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Application.Test
{
    [TestFixture]
    public class PropostaRequestToEntityTest
    {
        private PropostaRequestToEntity _mapper;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _mapper = new PropostaRequestToEntity();
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
            var request = _fixture.Create<PropostaRequest>();
            // Act
            var resultado = _mapper.Map(request);
            // Assert
            Assert.NotNull(resultado);
            Assert.That(resultado.NumeroProposta, Is.EqualTo(request.NumeroProposta));
            Assert.That(resultado.TipoSeguro, Is.EqualTo(request.TipoSeguro));
            Assert.That(resultado.DataCriacao, Is.EqualTo(request.DataCriacao));
            Assert.That(resultado.DataValidade, Is.EqualTo(request.DataValidade));
            Assert.That(resultado.FormaPagamento, Is.EqualTo(request.FormaPagamento));
            Assert.That(resultado.ValorCobertura, Is.EqualTo(request.ValorCobertura));
            Assert.That(resultado.Premio, Is.EqualTo(request.Premio));
            Assert.That(resultado.QuantidadeParcelas, Is.EqualTo(request.QuantidadeParcelas));
            Assert.That(resultado.CanalVenda, Is.EqualTo(request.CanalVenda));
            Assert.That(resultado.Observacoes, Is.EqualTo(request.Observacoes));
            Assert.That(resultado.IdCliente, Is.EqualTo(request.IdCliente));

        }

    }
}
