using Contracts.Requests;
using Contracts.Responses;
using Domain.Model;

namespace Application.Mappings;

public static class ContractMapping
{
    public static UserResponse MapToResponse(this ApplicationUser applicationUser) => new()
    {
        Id = applicationUser.Id,
        Username = applicationUser.Username,
        FirstName = applicationUser.FirstName,
        LastName = applicationUser.LastName,
        Email = applicationUser.Email,
        AdditionalData = new(),
        CreatedOn = applicationUser.CreatedOn,
        Address = new(),
        Roles = new(),
        Password = new(),
        RefreshToken = applicationUser.RefreshToken,
        RefreshTokenExpiryTime = applicationUser.RefreshTokenExpiryTime
    };

    public static ApplicationUser MapToApplicationUser(this ApplicationUserRequest request) => new()
    {
        Id = 1,
        Username = string.Empty,
        FirstName = request.FirstName,
        LastName = request.LastName,
        Email = request.Email,
        Address = new(),
        Roles = new(),
        AdditionalData = new(),
        CreatedOn = DateTime.Now,
        Password = new(),
        RefreshToken = string.Empty,
        RefreshTokenExpiryTime = new()
    };
}
