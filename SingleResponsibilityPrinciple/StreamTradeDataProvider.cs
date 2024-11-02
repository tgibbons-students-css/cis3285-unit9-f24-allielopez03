using System.Collections.Generic;
using System.IO;

using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class StreamTradeDataProvider : ITradeDataProvider
    {
        private readonly Stream stream;
        private readonly ILogger logger;

        public StreamTradeDataProvider(Stream stream, ILogger logger)
        {
            this.stream = stream;
            this.logger = logger;
        }

        public Task<IEnumerable<string>> GetTradeData()
        {
            var tradeData = new List<string>();
            logger.LogInfo("Reading trades from file stream.");
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tradeData.Add(line);
                }
            }

            return Task.FromResult<IEnumerable<string>>(tradeData);
        }
    }
}
