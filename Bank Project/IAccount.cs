using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    internal interface IAccount
    {
        decimal Balance { get; }
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
    }
}
