using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services.AuthenticationServices;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        //private readonly IAccountStore _accountStore;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            //_accountStore = accountStore;
        }

       
        

        public async Task Login(string passportNumber)
        {
            await _authenticationService.Login(passportNumber);
        }
        public async Task Login(string username,string password)
        {
            await _authenticationService.Login(username,password);
        }
        /*public void Logout()
        {
            CurrentAccount = null;
        }*/
        public event Action StateChanged;

        public async Task<RegistrationResult> Register(string username, string passportNumber, decimal deposit)
        {
            
            return await _authenticationService.Register(username, passportNumber, deposit);
        }
    }
}
