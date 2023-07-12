using Microsoft.AspNet.Identity;
using MVVM_FirsTry.Exeptions;
using MVVM_FirsTry.Models;
using SimpleTrader.Domain.Exeptions;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAdminService _adminService;
        private readonly IAccountStore _accountStore;

        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher, IAdminService adminService, IAccountStore accountStore)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
            _adminService = adminService;
            _accountStore = accountStore;
        }
        public User CurrentAccount
        {
            get
            {
                return _accountStore.CurrentAccount;
            }
            private set
            {
                _accountStore.CurrentAccount = value;

            }
        }
        public bool IsLoggedIn => CurrentAccount != null;

        public async Task<User> Login(string passportNumber)
        {
            User storedAccount = await _accountService.GetByPassportNumber(passportNumber);
            if (passportNumber == null)
            {
                throw new EmptyField(passportNumber);
            }
            if (storedAccount == null)
            {
                throw new UserNotFoundExeption(passportNumber);
            }
            CurrentAccount = storedAccount; 
            return storedAccount;
        }
        public async Task<Administrator> Login(string username,string password)
        {
            Administrator administrator = await _adminService.GetByUsername(username);
            if (administrator == null)
            {
                throw new UserNotFoundExeption(username);
            }
            var passwordResult = _passwordHasher.VerifyHashedPassword(administrator.Password, password);
            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordExeption(username, password);
            }
            return administrator;
        }
        public async Task<RegistrationResult> Register(string username, string passportNumber, decimal deposit)
        {
            RegistrationResult result = RegistrationResult.Success;
            User usernameAccount = await _accountService.GetByUsername(username);
            if (usernameAccount != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }
            User passportNumberAccount = await _accountService.GetByPassportNumber(passportNumber);
            if (passportNumberAccount != null)
            {
                result = RegistrationResult.PassportAlreadyExists;
            }
            if (username == null)
            {
                result = RegistrationResult.EmptyUsername;
            }
            if (passportNumber == null)
            {
                result = RegistrationResult.EmptyPassportNumber;
            }
            if (result == RegistrationResult.Success)
            {
                
                User user = new User()
                {
                    Username = username,
                    PassportNumber = passportNumber,
                    Balance = deposit,
                    ClientOrders = new List<Order>()

                };
                CurrentAccount = user;

               // await _accountService.Create(user);
            }

            return result;
        }
    }
}
