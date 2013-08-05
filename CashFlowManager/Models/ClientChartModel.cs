using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using CashFlowManager.Common.Enumerations;
using CashFlowManager.Common.Helpers;
using CashFlowManager.Common.POCO;
using CashFlowManager.Contract;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Service;

namespace CashFlowManager.Models
{
    public class ClientChartModel
    {

        public List<TransactionInfo> Transactions { get; set; }
        public List<ClientChartData> ChartData { get; private set; }
        public List<ClientChartData> SummaryChartData { get; private set; }
        public List<BankAccountInfo> BankAccounts { get; set; }
        public string ClientId { get; set; }

        public string PracticeId { get; set; }

        public void Load(string clientId, string practiceId, int numberMonths)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            Transactions = service.GetTransactions(new Guid(clientId));
            BankAccounts = service.GetBankAccounts(new Guid(clientId));
            PracticeId = practiceId;
            ClientId = clientId;

            TransactionHelper helper = new TransactionHelper();
            var results = helper.GenerateData(Transactions, numberMonths);
            SummaryChartData = helper.SummariseData(results, BankAccounts);
        }



        public List<ClientChartData> SummaryChartGroupByMonth(List<ClientChartData> data)
        {
            List<ClientChartData> groupByMonth = new List<ClientChartData>();
            var grouped = (from d in data select d).GroupBy(g => g.Date.ToString("MMM"));
            
            foreach (var g in grouped)
            {
                var sumExp = (from i in g select i.Expense).Sum();
                var sumCoH = (from i in g select i.CashOnHand).Sum();
                var sumInc = (from i in g select i.Income).Sum();

                groupByMonth.Add(new ClientChartData()
                {
                    CashOnHand = sumCoH,
                    Expense = sumExp,
                    Income = sumInc,
                    DateGroup = g.Key
                });

            }

            return groupByMonth;
        }

        public List<ClientChartData> SummaryChartGroupByWeeks(List<ClientChartData> data)
        {
            List<ClientChartData> GroupBWeek = new List<ClientChartData>();

             DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            var grouped = (from d in data select d).GroupBy(g => cal.GetWeekOfYear(g.Date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday));

            foreach (var g in grouped)
            {
                var sumExp = (from i in g select i.Expense).Sum();
                var sumCoH = (from i in g select i.CashOnHand).Sum();
                var sumInc = (from i in g select i.Income).Sum();

                GroupBWeek.Add(new ClientChartData()
                {
                    CashOnHand = sumCoH,
                    Expense = sumExp,
                    Income = sumInc,
                    DateGroup = g.Key.ToString()
                });

            }

            return GroupBWeek;
        }




    }

 
}