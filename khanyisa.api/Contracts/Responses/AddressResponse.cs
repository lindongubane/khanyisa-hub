namespace Contracts.Responses;

public class AddressResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public DateTime CreatedOn { get; init; }
    public required string Type { get; init; }
    public required string City { get; set; }
    public required string Province { get; set; }
    public required string Country { get; set; }
    public required string Line1 { get; set; }
    public string? Line2 { get; set; }
    public int ZipCode { get; set; }
}

