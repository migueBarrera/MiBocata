using System.Threading.Tasks;
using Models;
using Refit;

namespace MiBocata.Businnes.Services.API.Interfaces
{
    public interface IAuthApi
    {
        [Post("/api/clients/signin")]
        Task<Client> SignIn([Body] Client user);

        [Post("/api/clients/signup")]
        Task<Client> SignUp([Body] Client shopkeeper);

        [Post("/api/shopkeepers/signin")]
        Task<Shopkeeper> SignIn([Body] Shopkeeper shopkeeper);
        
        [Post("/api/shopkeepers/signup")]
        Task<Shopkeeper> SignUp([Body] Shopkeeper shopkeeper);
    }
}
