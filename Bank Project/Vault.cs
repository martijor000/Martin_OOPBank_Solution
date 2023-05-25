using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    internal class Vault : IVault
    {
        private decimal cashBalance;

        public Vault()
        {
            
        }

        public void AddCash(decimal amount)
        {
            cashBalance += amount;
        }

        public bool RemoveCash(decimal amount)
        {
            if (cashBalance >= amount)
            {
                cashBalance -= amount;
                return true;
            }
            return false;
        }

        public decimal CheckBalance()
        {
            return cashBalance;
        }

    }
}
