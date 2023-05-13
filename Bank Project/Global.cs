using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    public static class Global
    {
        private static double checkingBalance = 2000;
        private static double savingsBalance = 3000;

        public static double CheckingBalance
        {
            get { return checkingBalance; }
        }

        public static double SavingsBalance
        {
            get { return savingsBalance; }
        }


    }
}

