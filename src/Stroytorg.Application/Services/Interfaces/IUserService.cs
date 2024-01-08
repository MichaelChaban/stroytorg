﻿using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Services.Interfaces;

public interface IUserService
{
    Task<BusinessResponse<User>> GetByIdAsync(int userId);

    Task<BusinessResponse<User>> GetByEmailAsync(string email);

    Task<BusinessResponse<User>> CreateAsync(UserRegister user);

    Task<BusinessResponse<User>> CreateWithGoogleAsync(UserGoogleAuth user);
}
