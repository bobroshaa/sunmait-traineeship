using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
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
}