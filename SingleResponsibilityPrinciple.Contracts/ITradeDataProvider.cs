using System.Collections.Generic;

namespace SingleResponsibilityPrinciple.Contracts
{
    public interface ITradeDataProvider
    {
        //IEnumerable<string> GetTradeData();
        Task<IEnumerable<string>> GetTradeData(); // Update this to be asynchronous
    }
}