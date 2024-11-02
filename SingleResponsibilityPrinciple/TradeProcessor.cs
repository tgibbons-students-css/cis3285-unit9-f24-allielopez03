
using SingleResponsibilityPrinciple.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class TradeProcessor
    {
        public TradeProcessor(ITradeDataProvider tradeDataProvider, ITradeParser tradeParser, ITradeStorage tradeStorage)
        {
            this.tradeDataProvider = tradeDataProvider;
            this.tradeParser = tradeParser;
            this.tradeStorage = tradeStorage;
        }

        // Mark the method as async and change the return type to Task
        public async Task ProcessTrades()
        {
            // Await the call to GetTradeData
            var lines = await tradeDataProvider.GetTradeData(); // Correct usage
            var trades = tradeParser.Parse(lines);
            tradeStorage.Persist(trades);
        }

        private readonly ITradeDataProvider tradeDataProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;
    }
}
