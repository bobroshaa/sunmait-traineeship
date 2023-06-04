using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface ISectionService
{
    Task<List<SectionViewModel>> GetAll();
    Task<SectionViewModel?> GetById(int id);
    Task<PostResponseViewModel> Add(SectionInputModel sectionInputModel);
    Task Update(int id, SectionInputModel sectionInputModel);
    Task Delete(int id);
}