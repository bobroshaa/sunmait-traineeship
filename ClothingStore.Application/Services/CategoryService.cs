using AutoMapper;
using ClothingStore.Application.Exceptions;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, ICategoryRepository categoryRepository, ISectionRepository sectionRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _sectionRepository = sectionRepository;
    }

    public async Task<CategoryViewModel?> GetById(int id)
    {
        var category = await GetCategoryById(id);
        return _mapper.Map<CategoryViewModel>(category);
    }

    public async Task<int> Add(CategoryInputModel categoryInputModel)
    {
        var category = _mapper.Map<Category>(categoryInputModel);
        _categoryRepository.Add(category);
        await _categoryRepository.Save();
        return category.ID;
    }

    public async Task Update(int id, CategoryInputModel categoryInputModel)
    {
        var category = await GetCategoryById(id);
        category.Name = categoryInputModel.Name;
        category.ParentCategoryID = categoryInputModel.ParentCategoryID;
        await _categoryRepository.Save();
    }

    public async Task Delete(int id)
    {
        var category = await GetCategoryById(id);
        _categoryRepository.Delete(category);
        await _categoryRepository.Save();
    }

    public async Task LinkCategoryToSection(int sectionId, int categoryId)
    {
        await ValidateSection(sectionId);
        await ValidateCategory(categoryId);
        await ValidateSectionCategoryExistence(sectionId, categoryId);
        _categoryRepository.LinkCategoryToSection(new SectionCategory
            { SectionID = sectionId, CategoryID = categoryId });
        await _categoryRepository.Save();
    }

    private async Task<Category> GetCategoryById(int id)
    {
        var category = await _categoryRepository.GetById(id);
        if (category is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.CategoryNotFound, id));
        }

        return category;
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

    private async Task ValidateSectionCategoryExistence(int sectionId, int categoryId)
    {
        if (await _categoryRepository.DoesSectionCategoryExist(sectionId, categoryId))
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.CategoryLinked, categoryId, sectionId));
        }
    }
}