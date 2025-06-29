using STOX.Service.DTOs;

namespace STOX.Service.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    Task<ReviewDto> GetByIdAsync(Guid id);
    Task CreateAsync(ReviewDto reviewDto);
    Task UpdateAsync(ReviewDto reviewDto);
    Task DeleteAsync(Guid id);
}