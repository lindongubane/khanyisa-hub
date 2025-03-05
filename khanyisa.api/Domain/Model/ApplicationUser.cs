namespace Domain.Model;

public class ApplicationUser
{
    public int Id { get; set; }
    //[Reducted="Student"]
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public AdditionalData? AdditionalData { get; set; }
    public DateTime? CreatedOn { get; set; }
    public Address? Address { get; set; }
    public List<UserRoles> Roles { get; set; }
    public UserToken? Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}

public class AdditionalData
{
    public string? MobileNumber { get; set; }
    public string? Gender { get; set; }
    public string? MaritalStatus { get; set; }
    public string? IdentityNumber { get; set; }
    public string? EmergencyContact { get; set; }
    public string? EmergencyRelationship { get; set; }
    public string? Nationality { get; set; }
}
