using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManager.Common.POCO
{
    public class ClientChartData
    {
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal CashOnHand { get; set; }
        public DateTime Date { get; set; }
        public string DateStr
        {
            get
            {
                if (Date > DateTime.Now.AddDays(-1))
                    return Date.Date.ToShortDateString();
                else
                {
                    return string.Empty;
                }
            }
        }

    }
}
