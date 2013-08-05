using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web;
using CashFlowManager.Common.Attributes;
using CashFlowManager.Common.Enumerations;
using CashFlowManager.Contract;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Service;

namespace CashFlowManager.Models
{
    public class TransactionModel
    {

        public TransactionModel()
        {
            Transactions = new List<TransactionInfo>();
        }

        public Guid ClientId { get; set; }

        public Guid PracticeId { get; set; }

        public List<TransactionInfo> Transactions { get; set; }

        public List<ScheduleInfo> Schedules { get; set; }

        public List<TransactionTypeInfo> TransactionTypes { get; set; }

        public TransactionInfo SelectedTransaction { get; set; }

        public Guid SelectedSchedule { get; set; }

        public Guid SelectedTransactionType { get; set; }


        public List<TransactionInfo> IncomeTransactions { get; private set; }
        public List<TransactionInfo> ExpenseTransactions { get; private set; }

        public void LoadData(Guid clientId)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            SelectedTransaction = new TransactionInfo(){StartDate=DateTime.Now};
            Transactions = service.GetTransactions(clientId);
            IncomeTransactions = new List<TransactionInfo>();
            ExpenseTransactions = new List<TransactionInfo>();
            IncomeTransactions.AddRange((from t in Transactions where t.TransactionTypeId == new Guid(StringEnum.GetStringValue(Enumerations.TransactionType.Income)) select t).ToList());
            ExpenseTransactions.AddRange((from t in Transactions where t.TransactionTypeId == new Guid(StringEnum.GetStringValue(Enumerations.TransactionType.Expense)) select t).ToList());
            Schedules = service.GetScheduleTypes();
            TransactionTypes = service.GetTransactionTypes();
        }

        public void LoadData(Guid clientId , Guid transactionId)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            var trans = service.GetTransactions(clientId,transactionId);
            Schedules = service.GetScheduleTypes();
            TransactionTypes = service.GetTransactionTypes();
            
            if (trans != null && trans.Count > 0)
            {
                SelectedTransaction = trans.First();
                SelectedSchedule = SelectedTransaction.ScheduleId;
                SelectedTransactionType = SelectedTransaction.TransactionTypeId;
            }

        }

    }
} 