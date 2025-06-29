using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Moq;
using STOX.Data.Entities;
using STOX.Data.Enums;
using STOX.Repo.Repositories;
using STOX.Service.DTOs;
using STOX.Service.DTOs.User;
using STOX.Service.Interfaces;
using STOX.Service.Services;
using STOX.Web.Controllers;

namespace STOX.Test;

public class CartControllerTest
{
        private readonly Mock<ICartService> _cartServiceMock;
        private readonly CartController _controller;

        public CartControllerTest()
        {
            _cartServiceMock = new Mock<ICartService>();
            _controller = new CartController(_cartServiceMock.Object);
        }

        [Fact]
        public async Task Create_ValidCartDto_ReturnsCreatedAtAction()
        {
            var cartDto = new CartDto
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            _controller.ModelState.Clear();
            
            var result = await _controller.Create(cartDto);
            
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", createdAtActionResult.ActionName);
            Assert.Equal(cartDto.Id, ((CartDto)createdAtActionResult.Value).Id);
            _cartServiceMock.Verify(x => x.CreateAsync(cartDto), Times.Once());
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }

        [Fact]
        public async Task Create_InvalidModelState_ReturnsBadRequest()
        {
            var cartDto = new CartDto
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Empty
            };

            _controller.ModelState.AddModelError("UserId", "UserId is required");
            
            var result = await _controller.Create(cartDto);
            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            _cartServiceMock.Verify(x => x.CreateAsync(It.IsAny<CartDto>()), Times.Never());
        }
}