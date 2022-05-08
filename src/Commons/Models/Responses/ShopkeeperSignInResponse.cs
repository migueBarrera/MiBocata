namespace Models.Responses;

public class ShopkeeperSignInResponse
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Token { get; set; }

    public int IdStore { get; set; }

    public static Shopkeeper Parse(ShopkeeperSignInResponse response)
    {
        return new Shopkeeper()
        {
            Email = response.Email,
            Name = response.Name,
            Id = response.Id,
            IdStore = response.IdStore,
            Token = response.Token,
        };
    }
}
