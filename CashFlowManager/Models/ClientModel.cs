using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowManager.Common.Enumerations;
using CashFlowManager.Common.Helpers;
using CashFlowManager.Common.POCO;
using CashFlowManager.Contract;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Service;
using WebMatrix.WebData;

namespace CashFlowManager.Models
{
    public class ClientModel
    {
        public List<ClientInfo> Clients { get; set; }

        public ClientInfo SelectedClient { get; set; }

        public Guid PracticeId { get; set; }

        private List<TransactionInfo> Transactions { get; set; }

        public List<BankAccountInfo> BankAccounts { get; set; }

        public List<ClientChartData> SummaryChartData { get; private set; }

        public void LoadData(string practiceId)
        {
            ICashFlowManagerService service = new CashFlowManagerService();

            PracticeId = new Guid(practiceId);
            Clients = service.GetClients(PracticeId);

            Transactions = service.GetTransactions();
            BankAccounts = service.GetBankAccounts();
            PracticeId = new Guid(practiceId);
            PopulateClientPosition();

        }

        public void PopulateClientPosition()
        {
            TransactionHelper helper = new TransactionHelper();

            foreach (ClientInfo c in Clients)
            {
                if (c.ClientPosition == null)
                    c.ClientPosition = new List<ClientPositionIndicator>();

                var results = helper.GenerateData(Transactions.Where(t => t.ClientId == c.Id).ToList(), 3);
                if (Transactions.Any(t => t.ClientId == c.Id))
                {

                    var bas = BankAccounts.Where(b => b.ClientId == c.Id).ToList();
                    SummaryChartData = helper.SummariseData(results, bas);

                    //Forcasting 7 Days 
                    ClientPositionIndicator day1 = new ClientPositionIndicator();
                    day1.Date = DateTime.Now.Date;
                    day1.Name = "Day 1";
                    var p1 = (from sd in SummaryChartData where sd.Date.Date == day1.Date select sd.CashOnHand).SingleOrDefault();
                    var e1 = (from sd in SummaryChartData where sd.Date.Date == day1.Date select sd.Expense).SingleOrDefault();
                    day1.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p1, e1);
                    day1.Position = AssignZone(p1, e1);
                    c.ClientPosition.Add(day1);

                    ClientPositionIndicator day2 = new ClientPositionIndicator();
                    day2.Date = DateTime.Now.Date.AddDays(1);
                    day2.Name = "Day 2";
                    var p2 = (from sd in SummaryChartData where sd.Date.Date <= day2.Date select sd.CashOnHand).Sum();
                    var e2 = (from sd in SummaryChartData where sd.Date.Date <= day2.Date select sd.Expense).Sum();
                    day2.Position = AssignZone(p2, e2);
                    day2.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p2, e2);
                    c.ClientPosition.Add(day2);

                    ClientPositionIndicator day3 = new ClientPositionIndicator();
                    day3.Date = DateTime.Now.Date.AddDays(2);
                    day3.Name = "Day 3";
                    var p3 = (from sd in SummaryChartData where sd.Date.Date <= day3.Date select sd.CashOnHand).Sum();
                    var e3 = (from sd in SummaryChartData where sd.Date.Date <= day3.Date select sd.Expense).Sum();
                    day3.Position = AssignZone(p3, e3);
                    day3.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p3, e3);
                    c.ClientPosition.Add(day3);

                    ClientPositionIndicator day4 = new ClientPositionIndicator();
                    day4.Date = DateTime.Now.Date.AddDays(3);
                    day4.Name = "Day 4";
                    var p4 = (from sd in SummaryChartData where sd.Date.Date <= day4.Date select sd.CashOnHand).Sum();
                    var e4 = (from sd in SummaryChartData where sd.Date.Date <= day4.Date select sd.Expense).Sum();
                    day4.Position = AssignZone(p4, e4);
                    day4.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p4, e4);
                    c.ClientPosition.Add(day4);

                    ClientPositionIndicator day5 = new ClientPositionIndicator();
                    day5.Date = DateTime.Now.Date.AddDays(4);
                    day5.Name = "Day 5";
                    var p5 = (from sd in SummaryChartData where sd.Date.Date <= day5.Date select sd.CashOnHand).Sum();
                    var e5 = (from sd in SummaryChartData where sd.Date.Date <= day5.Date select sd.Expense).Sum();
                    day5.Position = AssignZone(p5, e5);
                    day5.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p5, e5);
                    c.ClientPosition.Add(day5);

                    ClientPositionIndicator day6 = new ClientPositionIndicator();
                    day6.Date = DateTime.Now.Date.AddDays(5);
                    day6.Name = "Day 6";
                    var p6 = (from sd in SummaryChartData where sd.Date.Date <= day6.Date select sd.CashOnHand).Sum();
                    var e6 = (from sd in SummaryChartData where sd.Date.Date <= day6.Date select sd.Expense).Sum();
                    day6.Position = AssignZone(p6, e6);
                    day6.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p6, e6);
                    c.ClientPosition.Add(day6);

                    ClientPositionIndicator day7 = new ClientPositionIndicator();
                    day7.Date = DateTime.Now.Date.AddDays(6);
                    day7.Name = "Day 7";
                    var p7 = (from sd in SummaryChartData where sd.Date.Date <= day7.Date select sd.CashOnHand).Sum();
                    var e7 = (from sd in SummaryChartData where sd.Date.Date <= day7.Date select sd.Expense).Sum();
                    day7.Position = AssignZone(p7, e7);
                    day7.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p7, e7);
                    c.ClientPosition.Add(day7);

                    // Forcasting in a Fortnight
                    ClientPositionIndicator fortnight = new ClientPositionIndicator();
                    fortnight.Date = DateTime.Now.Date.AddDays(14);
                    fortnight.Name = "Fortnight";
                    var p9 = (from sd in SummaryChartData where sd.Date.Date <= day7.Date select sd.CashOnHand).Sum();
                    var e9 = (from sd in SummaryChartData where sd.Date.Date <= day7.Date select sd.Expense).Sum();
                    fortnight.Position = AssignZone(p9, e9);
                    fortnight.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p9, e9);
                    c.ClientPosition.Add(fortnight);

                    // Forcasting in a Month
                    ClientPositionIndicator oneMonth = new ClientPositionIndicator();
                    oneMonth.Date = DateTime.Now.Date.AddMonths(1);
                    oneMonth.Name = "Month One";
                    var p10 =
                        (from sd in SummaryChartData where sd.Date.Date <= oneMonth.Date select sd.CashOnHand).Sum();
                    var e10 = (from sd in SummaryChartData where sd.Date.Date <= oneMonth.Date select sd.Expense).Sum();
                    oneMonth.Position = AssignZone(p10, e10);
                    oneMonth.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p10, e10);
                    c.ClientPosition.Add(oneMonth);

                    // Forcasting in two Months
                    ClientPositionIndicator twoMonths = new ClientPositionIndicator();
                    twoMonths.Date = DateTime.Now.Date.AddMonths(2);
                    twoMonths.Name = "Month Two";
                    var p11 =
                        (from sd in SummaryChartData where sd.Date.Date <= twoMonths.Date select sd.CashOnHand).Sum();
                    var e11 = (from sd in SummaryChartData where sd.Date.Date <= twoMonths.Date select sd.Expense).Sum();
                    twoMonths.Position = AssignZone(p11, e11);
                    twoMonths.ExpenseToCashOnHandPercentage = ExpenseToCashOnHandPercentage(p11, e11);
                    c.ClientPosition.Add(twoMonths);
                }
            }

        }

        public Enumerations.Zone AssignZone(decimal cashOnHand, decimal Expense)
        {
            decimal percentage = 0;
            if (cashOnHand > 0)
            {
                percentage = (Expense / cashOnHand) * 100;


                if (percentage <= 25)
                    return Enumerations.Zone.Zone3;

                if (percentage > 25 && percentage <= 50)
                    return Enumerations.Zone.Zone2;

                if (percentage > 50 && percentage <= 75)
                    return Enumerations.Zone.Zone1;

                if (percentage > 75 && percentage <= 100)
                    return Enumerations.Zone.Zone0;
            }

            return Enumerations.Zone.Zone0;
        }

        private decimal ExpenseToCashOnHandPercentage(decimal cashOnHand, decimal Expense)
        {
            decimal percentage = 0;

            if (cashOnHand > 0)
            {
                percentage = (Expense/cashOnHand)*100;

                if (percentage < 0)
                {
                    percentage = 100;
                }
            }
            else
            {
                if (Expense > 0)
                    percentage = 100;
            }

            return Math.Round(percentage, 2);


            return percentage;
        }

        public UserProfile GetUser(string userName)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            UserProfile user;
            user = service.GetUser(userName);
            return user;
        }

    }

}