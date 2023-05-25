using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    internal interface IVault
    {
        void AddCash(decimal amount);
        bool RemoveCash(decimal amount);
        decimal CheckBalance();
    }
}
