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





    }

 
}