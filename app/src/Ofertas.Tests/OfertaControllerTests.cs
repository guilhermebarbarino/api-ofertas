using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;
using Ofertas.API.Controllers;
using Ofertas.Application.Services;
using Ofertas.Domain.Entidades;
using Ofertas.Infrastructure.interfaces;
using Ofertas.Application;

namespace ofertas_solutions.Tests
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
            _controller = new OfertasController(_mockService.Object,_mockCache.Object);
        }

       
        [Fact]
        public async Task Post_ReturnsOkResult()
        {
            // Arrange
            var oferta = new Oferta { Titulo = "Nova Oferta", Descricao = "Descrição da Nova Oferta" };
            _mockService.Setup(service => service.AddAsync(oferta)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(oferta);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCache.Verify(cache => cache.Remove(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var oferta = new Oferta { Id = id, Titulo = "Oferta Atualizada", Descricao = "Descrição Atualizada" };
            _mockService.Setup(service => service.UpdateAsync(oferta)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(id, oferta);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCache.Verify(cache => cache.Remove(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCache.Verify(cache => cache.Remove(It.IsAny<object>()), Times.Once);
        }
    }
}
