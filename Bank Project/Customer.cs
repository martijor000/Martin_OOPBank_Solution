using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    internal class Customer
    {
        public string Name { get; private set; }

        public Customer(string name)
        {
            Name = name;
        }
    }
}
