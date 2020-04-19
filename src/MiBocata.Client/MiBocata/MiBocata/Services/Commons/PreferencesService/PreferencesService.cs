using Models;
using Newtonsoft.Json;

namespace MiBocata.Services.PreferencesService
{
    public class PreferencesService : IPreferencesService
    {
        private static string KEY_USER = nameof(KEY_USER);
        private static string KEY_API_TOKEN = nameof(KEY_API_TOKEN);

        public string GetToken()
        {
            return Xamarin.Essentials.Preferences.Get(KEY_API_TOKEN, string.Empty);
        }

        public Client GetUser()
        {
            var value = Xamarin.Essentials.Preferences.Get(KEY_USER, string.Empty);
            return JsonConvert.DeserializeObject<Client>(value);
        }

        public bool IsLogged()
        {
            return GetUser() != null;
        }

        public void SetUser(Client client)
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
