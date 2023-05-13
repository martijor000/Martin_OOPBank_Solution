using Bank_Project;
using System;


Run();

static void Run()
{

    bool exit = false;
    while (!exit)
    {
        Console.WriteLine("1. Deposit");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Exit");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Deposit();
                break;
            case "2":
                Withdraw();
                break;
            case "3":
                exit = true;
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

        Console.WriteLine();
    }
}
//static void Deposit()
//{
//    Console.Write("Enter the amount to deposit: ");
//    double amount = Convert.ToDouble(Console.ReadLine());

//    Global.CheckingBalance += amount;
//    Console.WriteLine($"Deposit successful. New checking balance: {checkingBalance}");
//}

//static void Withdraw()
//{
//    Console.Write("Enter the amount to withdraw: ");
//    double amount = Convert.ToDouble(Console.ReadLine());

//    if (amount > checkingBalance + savingsBalance)
//    {
//        throw new Exception("Insufficient funds!");
//    }

//    if (amount > checkingBalance)
//    {
//        double shortage = amount - checkingBalance;
//        savingsBalance -= shortage;
//        checkingBalance = 0;
//        Console.WriteLine($"Withdrawal successful. New checking balance: {checkingBalance}");
//        Console.WriteLine($"Funds transferred from savings: {shortage}");
//    }
//    else
//    {
//        checkingBalance -= amount;
//        Console.WriteLine($"Withdrawal successful. New checking balance: {checkingBalance}");
//    }
//}
