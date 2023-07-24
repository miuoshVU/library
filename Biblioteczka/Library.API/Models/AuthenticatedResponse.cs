using Library.API.Models;

public class AuthenticatedResponse
{
    public string? Token { get; set; }

    public string? Exp { get; set; }

    public UserDto? User {get; set; }
}