﻿using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.Navigation
{
    public interface IMiBocataNavigationService
    {
        Task InitializeAsync();

        Task NavigateToLogIn();

        Task NavigateToRegister();

        Task NavigateToHome();

        Task NavigateToCreateStore();

        Task NavigateToChooseLocationStore();
    }
}
