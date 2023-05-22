using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public BrandService(IMapper mapper, IBrandRepository brandRepository)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
    }

    public async Task<List<BrandViewModel>> GetAll()
    {
        return _mapper.Map<List<BrandViewModel>>(await _brandRepository.GetAll());
    }

    public async Task<BrandViewModel?> GetById(int id)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand is null)
        {
            throw new Exception(ExceptionMessages.NotFoundMessage);
        }

        return _mapper.Map<BrandViewModel>(brand);
    }

    public async Task<int> Add(BrandInputModel brandInputModel)
    {
        var brand = _mapper.Map<Brand>(brandInputModel);
        await _brandRepository.Add(brand);
        return brand.ID;
    }

    public async Task Update(int id, BrandInputModel brandInputModel)
    {
        var updatingBrand = await _brandRepository.GetById(id);
        if (updatingBrand is null)
        {
            throw new Exception(ExceptionMessages.NotFoundMessage);
        }

        await _brandRepository.Update(updatingBrand, _mapper.Map<Brand>(brandInputModel));
    }

    public async Task Delete(int id)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand is null)
        {
            throw new Exception(ExceptionMessages.NotFoundMessage);
        }

        await _brandRepository.Delete(brand);
    }
}