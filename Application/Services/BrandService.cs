using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IEnumerable<Brand>> GetAll()
    {
        return await _brandRepository.GetAll();
    }
    
    public async Task<Brand?> GetById(int id)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand is null)
        {
            throw new Exception("Sorry, this brand does not exist!");
        }
        return brand;
    }
    
    public async Task<bool> Add(Brand brand)
    {
        return await _brandRepository.Add(brand);
    }
    
    public async Task<bool> Update(int id, Brand brand)
    {
        var updatingBrand = await _brandRepository.GetById(id);
        if (updatingBrand is null)
        {
            throw new Exception("Sorry, this brand does not exist!");
        }
        
        return await _brandRepository.Update(updatingBrand, brand);
    }
    
    public async Task<bool> Delete(int id)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand is null)
        {
            throw new Exception("Sorry, this brand does not exist!");
        }
        
        return await _brandRepository.Delete(brand);
    }
}