using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlowManager.Contract;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Models;
using CashFlowManager.Service;

namespace CashFlowManager.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Practice/

        public ActionResult Index(string practiceId)
        {
            ClientModel model = new ClientModel();
            model.LoadData(practiceId);
            return View(model);
        }

        public ActionResult Create(string practiceId)
        {
            ClientModel model = new ClientModel();
            model.LoadData(practiceId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ClientModel model)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            model.SelectedClient.PracticeId = model.PracticeId;
            service.AddClient(new List<ClientInfo>(){model.SelectedClient});
            return RedirectToAction("Index", "Client",new {practiceId= model.PracticeId.ToString()});
        }

        public ActionResult Edit(string practiceId, string clientId)
        {
            ClientModel model = new ClientModel();
            model.LoadData(practiceId);
            model.SelectedClient = (from c in model.Clients where c.Id == new Guid(clientId) select c).SingleOrDefault();
            return View(model);
        }

        public ActionResult GetData(string practiceId)
        {
            ClientModel model = new ClientModel();
            model.LoadData(practiceId);
            var clientsPostion = (from c in model.Clients select c.ClientPosition).SingleOrDefault();
            return Json(clientsPostion, JsonRequestBehavior.AllowGet);
        }

    }
}
