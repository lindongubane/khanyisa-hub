namespace Contracts.Requests;

public class ApplicationUserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public AdditionalDataRequest? AdditionalData { get; set; }
    public AddressRequest? Address { get; set; }
    public List<UserRolesRequest>? Roles { get; set; }
}

public class AdditionalDataRequest
{
    public string? MobileNumber { get; set; }
    public string? Gender { get; set; }
    public string? MaritalStatus { get; set; }
    public string? IdentityNumber { get; set; }
    public string? EmergencyContact { get; set; }
    public string? EmergencyRelationship { get; set; }
    public string? Nationality { get; set; }
}

public class UserRolesRequest
{
    public int RoleId { get; set; }
    public string? Role { get; set; }
}


public class UserTokenRequest
{
    public string? Type { get; set; }
    public string? Token { get; set; }
    public int? UserId { get; set; }
    public string? AdditionalData { get; set; }
}

public class AddressRequest
{
    public required int UserId { get; init; }
    public required string Type { get; init; }
    public required string Line1 { get; set; }
    public string? Line2 { get; set; }
    public required int ZipCode { get; set; }
}
