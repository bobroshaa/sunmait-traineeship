﻿using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class SectionService : ISectionService
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IMapper _mapper;

    public SectionService(IMapper mapper, ISectionRepository sectionRepository)
    {
        _mapper = mapper;
        _sectionRepository = sectionRepository;
    }
    
    public async Task<List<SectionViewModel>> GetAll()
    {
        return _mapper.Map<List<SectionViewModel>>(await _sectionRepository.GetAll());
    }

    public async Task<SectionViewModel?> GetById(int id)
    {
        var section = await _sectionRepository.GetById(id);
        if (section is null)
        {
            throw new Exception(ExceptionMessages.SectionNotFound);
        }

        return _mapper.Map<SectionViewModel>(section);
    }
    
    public async Task<int> Add(SectionInputModel sectionInputModel)
    {
        var section = _mapper.Map<Section>(sectionInputModel);
        await _sectionRepository.Add(section);
        return section.ID;
    }

    public async Task Update(int id, SectionInputModel sectionInputModel)
    {
        var updatingSection = await _sectionRepository.GetById(id);
        if (updatingSection is null)
        {
            throw new Exception(ExceptionMessages.SectionNotFound);
        }

        await _sectionRepository.Update(updatingSection, _mapper.Map<Section>(sectionInputModel));
    }
    
    public async Task Delete(int id)
    {
        var section = await _sectionRepository.GetById(id);
        if (section is null)
        {
            throw new Exception(ExceptionMessages.SectionNotFound);
        }

        await _sectionRepository.Delete(section);
    }
}