using STOX.Service.DTOs;

namespace STOX.Service.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetAllAsync();
    Task<NotificationDto> GetByIdAsync(Guid id);
    Task CreateAsync(NotificationDto notificationDto);
    Task UpdateAsync(NotificationDto notificationDto);
    Task DeleteAsync(Guid id);
}