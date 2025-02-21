using NUnit.Framework;
using Moq;
using WebApiSAC.Controllers;
using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebApiSAC.Dtos;
using AutoMapper;
using System.Threading.Tasks;
using Core.Entities;
using Core.Request;
using Core.Response;

namespace WebApiSAC.Tests;

[TestFixture]
public class SolicitudControllerTests
{
    private SolicitudController _controller;
    private Mock<ISolicitudService> _serviceMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        // Crear instancias simuladas de las dependencias
        var serviceMock = Mock.Of<ISolicitudService>();
        var mapperMock = Mock.Of<IMapper>();

        // Inicializar el controlador con dependencias simuladas
        _controller = new SolicitudController(serviceMock, mapperMock);
    }

    [Test]
    public void Ping_ReturnsOkResult()
    {
        // Act
        var result = _controller.Ping();

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());  // Verifica que el resultado es un OkObjectResult
        var okResult = result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo("Escuchando"));  // Verifica el contenido del OkResult
    }

    /*[Test]
    public async Task Upsert_CreatesNewSolicitud_WhenIdIsZero()
    {
        // Arrange
        var solicitudReqDto = new SolicitudReqDto { so_ts_id = 1, so_descripcion = "test de pruebas" };

        var solicitudRequest = new SolicitudRequest { so_ts_id = 1, so_descripcion = "test de pruebas" };

        // Configura el mock del mapeo
        var _mapperMock = new Mock<IMapper>();
        _mapperMock.Setup(mapper => mapper.Map<SolicitudRequest>(It.IsAny<SolicitudReqDto>())).Returns(solicitudRequest);


        // Configura el mock del servicio para que devuelva una nueva solicitud
        _serviceMock.Setup(service => service.Add(solicitudRequest, It.IsAny<CancellationToken>())).ReturnsAsync(new Solicitudes());


        // Act
        var result = await _controller.Upsert(solicitudReqDto, It.IsAny<CancellationToken>());

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);  // Verifica que el resultado no sea nulo
        Assert.IsInstanceOf<Solicitudes>(okResult.Value); // Verifica que el valor retornado sea una instancia de Solicitud

 
    }*/
}