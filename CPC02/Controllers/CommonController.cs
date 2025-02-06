using CPC02.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPC02.Controllers
{
    public class CommonController : Controller
    {
        CPC2Context _erpcpc = new CPC2Context();
        //TWNCPCContext _erp = new TWNCPCContext();

        #region 個人工作備忘錄
        [HttpGet]
        public ActionResult WorkLogList()
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }

            var mid = Session["Mid"]?.ToString();
            var model = _erpcpc.WLOGA.Where(x => x.Mid == mid).OrderByDescending(x=>x.LOG001).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult WorkLogEdit(WLOGA model)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("LogLogin", "Member");
            }

            ViewBag.IsUpdate = false;
            model = _erpcpc.WLOGA.Find(model.LOG000);
            if (model != null)
            {
                ViewBag.formattedDate = model.LOG001.ToString("yyyy-MM-dd"); 

                ViewBag.IsUpdate = true;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult WorkLogEdit(WLOGA model, bool IsUpdate = false)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("LogLogin", "Member");
            }

            if (IsUpdate)
            {
                var existingDevice = _erpcpc.WLOGA.Find(model.LOG000); 
                if (existingDevice != null)
                {
                    _erpcpc.Entry(existingDevice).CurrentValues.SetValues(model);

                    try
                    {
                        _erpcpc.SaveChanges();
                        TempData["SuccessMessage"] = "更新成功！"; 
                    }
                    catch
                    {
                        TempData["ErrorMessage"] = "更新失敗！";
                    }
                }
            }
            else
            {
                model.LOG000 =  Guid.NewGuid();
                model.CreaTime = DateTime.Now;
                var mid = Session["Mid"]?.ToString();
                var Department = Session["Department"]?.ToString();
                var Permission = Session["Permission"];
                model.LOG009 = Permission != null ? Convert.ToInt32(Permission) : (int?)null;
                model.Mid = mid;
                model.LOG008 = Department;

                model.Status = 0;
                _erpcpc.WLOGA.Add(model);  

                try
                {
                    _erpcpc.SaveChanges();
                    TempData["SuccessMessage"] = "新增成功！"; 
                }
                catch
                {
                    TempData["ErrorMessage"] = "新增失敗！";
                }
            }

            return RedirectToAction("WorkLogList");
        }


        #endregion
    }
}