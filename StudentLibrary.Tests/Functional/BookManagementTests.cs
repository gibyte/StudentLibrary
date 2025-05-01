using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Tests.Functional
{
    public class BookManagementTests
    {
        private IWebDriver driver;

        public BookManagementTests()
        {
            driver = new ChromeDriver();
        }

        [Fact]
        public void CanAddBook()
        {
            // Arrange
            driver.Navigate().GoToUrl("http://localhost:5202/Books");

            // Act
            driver.FindElement(By.Id("Title")).SendKeys("C# in Depth");
            driver.FindElement(By.Id("Author")).SendKeys("Jon Skeet");
            driver.FindElement(By.Id("Year")).SendKeys("2020");
            driver.FindElement(By.Id("SubmitButton")).Click();

            // Assert
            var confirmationMessage = driver.FindElement(By.Id("ConfirmationMessage")).Text;
            Assert.Equal("Книга успешно добавлена", confirmationMessage);

            var booksList = driver.FindElement(By.Id("BooksList")).Text;
            Assert.Contains("C# in Depth", booksList);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
