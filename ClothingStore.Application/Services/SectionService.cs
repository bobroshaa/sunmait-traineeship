using AutoMapper;
using ClothingStore.Application.Exceptions;
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
        var section = await GetSectionById(id);
        return _mapper.Map<SectionViewModel>(section);
    }
    
    public async Task<int> Add(SectionInputModel sectionInputModel)
    {
        var section = _mapper.Map<Section>(sectionInputModel);
        _sectionRepository.Add(section);
        await _sectionRepository.Save();
        return section.ID;
    }

    public async Task Update(int id, SectionInputModel sectionInputModel)
    {
        var section = await GetSectionById(id);
        section.Name = sectionInputModel.Name;
        await _sectionRepository.Save();
    }
    
    public async Task Delete(int id)
    {
        var section = await GetSectionById(id);
        _sectionRepository.Delete(section);
        await _sectionRepository.Save();
    }
    
    private async Task<Section> GetSectionById(int id)
    {
        var section = await _sectionRepository.GetById(id);
        if (section is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.SectionNotFound, id));
        }

        return section;
    }
}