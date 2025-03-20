namespace Contracts.Requests;

public class UserRequest
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Cell { get; init; }
    public required AddressRequest Address { get; set; }
}
