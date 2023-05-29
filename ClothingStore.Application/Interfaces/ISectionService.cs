using ClothingStore.Application.Models.InputModels;

namespace ClothingStore.Application.Interfaces;

public interface ISectionService
{
    public Task<int> Add(SectionInputModel sectionInputModel);
    public Task Update(int id, string newName);
}