﻿
using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.State.Accounts
{
    public interface IAccountStore
    {
        User CurrentAccount { get; set; }
    }
}
