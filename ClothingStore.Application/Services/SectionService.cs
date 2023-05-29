using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
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
    public async Task<int> Add(SectionInputModel sectionInputModel)
    {
        var section = _mapper.Map<Section>(sectionInputModel);
        await _sectionRepository.Add(section);
        return section.ID;
    }

    public async Task Update(int id, string newName)
    {
        var updatingSection = await _sectionRepository.GetById(id);
        if (updatingSection is null)
        {
            throw new Exception(ExceptionMessages.SectionNotFound);
        }

        await _sectionRepository.Update(updatingSection, newName);
    }
}