using Bank_Project;
using System;


BankMenu();

static bool TryParseDecimal(string input, out decimal value)
{
    return decimal.TryParse(input, out value);
}

static void BankMenu()
{
    Bank bank = new Bank();

    Console.WriteLine("Welcome to the Martin Bank!");
    while (true)
    {
        Console.WriteLine("\nPlease select an option:");
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. Deposit");
        Console.WriteLine("3. Withdraw");
        Console.WriteLine("4. Check Vault Balance");
        Console.WriteLine("5. Add Cash to Vault");
        Console.WriteLine("6. Remove Cash from Vault");
        Console.WriteLine("7. Exit");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();
        if (!int.TryParse(choice, out int menuChoice) || menuChoice < 1 || menuChoice > 7)
        {
            Console.WriteLine("Invalid choice. Please try again.");
            continue;
        }
        switch (menuChoice)
        {
            case 1:
                Console.Write("Enter the customer name: ");
                string customerName = Console.ReadLine();
                bank.AddCustomer(customerName, Global.SavingsBalance, Global.CheckingBalance);
                Console.WriteLine($"Customer '{customerName}' added with initial balances of $3000 (Savings) and $2000 (Checking).");
                break;
            case 2:
                Console.Write("Enter the customer name: ");
                string depositCustomerName = Console.ReadLine();
                Customer depositCustomer = bank.GetCustomerByName(depositCustomerName);
                if (depositCustomer != null)
                {
                    ProcessTransaction(bank, depositCustomer, TransactionType.Type.Deposit);
                }
                else
                {
                    Console.WriteLine($"Customer '{depositCustomerName}' does not exist.");
                }
                break;
            case 3:
                Console.Write("Enter the customer name: ");
                string withdrawalCustomerName = Console.ReadLine();
                Customer withdrawalCustomer = bank.GetCustomerByName(withdrawalCustomerName);
                if (withdrawalCustomer != null)
                {
                    ProcessTransaction(bank, withdrawalCustomer, TransactionType.Type.Withdrawal);
                }
                else
                {
                    Console.WriteLine($"Customer '{withdrawalCustomerName}' does not exist.");
                }
                break;
            case 4:
                Console.WriteLine("Vault Balance: $" + Global.GetVault.CheckBalance());
                break;
            case 5:
                AddCashToVault();
                break;
            case 6:
                RemoveCashFromVault();
                break;
            case 7:
                Console.WriteLine("Thank you for using the Martin Bank. Goodbye!");
                return;
        }
        Console.WriteLine("\nCurrent Balances:");
        foreach (Customer customer in bank.GetCustomers())
        {
            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine("Savings Balance: $" + bank.GetAccountBalance(customer, AccountType.Type.Savings));
            Console.WriteLine("Checking Balance: $" + bank.GetAccountBalance(customer, AccountType.Type.Checking));
            Console.WriteLine();
        }
    }
}

static void ProcessTransaction(Bank bank, Customer customer, TransactionType.Type transactionType)
{
    Console.Write("Enter the account type (Savings/Checking): ");
    string accountTypeInput = Console.ReadLine();

    if (!Enum.TryParse(accountTypeInput, true, out AccountType.Type accountType))
    {
        Console.WriteLine("Invalid account type. Transaction failed.");
        return;
    }

    Console.Write("Enter the amount: ");
    string amountInput = Console.ReadLine();

    if (TryParseDecimal(amountInput, out decimal amount) && amount >= 0)
    {
        switch (transactionType)
        {
            case TransactionType.Type.Deposit:
                bank.DepositToAccount(customer.Name, accountType, amount);
                Console.WriteLine($"Deposit successful. Amount of ${amount} deposited to {accountType} for customer '{customer.Name}'.");
                break;
            case TransactionType.Type.Withdrawal:
                bool success = bank.WithdrawFromAccount(customer.Name, accountType, amount);

                if (success)
                    Console.WriteLine($"Withdrawal successful. Amount of ${amount} withdrawn from {accountType} for customer '{customer.Name}'.");
                else
                    Console.WriteLine("Insufficient funds. Withdrawal failed.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Invalid amount. Transaction failed.");
    }
}

static void AddCashToVault()
{
    Console.Write("Enter the amount to add to the vault: ");
    string amountInput = Console.ReadLine();

    if (TryParseDecimal(amountInput, out decimal amount) && amount >= 0)
    {
        Global.GetVault.AddCash(amount);
        Console.WriteLine($"Added ${amount} to the vault.");
    }
    else
    {
        Console.WriteLine("Invalid amount. Cash addition to the vault failed.");
    }
}

static void RemoveCashFromVault()
{
    Console.Write("Enter the amount to remove from the vault: ");
    string amountInput = Console.ReadLine();

    if (TryParseDecimal(amountInput, out decimal amount) && amount >= 0)
    {
        if (Global.GetVault.RemoveCash(amount))
        {
            Global.GetVault.RemoveCash(amount);
            Console.WriteLine($"Removed ${amount} from the vault.");
        }
        else
        {
            Console.WriteLine("Insufficient cash in the vault. Cash removal failed.");
        }
    }
    else
    {
        Console.WriteLine("Invalid amount. Cash removal from the vault failed.");
    }
}
   