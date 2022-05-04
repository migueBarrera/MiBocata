namespace MiBocata.Services;

internal class AppSecretsService : IAppSecretsService
{
    public string GetUrlBase()
    {
        return DefaultSettings.URL_BASE;
    }
}
