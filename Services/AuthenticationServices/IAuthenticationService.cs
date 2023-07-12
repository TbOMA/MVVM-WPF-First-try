using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Services.AuthenticationServices
{
    public enum RegistrationResult
    {
        Success,
        PassportAlreadyExists,
        DepositIncorrect,
        UsernameAlreadyExists,
        EmptyPassportNumber,
        EmptyUsername
    }
    public enum LoginResult
    {
        Success,
        EmptyPassportNumber,
        UserNotRegistered
    }
    public interface IAuthenticationService
    {
        bool IsLoggedIn { get; }
        Task<RegistrationResult> Register(string username, string passportNumber, decimal deposit);
        Task<User> Login(string passportNumber);
        Task<Administrator> Login(string username,string password);
    }
}
