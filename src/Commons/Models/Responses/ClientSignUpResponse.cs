using Models.Core;

namespace Models.Responses
{
    public class ClientSignUpResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public static Client Parse(ClientSignUpResponse response)
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
}
