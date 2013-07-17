using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlowManager.Contract;
using CashFlowManager.Models;
using CashFlowManager.Service;

namespace CashFlowManager.Controllers
{
    public class BankAccountController : Controller
    {
        //
        // GET: /BankAccount/

        public ActionResult Index(string clientId, string practiceId)
        {
            BankAccountModel model = new BankAccountModel();
            model.Load(new Guid(clientId),new Guid(practiceId));

            return View(model);
        }

        public ActionResult Create(string clientId, string practiceId)
        {
            BankAccountModel model = new BankAccountModel();
            model.Load(new Guid(clientId), new Guid(practiceId));
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(BankAccountModel model)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            model.SelectedBankAccount.ClientId = model.ClientId;
            service.AddBankAccount(model.SelectedBankAccount);
            return RedirectToAction("Index", "BankAccount", new { clientId = model.ClientId.ToString(), practiceId = model.PracticeId.ToString() });
        }

        public ActionResult Edit(string clientId, string practiceId, string bankAccountId)
        {
            BankAccountModel model = new BankAccountModel();
            model.Load(new Guid(clientId), new Guid(practiceId), new Guid(bankAccountId));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BankAccountModel model)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            model.SelectedBankAccount.ClientId = model.ClientId;
            service.UpdateBankAccount(model.SelectedBankAccount);
            return RedirectToAction("Index", "BankAccount", new { clientId = model.ClientId.ToString(), practiceId = model.PracticeId.ToString() });
        }

        public ActionResult Delete(string clientId, string practiceId, string bankAccountId)
        {
            BankAccountModel model = new BankAccountModel();
            model.Load(new Guid(clientId), new Guid(practiceId),new Guid(bankAccountId));
            return View(model);
        }

                [HttpPost]
        public ActionResult Delete(BankAccountModel model)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            model.SelectedBankAccount.ClientId = model.ClientId;
            service.DeleteBankAccount(model.SelectedBankAccount);
            return RedirectToAction("Index", "BankAccount", new { clientId = model.ClientId.ToString(), practiceId = model.PracticeId.ToString() });
        }

    }
}
