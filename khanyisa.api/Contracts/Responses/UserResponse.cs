namespace Contracts.Responses;

public class UserResponse
{
    public int Id { get; set; }
    //[Reducted="Student"]
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public AdditionalDataResponse? AdditionalData { get; set; }
    public DateTime? CreatedOn { get; set; }
    public AddressResponse? Address { get; set; }
    public List<UserRolesResponse>? Roles { get; set; }
    public UserTokenResponse? Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}

public class AdditionalDataResponse
{
    public string? MobileNumber { get; set; }
    public string? Gender { get; set; }
    public string? MaritalStatus { get; set; }
    public string? IdentityNumber { get; set; }
    public string? EmergencyContact { get; set; }
    public string? EmergencyRelationship { get; set; }
    public string? Nationality { get; set; }
}

public class UserRolesResponse
{
    public int RoleId { get; set; }
    public int UserId { get; set; }
    public string? Role { get; set; }
}


public class UserTokenResponse
{
    public int? Id { get; set; }
    public string? Type { get; set; }
    public string? Token { get; set; }
    public int? UserId { get; set; }
    public string? AdditionalData { get; set; }
}

public class AddressResponse
{
    public required int Id { get; init; }
    public required int UserId { get; init; }
    public required string Type { get; init; }
    public required string Line1 { get; set; }
    public string? Line2 { get; set; }
    public required int ZipCode { get; set; }
}
