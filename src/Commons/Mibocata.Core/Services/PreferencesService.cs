using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Newtonsoft.Json;

namespace Mibocata.Core.Services
{
    // todo investigar sharedName
    public class PreferencesService : IPreferencesService
    {
        private static string KEY_USER = nameof(KEY_USER);
        private static string KEY_SHOPKEEPER = nameof(KEY_SHOPKEEPER);
        private static string KEY_API_TOKEN = nameof(KEY_API_TOKEN);
        private static string KEY_STORE = nameof(KEY_STORE);
        private static string KEY_PUSH_TOKEN = nameof(KEY_PUSH_TOKEN);

        public Store GetStore()
        {
            var value = Xamarin.Essentials.Preferences.Get(KEY_STORE, string.Empty);
            return JsonConvert.DeserializeObject<Store>(value);
        }

        public string GetToken()
        {
            return Xamarin.Essentials.Preferences.Get(KEY_API_TOKEN, string.Empty);
        }

        public Shopkeeper GetShopkeeper()
        {
            var value = Xamarin.Essentials.Preferences.Get(KEY_SHOPKEEPER, string.Empty);
            return JsonConvert.DeserializeObject<Shopkeeper>(value);
        }

        public Client GetClient()
        {
            var value = Xamarin.Essentials.Preferences.Get(KEY_USER, string.Empty);
            return JsonConvert.DeserializeObject<Client>(value);
        }

        public bool IsLogged()
        {
            return GetShopkeeper() != null || GetClient() != null;
        }

        public string PushToken()
        {
            return Xamarin.Essentials.Preferences.Get(KEY_PUSH_TOKEN, string.Empty);
        }

        public void SetPushToken(string token)
        {
            Xamarin.Essentials.Preferences.Set(KEY_PUSH_TOKEN, token);
        }

        public void SetStore(Store store)
        {
            if (store == null)
            {
                Xamarin.Essentials.Preferences.Set(KEY_STORE, string.Empty);
            }
            else
            {
                string output = JsonConvert.SerializeObject(store);
                Xamarin.Essentials.Preferences.Set(KEY_STORE, output);
            }
        }

        public void SetShopkeeper(Shopkeeper shopkeeper)
        {
            if (shopkeeper == null)
            {
                Xamarin.Essentials.Preferences.Set(KEY_SHOPKEEPER, string.Empty);
                Xamarin.Essentials.Preferences.Set(KEY_API_TOKEN, string.Empty);
            }
            else
            {
                string output = JsonConvert.SerializeObject(shopkeeper);
                Xamarin.Essentials.Preferences.Set(KEY_SHOPKEEPER, output);
                Xamarin.Essentials.Preferences.Set(KEY_API_TOKEN, shopkeeper.Token);
            }
        }

        public void SetClient(Client client)
        {
            if (client == null)
            {
                Xamarin.Essentials.Preferences.Set(KEY_USER, string.Empty);
                Xamarin.Essentials.Preferences.Set(KEY_API_TOKEN, string.Empty);
            }
            else
            {
                string output = JsonConvert.SerializeObject(client);
                Xamarin.Essentials.Preferences.Set(KEY_USER, output);
                Xamarin.Essentials.Preferences.Set(KEY_API_TOKEN, client.Token);
            }
        }
    }
}
