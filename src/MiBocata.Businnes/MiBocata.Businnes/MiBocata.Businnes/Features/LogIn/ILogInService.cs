using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Features.LogIn
{
    public interface ILogInService
    {
        Task DoLoginAsync(string email, string pass);
    }
}
