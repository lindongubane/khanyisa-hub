namespace Domain.Model;

public class UserToken
{
    public int? Id { get; set; }
    public string? Type { get; set; }
    public string? Token { get; set; }
    public int? UserId { get; set; }
    public string? AdditionalData { get; set; }
}
