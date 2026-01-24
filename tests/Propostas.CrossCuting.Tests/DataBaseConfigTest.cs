using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Propostas.Infra.CrossCuting.Config;
using Propostas.Infra.Data.Contexto;
using System;

namespace Propostas.CrossCuting.Tests
{
    [TestFixture]
    public class DataBaseConfigTest
    {
        private IServiceCollection _services;
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private Mock<IConfigurationSection> _mockSection;

        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();

            var inMemorySettings = new Dictionary<string, string>
        {
            { "ConnectionStrings:DefaultConnectionString",
              "Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;" }
        };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        [Test]
        public void AddDataBaseConfiguration_ServicesNull_DeveLancarArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                DataBaseConfig.AddDataBaseConfiguration(null, _configuration));

            Assert.AreEqual("services", ex.ParamName);
        }

        [Test]
        public void AddDataBaseConfiguration_DeveRegistrarDbContext()
        {
            // Act
            _services.AddDataBaseConfiguration(_configuration);
            var provider = _services.BuildServiceProvider();

            // Assert
            var dbContext = provider.GetService<PropostaDbContext>();
            Assert.NotNull(dbContext);

            // Opcional: verificar se o provider é do tipo correto
            Assert.IsInstanceOf<PropostaDbContext>(dbContext);
        }
    }
}
