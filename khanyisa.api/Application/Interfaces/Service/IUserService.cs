using Contracts.Requests;
using Contracts.Responses;
using Domain.Model;

namespace Application.Interfaces.Service;

public interface IUserService
{
    Task<UserResponse?> GetUserByUsernameAsync(string username, CancellationToken token = default);
    Task<IEnumerable<ApplicationUser>> GetUserListAsync(CancellationToken token = default);
    Task<UserResponse?> CreateUser(ApplicationUserRequest user, CancellationToken token = default);
}
