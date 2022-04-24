using Models.Core;

namespace Models.Requests
{
    public class ClientUpdateRequest
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public static ClientUpdateRequest Parse(Client client)
        {
            return new ClientUpdateRequest()
            {
                Id = client.Id,
                Email = client.Email,
                Name = client.Name,
                Phone = client.Phone,
            };
        }
    }
}
