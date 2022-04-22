using Models.Core;

namespace Models.Requests
{
    public class ClientUpdateRequest
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        ////[JsonProperty(PropertyName = "pushToken")]
        ////public string PushToken { get; set; }

        public string Image { get; set; } = string.Empty;

        public string Phone { get; set; }

        public string Token { get; set; }

        public static ClientUpdateRequest Parse(Client client)
        {
            return new ClientUpdateRequest()
            {
                Id = client.Id,
                Email = client.Email,
                Name = client.Name,
                Password = client.Password,
                Image = client.Image,
                Phone = client.Phone,
                Token = client.Token,
            };
        }
    }
}
