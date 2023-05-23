using StocksMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StocksMarket.Controllers
{
    public class HomeController : Controller
    {




        public ActionResult OnDDLChange(string GrpId)
        {
            //
            // Grid View Binding
            TempData["GroupId"] = GrpId;
            if (GrpId != null)
            {
                StockGrpDetails stockmodel = new StockGrpDetails();
                stockmodel.GetStockInGrp(GrpId);
                int count = Convert.ToInt32(stockmodel.lstStockGrpGrid.Count);
                ViewData["count"] = count;
                //EmployeeDir empDir = new EmployeeDir();
                return Json(stockmodel.lstStockGrpGrid, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("", JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult StockGroup()
        {
            StockGrpDetails stockmodel = new StockGrpDetails();
            // ViewData["viewDt"] = stockmodel.GetStockDetails(search);
            return View(stockmodel);
        }

        [HttpPost]
        public ActionResult StockGroup(FormCollection formCollection,StocksMarket.Models.StockGrpDetails stockgrpDetails)
        {

            //string grpname="";
             string[] ids = formCollection["ID"].Split(new char[] { ',' });

            // StockGrpDetails stockmodel = new StockGrpDetails();
            if (TempData["GroupId"] != null)
            {
                //  grpname = TempData["GroupId"].ToString(); 
            }
            else
            {
                TempData["Message"] = "Please select group name";
            }


            //string val = stockmodel.AddGrpStocks(ids, grpname);
            return View(stockgrpDetails);

        }



        public JsonResult DeleteSelected(int[] selectedIDs)
        {
            try
            {
                if (TempData["GroupId"].ToString()!=null)
                {
                    StockGrpDetails stockmodel = new StockGrpDetails();
                string grpname = TempData["GroupId"].ToString();

                string val = stockmodel.DelToBothDB(selectedIDs, grpname);
               // string val2 = stockmodel.DelGrpStocksM(selectedIDs, grpname);
                    TempData["Message"] = "Deleted";
                    return Json("Success", JsonRequestBehavior.AllowGet);
                    // stockmodel.GetStockInGrp(grpname);
                }
                else { return Json("Group Not Selected Please select Group", JsonRequestBehavior.AllowGet); }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddSelected(int[] selectedIDs)
        {
            try
            {
                if (selectedIDs != null && TempData["GroupId"].ToString() != null)
                {
                    int count = selectedIDs.Length;
                    StockGrpDetails stockmodel = new StockGrpDetails();
                    string grpname = TempData["GroupId"].ToString();
                    string val = stockmodel.AddToBothDB(selectedIDs, grpname);
                   // string main = stockmodel.AddGrpStocksM(selectedIDs, grpname);
                    if(val == "true")
                    {
                        TempData["Message"] = "Added";
                        return Json("Success -" + count + " Stock Added To " + grpname + " Group", JsonRequestBehavior.AllowGet);
                    }
                    else { return Json("Group Not Selected --or-- Stock already Added", JsonRequestBehavior.AllowGet); }

                }
                else { return Json("Group Not Selected --or-- Stock already Added", JsonRequestBehavior.AllowGet); }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }





        [HttpGet]
        public ActionResult StockPage()
        {
            StockDetails stockmodel = new StockDetails();
            // ViewData["viewDt"] = stockmodel.GetStockDetails(search);
            return View(stockmodel);
        }

        [HttpPost]
        public ActionResult StockPage(string command, StocksMarket.Models.StockDetails stockDetails)
        {

            // ViewData["viewDt"] = stockmodel.GetStockDetails(search);

            //return View(stockmodel);

            if (command == "Save")
            {
                // string username = Session["LoggedInUser"].ToString();
                StockDetails stockmodel = new StockDetails();
                string val = stockmodel.InsertToBothDB(stockDetails);
               // string val2 = stockmodel.AddToGroup(stockDetails);
                if (val == "true")
                {
                    TempData["Message"] = "Success";
                    ResetStData(stockmodel);
                    return RedirectToAction("StockPage", "Home");
                    //return View(conDet);
                }
                else
                {
                    TempData["Message"] = "Failure";
                    return View(stockmodel);
                }
            }
            else if (command == "Update")
            {
                TempData["message"] = "update";
                StockDetails stockmodel = new StockDetails();
                string val = stockmodel.UpdateStock(stockDetails);
                ResetStData(stockmodel);
                TempData["Message"] = "Update";
                return RedirectToAction("StockPage", "Home");
              

            }
            else if (command == "reset")
            {
                TempData["message"] = "reset";
                ResetStData(stockDetails);
                return View(stockDetails);
            }
            else
                return View(stockDetails);
        }

        [HttpPost]
        public JsonResult EditStocks(string Stockid)
        {
            TempData["Empid"] = Stockid;
            if (Stockid != null)
            {
                StockDetails stockmodel = new StockDetails();
                stockmodel.getStockList(Stockid);
                //EmployeeDir empDir = new EmployeeDir();
                return Json(stockmodel.lstStockGrid, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult HolidayList()
        {
            HolidayList holidayList = new HolidayList();

            return View(holidayList);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void ResetStData(StockDetails stockDetails)
        {
            stockDetails.StockName = string.Empty;
            stockDetails.Stack_Name = string.Empty;
            stockDetails.CodeN = string.Empty;
            stockDetails.CodeR = string.Empty;
            stockDetails.Graphic_Name = string.Empty;
            stockDetails.Ticker_Name = string.Empty;

        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            //FormsAuthentication.SignOut();
            return RedirectToAction("LoginAction", "Login");
        }

    }
}