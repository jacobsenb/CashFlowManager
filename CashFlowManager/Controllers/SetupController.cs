using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlowManager.Models;

namespace CashFlowManager.Controllers
{
    public class SetupController : Controller
    {
        //
        // GET: /Setup/
        [Authorize]
        public ActionResult Index()
        {
            SetupModel model = new SetupModel();
            model.LoadData();
            return View(model);
        }

    }
}
