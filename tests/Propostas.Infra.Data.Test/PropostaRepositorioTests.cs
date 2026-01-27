using AutoFixture;
using AutoFixture.AutoMoq;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Propostas.Domain.Entidade;
using Propostas.Infra.Data.Contexto;
using Propostas.Infra.Data.Repositorio;

namespace Propostas.Infra.Data.Test
{
    [TestFixture]
    public class PropostaRepositorioTests
    {
        private PropostaRepositorio _repositorio;
        private PropostaDbContext _context;
        protected IFixture Fixture = null!;



        [SetUp]
        public void ResetDatabase()
        {
            Fixture = new Fixture()
               .Customize(new AutoMoqCustomization
               {
                   ConfigureMembers = true
               });
            _context = PropostaDbContextTest.CreateContext();
            _repositorio = new PropostaRepositorio(_context);
            
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AdicionarAsync_DevePersistir()
        {
            var proposta = Fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .Without(p => p.Apolice)
                .Create();
            
            await _repositorio.AdicionarAsync(proposta);

            var entry = _context.Entry(proposta);

            Assert.AreEqual(EntityState.Added, entry.State);
        }


        [Test]
        public async Task AtualizarAsync_DevePersistir()
        {

            var proposta = Fixture.Build<Proposta>()
                .Without(p => p.Cliente)
                .Without(p => p.Apolice)
                .Create();
            proposta.NumeroProposta = "Atualizado-123";
            await _repositorio.AdicionarAsync(proposta);

            var entry = _context.Entry(proposta);
            await _repositorio.AtualizarAsync(proposta, proposta.Id);

            Assert.AreEqual(EntityState.Modified, entry.State);


        }

       

        [Test]
        public async Task ExcluirAsync_DevePersistir()
        {
            var proposta = Fixture.Build<Proposta>()
                                        .Without(p => p.Cliente)
                                        .Without(p => p.Apolice)
                                        .Create();
            await _repositorio.AdicionarAsync(proposta);

            await _repositorio.ExcluirAsync(proposta.Id);

            Assert.AreEqual(0, _context.Set<Proposta>().Count());
        }

        [Test]
        public async Task ObterPorIdAsync_DevePersistir()
        {
            var proposta = Fixture.Build<Proposta>()
                                        .Without(p => p.Cliente)
                                        .Without(p => p.Apolice)
                                        .Create();

            await _repositorio.AdicionarAsync(proposta);
            var retorno = await _repositorio.ObterPorIdAssyn(proposta.Id);

            Assert.NotNull(retorno);


        }

        [Test]
        public async Task ObterTodosdAsync_DevePersistir()
        {
            var proposta = Fixture.Build<Proposta>()
                                        .Without(p => p.Cliente)
                                        .Without(p => p.Apolice)
                                        .Create();
            await _repositorio.AdicionarAsync(proposta);
            await _repositorio.SaveChangesAsync();

            var retorno = await _repositorio.ObterTodosAsync();

            Assert.AreEqual(1, _context.Set<Proposta>().Count());
        }

        [Test]
        public async Task ObterDadosPropostaClienteAsync_DevePersistir()
        {
            var proposta = Fixture.Build<Proposta>()
                                        .Without(p => p.Cliente)
                                        .Without(p => p.Apolice)
                                        .Create();
            await _repositorio.AdicionarAsync(proposta);
            await _repositorio.SaveChangesAsync();
            var retorno = await _repositorio.ObterDadosPropostaClienteAsync();

            Assert.AreEqual(1, _context.Set<Proposta>().Count());
        }

        [Test]
        public async Task ObterPropostaAprovadaSemApoliceAsync_DevePersistir()
        {
            var proposta = Fixture.Build<Proposta>()
                                        .Without(p => p.Cliente)
                                        .Without(p => p.Apolice)
                                        .Create();
            await _repositorio.AdicionarAsync(proposta);
            await _repositorio.SaveChangesAsync();
            var retorno = await _repositorio.ObterPropostaAprovadaSemApoliceAsync();

            Assert.AreEqual(1, _context.Set<Proposta>().Count());
        }

        [Test]
        public async Task ObterPorFiltroAsync_FiltraCorretamente()
        {
            

            var proposta1 = Fixture.Build<Proposta>()
                                        .Without(p => p.Cliente)
                                        .Without(p => p.Apolice)
                                        .Create();
            proposta1.NumeroProposta = "A123";

            var proposta2 = Fixture.Build<Proposta>()
                                        .Without(p => p.Cliente)
                                        .Without(p => p.Apolice)
                                        .Create();
            proposta2.NumeroProposta = "B456";

            await _context.Set<Proposta>().AddRangeAsync(proposta1, proposta2);
            await _context.SaveChangesAsync();

            // Act - filtra por NumeroProposta
            var result = await _repositorio.ObterPorFiltroAsync(p => p.NumeroProposta.StartsWith("A"));

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("A123", result.First().NumeroProposta);
        }


    }
}
