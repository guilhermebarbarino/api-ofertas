using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;
using Ofertas.API.Controllers;
using Ofertas.Application.Services;
using Ofertas.Application.ViewModels;
using Ofertas.Application;

namespace Ofertas.Tests
{
    public class OfertasControllerTests
    {
        private readonly OfertasController _controller;
        private readonly Mock<IOfertaService> _mockService;
        private readonly Mock<IMemoryCache> _mockCache;

        public OfertasControllerTests()
        {
            _mockService = new Mock<IOfertaService>();
            _mockCache = new Mock<IMemoryCache>();
            _controller = new OfertasController(_mockService.Object, _mockCache.Object);
        }


        [Fact]
        public async Task GetById_ReturnsOfertaFromService()
        {
            // Arrange
            var id = Guid.NewGuid();
            var oferta = new OfertaResponse { Titulo = "Oferta 1" , Preco = 10 };
            _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(oferta);

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.IsType<OfertaResponse>(result);
            Assert.Equal(oferta.Id, result.Id);
        }

        [Fact]
        public async Task Post_ReturnsOkResult()
        {
            // Arrange
            var ofertaRequest = new OfertaRequest { Titulo = "Nova Oferta", Descricao = "Descrição da Nova Oferta", Preco = 100, DataCriacao = DateTime.UtcNow };
            _mockService.Setup(service => service.AddAsync(ofertaRequest)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(ofertaRequest);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCache.Verify(cache => cache.Remove(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var ofertaRequest = new OfertaRequest { Titulo = "Oferta Atualizada", Descricao = "Descrição Atualizada", Preco = 200, DataCriacao = DateTime.UtcNow };
            _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(new OfertaResponse { Titulo = "Oferta teste" });
            _mockService.Setup(service => service.UpdateAsync(ofertaRequest)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(id, ofertaRequest);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCache.Verify(cache => cache.Remove(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(new OfertaResponse { Titulo = "Oferta teste"});
            _mockService.Setup(service => service.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCache.Verify(cache => cache.Remove(It.IsAny<object>()), Times.Once);
        }
    }
}
