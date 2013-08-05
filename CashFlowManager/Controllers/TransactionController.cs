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
    public class TransactionController : Controller
    {

        public ActionResult Index(string clientId, string practiceId)
        {
            TransactionModel model = new TransactionModel();
            model.ClientId = new Guid(clientId);
            model.PracticeId = new Guid(practiceId);
            model.LoadData(new Guid(clientId));
            return View(model);
        }

        public ActionResult GetExpenseTransactions(string clientId, string practiceId)
        {
            TransactionModel model = new TransactionModel();
            model.ClientId = new Guid(clientId);
            model.PracticeId = new Guid(practiceId);
            model.LoadData(new Guid(clientId));
            return Json(model.ExpenseTransactions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIncomeTransactions(string clientId, string practiceId)
        {
            TransactionModel model = new TransactionModel();
            model.ClientId = new Guid(clientId);
            model.PracticeId = new Guid(practiceId);
            model.LoadData(new Guid(clientId));
            return Json(model.IncomeTransactions, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Transaction/
        public ActionResult Create(string clientId, string practiceId)
        {
            TransactionModel model = new TransactionModel();
            model.LoadData(new Guid(clientId));
            model.ClientId = new Guid(clientId);
            model.PracticeId = new Guid(practiceId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TransactionModel model)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            model.SelectedTransaction.ScheduleId = model.SelectedSchedule;
            model.Transactions.Add(model.SelectedTransaction);
            model.SelectedTransaction.ClientId = model.ClientId;
            model.SelectedTransaction.TransactionTypeId = model.SelectedTransactionType;
            service.AddTransactions(model.Transactions);
            return RedirectToAction("Index", "Transaction", new { clientId = model.ClientId.ToString(), practiceId = model.PracticeId.ToString() });
        }

        public ActionResult Delete(String transactionId, string clientId, string practiceId)
        {
            TransactionModel model = new TransactionModel();
            if (clientId != null && transactionId != null)
            {
                model.ClientId = new Guid(clientId);
                model.PracticeId = new Guid(practiceId);
                model.LoadData(new Guid(clientId), new Guid(transactionId));
                return View(model);
            }

            return RedirectToAction("Index", "Transaction", new { clientId = model.ClientId.ToString(), practiceId = model.PracticeId.ToString() });
        }

        [HttpPost]
        public ActionResult Delete(TransactionModel model, String transactionId, string clientId, string practiceId)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            service.DeleteTransaction(new Guid(transactionId));
            return RedirectToAction("Index", "Transaction", new { clientId = clientId, practiceId = practiceId });
        }


        public ActionResult Edit(String transactionId, string clientId,string practiceId)
        {
            TransactionModel model = new TransactionModel();
            model.ClientId = new Guid(clientId);
            model.PracticeId = new Guid(practiceId);
            model.LoadData(new Guid(clientId), new Guid(transactionId));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TransactionModel model)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            model.SelectedTransaction.ScheduleId = model.SelectedSchedule;
            model.SelectedTransaction.ClientId = model.ClientId;
            model.SelectedTransaction.TransactionTypeId = model.SelectedTransactionType;
            service.UpdateTransaction(model.SelectedTransaction);
            return RedirectToAction("Index", "Transaction", new { clientId = model.ClientId.ToString(),practiceId= model.PracticeId.ToString() });
        }

    }
}
