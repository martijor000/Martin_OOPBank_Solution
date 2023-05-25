using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    internal class SavingsAccount : Account
    {
        private const decimal MinimumBalance = 10;

        public override void Withdraw(decimal amount)
        {
            if (balance - amount < MinimumBalance)
                throw new InvalidOperationException("Insufficient funds.");

            balance -= amount;
        }
    }
}
