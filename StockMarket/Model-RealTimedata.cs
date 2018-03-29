using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockExchangeMarket
{
    public class RealTimedata : StockMarket
    {
        private List<Company> StockCompanies = new List<Company>();

        public void addCompany(String symbol, String _name, double price)
        {
           Company _company = new Company(symbol, _name, price, this);
           StockCompanies.Add(_company);
        }

        public List<Company> getCompanies()
        {
            return StockCompanies;
        }
    }
}
