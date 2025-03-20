using Contracts.Requests;
using Contracts.Responses;
using Domain.Model;

namespace Application.Mappings;

public static class ContractMapping
{
    public static UserResponse MapToResponse(this User user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        Cell = user.Cell,
        CreatedOn = user.CreatedOn,
        Address = MapToResponse(user.Address),
    };

    public static User MapUser(this UserRequest request)
    {
        var userId = Guid.CreateVersion7();
        User? user = new()
        {
            Id = userId,
            Username = string.Empty,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Cell = request.Cell,
            Address = MapAddress(request.Address, userId),
            CreatedOn = DateTime.Now,
        };

        return user;
    }

    public static Address MapAddress(this AddressResponse addressResponse) => new()
    {
        Id = Guid.CreateVersion7(),
        CreatedOn = addressResponse.CreatedOn,
        City = addressResponse.City,
        Province = addressResponse.Province,
        Country = addressResponse.Country,
        Line1 = addressResponse.Line1,
        Line2 = addressResponse.Line2,
        ZipCode = addressResponse.ZipCode,
        UserId = addressResponse.UserId,
        Type = addressResponse.Type
    };

    public static Address MapAddress(this AddressRequest addressRequest, Guid? userId = null) => new()
    {
        Id = Guid.CreateVersion7(),
        CreatedOn = DateTime.Now,
        City = addressRequest.City,
        Province = addressRequest.Province,
        Country = addressRequest.Country,
        Line1 = addressRequest.Line1,
        Line2 = addressRequest.Line2,
        ZipCode = addressRequest.ZipCode,
        UserId = userId ?? Guid.CreateVersion7(),
        Type = addressRequest.Type
    };

    public static AddressResponse MapToResponse(this Address address) => new()
    {
        City = address.City,
        Province = address.Province,
        Country = address.Country,
        Line1 = address.Line1,
        Line2 = address.Line2,
        ZipCode = address.ZipCode,
        Id = address.Id,
        UserId = address.UserId,
        Type = address.Type,
        CreatedOn = address.CreatedOn
    };
}
