using System;
using System.Collections.Generic;
using System.Text;

namespace TestAssistant1
{
    // Represents a video game deal returned from Cheapshark.
    public class Deal
    {
        public double salePrice { get; set; }
        public int storeID { get; set; }
        public string dealID { get; set; }

        public Deal(double saleprice, int storeid, string dealid)
        {
            salePrice = saleprice;
            storeID = storeid;
            dealID = dealid;
        }
    }
}
