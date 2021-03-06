﻿namespace MiBocata.Services.SessionService
{
    public interface ISessionService
    {
        void Save<T>(string key, T data);

        T Get<T>(string key);

        void Remove(string key);

        void Clear();
    }
}
