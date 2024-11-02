using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        //initializing variables
        private readonly string url;
        private readonly ILogger logger;
        private readonly HttpClient httpClient;


        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            //costructor values to create and read in a restfulTradeData
            this.url = url;
            this.logger = logger;
            this.httpClient = new HttpClient();
        }

        public async Task<IEnumerable<string>> GetTradeData()
        {
            try
            {
                // Make asynchronous GET request
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws if status is not 2xx

                // Read response content as a string
                string jsonData = await response.Content.ReadAsStringAsync();

                // Deserialize JSON to a list of strings (assuming JSON array of strings)
                var tradeData = JsonSerializer.Deserialize<IEnumerable<string>>(jsonData);

                return tradeData ?? new List<string>();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Error fetching trade data: {ex.Message}");
                return new List<string>();
            }
        }
    }
}
