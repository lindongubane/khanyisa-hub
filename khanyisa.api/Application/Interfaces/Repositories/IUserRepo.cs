using Domain.Model;

namespace Application.Interfaces.Repositories;

public interface IUserRepo
{
    Task<ApplicationUser?> GetUserByUsernameAsync(string username, CancellationToken token = default);
    Task<IEnumerable<ApplicationUser>> GetUserListAsync(CancellationToken token = default);
    Task<ApplicationUser?> CreateUser(ApplicationUser user, CancellationToken token = default);
    Task<string?> GetLastUsername(CancellationToken token = default);
}
