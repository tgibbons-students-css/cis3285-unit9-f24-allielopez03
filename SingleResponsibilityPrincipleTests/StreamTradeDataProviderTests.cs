using Microsoft.Data.SqlClient;
using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class StreamTradeDataProviderTests
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
            String fileName = "SingleResponsibilityPrincipleTests.trades_1good.txt";
            Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);

            ITradeDataProvider tradeProvider = new StreamTradeDataProvider(tradeStream, logger);

            // Act
            IEnumerable<string> trades = await tradeProvider.GetTradeData(); // Awaiting the asynchronous call

            // Assert
            Assert.AreEqual(countStrings(trades), 1);
        }

        [TestMethod()]
        public async Task TestSize5() // Marking the method as async
        {
            // Arrange
            ILogger logger = new ConsoleLogger();
            String fileName = "SingleResponsibilityPrincipleTests.trades_5good.txt";
            Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);

            ITradeDataProvider tradeProvider = new StreamTradeDataProvider(tradeStream, logger);

            // Act
            IEnumerable<string> trades = await tradeProvider.GetTradeData(); // Awaiting the asynchronous call

            // Assert
            Assert.AreEqual(countStrings(trades), 5);
        }
    }
}