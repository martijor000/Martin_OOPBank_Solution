using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    internal static class Global
    {
        private static decimal checkingBalance = 2000M;
        private static decimal savingsBalance = 3000M;
        private static IVault vault = new Vault();

        public static decimal CheckingBalance
        {
            get { return checkingBalance; }
        }

        public static decimal SavingsBalance
        {
            get { return savingsBalance; }
        }

        public static IVault GetVault
        {
            get { return vault; }
        }

    }
}

