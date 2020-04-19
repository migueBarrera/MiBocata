using Models;

namespace MiBocata.Services.PreferencesService
{
    public interface IPreferencesService
    {
        void SetUser(Client client);

        Client GetUser();

        bool IsLogged();

        string GetToken();
    }
}
