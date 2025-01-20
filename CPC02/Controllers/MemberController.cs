using CPC02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPC02.Controllers
{
    public class MemberController : Controller
    {
        CPCContext _db=new CPCContext();
        // GET: Member
        #region 會員登入
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Member model)
        {
            model = _db.Member.FirstOrDefault(m => m.Mem002 == model.Mem002 && m.Mem003 == model.Mem003);
            if (model != null)
            {
                Session.Timeout = 60;
                Session["Mid"] = model.Mem000;
                Session["MName"] = model.Mem001;

                return RedirectToAction(model.Action,model.Controller);
            }
            else
            {
                TempData["Message"] = "帳號或密碼錯誤";
                return RedirectToAction("Login","Member");
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
        #endregion
    }
}