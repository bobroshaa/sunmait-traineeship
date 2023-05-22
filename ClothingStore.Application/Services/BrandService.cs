using ClothingStore.Application.Interfaces;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

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

    public async Task Add(Brand brand)
    {
        await _brandRepository.Add(brand);
    }

    public async Task Update(int id, Brand brand)
    {
        var updatingBrand = await _brandRepository.GetById(id);
        if (updatingBrand is null)
        {
            throw new Exception("Sorry, this brand does not exist!");
        }

        await _brandRepository.Update(updatingBrand, brand);
    }

    public async Task Delete(int id)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand is null)
        {
            throw new Exception("Sorry, this brand does not exist!");
        }

        await _brandRepository.Delete(brand);
    }
}