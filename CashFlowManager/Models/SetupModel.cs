using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowManager.Contract;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Service;

namespace CashFlowManager.Models
{
    public class SetupModel
    {
        public List<PracticeInfo> Practices { get; set; }


        public void LoadData()
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            Practices = service.GetPractices();
        }
    }
}