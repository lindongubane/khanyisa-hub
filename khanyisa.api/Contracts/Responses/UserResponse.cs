namespace Contracts.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Cell { get; set; }
    public DateTime CreatedOn { get; set; }
    public AddressResponse? Address { get; set; }
}
