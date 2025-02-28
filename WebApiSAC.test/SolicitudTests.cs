using Moq;
using NUnit.Framework;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using WebApiSAC.Controllers;
using Core.Contracts;
using Core.Entities;
using Core.Request;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Core.Response;
using WebApiSAC.Dtos;

namespace WebApiSAC.Tests
{
    [TestFixture]
    public class SolicitudTests
    {
        private Mock<ISolicitudService> _mockService;
        private Mock<IMapper> _mockMapper;
        private SolicitudController _controller;

        [SetUp]
        public void SetUp()
        {
            // Crear mocks para las dependencias
            _mockService = new Mock<ISolicitudService>();
            _mockMapper = new Mock<IMapper>();

            // Inicializar el controlador con las dependencias mockeadas
            _controller = new SolicitudController(_mockService.Object, _mockMapper.Object);
        }

        #region Ping Test
        [Test]
        public void Ping_ReturnsOkResult_WithCorrectMessage()
        {
            // Act
            var result = _controller.Ping();

            // Assert
            result.ShouldBeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.ShouldBe("Escuchando");
        }
        #endregion

        #region Upsert Test
        [Test]
        public async Task Upsert_ShouldReturnOk_WhenSolicitudIsCreated()
        {
            // Arrange
            var solicitudReqText = "{\"so_descripcion\":\"Descripción de la solicitud MYSQL\",\"so_ts_id\":1,\"so_es_id\":1}";
            var fileMock = new Mock<IFormFile>();
            var solicitudReqDto = new SolicitudReqDto
            {
                so_descripcion = "Descripción de la solicitud MYSQL",
                so_ts_id = 1,
                so_es_id = 1
            };
            var solicitudRequest = new SolicitudRequest
            {
                so_descripcion = solicitudReqDto.so_descripcion,
                so_ts_id = solicitudReqDto.so_ts_id,
                so_es_id = solicitudReqDto.so_es_id
            };

            _mockMapper.Setup(m => m.Map<SolicitudRequest>(It.IsAny<SolicitudReqDto>())).Returns(solicitudRequest);
            _mockService.Setup(s => s.Add(It.IsAny<SolicitudRequest>(), fileMock.Object, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new SolicitudResponse());  // Simula la respuesta de Add

            // Act
            var result = await _controller.Upsert(solicitudReqText, fileMock.Object, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<OkObjectResult>(); // El resultado debe ser un OkObjectResult
            var okResult = result as OkObjectResult;
            okResult.Value.ShouldNotBeNull();
        }
        #endregion

        [Test]
        public async Task GetAll_ShouldReturnOk_WhenRequestsAreFound()
        {
            // Arrange
            var solicitudResponse = new List<SolicitudResponse>
            {
                new SolicitudResponse { so_id = 1, so_descripcion = "Solicitud 1" },
                new SolicitudResponse { so_id = 2, so_descripcion = "Solicitud 2" }
            };

                    var solicitudResDto = new List<SolicitudResDto>
            {
                new SolicitudResDto { so_id = 1, so_descripcion = "Solicitud 1" },
                new SolicitudResDto { so_id = 2, so_descripcion = "Solicitud 2" }
            };

            _mockService.Setup(s => s.GetAll())
                        .ReturnsAsync(solicitudResponse);

            _mockMapper.Setup(m => m.Map<IEnumerable<SolicitudResDto>>(It.IsAny<IEnumerable<SolicitudResponse>>()))
                       .Returns(solicitudResDto);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.ShouldBeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.ShouldBeOfType<List<SolicitudResDto>>();
            var data = okResult.Value as List<SolicitudResDto>;
            data.Count.ShouldBe(2);
        }

        [Test]
        public async Task SolicitudEmail_ShouldReturnOk_WhenRequestsAreFound()
        {
            // Arrange
            var email = "cabra_js@javeriana.edu.co";
            var solicitudResponse = new List<SolicitudResponse>
            {
                new SolicitudResponse { so_id = 1, so_descripcion = "Solicitud 1" },
                new SolicitudResponse { so_id = 2, so_descripcion = "Solicitud 2" }
            };

                    var solicitudResDto = new List<SolicitudResDto>
            {
                new SolicitudResDto { so_id = 1, so_descripcion = "Solicitud 1" },
                new SolicitudResDto { so_id = 2, so_descripcion = "Solicitud 2" }
            };

            _mockService.Setup(s => s.GetByEmail(email))
                        .ReturnsAsync(solicitudResponse);

            _mockMapper.Setup(m => m.Map<IEnumerable<SolicitudResDto>>(It.IsAny<IEnumerable<SolicitudResponse>>()))
                       .Returns(solicitudResDto);

            // Act
            var result = await _controller.SolicitudEmail(email);

            // Assert
            result.ShouldBeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.ShouldBeOfType<List<SolicitudResDto>>();
            var data = okResult.Value as List<SolicitudResDto>;
            data.Count.ShouldBe(2);
        }

        [Test]
        public async Task SolicitudNumber_ShouldReturnOk_WhenRequestsAreFound()
        {
            // Arrange
            var number = "A00000001";
            var solicitudResponse = new List<SolicitudResponse>
            {
                new SolicitudResponse { so_id = 1, so_descripcion = "Solicitud 1" },
                new SolicitudResponse { so_id = 2, so_descripcion = "Solicitud 2" }
            };

                    var solicitudResDto = new List<SolicitudResDto>
            {
                new SolicitudResDto { so_id = 1, so_descripcion = "Solicitud 1" },
                new SolicitudResDto { so_id = 2, so_descripcion = "Solicitud 2" }
            };

            _mockService.Setup(s => s.GetByNumber(number))
                        .ReturnsAsync(solicitudResponse);

            _mockMapper.Setup(m => m.Map<IEnumerable<SolicitudResDto>>(It.IsAny<IEnumerable<SolicitudResponse>>()))
                       .Returns(solicitudResDto);

            // Act
            var result = await _controller.SolicitudNumber(number);

            // Assert
            result.ShouldBeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.ShouldBeOfType<List<SolicitudResDto>>();
            var data = okResult.Value as List<SolicitudResDto>;
            data.Count.ShouldBe(2);
        }

    }
}
