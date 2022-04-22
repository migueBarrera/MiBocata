using System.Threading.Tasks;
using Models.Requests;
using Models.Responses;
using Refit;

namespace Mibocata.Core.Features.Auth
{
    public interface IAuthApi
    {
        [Post("/api/clients/signin")]
        Task<ClientSignInResponse> SignIn([Body] ClientSignInRequest request);

        [Post("/api/clients/signup")]
        Task<ClientSignUpResponse> SignUp([Body] ClientSignUpRequest request);

        [Post("/api/shopkeepers/signin")]
        Task<ShopkeeperSignInResponse> SignIn([Body] ShopkeeperSignInRequest request);

        [Post("/api/shopkeepers/signup")]
        Task<ShopkeeperSignUpResponse> SignUp([Body] ShopkeeperSignUpRequest request);
    }
}
