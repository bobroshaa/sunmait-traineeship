using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public BrandService(IMapper mapper, IBrandRepository brandRepository, IProductRepository productRepository)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
        _productRepository = productRepository;
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
            throw new Exception(ExceptionMessages.BrandNotFound);
        }

        return _mapper.Map<BrandViewModel>(brand);
    }

    public async Task<int> Add(BrandInputModel brandInputModel)
    {
        if (!(await _brandRepository.NameIsUnique(brandInputModel.Name)))
        {
            throw new Exception(ExceptionMessages.BrandAlreadyExists);
        }

        var brand = _mapper.Map<Brand>(brandInputModel);
        await _brandRepository.Add(brand);
        return brand.ID;
    }

    public async Task Update(int id, BrandInputModel brandInputModel)
    {
        var updatingBrand = await _brandRepository.GetById(id);
        if (updatingBrand is null)
        {
            throw new Exception(ExceptionMessages.BrandNotFound);
        }

        if (!await _brandRepository.NameIsUnique(brandInputModel.Name))
        {
            throw new Exception(ExceptionMessages.BrandAlreadyExists);
        }

        await _brandRepository.Update(updatingBrand, _mapper.Map<Brand>(brandInputModel));
    }

    public async Task Delete(int id)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand is null)
        {
            throw new Exception(ExceptionMessages.BrandNotFound);
        }

        await _brandRepository.Delete(brand);
    }

    public async Task AssignProduct(int productId, int brandId)
    {
        var product = await _productRepository.GetById(productId);
        if (product is null)
        {
            throw new Exception(ExceptionMessages.ProductNotFound);
        }
        
        var brand = await _brandRepository.GetById(brandId);
        if (brand is null)
        {
            throw new Exception(ExceptionMessages.BrandNotFound);
        }

        await _brandRepository.AssignProduct(product, brandId);
    }

    public async Task UnassignProduct(int productId)
    {
        var product = await _productRepository.GetById(productId);
        if (product is null)
        {
            throw new Exception(ExceptionMessages.ProductNotFound);
        }
        
        await _brandRepository.UnassignProduct(product);
    }
}