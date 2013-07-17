using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlowManager.Models;

namespace CashFlowManager.Controllers
{
    public class ClientChartController : Controller
    {
        //
        // GET: /ClientChart/

        public ActionResult Index(string clientId,string practiceId, int numberMonths)
      {
          ClientChartModel model = new ClientChartModel();
          model.Load(clientId,practiceId, numberMonths);
          return View(model);
      }

        public ActionResult GetData(string clientId, string practiceId, int numberMonths)
         {
             ClientChartModel model = new ClientChartModel();
             model.Load(clientId, practiceId, numberMonths);
             return Json(model.SummaryChartData, JsonRequestBehavior.AllowGet);
         }


 



    }


}
