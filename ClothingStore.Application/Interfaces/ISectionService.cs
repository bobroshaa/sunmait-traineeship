using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface ISectionService
{
    Task<List<SectionViewModel>> GetAll();
    Task<SectionViewModel?> GetById(int id);
    public Task<int> Add(SectionInputModel sectionInputModel);
    public Task Update(int id, SectionInputModel sectionInputModel);
    Task Delete(int id);
}