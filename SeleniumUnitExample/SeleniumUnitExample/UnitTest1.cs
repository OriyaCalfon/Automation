using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumUnitExample.Tests;


namespace SeleniumUnitExample
{
    [TestFixture]
    public class GoogleSearchTest
    {
        private IWebDriver driver;
        private GoogleHomePage googleHomePage;
        private GoogleResultsPage googleResultsPage;

    

       [SetUp]
        public void SetUp()
        {
            string path = "C:\\Users\\user1\\Desktop\\SeleniumUnitExample\\SeleniumUnitExample";

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver(path + @"\Drivers\");
            googleHomePage = new GoogleHomePage(driver);
            googleResultsPage = new GoogleResultsPage(driver);
        }


        [Test]
        public void TestGoogleSearch()
        {
            // Navigate to Google
            googleHomePage.NavigateTo();

            // Verify the title of the page
            Assert.AreEqual("Google", driver.Title);

            // Search for a term
            googleHomePage.Search("Selenium WebDriver");

            // Verify that results are displayed
            Assert.IsTrue(googleResultsPage.ResultsDisplayed());

            // Get the title of the first result and click it
            string firstResultTitle = googleResultsPage.GetFirstResultTitle();
            googleResultsPage.ClickFirstResult();

            // Verify the title of the new page
            Assert.IsFalse(driver.Title.Contains(firstResultTitle));

            // Navigate back to the Google search results page
            driver.Navigate().Back();

            // Verify the search box still contains the search term
            Assert.AreEqual("Selenium WebDriver", driver.FindElement(By.Name("q")).GetAttribute("value"));
        }


        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}