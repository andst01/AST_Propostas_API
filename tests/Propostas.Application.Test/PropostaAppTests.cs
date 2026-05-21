using AutoFixture;
using Moq;
using NUnit.Framework;
using Propostas.Application.DTO;
using Propostas.Application.Interfaces.Map;
using Propostas.Application.Request;
using Propostas.Domain.Entidade;
using Propostas.Domain.Enums;
using Propostas.Domain.Interfaces;

namespace Propostas.Application.Test
{
    public class PropostaAppTests : AppBaseTest<PropostaApp>
    {
        private Mock<IPropostaRepositorio> _repositorioMock = null!;
        private Mock<IMapBase<Proposta, PropostaRequest>> _mapRequestToEntityMock = null!;
        private Mock<IMapBase<PropostaDTO, Proposta>> _mapEntityToDtoMock = null!;
        private PropostaApp _app = null!;


        [SetUp]
        public void Setup()
        {
            _repositorioMock = new Mock<IPropostaRepositorio>();
            _mapEntityToDtoMock = new Mock<IMapBase<PropostaDTO, Proposta>>();
            _mapRequestToEntityMock = new Mock<IMapBase<Proposta, PropostaRequest>>();
            //_mapperMock = FreezeMock<IMapper>();


            _app = new PropostaApp(_repositorioMock.Object,
                                   _mapRequestToEntityMock.Object,
                                   _mapEntityToDtoMock.Object);
        }

        [Test]
        public async Task AdicionarAsync_DeveAdicionarERetornarViewModel()
        {
            var dto = Fixture.Build<PropostaDTO>()
                .Create();
            var request = Fixture.Create<PropostaRequest>();
            var entity = Fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .Without(p => p.Apolice)
                .Create();

            _mapRequestToEntityMock
                .Setup(m => m.Map(request))
                .Returns(entity);
            //_mapperMock
            //    .Setup(m => m.Map<Proposta>(request))
            //    .Returns(entity);

            _repositorioMock
                .Setup(r => r.AdicionarAsync(entity))
                .ReturnsAsync(entity);

            _mapEntityToDtoMock
                .Setup(m => m.Map(entity))
                .Returns(dto);

            //_mapperMock
            //    .Setup(m => m.Map<PropostaDTO>(entity))
            //    .Returns(dto);

            _repositorioMock.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var result = await _app.AdicionarAsync(request);


            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AdicionarAsync(entity), Times.Once);
        }

        [Test]
        public async Task AtualizarAsync_ComId_DeveAtualizar()
        {
            var dto = Fixture.Create<PropostaDTO>();
            var request = Fixture.Create<PropostaRequest>();
            var entity = Fixture.Build<Proposta>()
                 .Without(p => p.Cliente)
                 .Without(p => p.Apolice)
                 .Create();

            var id = Fixture.Create<int>();

            _mapRequestToEntityMock
                .Setup(m => m.Map(request))
                .Returns(entity);
            //_mapperMock.Setup(m => m.Map<Proposta>(request))
            //           .Returns(entity);

            _repositorioMock.Setup(r => r.AtualizarAsync(entity, id))
                            .ReturnsAsync(entity);

            _mapEntityToDtoMock.Setup(m => m.Map(entity))
                            .Returns(dto);

            //_mapperMock.Setup(m => m.Map<PropostaDTO>(entity))
            //           .Returns(dto);

            _repositorioMock.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var result = await _app.AtualizarAsync(request, id);

            Assert.NotNull(result);
            _repositorioMock.Verify(r => r.AtualizarAsync(entity, id), Times.Once);
        }

        [Test]
        public async Task ExcluirAsync_DeveChamarRepositorio()
        {
            var id = Fixture.Create<int>();

            _repositorioMock
                .Setup(r => r.ExcluirAsync(id))
                .Returns(Task.CompletedTask);

            _repositorioMock.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var result = await _app.ExcluirAsync(id);

            Assert.AreEqual(true, result.Mensagem.Sucesso);

            _repositorioMock.Verify(r => r.ExcluirAsync(id), Times.Once);
        }

        [Test]
        public async Task ObterTodosAsync_DeveRetornarLista()
        {
            var entities = Fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .Without(p => p.Apolice)
                .CreateMany<Proposta>(3).ToList();

            var dtos = Fixture.CreateMany<PropostaDTO>(3).ToList();

            _repositorioMock.Setup(r => r.ObterTodosAsync())
                            .ReturnsAsync(entities);

            //_mapperMock.Setup(m => m.Map<List<PropostaDTO>>(entities))
            //           .Returns(dtos);

            var result = await _app.ObterTodosAsync();

            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task ObterDadosPropostaClienteAsync_DeveRetornarLista()
        {

            var entities = Fixture.Build<Proposta>()
                .With(p => p.Cliente, new Cliente() { Id = 9, Nome = "Ana" })
                .Without(p => p.Apolice)
                .CreateMany<Proposta>(3).ToList();

            var dtos = Fixture.CreateMany<PropostaDTO>(3).ToList();

            _repositorioMock.Setup(r => r.ObterPropostaClienteAsync())
                            .ReturnsAsync(entities);

            //_mapperMock.Setup(m => m.Map<List<PropostaDTO>>(entities))
            //           .Returns(dtos);

            var result = await _app.ObterDadosPropostaClienteAsync();

            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task ObterPropostaAprovadaSemApoliceAsync_DeveRetornarLista()
        {

            var entities = Fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .Without(p => p.Apolice)
                .CreateMany<Proposta>(3).ToList();

            var dtos = Fixture.CreateMany<PropostaDTO>(3).ToList();

            _repositorioMock.Setup(r => r.ObterPropostaAprovadaSemApoliceAsync())
                            .ReturnsAsync(entities);

            //_mapperMock.Setup(m => m.Map<List<PropostaDTO>>(entities))
            //           .Returns(dtos);

            var result = await _app.ObterPropostaAprovadaSemApoliceAsync();

            Assert.AreEqual(3, result.Count);
        }


        [Test]
        public async Task ObterPorIdAsync_DeveRetornarLista()
        {
            var dto = Fixture.Create<PropostaDTO>();
            var entity = Fixture.Build<Proposta>()
                 .Without(p => p.Cliente)
                 .Without(p => p.Apolice)
                 .Create();

            _repositorioMock.Setup(r => r.ObterPorIdAssyn(entity.Id))
                            .ReturnsAsync(entity);

            //_mapperMock.Setup(m => m.Map<PropostaDTO>(entity))
            //           .Returns(dto);

            _mapEntityToDtoMock.Setup(m => m.Map(entity))
                            .Returns(dto);

            var result = await _app.ObterPorIdAssyn(entity.Id);

            Assert.NotNull(result);
        }

        [Test]
        public async Task ObterPropostaClientePorIdAsync_DeveRetornarProposta()
        {
            var dto = Fixture.Create<PropostaDTO>();
            var entity = Fixture.Build<Proposta>()
                 .With(p => p.Cliente, new Cliente() { Id = 9, Nome = "Ana" })
                 .Without(p => p.Apolice)
                 .Create();
            _repositorioMock.Setup(r => r.ObterPropostaClientePorIdAsync(entity.Id))
                            .ReturnsAsync(entity);
            //_mapperMock.Setup(m => m.Map<PropostaDTO>(entity))
            //           .Returns(dto);
            _mapEntityToDtoMock.Setup(m => m.Map(entity))
                            .Returns(dto);
            var result = await _app.ObterPropostaClientePorIdAsync(entity.Id);
            Assert.NotNull(result);

        }

        [Test]
        [TestCase(null, null, 1)]
        [TestCase("2024-01-01", null, 1)]
        [TestCase(null, "PROP-001", 1)]
        public async Task ObterTodosComFiltroAsync_DeveRetornarLista(DateTime? dataFiltro, string? numeroProposta, int status)
        {
            var entities = Fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .Without(p => p.Apolice)
                .With(x => x.DataCriacao, dataFiltro ?? DateTime.Now)
                .With(x => x.NumeroProposta, numeroProposta ?? "Teste")
                .With(x => x.Status, (EnumStatusProposta)status)
                .CreateMany<Proposta>(3).ToList();
            var dtos = Fixture.CreateMany<PropostaDTO>(3).ToList();
            _repositorioMock.Setup(r => r.ObterTodosComFiltroAsync(dataFiltro, numeroProposta, status))
                            .ReturnsAsync(entities);
            //_mapperMock.Setup(m => m.Map<List<PropostaDTO>>(entities))
            //           .Returns(dtos);
            var result = await _app.ObterTodosComFiltroAsync(dataFiltro, numeroProposta, status);
            Assert.AreEqual(3, result.Count);
        }
    }
}
