using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace StockExchangeMarket
{
    public class RealTimedata : StockMarket
    {
        private List<Company> StockCompanies = new List<Company>();
        public Array companies { get; set; }

        public RealTimedata(string json, TcpClient client, ref int CSeq, int SessionNum)
        {
            JObject jObject = JObject.Parse(json);
            IList<JToken> res = jObject["companies"].Children().ToList();

            foreach(JToken result in res)
            {
                string name = (string)result["name"];
                string symbol = (string)result["symbol"];
                double openPrice = Convert.ToDouble(((string)result["openPrice"]));
                double currentPrice = Convert.ToDouble(((string)result["currentPrice"]));
                double closedPrice = Convert.ToDouble(((string)result["closedPrice"]));

                addCompany(symbol, name, openPrice, closedPrice, currentPrice, client,ref CSeq, SessionNum);
            }
        }

        public void addCompany(String symbol, String _name, double price, double closePrice, double currentPrice, TcpClient client, ref int CSeq, int SessionNum)
        {
           Company _company = new Company(symbol, _name, price, this, closePrice, currentPrice, client, ref CSeq, SessionNum);
           StockCompanies.Add(_company);
        }

        public List<Company> getCompanies()
        {
            return StockCompanies;
        }

        
    }
}
