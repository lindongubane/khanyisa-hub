namespace Domain.Model;

public class Address
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required DateTime CreatedOn { get; init; }
    public required string Type { get; init; }
    public required string City { get; set; }
    public required string Province { get; set; }
    public required string Country { get; set; }
    public required string Line1 { get; set; }
    public string? Line2 { get; set; }
    public required int ZipCode { get; set; }
}
