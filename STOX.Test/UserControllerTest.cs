using Microsoft.AspNetCore.Mvc;
using Moq;
using STOX.Service.DTOs.User;
using STOX.Service.Interfaces;
using STOX.Web.Controllers;

namespace STOX.Test;

public class UserControllerTest
{
        private readonly Mock<IUserService> _userServiceMock;
        private readonly AuthController _controller;

        public UserControllerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new AuthController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Register_ValidRegisterUserDto_ReturnsCreated()
        {
            var registerUserDto = new RegisterUserDto
            {
                Name = "Test User",
                ContactInfo = "1234567890",
                Email = "test@example.com",
                Password = "SecurePass123"
            };

            _controller.ModelState.Clear();
            _userServiceMock.Setup(x => x.RegisterAsync(registerUserDto)).Returns(Task.CompletedTask);
            
            var result = await _controller.Register(registerUserDto);
            
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal("", createdResult.Location);
            Assert.Null(createdResult.Value);
            _userServiceMock.Verify(x => x.RegisterAsync(registerUserDto), Times.Once());
        }

        [Fact]
        public async Task Register_InvalidModelState_ReturnsBadRequest()
        {
            var registerUserDto = new RegisterUserDto
            {
                Name = "",
                ContactInfo = "1234567890",
                Email = "invalid-email",
                Password = "123"
            };

            _controller.ModelState.AddModelError("Name", "Name is required");
            _controller.ModelState.AddModelError("Email", "Email is not valid");
            _controller.ModelState.AddModelError("Password", "Password must be at least 6 characters long");
            
            var result = await _controller.Register(registerUserDto);
            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            _userServiceMock.Verify(x => x.RegisterAsync(It.IsAny<RegisterUserDto>()), Times.Never());
        }
}