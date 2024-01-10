using Stroytorg.Application.Constants;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper;

    public UserService(IUserRepository userRepository, IAutoMapperTypeMapper autoMapperTypeMapper)
    {
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
    }

    public async Task<BusinessResponse<User>> GetByIdAsync(int userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return new BusinessResponse<User>(
                BusinessErrorMessage: BusinessErrorMessage.NotExistingUser,
                IsSuccess: false);
        }

        return new BusinessResponse<User>(Value: autoMapperTypeMapper.Map<User>(user));
    }

    public async Task<BusinessResponse<User>> GetByEmailAsync(string email)
    {
        var user = await userRepository.GetByEmailAsync(email);
        if (user is null)
        {
            return new BusinessResponse<User>(
                BusinessErrorMessage: BusinessErrorMessage.NotExistingUser,
                IsSuccess: false);
        }

        return new BusinessResponse<User>(Value: autoMapperTypeMapper.Map<User>(user));
    }

    public async Task<BusinessResponse<User>> CreateAsync(UserRegister user)
    {
        var entityUser = await userRepository.GetByEmailAsync(user.Email);
        if (entityUser is not null)
        {
            return new BusinessResponse<User>(
                BusinessErrorMessage: BusinessErrorMessage.AlreadyExistingUser,
                IsSuccess: false);
        }

        var userToAdd = autoMapperTypeMapper.Map<Domain.Data.Entities.User>(user);

        await userRepository.AddAsync(userToAdd);
        await userRepository.UnitOfWork.Commit();
        
        return new BusinessResponse<User>(Value: autoMapperTypeMapper.Map<User>(userToAdd));
    }

    public async Task<BusinessResponse<User>> CreateWithGoogleAsync(UserGoogleAuth user)
    {
        var entityUser = await userRepository.GetByEmailAsync(user.Email);
        if (entityUser is not null)
        {
            return new BusinessResponse<User>(
                BusinessErrorMessage: BusinessErrorMessage.AlreadyExistingUser,
                IsSuccess: false);
        }

        var userToAdd = autoMapperTypeMapper.Map<Domain.Data.Entities.User>(user);

        await userRepository.AddAsync(userToAdd);
        await userRepository.UnitOfWork.Commit();

        return new BusinessResponse<User>(Value: autoMapperTypeMapper.Map<User>(userToAdd));
    }
}
