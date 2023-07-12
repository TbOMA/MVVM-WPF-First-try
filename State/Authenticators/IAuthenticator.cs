using MVVM_FirsTry.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.State.Authenticators
{
    public interface IAuthenticator
    {
/*        bool IsLoggedIn { get; }
*/        event Action StateChanged;
        Task<RegistrationResult> Register(string username, string passportNumber, decimal deposit);
        Task Login(string passportNumber);
        Task Login(string username, string password);   
/*        void Logout();
*/    }
}
