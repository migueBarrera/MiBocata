namespace Models.Core;

public class Shopkeeper
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; }

    public string Token { get; set; }

    public int IdStore { get; set; }
}