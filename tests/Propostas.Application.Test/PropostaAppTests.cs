using AutoFixture;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;
using Propostas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Application.Test
{
    public class PropostaAppTests : AppBaseTest<PropostaApp>
    {
        private Mock<IRepositorioBase<Proposta>> _repositorioMock = null!;
        private Mock<IMapper> _mapperMock = null!;
        private PropostaApp _app = null!;


        [SetUp]
        public void Setup()
        {
            _repositorioMock = FreezeMock<IRepositorioBase<Proposta>>();
            _mapperMock = FreezeMock<IMapper>();

            _app = CreateSut();
        }

        [Test]
        public async Task AdicionarAsync_DeveAdicionarERetornarViewModel()
        {
            var dto = Fixture.Create<PropostaDTO>();
            var entity = Fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .Create();

            _mapperMock
                .Setup(m => m.Map<Proposta>(dto))
                .Returns(entity);

            _repositorioMock
                .Setup(r => r.AdicionarAsync(entity))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<PropostaDTO>(entity))
                .Returns(dto);

            var result = await _app.AdicionarAsync(dto);

            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AdicionarAsync(entity), Times.Once);
        }

        [Test]
        public async Task AtualizarAsync_ComId_DeveAtualizar()
        {
            var dto = Fixture.Create<PropostaDTO>();
            var entity = Fixture.Build<Proposta>()
                 .Without(p => p.Cliente)
                 .Create();
            var id = Fixture.Create<int>();

            _mapperMock.Setup(m => m.Map<Proposta>(dto))
                       .Returns(entity);

            _repositorioMock.Setup(r => r.AtualizarAsync(entity, id))
                            .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map<PropostaDTO>(entity))
                       .Returns(dto);

            var result = await _app.AtualizarAsync(dto, id);

            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AtualizarAsync(entity, id), Times.Once);
        }

        [Test]
        public async Task ExcluirAsync_DeveChamarRepositorio()
        {
            var id = Fixture.Create<int>();

            _repositorioMock
                .Setup(r => r.ExcluirAsync(id))
                .ReturnsAsync(1);

            var result = await _app.ExcluirAsync(id);

            Assert.AreEqual(1, result);
            _repositorioMock.Verify(r => r.ExcluirAsync(id), Times.Once);
        }

        [Test]
        public async Task ObterTodosAsync_DeveRetornarLista()
        {
            var entities = Fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .CreateMany<Proposta>(3).ToList();

            var dtos = Fixture.CreateMany<PropostaDTO>(3).ToList();

            _repositorioMock.Setup(r => r.ObterTodosAsync())
                            .ReturnsAsync(entities);

            _mapperMock.Setup(m => m.Map<List<PropostaDTO>>(entities))
                       .Returns(dtos);

            var result = await _app.ObterTodosAsync();

            Assert.AreEqual(3, result.Count);
        }


        [Test]
        public async Task ObterPorIdAsync_DeveRetornarLista()
        {
            var dto = Fixture.Create<PropostaDTO>();
            var entity = Fixture.Build<Proposta>()
                 .Without(p => p.Cliente)
                 .Create();

            _repositorioMock.Setup(r => r.ObterPorIdAssyn(entity.Id))
                            .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map<PropostaDTO>(entity))
                       .Returns(dto);

            var result = await _app.ObterPorIdAssyn(entity.Id);

            Assert.NotNull(result);
        }




    }
}
