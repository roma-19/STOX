using AutoMapper;
using STOX.Data.Entities;
using STOX.Repo;
using STOX.Repo.Interfaces;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Service.Services;

public class ReviewService : IReviewService
{
    private readonly IRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewService(IRepository<Review> reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        var reviews = await _reviewRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto> GetByIdAsync(Guid id)
    {
        var review = await _reviewRepository.GetByKeysAsync(id);
        return _mapper.Map<ReviewDto>(review);
    }

    public async Task CreateAsync(ReviewDto reviewDto)
    {
        var review = _mapper.Map<Review>(reviewDto);
        await _reviewRepository.AddAsync(review);
        await _reviewRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(ReviewDto reviewDto)
    {
        var review = await _reviewRepository.GetByKeysAsync(reviewDto.Id);
        if (review == null)
        {
            throw new KeyNotFoundException("Review not found.");
        }

        _mapper.Map(reviewDto, review);
        _reviewRepository.Update(review);
        await _reviewRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var review = await _reviewRepository.GetByKeysAsync(id);
        if (review != null)
        {
            _reviewRepository.Delete(review);
            await _reviewRepository.SaveChangesAsync();
        }
    }
}