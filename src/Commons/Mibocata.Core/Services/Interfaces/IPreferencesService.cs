using Models.Core;

namespace Mibocata.Core.Services.Interfaces
{
    public interface IPreferencesService
    {
        void SetShopkeeper(Shopkeeper shopkeeper);

        void SetClient(Client client);

        Shopkeeper GetShopkeeper();

        Client GetClient();

        string GetToken();

        void SetStore(Store store);

        Store GetStore();

        void SetPushToken(string token);

        string PushToken();

        bool IsLogged();
    }
}
