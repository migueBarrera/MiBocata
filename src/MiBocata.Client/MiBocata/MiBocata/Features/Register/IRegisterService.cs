using System.Threading.Tasks;

namespace MiBocata.Features.Register
{
    public interface IRegisterService
    {
        Task DoRegisterAsync(string email, string password, string name);
    }
}
