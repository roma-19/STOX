using Microsoft.AspNetCore.Mvc;
using Moq;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;
using STOX.Web.Controllers;

namespace STOX.Test;

public class CartItemControllerTest
{
    private readonly Mock<ICartItemService> _cartItemServiceMock;
        private readonly CartItemController _controller;

        public CartItemControllerTest()
        {
            _cartItemServiceMock = new Mock<ICartItemService>();
            _controller = new CartItemController(_cartItemServiceMock.Object);
        }

        [Fact]
        public async Task Create_ValidCartItemDto_ReturnsCreatedAtAction()
        {
            var cartItemDto = new CartItemDto
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                ProductName = "Test Product",
                Quantity = 2,
                Price = 10.99m
            };

            _controller.ModelState.Clear();
            
            var result = await _controller.Create(cartItemDto);
            
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", createdAtActionResult.ActionName);
            Assert.Equal(cartItemDto.Id, ((CartItemDto)createdAtActionResult.Value).Id);
            _cartItemServiceMock.Verify(x => x.CreateAsync(cartItemDto), Times.Once());
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }

        [Fact]
        public async Task Create_InvalidModelState_ReturnsBadRequest()
        {
            var cartItemDto = new CartItemDto
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.Empty,
                ProductName = "",
                Quantity = 0,
                Price = -1
            };

            _controller.ModelState.AddModelError("ProductId", "ProductId is required");
            _controller.ModelState.AddModelError("ProductName", "ProductName is required");
            _controller.ModelState.AddModelError("Quantity", "Quantity must be greater than 0");
            _controller.ModelState.AddModelError("Price", "Price must be greater than 0");
            
            var result = await _controller.Create(cartItemDto);
            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            _cartItemServiceMock.Verify(x => x.CreateAsync(It.IsAny<CartItemDto>()), Times.Never());
        }
}