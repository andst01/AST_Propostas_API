using AutoFixture;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Propostas.Application.DTO;
using Propostas.Application.Request;
using Propostas.Domain.Entidade;
using Propostas.Domain.Interfaces;

namespace Propostas.Application.Test
{
    public class PropostaAppTests : AppBaseTest<PropostaApp>
    {
        private Mock<IPropostaRepositorio> _repositorioMock = null!;
        private Mock<IMapper> _mapperMock = null!;
        private PropostaApp _app = null!;


        [SetUp]
        public void Setup()
        {
            _repositorioMock = FreezeMock<IPropostaRepositorio>();
            _mapperMock = FreezeMock<IMapper>();

            _app = CreateSut();
        }

        [Test]
        public async Task AdicionarAsync_DeveAdicionarERetornarViewModel()
        {
            var dto = Fixture.Create<PropostaDTO>();
            var request = Fixture.Create<PropostaRequest>();
            var entity = Fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .Without(p => p.Apolice)
                .Create();

            _mapperMock
                .Setup(m => m.Map<Proposta>(request))
                .Returns(entity);

            _repositorioMock
                .Setup(r => r.AdicionarAsync(entity))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<PropostaDTO>(entity))
                .Returns(dto);

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

            _mapperMock.Setup(m => m.Map<Proposta>(request))
                       .Returns(entity);

            _repositorioMock.Setup(r => r.AtualizarAsync(entity, id))
                            .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map<PropostaDTO>(entity))
                       .Returns(dto);

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

            _mapperMock.Setup(m => m.Map<List<PropostaDTO>>(entities))
                       .Returns(dtos);

            var result = await _app.ObterTodosAsync();

            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task ObterDadosPropostaClienteAsync_DeveRetornarLista()
        {
            
            var entities = Fixture.Build<Proposta>()
                .With(p => p.Cliente, new Cliente() { Id = 9, Nome = "Ana"})
                .Without(p => p.Apolice)
                .CreateMany<Proposta>(3).ToList();

            var dtos = Fixture.CreateMany<PropostaDTO>(3).ToList();

            _repositorioMock.Setup(r => r.ObterDadosPropostaClienteAsync())
                            .ReturnsAsync(entities);

            _mapperMock.Setup(m => m.Map<List<PropostaDTO>>(entities))
                       .Returns(dtos);

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

            _mapperMock.Setup(m => m.Map<List<PropostaDTO>>(entities))
                       .Returns(dtos);

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

            _mapperMock.Setup(m => m.Map<PropostaDTO>(entity))
                       .Returns(dto);

            var result = await _app.ObterPorIdAssyn(entity.Id);

            Assert.NotNull(result);
        }




    }
}
