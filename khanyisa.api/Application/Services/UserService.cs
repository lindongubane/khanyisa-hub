using System.Threading.Tasks;
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
    private readonly IValidator<ApplicationUser> _userValidator;

    public UserService(IUserRepo userRepo, IValidator<ApplicationUser> userValidator)
    {
        _userRepo = userRepo;
        _userValidator = userValidator;
    }

    public async Task<UserResponse?> GetUserByUsernameAsync(string username, CancellationToken token = default)
    {
        ApplicationUser? applicationUser = await _userRepo.GetUserByUsernameAsync(username, token);

        if (applicationUser is null)
        {
            return applicationUser?.MapToResponse();
        }

        //ApplicationUser.Roles = _roles.GetAllUserRolesAsync(ApplicationUser.Id).Result.ToList();
        //ApplicationUser.Address = await _addressService.GetAddressAsync(ApplicationUser.Id);

        return applicationUser?.MapToResponse();
    }

    public async Task<UserResponse?> CreateUser(ApplicationUserRequest request, CancellationToken token = default)
    {
        ApplicationUser? user = request.MapToApplicationUser();
        await _userValidator.ValidateAndThrowAsync(user, token);
        user.Username = (await GenerateUsername(token)).ToString();

        ApplicationUser newUser = await _userRepo.CreateUser(user, token);

    }

    public Task<IEnumerable<ApplicationUser>> GetUserListAsync(CancellationToken token = default) => throw new NotImplementedException();

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
