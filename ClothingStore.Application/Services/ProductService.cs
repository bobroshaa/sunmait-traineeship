using AutoMapper;
using ClothingStore.Application.Exceptions;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper, IProductRepository productRepository, ISectionRepository sectionRepository,
        ICategoryRepository categoryRepository, IBrandRepository brandRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _sectionRepository = sectionRepository;
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
    }

    public async Task<List<ProductViewModel>> GetAll()
    {
        return _mapper.Map<List<ProductViewModel>>(await _productRepository.GetAll());
    }

    public async Task<ProductViewModel?> GetById(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, id));
        }

        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task<int> Add(ProductInputModel productInputModel)
    {
        var sectionCategory = await _categoryRepository.GetSectionCategoryById(productInputModel.SectionCategoryID);
        if (sectionCategory is null)
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.SectionCategoryNotFound, productInputModel.SectionCategoryID));
        }

        if (productInputModel.BrandID is not null)
        {
            var brand = await _brandRepository.GetById((int)productInputModel.BrandID);
            if (brand is null)
            {
                throw new EntityNotFoundException(string.Format(ExceptionMessages.BrandNotFound, (int)productInputModel.BrandID));
            }
        }

        var product = _mapper.Map<Product>(productInputModel);
        await _productRepository.Add(product);
        return product.ID;
    }

    public async Task Update(int id, ProductInputModel productInputModel)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, id));
        }

        var sectionCategory = await _categoryRepository.GetById(productInputModel.SectionCategoryID);
        if (sectionCategory is null)
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.SectionCategoryNotFound, productInputModel.SectionCategoryID));
        }

        if (productInputModel.BrandID is not null)
        {
            var brand = await _brandRepository.GetById((int)productInputModel.BrandID);
            if (brand is null)
            {
                throw new EntityNotFoundException(string.Format(ExceptionMessages.BrandNotFound, (int)productInputModel.BrandID));
            }
        }

        await _productRepository.Update(product, _mapper.Map<Product>(productInputModel));
    }

    public async Task Delete(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, id));
        }

        await _productRepository.Delete(product);
    }

    public async Task<List<ProductViewModel>> GetProductsBySectionAndCategory(int sectionId, int categoryId)
    {
        var section = await _sectionRepository.GetById(sectionId);
        if (section is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.SectionNotFound, sectionId));
        }

        var category = await _categoryRepository.GetById(categoryId);
        if (category is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.CategoryNotFound, categoryId));
        }

        return _mapper.Map<List<ProductViewModel>>(
            await _productRepository.GetProductsBySectionAndCategory(sectionId, categoryId));
    }

    public async Task<List<ProductViewModel>> GetProductsByBrand(int brandId)
    {
        var brand = await _brandRepository.GetById(brandId);
        if (brand is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.BrandNotFound, brandId));
        }

        return _mapper.Map<List<ProductViewModel>>(
            await _productRepository.GetProductsByBrand(brandId));
    }
    
    public async Task AssignToBrand(int productId, int brandId)
    {
        var product = await _productRepository.GetById(productId);
        if (product is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, productId));
        }
        
        var brand = await _brandRepository.GetById(brandId);
        if (brand is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.BrandNotFound, brandId));
        }

        await _productRepository.AssignToBrand(product, brandId);
    }

    public async Task UnassignFromBrand(int productId)
    {
        var product = await _productRepository.GetById(productId);
        if (product is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, productId));
        }
        
        await _productRepository.UnassignFromBrand(product);
    }
}