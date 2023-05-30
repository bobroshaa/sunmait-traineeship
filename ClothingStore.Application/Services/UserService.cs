using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<List<UserViewModel>> GetAll()
    {
        return _mapper.Map<List<UserViewModel>>(await _userRepository.GetAll());
    }

    public async Task<UserViewModel?> GetById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            throw new Exception(ExceptionMessages.UserNotFound);
        }

        return _mapper.Map<UserViewModel>(user);
    }

    public async Task<int> Add(UserInputModel userInputModel)
    {
        var user = _mapper.Map<UserAccount>(userInputModel);
        user.Password = GetHashSha256(user.Password);
        user.Role = Role.Customer;
        await _userRepository.Add(user);
        return user.ID;
    }

    public async Task Update(int id, UserInputModel userInputModel)
    {
        var updatingUser = await _userRepository.GetById(id);
        if (updatingUser is null)
        {
            throw new Exception(ExceptionMessages.UserNotFound);
        }

        await _userRepository.Update(updatingUser, _mapper.Map<UserAccount>(userInputModel));
    }

    public async Task Delete(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            throw new Exception(ExceptionMessages.UserNotFound);
        }

        await _userRepository.Delete(user);
    }
    
    private string GetHashSha256(string str)
    {
        var hashValue = SHA256.HashData(Encoding.UTF8.GetBytes(str));
        return Convert.ToHexString(hashValue);
    }
}