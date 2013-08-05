using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlowManager.Common.POCO;
using CashFlowManager.Contract.Entities;


namespace CashFlowManager.Common.Helpers
{
    public class TransactionHelper
    {
        

        public List<ClientChartData> GenerateData(List<TransactionInfo> transactions, int numberMonths )
        {
            DateTime now = DateTime.Now;
            DateTime finish = DateTime.Now.AddMonths(numberMonths-1);
            List<ClientChartData> data = new List<ClientChartData>();

            while (now <= finish)
            {
                foreach (TransactionInfo t in transactions)
                {
                    if (t.StartDate != null)
                    {
                        switch (t.ScheduleTypeEnum)
                        {
                            case (Enumerations.Enumerations.ScheduleType.OneOff):
                                {
                                    if (t.StartDate.Value.Day == now.Day && t.StartDate.Value.Month == now.Month &&
                                        t.StartDate.Value.Year == now.Year)
                                    {
                                        if (t.TransactionTypeEnum == Enumerations.Enumerations.TransactionType.Income)
                                            data.Add(new ClientChartData() {Income = t.Amount ?? 0, Date = now});
                                        else
                                            data.Add(new ClientChartData() {Expense = t.Amount ?? 0, Date = now});
                                    }
                                    break;
                                }
                            case (Enumerations.Enumerations.ScheduleType.Daily):
                                {
                                    if (t.TransactionTypeEnum == Enumerations.Enumerations.TransactionType.Income)
                                        data.Add(new ClientChartData() {Income = t.Amount ?? 0, Date = now});
                                    else
                                        data.Add(new ClientChartData() {Expense = t.Amount ?? 0, Date = now});
                                    break;
                                }
                            case (Enumerations.Enumerations.ScheduleType.Weekly):
                                {
                                    var timeSpan = now.Date.Subtract(t.StartDate.Value.Date);
                                    var remainder = timeSpan.TotalDays%7.0;
                                    if (remainder == 0)
                                    {
                                        if (t.TransactionTypeEnum == Enumerations.Enumerations.TransactionType.Income)
                                            data.Add(new ClientChartData() {Income = t.Amount ?? 0, Date = now});
                                        else
                                            data.Add(new ClientChartData() {Expense = t.Amount ?? 0, Date = now});
                                    }
                                    break;
                                }
                            case (Enumerations.Enumerations.ScheduleType.Fortnightly):
                                {
                                    var timeSpan = now.Date.Subtract(t.StartDate.Value.Date);
                                    var remainder = timeSpan.TotalDays%14.0;
                                    if (remainder == 0)
                                    {
                                        if (t.TransactionTypeEnum == Enumerations.Enumerations.TransactionType.Income)
                                            data.Add(new ClientChartData() {Income = t.Amount ?? 0, Date = now});
                                        else
                                            data.Add(new ClientChartData() {Expense = t.Amount ?? 0, Date = now});
                                    }
                                    break;
                                }
                            case (Enumerations.Enumerations.ScheduleType.Monthly):
                                {

                                    var remainder = DateTimeSpan.CompareDates(now.Date.Date, t.StartDate.Value.Date);

                                    if (remainder.Months > 0 && remainder.Days == 0)
                                    {
                                        if (t.TransactionTypeEnum == Enumerations.Enumerations.TransactionType.Income)
                                            data.Add(new ClientChartData() {Income = t.Amount ?? 0, Date = now});
                                        else
                                            data.Add(new ClientChartData() {Expense = t.Amount ?? 0, Date = now});
                                    }
                                    break;
                                }
                            case (Enumerations.Enumerations.ScheduleType.Yearly):
                                {
                                    var remainder = DateTimeSpan.CompareDates(now.Date.Date, t.StartDate.Value.Date);

                                    if (remainder.Years > 0)
                                    {
                                        if (t.TransactionTypeEnum == Enumerations.Enumerations.TransactionType.Income)
                                            data.Add(new ClientChartData() {Income = t.Amount ?? 0, Date = now});
                                        else
                                            data.Add(new ClientChartData() {Expense = t.Amount ?? 0, Date = now});
                                    }
                                    break;
                                }
                        }
                    }
                }
                data.Add(new ClientChartData() { Income = 0, Expense = 0, CashOnHand = 0, Date = now });
                now = now.AddDays(1);
            }
            return data;
        }

        public List<ClientChartData> SummariseData( List<ClientChartData> chartData,List<BankAccountInfo> bankAccounts )
        {
            List<ClientChartData>  SummaryChartData = new List<ClientChartData>();
            DateTime? date = null;
            foreach (ClientChartData c in chartData.OrderBy(c => c.Date))
            {
                if (date == null || (c.Date.Date > date))
                {
                    date = c.Date.Date;
                }
                else
                {
                    continue;
                }

                ClientChartData day = new ClientChartData();
                day.Expense = chartData.Where(t => t.Date.Date == c.Date.Date).Sum(t => t.Expense);
                day.Income = chartData.Where(t => t.Date.Date == c.Date.Date).Sum(t => t.Income);

                var incomeToDate = (from ch in chartData where ch.Date.Date == date.Value.Date select ch).Sum(t => t.Income);
                var expenseToDate = (from e in chartData where e.Date.Date == date.Value.Date select e).Sum(t => t.Expense);
                var sumBankAccounts = bankAccounts.Sum(b => b.Balance).Value;
                day.CashOnHand = (sumBankAccounts + incomeToDate) - expenseToDate;
                day.Date = c.Date.Date;

                SummaryChartData.Add(day);
            }

            return SummaryChartData.OrderBy(s=>s.Date).ToList();
        }
    }
}
