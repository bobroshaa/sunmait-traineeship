using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ClothingStore.Application.Exceptions;
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
        var user = await GetUserById(id);
        return _mapper.Map<UserViewModel>(user);
    }

    public async Task<int> Add(UserInputModel userInputModel)
    {
        await ValidateEmail(userInputModel.Email);
        if (userInputModel.Phone is not null)
        {
            await ValidatePhoneNumber(userInputModel.Phone);
        }

        var user = _mapper.Map<UserAccount>(userInputModel);
        user.Password = GetHashSha256(user.Password);
        user.Role = Role.Customer;
        await _userRepository.Add(user);
        return user.ID;
    }

    public async Task Update(int id, UserInputModel userInputModel)
    {
        var user = await GetUserById(id);
        await ValidateEmail(userInputModel.Email);
        if (userInputModel.Phone is not null)
        {
            await ValidatePhoneNumber(userInputModel.Phone);
        }

        await _userRepository.Update(user, _mapper.Map<UserAccount>(userInputModel));
    }

    public async Task Delete(int id)
    {
        var user = await GetUserById(id);
        await _userRepository.Delete(user);
    }

    public async Task UpdateAddress(int userId, AddressInputModel addressInputModel)
    {
        await GetUserById(userId);
        var mappedAddress = _mapper.Map<Address>(addressInputModel);
        mappedAddress.UserID = userId;
        var address = await _userRepository.GetAddressByUserId(userId);
        if (address is null)
        {
            await _userRepository.AddAddress(mappedAddress);
        }
        else
        {
            await _userRepository.UpdateAddress(address, mappedAddress);
        }
    }

    public async Task UpdateRole(int id, Role role)
    {
        var user = await GetUserById(id);
        await _userRepository.UpdateRole(user, role);
    }

    private string GetHashSha256(string str)
    {
        var hashValue = SHA256.HashData(Encoding.UTF8.GetBytes(str));
        return Convert.ToHexString(hashValue);
    }

    private async Task<UserAccount> GetUserById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.UserNotFound, id));
        }

        return user;
    }

    private async Task ValidateEmail(string email)
    {
        if (await _userRepository.DoesEmailExist(email))
        {
            throw new NotUniqueException(string.Format(ExceptionMessages.EmailIsNotUnique, email));
        }
    }

    private async Task ValidatePhoneNumber(string phoneNumber)
    {
        if (await _userRepository.DoesPhoneNumberExist(phoneNumber))
        {
            throw new NotUniqueException(string.Format(ExceptionMessages.PhoneNumberIsNotUnique, phoneNumber));
        }
    }
}