using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Propostas.Infra.CrossCuting.Config; // namespace da AutoMapperConfig

[TestFixture]
public class AutoMapperConfigTest
{
    private IServiceCollection _services;

    [SetUp]
    public void Setup()
    {
        _services = new ServiceCollection();
    }

    [Test]
    public void AddAutoMappingConfig_DeveRegistrarPerfisAutoMapper()
    {
        // Act
        _services.AddAutoMappingConfig();

        var provider = _services.BuildServiceProvider();
        var mapper = provider.GetService<IMapper>();

        // Assert
        Assert.NotNull(mapper);

        // Opcional: verificar se os perfis estão carregados
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Test]
    public void AddAutoMappingConfig_ServicesNulo_DeveLancarArgumentNullException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => AutoMapperConfig.AddAutoMappingConfig(null));
        Assert.AreEqual("services", ex.ParamName);
    }
}
