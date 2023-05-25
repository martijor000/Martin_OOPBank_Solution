using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    abstract class Account : IAccount
    {
        protected decimal balance;

        public decimal Balance
        {
            get { return balance; }
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Invalid deposit amount.");

            balance += amount;
        }

        public abstract void Withdraw(decimal amount);
    }


}
