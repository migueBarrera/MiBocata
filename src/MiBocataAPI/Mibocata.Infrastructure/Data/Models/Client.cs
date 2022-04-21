using System.ComponentModel.DataAnnotations.Schema;

namespace Mibocata.Infrastructure.Data.Models;

[Table("Clients")]
public class Client
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
}
