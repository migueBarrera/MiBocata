namespace Models.Responses;

public class ClientSignInResponse
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Token { get; set; }

    public static Client Parse(ClientSignInResponse response)
    {
        return new Client()
        {
            Id = response.Id,
            Email = response.Email,
            Name = response.Name,
            Token = response.Token,
        };
    }
}
