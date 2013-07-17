using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowManager.Contract;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Service;

namespace CashFlowManager.Models
{
    public class BankAccountModel
    {

        public Guid PracticeId { get; set; }
        public Guid ClientId { get; set; }
        public BankAccountInfo SelectedBankAccount { get; set; }
        public List<BankAccountInfo> BankAccounts { get; set; }


        public void Load(Guid clientId, Guid practiceId)
        {
            PracticeId = practiceId;
            ClientId = clientId;
            ICashFlowManagerService service = new CashFlowManagerService();
            BankAccounts = service.GetBankAccounts(clientId);
        }

        public void Load(Guid clientId, Guid practiceId, Guid bankAccountId)
        {
            PracticeId = practiceId;
            ClientId = clientId;
            ICashFlowManagerService service = new CashFlowManagerService();
            SelectedBankAccount  = service.GetBankAccount(bankAccountId);
        }

    }
}