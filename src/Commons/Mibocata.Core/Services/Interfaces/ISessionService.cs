namespace Mibocata.Core.Services.Interfaces;

public interface ISessionService
{
    void Save<T>(string key, T data);

    T Get<T>(string key);

    void Remove(string key);

    void Clear();
}
