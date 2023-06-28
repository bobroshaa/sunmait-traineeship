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

    public ProductService(
        IMapper mapper,
        IProductRepository productRepository,
        ISectionRepository sectionRepository,
        ICategoryRepository categoryRepository,
        IBrandRepository brandRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _sectionRepository = sectionRepository;
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
    }

    public async Task<List<ProductViewModel>> GetAll()
    {
        var products = await _productRepository.GetAll();
        var productVms = _mapper.Map<List<ProductViewModel>>(products);
        
        return productVms;
    }

    public async Task<ProductViewModel?> GetById(int id)
    {
        var product = await GetProductById(id);
        var productVm = _mapper.Map<ProductViewModel>(product);
        
        return productVm;
    }

    public async Task<PostResponseViewModel> Add(ProductInputModel productInputModel)
    {
        await ValidateSectionCategory(productInputModel.SectionCategoryID);
        if (productInputModel.BrandID is not null)
        {
            await ValidateBrand((int)productInputModel.BrandID);
        }

        var product = _mapper.Map<Product>(productInputModel);

        _productRepository.Add(product);

        await _productRepository.SaveChanges();

        var response = new PostResponseViewModel { Id = product.ID };
        
        return response;
    }

    public async Task Update(int id, ProductInputModel productInputModel)
    {
        var product = await GetProductById(id);

        await ValidateSectionCategory(productInputModel.SectionCategoryID);
        if (productInputModel.BrandID is not null)
        {
            await ValidateBrand((int)productInputModel.BrandID);
        }

        product.Name = productInputModel.Name;
        product.Price = productInputModel.Price;
        product.InStockQuantity = productInputModel.InStockQuantity;
        product.Description = productInputModel.Description;
        product.ImageURL = productInputModel.ImageURL;
        product.SectionCategoryID = productInputModel.SectionCategoryID;
        product.BrandID = productInputModel.BrandID;

        await _productRepository.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var product = await GetProductById(id);

        _productRepository.Delete(product);

        await _productRepository.SaveChanges();
    }

    public async Task<List<ProductViewModel>> GetProductsBySectionAndCategory(int sectionId, int categoryId)
    {
        await ValidateSection(sectionId);
        await ValidateCategory(categoryId);

        var products = await _productRepository.GetProductsBySectionAndCategory(sectionId, categoryId);
        var productVms = _mapper.Map<List<ProductViewModel>>(products);
        
        return productVms;
    }

    public async Task<List<ProductViewModel>> GetProductsByBrand(int brandId)
    {
        await ValidateBrand(brandId);

        var products = await _productRepository.GetProductsByBrand(brandId);
        var productVms = _mapper.Map<List<ProductViewModel>>(products);

        return productVms;
    }

    public async Task AssignToBrand(int productId, int brandId)
    {
        var product = await GetProductById(productId);

        await ValidateBrand(brandId);

        _productRepository.AssignToBrand(product, brandId);

        await _productRepository.SaveChanges();
    }

    public async Task UnassignFromBrand(int productId)
    {
        var product = await GetProductById(productId);

        _productRepository.UnassignFromBrand(product);

        await _productRepository.SaveChanges();
    }

    private async Task<Product> GetProductById(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, id));
        }

        return product;
    }

    private async Task ValidateSectionCategory(int sectionCategoryId)
    {
        if (!await _categoryRepository.DoesSectionCategoryExist(sectionCategoryId))
        {
            throw new IncorrectParamsException(
                string.Format(ExceptionMessages.SectionCategoryNotFound, sectionCategoryId));
        }
    }

    private async Task ValidateBrand(int brandId)
    {
        if (!await _brandRepository.DoesBrandExist(brandId))
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.BrandNotFound, brandId));
        }
    }

    private async Task ValidateCategory(int id)
    {
        if (!await _categoryRepository.DoesCategoryExist(id))
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.CategoryNotFound, id));
        }
    }

    private async Task ValidateSection(int id)
    {
        if (!await _sectionRepository.DoesSectionExist(id))
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.SectionNotFound, id));
        }
    }
}