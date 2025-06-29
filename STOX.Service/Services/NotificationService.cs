using AutoMapper;
using STOX.Data.Entities;
using STOX.Repo;
using STOX.Repo.Interfaces;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Service.Services;

public class NotificationService : INotificationService
{
    private readonly IRepository<Notification> _notificationRepository;
    private readonly IMapper _mapper;

    public NotificationService(IRepository<Notification> notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationDto>> GetAllAsync()
    {
        var notifications = await _notificationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task<NotificationDto> GetByIdAsync(Guid id)
    {
        var notification = await _notificationRepository.GetByKeysAsync(id);
        return _mapper.Map<NotificationDto>(notification);
    }

    public async Task CreateAsync(NotificationDto notificationDto)
    {
        var notification = _mapper.Map<Notification>(notificationDto);
        await _notificationRepository.AddAsync(notification);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(NotificationDto notificationDto)
    {
        var notification = await _notificationRepository.GetByKeysAsync(notificationDto.Id);
        if (notification == null)
        {
            throw new KeyNotFoundException("Notification not found.");
        }

        _mapper.Map(notificationDto, notification);
        _notificationRepository.Update(notification);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var notification = await _notificationRepository.GetByKeysAsync(id);
        if (notification != null)
        {
            _notificationRepository.Delete(notification);
            await _notificationRepository.SaveChangesAsync();
        }
    }
}