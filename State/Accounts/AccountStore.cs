
using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.State.Accounts
{
    public class AccountStore: IAccountStore
    {
       
		private User _currentAccount;
		public User CurrentAccount
        {
			get
			{
				return _currentAccount;
			}
			set
			{
                _currentAccount = value;
			}
		}

        
    }
}
