using Application.Interfaces.Repositories;
using Application.Interfaces.Service;
using Application.Mappings;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Model;
using FluentValidation;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;
    private readonly IValidator<User> _userValidator;
    private readonly IValidator<Address> _addressValidator;

    public UserService(IUserRepo userRepo, IValidator<User> userValidator, IValidator<Address> addressValidator)
    {
        _userRepo = userRepo;
        _userValidator = userValidator;
        _addressValidator = addressValidator;
    }

    public async Task<UserResponse?> CreateUser(UserRequest request, CancellationToken token = default)
    {
        User? user = request.MapUser();

        await _userValidator.ValidateAndThrowAsync(user, token);
        await _addressValidator.ValidateAndThrowAsync(user.Address, token);

        user.Username = (await GenerateUsername(token)).ToString();

        User? newUser = await _userRepo.CreateUser(user, token);

        return newUser?.MapToResponse();
    }

    public async Task<UserResponse?> GetUserByUsernameAsync(string username, CancellationToken token = default)
    {
        User? applicationUser = await _userRepo.GetUserByUsernameAsync(username, token);

        if (applicationUser is null)
        {
            return applicationUser?.MapToResponse();
        }

        return applicationUser?.MapToResponse();
    }
    public Task<IEnumerable<User>> GetUserListAsync(CancellationToken token = default) => throw new NotImplementedException();

    private async Task<int> GenerateUsername(CancellationToken token = default)
    {
        string year = DateTime.Now.Year.ToString();
        string dateValue = $"{year[0]}{year[2]}{year[3]}";
        string? lastStudent = await _userRepo.GetLastUsername(token);

        if (string.IsNullOrEmpty(lastStudent) || lastStudent.Length < 4 || !lastStudent.StartsWith(dateValue))
        {
            return int.Parse(dateValue) * 10000 + 1;
        }

        if (!int.TryParse(lastStudent[3..], out int lastNumber))
        {
            return int.Parse(dateValue) * 10000 + 1;
        }

        return lastNumber + 1;
    }
}
