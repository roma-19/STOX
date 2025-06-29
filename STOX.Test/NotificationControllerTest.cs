using Microsoft.AspNetCore.Mvc;
using Moq;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;
using STOX.Web.Controllers;

namespace STOX.Test;

public class NotificationControllerTest
{
    private readonly Mock<INotificationService> _notificationServiceMock;
        private readonly NotificationController _controller;

        public NotificationControllerTest()
        {
            _notificationServiceMock = new Mock<INotificationService>();
            _controller = new NotificationController(_notificationServiceMock.Object);
        }

        [Fact]
        public async Task Create_ValidNotificationDto_ReturnsCreatedAtAction()
        {
            var notificationDto = new NotificationDto
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Message = "Test Notification",
                NotificationDate = DateTime.UtcNow,
                IsRead = false
            };

            _controller.ModelState.Clear();
            
            var result = await _controller.Create(notificationDto);
            
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", createdAtActionResult.ActionName);
            Assert.Equal(notificationDto.Id, ((NotificationDto)createdAtActionResult.Value).Id);
            _notificationServiceMock.Verify(x => x.CreateAsync(notificationDto), Times.Once());
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }

        [Fact]
        public async Task Create_InvalidModelState_ReturnsBadRequest()
        {
            var notificationDto = new NotificationDto
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Empty,
                Message = "",
                NotificationDate = DateTime.MinValue
            };

            _controller.ModelState.AddModelError("UserId", "UserId is required");
            _controller.ModelState.AddModelError("Message", "Message is required");
            _controller.ModelState.AddModelError("NotificationDate", "NotificationDate must be a valid date");
            
            var result = await _controller.Create(notificationDto);
            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            _notificationServiceMock.Verify(x => x.CreateAsync(It.IsAny<NotificationDto>()), Times.Never());
        }
}