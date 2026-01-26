using AutoFixture;
using AutoFixture.AutoMoq;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Propostas.Api.Controllers;
using Propostas.Application.DTO;
using Propostas.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestFixture]
public class PropostaControllerTest
{
    private Mock<IPropostaApp> _mockApp;
    private Mock<ILogger<PropostaController>> _mockLogger;
    private PropostaController _controller;
    private IFixture Fixture; 
    [SetUp]
    public void Setup()
    {
        _mockApp = new Mock<IPropostaApp>();
        _mockLogger = new Mock<ILogger<PropostaController>>();
        Fixture = new Fixture()
                .Customize(new AutoMoqCustomization
                {
                    ConfigureMembers = true
                });
        _controller = new PropostaController(_mockApp.Object, _mockLogger.Object);
    }

    [Test]
    public async Task ObterPorId_DeveRetornarOkComProposta()
    {
        // Arrange
        var id = 1;
        var propostaVm = new PropostaDTO { Id = id, NumeroProposta = "PROP-001" };

        _mockApp.Setup(a => a.ObterPorIdAssyn(id))
                .ReturnsAsync(propostaVm);

        // Act
        var result = await _controller.ObterPorId(id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.AreEqual(propostaVm, okResult.Value);
    }

    [Test]
    public async Task ObterTodos_DeveRetornarOkComLista()
    {
        // Arrange
        var lista = new List<PropostaDTO>
        {
            new PropostaDTO { Id = 1, NumeroProposta = "PROP-001" },
            new PropostaDTO { Id = 2, NumeroProposta = "PROP-002" }
        };

        _mockApp.Setup(a => a.ObterTodosAsync())
                .ReturnsAsync(lista);

        // Act
        var result = await _controller.ObterTodos();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.AreEqual(lista, okResult.Value);
    }

    [Test]
    public async Task ObterDadosPropostaClienteAsync_DeveRetornarOkComLista()
    {
        // Arrange
        
        var lista = Fixture.Create<List<PropostaDTO>>();

        _mockApp.Setup(a => a.ObterDadosPropostaClienteAsync())
                .ReturnsAsync(lista);

        // Act
        var result = await _controller.ObterDadosPropostaClienteAsync();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.AreEqual(lista, okResult.Value);
    }

    [Test]
    public async Task ObterPropostaAprovadaSemApoliceAsync_DeveRetornarOkComLista()
    {
        // Arrange

        var lista = Fixture.Create<List<PropostaDTO>>();

        _mockApp.Setup(a => a.ObterPropostaAprovadaSemApoliceAsync())
                .ReturnsAsync(lista);

        // Act
        var result = await _controller.ObterPropostaAprovadaSemApoliceAsync();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.AreEqual(lista, okResult.Value);
    }

    [Test]
    public async Task New_DeveAdicionarPropostaERetornarOk()
    {
        // Arrange
        var request = new PropostaDTO { Id = 1, NumeroProposta = "PROP-001" };

        _mockApp.Setup(a => a.AdicionarAsync(request))
                .ReturnsAsync(request);

        // Act
        var result = await _controller.Novo(request);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.AreEqual(request, okResult.Value);
    }

    [Test]
    public async Task Update_DeveAtualizarPropostaERetornarOk()
    {
        // Arrange
        var request = new PropostaDTO { Id = 1, NumeroProposta = "PROP-001" };

        _mockApp.Setup(a => a.AtualizarAsync(request, request.Id))
                .ReturnsAsync(request);

        // Act
        var result = await _controller.Atualizar(request);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.AreEqual(request, okResult.Value);
    }

    [Test]
    public async Task Excluir_DeveExcluirPropostaERetornarOk()
    {
        // Arrange
        var request = new PropostaDTO { Id = 1, NumeroProposta = "PROP-001" };

        _mockApp.Setup(a => a.ExcluirAsync(request.Id))
                .ReturnsAsync(0);

        // Act
        var result = await _controller.Excluir(request.Id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
       
    }
}
