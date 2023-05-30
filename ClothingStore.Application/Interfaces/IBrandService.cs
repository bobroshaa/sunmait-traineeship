﻿using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface IBrandService
{
    public Task<List<BrandViewModel>> GetAll();
    public Task<BrandViewModel?> GetById(int id);
    public Task<int> Add(BrandInputModel brandInputModel);
    public Task Update(int id, BrandInputModel brandInputModel);
    public Task Delete(int id);
    Task AssignProduct(int productId, int brandId);
    Task UnassignProduct(int productId);
}