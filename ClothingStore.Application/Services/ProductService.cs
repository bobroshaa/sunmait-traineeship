using AutoMapper;
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
            throw new Exception(ExceptionMessages.ProductNotFound);
        }

        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task<int> Add(ProductInputModel productInputModel)
    {
        var sectionCategory = await _categoryRepository.GetById(productInputModel.SectionCategoryID);
        if (sectionCategory is null)
        {
            throw new Exception(ExceptionMessages.CategoryNotLinked);
        }

        if (productInputModel.BrandID is not null)
        {
            var brand = await _brandRepository.GetById((int)productInputModel.BrandID);
            if (brand is null)
            {
                throw new Exception(ExceptionMessages.BrandNotFound);
            }
        }

        var product = _mapper.Map<Product>(productInputModel);
        product.AddDate = DateTime.UtcNow;
        await _productRepository.Add(product);
        return product.ID;
    }

    public async Task Update(int id, ProductInputModel productInputModel)
    {
        var updatingProduct = await _productRepository.GetById(id);
        if (updatingProduct is null)
        {
            throw new Exception(ExceptionMessages.ProductNotFound);
        }

        var sectionCategory = await _categoryRepository.GetById(productInputModel.SectionCategoryID);
        if (sectionCategory is null)
        {
            throw new Exception(ExceptionMessages.CategoryNotLinked);
        }

        if (productInputModel.BrandID is not null)
        {
            var brand = await _brandRepository.GetById((int)productInputModel.BrandID);
            if (brand is null)
            {
                throw new Exception(ExceptionMessages.BrandNotFound);
            }
        }

        await _productRepository.Update(updatingProduct, _mapper.Map<Product>(productInputModel));
    }

    public async Task Delete(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            throw new Exception(ExceptionMessages.ProductNotFound);
        }

        await _productRepository.Delete(product);
    }

    public async Task<List<ProductViewModel>> GetProductsBySectionAndCategory(int sectionId, int categoryId)
    {
        var section = await _sectionRepository.GetById(sectionId);
        if (section is null)
        {
            throw new Exception(ExceptionMessages.SectionNotFound);
        }

        var category = await _categoryRepository.GetById(categoryId);
        if (category is null)
        {
            throw new Exception(ExceptionMessages.CategoryNotFound);
        }

        return _mapper.Map<List<ProductViewModel>>(
            await _productRepository.GetProductsBySectionAndCategory(sectionId, categoryId));
    }

    public async Task<List<ProductViewModel>> GetProductsByBrand(int brandId)
    {
        var brand = await _brandRepository.GetById(brandId);
        if (brand is null)
        {
            throw new Exception(ExceptionMessages.BrandNotFound);
        }

        return _mapper.Map<List<ProductViewModel>>(
            await _productRepository.GetProductsByBrand(brandId));
    }
}