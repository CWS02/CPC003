﻿using CPC02.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPC02.Controllers
{
    public class MemberController : Controller
    {
        CPCContext _db=new CPCContext();
        TWNCPCContext _erp=new TWNCPCContext();
        // GET: Member
        #region 會員登入
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Members =_db.Member.Where(x => x.Status == 0).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Member model)
        {
            model = _db.Member.FirstOrDefault(m => m.Mem001==model.Mem001&&m.Mem002 == model.Mem002 && m.Mem003 == model.Mem003 && m.Status==0);
            if (model != null)
            {
                Session.Timeout = 60;
                Session["Mid"] = model.Mem000;
                Session["MName"] = model.Mem001;

                //權限
                Session["Perm_C"] = model.Perm_C;
                Session["Perm_U"] = model.Perm_U;
                Session["Perm_D"] = model.Perm_D;
                Session["Perm_Flag"] = model.Permission;

                //更新LastLoginTime
                model.LastLoginTime = DateTime.Now;
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction(model.Action, model.Controller);
            }
            else
            {
                TempData["Message"] = "帳號或密碼錯誤";
                return RedirectToAction("Login", "Member");
            }
        }
        #endregion

        #region 工作備忘錄登入
        public ActionResult Loglogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogLogin(Member model)
        {
            var data = _erp.CMSMV.FirstOrDefault(m => m.MV001 == model.Mem002&& m.MV009.Substring(m.MV009.Length - 5) == model.Mem003&& string.IsNullOrEmpty(m.MV022));
            if (data != null)
            {
                Session.Timeout = 60;
                Session["Mid"] = data.MV001;
                Session["MName"] = data.MV002;
                Session["Department"] =data.MV219;  //部門權限
                Session["Permission"] =data.MV220;  //權限
                Session["DepartmentP"] = data.MV004; //所屬部門
                if (data.MV220 > 98)
                {
                    return RedirectToAction("ManageLogList", "Common");
                }
                return RedirectToAction("WorkLogList","Common"); 
            }
            else
            {
                TempData["Message"] = "帳號或密碼錯誤";
                return RedirectToAction("Loglogin", "Member");
            }
        }
        #endregion

        #region 登出
        public ActionResult LoginOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        public ActionResult LogloginOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Loglogin");
        }
        #endregion
    }
}