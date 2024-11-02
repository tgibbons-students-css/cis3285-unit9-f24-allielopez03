using Microsoft.Data.SqlClient;
using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;
using Microsoft.VisualStudio.TestTools.UnitTesting; // Ensure you're using the right namespace for MSTest
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class URLTradeDataProviderTests
    {
        private int countStrings(IEnumerable<string> collectionOfStrings)
        {
            // Count the trades
            int count = 0;
            foreach (var trade in collectionOfStrings)
            {
                count++;
            }
            return count;
        }

        [TestMethod()]
        public async Task TestSize1() // Marking the method as async
        {
            // Arrange
            ILogger logger = new ConsoleLogger();
            string tradeURL = "http://raw.githubusercontent.com/tgibbons-css/CIS3285_Unit9_F24/refs/heads/master/SingleResponsibilityPrinciple/trades.txt";

            ITradeDataProvider tradeProvider = new URLTradeDataProvider(tradeURL, logger);

            // Act
            IEnumerable<string> trades = await tradeProvider.GetTradeData(); // Awaiting the asynchronous call

            // Assert
            Assert.AreEqual(countStrings(trades), 4);
        }
    }
}