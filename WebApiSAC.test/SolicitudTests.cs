using AutoMapper;
using Core.Contracts;
using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using WebApiSAC.Controllers;
using WebApiSAC.Dtos;
using NUnit.Framework;


namespace WebApiSAC.Tests
{
    public class SolicitudTests
    {
        private Mock<ISolicitudService> _mockSolicitudService;
        private SolicitudController _controller;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void SetUp()
        {
            _mockSolicitudService = new Mock<ISolicitudService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new SolicitudController(_mockSolicitudService.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Upsert_CreatesSolicitud_WhenIdIsZero()
        {


            // Arrange
            var solicitudReqDto = new SolicitudReqDto { so_id = 0, so_ts_id = 1, so_descripcion = "Prueba de nunit reqdto" };
            var solicitudRequest = new SolicitudRequest { so_id = 0, so_ts_id = 1, so_descripcion = "Prueba de nunit request" };

            // Aquí generamos un valor dinámico para so_id y so_numero_solicitud
            var expectedResponse = new SolicitudResponse
            {
                so_id = 1, // O el valor que sea generado dinámicamente durante la ejecución
                so_numero_solicitud = "A000000001" // O el valor generado dinámicamente
            };

            // Configurar los mocks
            _mockMapper.Setup(m => m.Map<SolicitudRequest>(It.IsAny<SolicitudReqDto>()))
                .Returns(solicitudRequest);
            _mockSolicitudService.Setup(service => service.Add(solicitudRequest, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Upsert(solicitudReqDto, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.ShouldNotBeNull();  // Verifica que el resultado no sea nulo
            okResult.Value.ShouldBeOfType<SolicitudResponse>();  // Verifica que el valor retornado sea una instancia de SolicitudResponse

            var solicitudResult = okResult.Value as SolicitudResponse;
            // Verifica que el valor de 'so_id' y 'so_numero_solicitud' sean correctos
            solicitudResult.so_id.ShouldNotBe(0);  // Verifica que 'so_id' no sea igual a 0

            solicitudResult.so_numero_solicitud.ShouldNotBeNullOrEmpty();  // Verifica que 'so_numero_solicitud' no sea nulo o vacío

            // Verifica que el servicio Add haya sido llamado una vez
            _mockSolicitudService.Verify(service => service.Add(It.IsAny<SolicitudRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Upsert_UpdatesSolicitud_WhenIdIsNotZero()
        {
            // Arrange
            var solicitudReqDto = new SolicitudReqDto { so_id = 1, so_ts_id = 1, so_descripcion = "Prueba de nunit reqdto para update" };
            var solicitudRequest = new SolicitudRequest { so_id = 1, so_ts_id = 1, so_descripcion = "Prueba de nunit request para update" };

            // Configurar el mock para que devuelva 'true' cuando se llame al método Update
            _mockMapper.Setup(m => m.Map<SolicitudRequest>(It.IsAny<SolicitudReqDto>()))
                .Returns(solicitudRequest);

            _mockSolicitudService.Setup(service => service.Update(solicitudRequest.so_id, solicitudRequest, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true); // Aquí cambiamos el valor devuelto a 'true' ya que el método ahora retorna un booleano

            // Act
            var result = await _controller.Upsert(solicitudReqDto, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.ShouldNotBeNull();  // Verifica que el resultado no sea nulo
            okResult.Value.ShouldBeOfType<bool>();  // Verifica que el valor retornado sea de tipo 'bool'

            var updateResult = (bool)okResult.Value; // Convierte el valor retornado a booleano

            updateResult.ShouldBe(true);  // Verifica que el resultado sea 'true', indicando que la actualización fue exitosa

            // Verifica que el servicio Update haya sido llamado una vez
            _mockSolicitudService.Verify(service => service.Update(It.IsAny<int>(), It.IsAny<SolicitudRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        //Para eliminar una solicitud
        /*[Test]
        public async Task Delete_RemovesSolicitud_WhenIdIsValid()
        {
            // Arrange
            var solicitudId = 1; // ID de la solicitud a eliminar

            // Configurar el mock para que devuelva 'true' cuando se llame a Delete
            _mockSolicitudService.Setup(service => service.Delete(solicitudId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true); // Asumimos que la eliminación es exitosa

            // Act
            var result = await _controller.Delete(solicitudId, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.ShouldNotBeNull();  // Verifica que el resultado no sea nulo
            okResult.Value.ShouldBeOfType<bool>();  // Verifica que el valor retornado sea un bool

            var deleteResult = (bool)okResult.Value;
            deleteResult.ShouldBe(true);  // Verifica que el resultado sea 'true', indicando que la eliminación fue exitosa

            // Verifica que el servicio Delete haya sido llamado una vez
            _mockSolicitudService.Verify(service => service.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }*/

        //Para obtener todas las solicitudes
        /*[Test]
        public async Task GetAll_ReturnsOkResult_WithListOfSolicitudes()
        {
            // Arrange
            var expectedResponse = new List<SolicitudResponse>
                {
                    new SolicitudResponse { so_id = 1, so_numero_solicitud = "A00000001" },
                    new SolicitudResponse { so_id = 2, so_numero_solicitud = "A00000002" }
                };

            // Configurar el mock para que devuelva la lista de respuestas
            _mockSolicitudService.Setup(service => service.GetAll())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.ShouldNotBeNull();  // Verifica que el resultado no sea nulo
            okResult.Value.ShouldBeOfType<IEnumerable<SolicitudResponse>>();  // Verifica que el valor retornado sea una lista de SolicitudResponse

            var solicitudesResult = okResult.Value as IEnumerable<SolicitudResponse>;
            solicitudesResult.ShouldNotBeEmpty();  // Verifica que la lista no esté vacía

            // Verifica que el servicio GetAll haya sido llamado una vez
            _mockSolicitudService.Verify(service => service.GetAll(), Times.Once);
        }*/
        
        //Para consultar una solicitud por ID
        /*[Test]
        public async Task GetById_ReturnsOkResult_WithSolicitud()
        {
            // Arrange
            var solicitudId = 1;
            var expectedResponse = new SolicitudResponse { so_id = solicitudId, so_numero_solicitud = "A000000001" };

            // Configurar el mock para que devuelva la solicitud correspondiente al ID
            _mockSolicitudService.Setup(service => service.GetById(solicitudId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetById(solicitudId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.ShouldNotBeNull();  // Verifica que el resultado no sea nulo
            okResult.Value.ShouldBeOfType<SolicitudResponse>();  // Verifica que el valor retornado sea de tipo SolicitudResponse

            var solicitudResult = okResult.Value as SolicitudResponse;
            solicitudResult.so_id.ShouldBe(solicitudId);  // Verifica que el ID de la solicitud sea el esperado

            // Verifica que el servicio GetById haya sido llamado una vez
            _mockSolicitudService.Verify(service => service.GetById(It.IsAny<int>()), Times.Once);
        }*/


    }
}