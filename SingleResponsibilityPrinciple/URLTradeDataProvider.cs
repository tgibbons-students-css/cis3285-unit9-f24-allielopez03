using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class URLTradeDataProvider : ITradeDataProvider
    {
        private readonly string url;
        private readonly ILogger logger;

        public URLTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        public async Task<IEnumerable<string>> GetTradeData()
        {
            List<string> tradeData = new List<string>();
            logger.LogInfo("Reading trades from URL: " + url);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    logger.LogWarning($"Failed to retrieve data. Status code: {response.StatusCode}");
                    throw new Exception($"Error retrieving data from URL: {url}");
                }

                // Set up a Stream and StreamReader to access the data
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    // Read each line of the text file using reader.ReadLine()
                    // Read until the reader returns a null line
                    // Add each line to the tradeData list
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        tradeData.Add(line);
                    }
                }
            }
            return tradeData;
        }
    }
}
