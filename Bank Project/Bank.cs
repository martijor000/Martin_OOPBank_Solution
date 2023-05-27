using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    public class Bank
    {
        private Dictionary<Customer, Dictionary<AccountType.Type, decimal>> customerAccounts;

        public Bank()
        {
            customerAccounts = new Dictionary<Customer, Dictionary<AccountType.Type, decimal>>();
        }

        public void AddCustomer(string name, decimal initialSavingsBalance, decimal initialCheckingBalance)
        {
            Customer customer = new Customer(name);
            Dictionary<AccountType.Type, decimal> accounts = new Dictionary<AccountType.Type, decimal>
        {
            { AccountType.Type.Savings, initialSavingsBalance },
            { AccountType.Type.Checking, initialCheckingBalance }
        };

            customerAccounts.Add(customer, accounts);
        }

        public bool CustomerExists(string name)
        {
            return customerAccounts.Keys.Any(c => c.Name == name);
        }

        public void DepositToAccount(string customerName, AccountType.Type accountType, decimal amount)
        {
            Customer customer = customerAccounts.Keys.FirstOrDefault(c => c.Name == customerName);

            if (customer != null && customerAccounts.TryGetValue(customer, out Dictionary<AccountType.Type, decimal> accounts))
            {
                if (accounts.ContainsKey(accountType))
                {
                    accounts[accountType] += amount;
                }
            }
        }

        public bool WithdrawFromAccount(string customerName, AccountType.Type accountType, decimal amount)
        {
            AccountType.Type otherAccountType = GetOtherAccountType(accountType);
            Customer customer = customerAccounts.Keys.FirstOrDefault(c => c.Name == customerName);

            if (customer != null && customerAccounts.TryGetValue(customer, out Dictionary<AccountType.Type, decimal> accounts))
            {
                if (accounts.ContainsKey(accountType))
                {
                    decimal currentBalance = accounts[accountType];

                    if (accountType == AccountType.Type.Savings && currentBalance + accounts[otherAccountType] - amount < 10)
                    {
                        Console.WriteLine("Withdrawal not allowed. Minimum balance of $10 must be maintained in the Savings account.");
                        return false;
                    }

                    if (currentBalance >= amount)
                    {
                        accounts[accountType] -= amount;
                        return true;
                    }
                    else if (CanOverdraft(customer, accountType, amount))
                    {
                        decimal overdraftAmount = amount - currentBalance;

                        if (accounts[otherAccountType] >= overdraftAmount)
                        {
                            accounts[accountType] = 0;
                            accounts[otherAccountType] -= overdraftAmount;
                            return true;
                        }
                    }
                }
            }

            return false;
        }



        private bool CanOverdraft(Customer customer, AccountType.Type accountType, decimal amount)
        {
            AccountType.Type otherAccountType = GetOtherAccountType(accountType);
            decimal otherAccountBalance = customerAccounts[customer][otherAccountType];
            return otherAccountBalance >= amount;
        }

        private AccountType.Type GetOtherAccountType(AccountType.Type accountType)
        {
            if (accountType == AccountType.Type.Savings)
                return AccountType.Type.Checking;
            else
                return AccountType.Type.Savings;
        }


        public List<Customer> GetCustomers()
        {
            return customerAccounts.Keys.ToList();
        }

        public decimal GetAccountBalance(Customer customer, AccountType.Type accountType)
        {
            if (customerAccounts.TryGetValue(customer, out Dictionary<AccountType.Type, decimal> accounts))
            {
                if (accounts.ContainsKey(accountType))
                {
                    return accounts[accountType];
                }
            }

            return 0;
        }

        public Customer GetCustomerByName(string customerName)
        {
            return customerAccounts.Keys.FirstOrDefault(c => c.Name == customerName);
        }
    }
}

