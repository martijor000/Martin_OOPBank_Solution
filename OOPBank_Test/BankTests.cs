using Bank_Project;
using Moq;

namespace OOPBank_Test
{
    [TestClass]
    public class BankTests
    {
        private Bank bank;

        [TestInitialize]
        public void Initialize()
        {
            bank = new Bank();
        }

        [TestMethod]
        public void AddCustomer()
        {
            // Arrange
            string name = "Jordan";
            decimal initialSavingsBalance = 1000;
            decimal initialCheckingBalance = 500;

            // Act
            bank.AddCustomer(name, initialSavingsBalance, initialCheckingBalance);

            // Assert
            Assert.IsTrue(bank.CustomerExists(name));
            Customer customer = bank.GetCustomerByName(name);
            Assert.IsNotNull(customer);
            Assert.AreEqual(name, customer.Name);
            Assert.AreEqual(initialSavingsBalance, bank.GetAccountBalance(customer, AccountType.Type.Savings));
            Assert.AreEqual(initialCheckingBalance, bank.GetAccountBalance(customer, AccountType.Type.Checking));
        }

        [TestMethod]
        public void CustomerExists()
        {
            // Arrange
            string name = "Jordan";
            bank.AddCustomer(name, 1000, 500);

            // Act
            bool exists = bank.CustomerExists(name);

            // Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void CustomerDoesNotExist()
        {
            // Arrange
            string name = "Jordam";

            // Act
            bool exists = bank.CustomerExists(name);


            // Assert
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void DepositToAccount_WhenCustomerDoesNotExist()
        {
            // Arrange
            string existingCustomerName = "John Doe";
            string nonExistingCustomerName = "Jane Smith";
            decimal initialSavingsBalance = 1000;
            decimal initialCheckingBalance = 500;
            decimal depositAmount = 200;
            bank.AddCustomer(existingCustomerName, initialSavingsBalance, initialCheckingBalance);

            // Act
            bank.DepositToAccount(nonExistingCustomerName, AccountType.Type.Savings, depositAmount);

            // Assert
            Customer existingCustomer = bank.GetCustomerByName(existingCustomerName);
            Assert.AreEqual(initialSavingsBalance, bank.GetAccountBalance(existingCustomer, AccountType.Type.Savings));
        }

        [TestMethod]
        public void DepositToAccount()
        {
            // Arrange
            string name = "Jordan";
            decimal initialSavingsBalance = 1000;
            decimal initialCheckingBalance = 500;
            decimal depositAmount = 200;
            bank.AddCustomer(name, initialSavingsBalance, initialCheckingBalance);

            // Act
            bank.DepositToAccount(name, AccountType.Type.Checking, depositAmount);

            // Assert
            Customer customer = bank.GetCustomerByName(name);
            Assert.AreEqual(initialCheckingBalance + depositAmount, bank.GetAccountBalance(customer, AccountType.Type.Checking));
        }

        [TestMethod]
        public void WithdrawFromAccount()
        {
            // Arrange
            string name = "Jordan";
            decimal initialSavingsBalance = 1000;
            decimal initialCheckingBalance = 500;
            decimal withdrawalAmount = 200;
            bank.AddCustomer(name, initialSavingsBalance, initialCheckingBalance);

            // Act
            bool success = bank.WithdrawFromAccount(name, AccountType.Type.Savings, withdrawalAmount);

            // Assert
            Assert.IsTrue(success);
            Customer customer = bank.GetCustomerByName(name);
            Assert.AreEqual(initialSavingsBalance - withdrawalAmount, bank.GetAccountBalance(customer, AccountType.Type.Savings));
        }

        [TestMethod]
        public void WithdrawToAccount_WhenCustomerNotPresent()
        {
            // Arrange
            string existingCustomerName = "John Doe";
            string nonExistingCustomerName = "Jane Smith";
            decimal initialSavingsBalance = 1000;
            decimal initialCheckingBalance = 500;
            decimal depositAmount = 200;
            bank.AddCustomer(existingCustomerName, initialSavingsBalance, initialCheckingBalance);

            // Act
            bank.WithdrawFromAccount(nonExistingCustomerName, AccountType.Type.Savings, depositAmount);

            // Assert
            Customer existingCustomer = bank.GetCustomerByName(existingCustomerName);
            Assert.AreEqual(initialSavingsBalance, bank.GetAccountBalance(existingCustomer, AccountType.Type.Savings));
        }

        [TestMethod]
        public void WithdrawFromAccount_WhenSavingsAccountHasInsufficientBalance()
        {
            // Arrange
            string customerName = "John Doe";
            decimal initialSavingsBalance = 50;
            decimal initialCheckingBalance = 0;
            bank.AddCustomer(customerName, initialSavingsBalance, initialCheckingBalance);

            AccountType.Type accountType = AccountType.Type.Savings;
            decimal amount = 60;

            // Act
            bool result = bank.WithdrawFromAccount(customerName, accountType, amount);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WithdrawFromCheckingAccount()
        {
            // Arrange
            string customerName = "John Doe";
            decimal initialSavingsBalance = 0;
            decimal initialCheckingBalance = 50;
            bank.AddCustomer(customerName, initialSavingsBalance, initialCheckingBalance);

            AccountType.Type accountType = AccountType.Type.Checking ;
            decimal amount = 40;

            // Act
            bool result = bank.WithdrawFromAccount(customerName, accountType, amount);

            // Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void WithdrawFromSavingsAccount()
        {
            // Arrange
            string customerName = "John Doe";
            decimal initialSavingsBalance = 0;
            decimal initialCheckingBalance = 50;
            bank.AddCustomer(customerName, initialSavingsBalance, initialCheckingBalance);

            AccountType.Type accountType = AccountType.Type.Savings;
            decimal amount = 40;

            // Act
            bool result = bank.WithdrawFromAccount(customerName, accountType, amount);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WithdrawFromAccount_Overdrafting()
        {
            // Arrange
            string customerName = "John Doe";
            decimal initialSavingsBalance = 50;
            decimal initialCheckingBalance = 100;
            bank.AddCustomer(customerName, initialSavingsBalance, initialCheckingBalance);

            Customer customer = bank.GetCustomerByName(customerName); 

            AccountType.Type accountType = AccountType.Type.Savings;
            decimal amount = 70;

            // Act
            bool result = bank.WithdrawFromAccount(customerName, accountType, amount); 

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, bank.GetAccountBalance(customer, AccountType.Type.Savings));
            Assert.AreEqual(80, bank.GetAccountBalance(customer, AccountType.Type.Checking));
        }






    }

}
