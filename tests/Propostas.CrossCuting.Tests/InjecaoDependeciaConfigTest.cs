using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Propostas.Infra.CrossCuting.Config;
using System;

[TestFixture]
public class InjecaoDependeciaConfigTest
{
    private IServiceCollection _services;

    [SetUp]
    public void Setup()
    {
        _services = new ServiceCollection();
    }

    [Test]
    public void AddInjecaoDependeciaConfig_ServicesNull_DeveLancarArgumentNullException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            InjecaoDependeciaConfig.AddInjecaoDependeciaConfig(null));

        Assert.AreEqual("services", ex.ParamName);
    }

    [Test]
    public void AddInjecaoDependeciaConfig_DeveExecutarSemErro()
    {
        // Act
        InjecaoDependeciaConfig.AddInjecaoDependeciaConfig(_services);

        // Assert
        Assert.NotNull(_services);
        Assert.IsTrue(_services.Count > 0, "O container deve conter serviços registrados pelo NativeInjectorBootStrapper");
    }
}
