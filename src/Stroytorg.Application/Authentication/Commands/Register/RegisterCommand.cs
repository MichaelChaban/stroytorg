﻿using MediatR;
using Stroytorg.Contracts.ResponseModels;

namespace Stroytorg.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTimeOffset BirthDate,
    string PhoneNumber) : IRequest<AuthResponse>;
