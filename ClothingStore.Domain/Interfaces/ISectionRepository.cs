using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface ISectionRepository
{
    Task Add(Section section);
    Task Update(Section updatingSection, string newName);
}