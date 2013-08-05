using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Models;
using Microsoft.VisualBasic.FileIO;

namespace CashFlowManager.Controllers
{
    public class ImportTransactionsController : Controller
    {
        //
        // GET: /ImportTransactions/

        public ActionResult Index(string practiceId, string clientId)
        {
            ImportTransactions model = new ImportTransactions();
            model.PracticeId = new Guid(practiceId);
            model.ClientId = new Guid(clientId);
            model.Load();
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult ImportRecords(ImportTransactions model, string practiceId, string clientId)
        {
            ImportTransactions trans = new ImportTransactions();
            trans.Load();

            trans.PracticeId = new Guid(practiceId);
            trans.ClientId = new Guid(clientId);
            trans.DisplayReoccurringTransaction = model.DisplayReoccurringTransaction;

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file];
                var fp = Path.Combine(HttpContext.Server.MapPath("~/ImportUploads"), Path.GetFileName(hpf.FileName));
                try
                {
                    hpf.SaveAs(fp);
                }
                catch (Exception)
                {
                    
                    //TODO: have to handle this.
                }
                Session.Add("UploadFile", fp);
            }

            return View(trans);
        }

        public ActionResult SaveTransactions(List<TransactionInfo> transactions, string practiceId, string clientId)
        {
            ImportTransactions model = new ImportTransactions();
            transactions.ForEach(t=> t.ClientId = new Guid(clientId));
            model.SaveTransactions(transactions);
            return Json("Finished");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult GetUploadedTransactions(bool displayRepeating)
        {
            ImportTransactions trans = new ImportTransactions();
            trans.Load();

            try
            {
                var file = Session["UploadFile"];
                var fileIn = new FileInfo(file.ToString());
                var reader = fileIn.OpenText();
                var tfp = new TextFieldParser(reader) { TextFieldType = FieldType.Delimited, Delimiters = new[] { "," } };
                while (!tfp.EndOfData)
                {
                    //Parse records into domain object and save to database
                    var currentRow = tfp.ReadFields();
                    List<Column> columns = new List<Column>();

                    if (currentRow.Length == 1)
                    {
                        if (trans.Headers == null)
                            trans.Headers = new List<string>();
                        trans.Headers.Add(currentRow[0]);
                    }
                    else
                    {
                        Row r = new Row();
                        r.Columns = new List<string>();
                        foreach (string currentField in currentRow)
                        {
                            if (trans.Rows == null)
                                trans.Rows = new List<Row>();

                            string c = currentField;
                            r.Columns.Add(c);
                        }
                        trans.Rows.Add(r);
                    }
                }
            }
            catch (Exception)
            {
                
            }



            if (displayRepeating)
                trans.Rows = ProcessTransactions(trans.Rows);

            return Json(trans.Rows, JsonRequestBehavior.AllowGet);
        }



        public List<Row> ProcessTransactions(List<Row> rows)
        {
            Dictionary<string,Row> rowsToReturn = new Dictionary<string, Row>();


            int amtCol = -1;
            int narrCol = -1;


            // If a narration or amount is repeated more than once then this transaction is added
            foreach (Row r in rows)
            {
                foreach (string c in r.Columns)
                {
                    decimal d;
                    if (decimal.TryParse(c, out d) && d%1 > 0)
                    {
                        var count = (from r1 in rows where r1.Columns.Contains(c) select r1).Count();
                        if (count >= 2)
                        {
                            if (!rowsToReturn.ContainsKey(c) && (narrCol > -1 && !rowsToReturn.ContainsKey(r.Columns[narrCol])))
                                rowsToReturn.Add(c,r);
                        }

                        // Setting which column is the amount column
                        if (amtCol == -1)
                        {
                            amtCol = r.Columns.IndexOf(c);
                        }
                    }

                    if (c.Length > 10)
                    {
                        var count = (from r1 in rows where r1.Columns.Contains(c) select r1).Count();
                        if (count >= 2)
                        {
                            if (!rowsToReturn.ContainsKey(c) && ( amtCol > -1 && !rowsToReturn.ContainsKey(r.Columns[amtCol])))
                                rowsToReturn.Add(c,r);
                        }

                        // Setting which column is the narration column
                        if (narrCol == -1)
                        {
                            narrCol = r.Columns.IndexOf(c);
                        }
                    }
                }
            }



            return rowsToReturn.Values.ToList();
        }


        public ActionResult AddTransaction()
        {
            ImportTransactions model = new ImportTransactions();
            TransactionInfo t = new TransactionInfo();
            t.TransactionTypes = model.GetTransactionTypes();
            t.Schedules = model.GetSchedules();
            return View("ImportTransRecord",t);
        }
    }
}
