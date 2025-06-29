using AutoMapper;
using STOX.Data.Entities;
using STOX.Repo;
using STOX.Repo.Interfaces;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Service.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByKeysAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task CreateAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductDto productDto)
    {
        var product = await _productRepository.GetByKeysAsync(productDto.Id);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        _mapper.Map(productDto, product);
        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByKeysAsync(id);
        if (product != null)
        {
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }
    }
}