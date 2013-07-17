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
    public class PracticeController : Controller
    {
        //
        // GET: /Practice/
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {

            return View();
        }
        [Authorize]
        public ActionResult Create()
        {
            PracticeModel model = new PracticeModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(PracticeModel model)
        {
            ICashFlowManagerService service = new CashFlowManagerService();
            service.AddPractice(model.SelectedPractice);
            return RedirectToAction("Index","Setup");
        }

    }
}
