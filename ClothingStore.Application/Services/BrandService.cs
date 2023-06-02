﻿using AutoMapper;
using ClothingStore.Application.Exceptions;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
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
        var brand = await GetBrandById(id);
        return _mapper.Map<BrandViewModel>(brand);
    }

    public async Task<int> Add(BrandInputModel brandInputModel)
    {
        await ValidateBrand(brandInputModel.Name);
        var brand = _mapper.Map<Brand>(brandInputModel);
        await _brandRepository.Add(brand);
        return brand.ID;
    }

    public async Task Update(int id, BrandInputModel brandInputModel)
    {
        var brand = await GetBrandById(id);
        await _brandRepository.Update(brand, _mapper.Map<Brand>(brandInputModel));
    }

    public async Task Delete(int id)
    {
        var brand = await GetBrandById(id);
        await _brandRepository.Delete(brand);
    }

    private async Task<Brand> GetBrandById(int id)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.BrandNotFound, id));
        }

        return brand;
    }
    
    private async Task ValidateBrand(string name)
    {
        if (await _brandRepository.DoesBrandExist(name))
        {
            throw new NotUniqueException(string.Format(ExceptionMessages.BrandAlreadyExists, name));
        }
    }
}