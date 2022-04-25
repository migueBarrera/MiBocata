namespace Models.Responses
{
    public class ShopkeeperSignInResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public int IdStore { get; set; }
    }
}
