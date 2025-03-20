using Domain.Model;

namespace Application.Interfaces.Repositories;

public interface IUserRepo
{
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken token = default);
    Task<IEnumerable<User>> GetUserListAsync(CancellationToken token = default);
    Task<User?> CreateUser(User user, CancellationToken token = default);
    Task<string?> GetLastUsername(CancellationToken token = default);
}
