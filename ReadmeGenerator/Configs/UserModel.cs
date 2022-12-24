namespace Quera.Configs;

public class UserModel {
    public string? Email { get; set; } = null!;
    public string? AvatarUrl { get; set; } = null!;
    public string? ProfileUrl { get; set; } = null!;
    public bool Ignore { get; set; }
}