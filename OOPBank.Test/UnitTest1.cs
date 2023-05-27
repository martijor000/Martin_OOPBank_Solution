using Bank_Project;
using Moq;

namespace OOPBank.Test
{
    [TestFixture]
    public class BankMockTests
    {
        private Bank bank;
        private Mock<Customer> mockCustomer;

        [SetUp]
        public void Setup()
        {
            bank = new Bank();
            mockCustomer = new Mock<Customer>("Jordan");
        }

        [Test]
        public void AddCustomer_ValidInput_CustomerAdded()
        {
            // Arrange
            decimal initialSavingsBalance = 3000;
            decimal initialCheckingBalance = 2000;

            // Act
            bank.AddCustomer(mockCustomer.Name, initialSavingsBalance, initialCheckingBalance);

            // Assert
            Assert.IsTrue(bank.CustomerExists(mockCustomer.Name));
        }

        [Test]
        public void DepositToAccount_ValidInput_AmountDeposited()
        {
            // Arrange
            AccountType accountType = AccountType.Type.Savings;
            decimal initialBalance = 500;
            decimal depositAmount = 200;
            mockCustomer.Setup(c => c.Name).Returns("Jordan");
            bank.AddCustomer(mockCustomer.Name, initialBalance, 0);

            // Act
            bank.DepositToAccount(mockCustomer.Object.Name, accountType, depositAmount);

            // Assert
            decimal expectedBalance = initialBalance + depositAmount;
            decimal actualBalance = bank.GetAccountBalance(mockCustomer.Object, accountType);
            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [Test]
        public void WithdrawFromAccount_SufficientBalance_AmountWithdrawn()
        {
            // Arrange
            AccountType accountType = AccountType.Checking;
            decimal initialBalance = 1000;
            decimal withdrawalAmount = 500;
            mockCustomer.Setup(c => c.Name).Returns("Jordan");
            bank.AddCustomer(mockCustomer.Name, 0, initialBalance);

            // Act
            bool result = bank.WithdrawFromAccount(mockCustomer.Object.Name, accountType, withdrawalAmount);

            // Assert
            Assert.IsTrue(result);
            decimal expectedBalance = initialBalance - withdrawalAmount;
            decimal actualBalance = bank.GetAccountBalance(mockCustomer.Object, accountType);
            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [Test]
        public void WithdrawFromAccount_InsufficientBalance_Failure()
        {
            // Arrange
            AccountType accountType = AccountType.Type.Savings;
            decimal initialBalance = 100;
            decimal withdrawalAmount = 200;
            mockCustomer.Setup(c => c.Name).Returns("Jordan");
            bank.AddCustomer(mockCustomer.Name, initialBalance, 0);

            // Act
            bool result = bank.WithdrawFromAccount(mockCustomer.Object.Name, accountType, withdrawalAmount);

            // Assert
            Assert.IsFalse(result);
            decimal actualBalance = bank.GetAccountBalance(mockCustomer.Object, accountType);
            Assert.AreEqual(initialBalance, actualBalance);
        }
    }
}
