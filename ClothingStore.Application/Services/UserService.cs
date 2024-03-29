﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        var users = await _userRepository.GetAll();
        var userVms = _mapper.Map<List<UserViewModel>>(users);

        return userVms;
    }

    public async Task<UserViewModel?> GetById(int id)
    {
        var user = await GetUserById(id);
        var userVm = _mapper.Map<UserViewModel>(user);

        return userVm;
    }

    public async Task<PostResponseViewModel> Add(UserInputModel userInputModel)
    {
        await ValidateEmail(userInputModel.Email);
        if (userInputModel.Phone is not null)
        {
            await ValidatePhoneNumber(userInputModel.Phone);
        }

        var user = _mapper.Map<UserAccount>(userInputModel);

        user.Password = GetHashSha256(user.Password);
        user.Role = Role.Customer;

        _userRepository.Add(user);

        await _userRepository.Save();

        var response = new PostResponseViewModel { Id = user.ID };

        return response;
    }

    public async Task Update(int id, UserInputModel userInputModel)
    {
        var user = await GetUserById(id);

        if (user.Email != userInputModel.Email)
        {
            await ValidateEmail(userInputModel.Email);
            
            user.Email = userInputModel.Email;
        }

        if (user.Phone != userInputModel.Phone && userInputModel.Phone is not null)
        {
            await ValidatePhoneNumber(userInputModel.Phone);
            
            user.Phone = userInputModel.Phone;
        }

        user.FirstName = userInputModel.FirstName;
        user.LastName = userInputModel.LastName;

        await _userRepository.Save();
    }

    public async Task Delete(int id)
    {
        var user = await GetUserById(id);

        _userRepository.Delete(user);

        await _userRepository.Save();
    }

    public async Task UpdateAddress(int userId, AddressInputModel addressInputModel)
    {
        await ValidateUser(userId);
        ValidateCountry(addressInputModel.Country);

        var address = await _userRepository.GetAddressByUserId(userId);

        if (address is null)
        {
            var newAddress = _mapper.Map<Address>(addressInputModel);

            newAddress.UserID = userId;

            _userRepository.AddAddress(newAddress);

            await _userRepository.Save();
        }
        else
        {
            address.Country = addressInputModel.Country;
            address.District = addressInputModel.District;
            address.City = addressInputModel.City;
            address.Postcode = addressInputModel.Postcode;
            address.AddressLine1 = addressInputModel.AddressLine1;
            address.AddressLine2 = addressInputModel.AddressLine2;

            await _userRepository.Save();
        }
    }

    public async Task UpdateRole(int id, Role role)
    {
        var user = await GetUserById(id);

        ValidateRole(role);

        _userRepository.UpdateRole(user, role);

        await _userRepository.Save();
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

    private void ValidateRole(Role role)
    {
        if (!Enum.IsDefined(role))
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.RoleNotFound, role));
        }
    }

    private void ValidateCountry(Country country)
    {
        if (!Enum.IsDefined(country))
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.CountryNotFound, country));
        }
    }

    private async Task ValidatePhoneNumber(string phoneNumber)
    {
        if (await _userRepository.DoesPhoneNumberExist(phoneNumber))
        {
            throw new NotUniqueException(string.Format(ExceptionMessages.PhoneNumberIsNotUnique, phoneNumber));
        }
    }

    private async Task ValidateUser(int id)
    {
        if (!await _userRepository.DoesUserExist(id))
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.UserNotFound, id));
        }
    }

    private async Task<UserAccount> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.UserNotFoundByEmail, email));
        }

        return user;
    }

    private void CheckPassword(UserAccount user, string password)
    {
        if (password != user.Password)
        {
            throw new IncorrectParamsException(string.Format(ExceptionMessages.IncorrectPassword));
        }
    }

    public async Task<UserViewModel> Authenticate(LoginInputModel loginInputModel)
    {
        var passwordHash = GetHashSha256(loginInputModel.Password);

        var user = await GetUserByEmail(loginInputModel.Email);
        CheckPassword(user, passwordHash);
        
        var userVm = _mapper.Map<UserViewModel>(user);

        return userVm;
    }
}