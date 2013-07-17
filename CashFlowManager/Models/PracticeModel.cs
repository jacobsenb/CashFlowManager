using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowManager.Contract;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Service;

namespace CashFlowManager.Models
{
    public class PracticeModel
    {
        public List<PracticeInfo> Practice { get; set; }

        public PracticeInfo SelectedPractice { get; set; }

        public void LoadData()
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            Practice = service.GetPractices();
        }
    }
}