using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    internal class Checking : IAccount
    {
        public decimal Balance => throw new NotImplementedException();

        public void Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
