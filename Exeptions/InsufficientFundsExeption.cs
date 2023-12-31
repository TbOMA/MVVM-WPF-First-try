﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Exeptions
{
    public class InsufficientFundsExeption : Exception
    {
        public double AccountBalance { get; set; }
        public double RequiredBalance { get; set; }
        public InsufficientFundsExeption(double accountBalance, double requiredBalance)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsExeption(double accountBalance, double requiredBalance,string? message) : base(message)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsExeption(double accountBalance, double requiredBalance,string? message, Exception? innerException) : base(message, innerException)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }
    }
}
