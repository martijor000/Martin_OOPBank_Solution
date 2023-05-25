using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    internal class CheckingAccount : Account
    {
        public override void Withdraw(decimal amount)
        {
            if (balance - amount < 0)
                throw new InvalidOperationException("Insufficient funds.");

            balance -= amount;
        }
    }
}
