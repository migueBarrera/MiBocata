using Models.Core;

namespace MiBocata.Businnes.Services.Commons.Preferences
{
    public interface IPreferencesService
    {
        void SetUser(Shopkeeper shopkeeper);

        Shopkeeper GetUser();

        string GetToken();

        void SetStore(Store store);

        Store GetStore();

        void SetPushToken(string token);

        string PushToken();
    }
}
