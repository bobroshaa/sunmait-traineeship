﻿using AutoMapper;
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
        var category = await _categoryRepository.GetById(id);
        if (category is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.CategoryNotFound, id));
        }

        return _mapper.Map<CategoryViewModel>(category);
    }

    public async Task<int> Add(CategoryInputModel categoryInputModel)
    {
        var category = _mapper.Map<Category>(categoryInputModel);
        await _categoryRepository.Add(category);
        return category.ID;
    }

    public async Task Update(int id, CategoryInputModel categoryInputModel)
    {
        var category = await _categoryRepository.GetById(id);
        if (category is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.CategoryNotFound, id));
        }

        await _categoryRepository.Update(category, _mapper.Map<Category>(categoryInputModel));
    }

    public async Task Delete(int id)
    {
        var category = await _categoryRepository.GetById(id);
        if (category is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.CategoryNotFound, id));
        }

        await _categoryRepository.Delete(category);
    }

    public async Task LinkCategoryToSection(int sectionId, int categoryId)
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

        if (await _categoryRepository.DoesSectionCategoryExist(sectionId, categoryId))
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.CategoryLinked, categoryId, sectionId));
        }

        await _categoryRepository.LinkCategoryToSection(new SectionCategory
            { SectionID = sectionId, CategoryID = categoryId });
    }
}