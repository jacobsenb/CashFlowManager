using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CashFlowManager.Contract;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Service;

namespace CashFlowManager.Models
{
    public class ImportTransactions
    {
        public List<string> Headers { get; set; }
        public List<Row> Rows { get; set; }
        public Guid PracticeId { get; set; }
        public Guid ClientId { get; set; }
        public List<ScheduleInfo> Schedules { get; set; }
        public List<TransactionTypeInfo> TransactionTypes { get; set; }
        public List<TransactionInfo> TransactionsToAdd { get; set; }
        [DisplayName("Display Repeating Transactions:")]
        public bool DisplayReoccurringTransaction { get; set; }

        public void Load()
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            Schedules = service.GetScheduleTypes();
            TransactionTypes = service.GetTransactionTypes();
            TransactionsToAdd = new List<TransactionInfo>();
            Headers = new List<string>();
            Rows = new List<Row>();
        }

        public List<ScheduleInfo> GetSchedules()
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            return service.GetScheduleTypes();
        }

        public List<TransactionTypeInfo> GetTransactionTypes()
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            return service.GetTransactionTypes();
        }

        public void SaveTransactions(List<TransactionInfo> trans)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            service.AddTransactions(trans);
        }

    
    }

    public class Column
    {
        public string Header { get; set;}
        public string Value { get; set;}
    }

    public class Row
    {
        public List<string> Columns { get; set; }
    }
}