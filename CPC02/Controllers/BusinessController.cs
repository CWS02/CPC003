using CPC02.Function;
using CPC02.Models;
using CPC02.Parameters;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Ajax.Utilities;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using NPOI.HSSF.Record.Chart;
using NPOI.OpenXml4Net.OPC.Internal;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace CPC02.Controllers
{
    public class BusinessController : Controller
    {
        // GET: Business
        CPCContext _db = new CPCContext();
        MemberController _member = new MemberController();

        #region 客戶訪談記錄
        [HttpGet]
        public ActionResult InterviewRecordList(bool? Per,int? Mid2, string INT001,string INT006)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }

            var modelQuery = _db.INTRA.AsQueryable();
            
            if (Mid2.HasValue)
            {
                Session["Mid2"] = Mid2;
                modelQuery = modelQuery.Where(x => x.Mid == Mid2);
            }
            else
            {
                Session["Mid2"] = null;
            }

            if (!string.IsNullOrEmpty(INT001))
            {
                Session["INT001"] = INT001;
                modelQuery = modelQuery.Where(x => x.INT001.Contains(INT001));
            }
            else
            {
                Session["INT001"] = null;
            }
            if (!string.IsNullOrEmpty(INT006))
            {
                Session["INT006"] = INT006;
                modelQuery = modelQuery.Where(x => x.INT006.Contains(INT006));
            }
            else
            {
                Session["INT006"] = null;
            }

            #region 登入權限
            int mid = Convert.ToInt32(Session["Mid"]);
            var member = _db.Member.FirstOrDefault(x => x.Mem000 == mid);
            if (member.IsCrossMember == "N" && string.IsNullOrEmpty(member.AllowedMem000)) // 只能看自己
            {
                modelQuery = modelQuery.Where(x => x.Mid == mid);
            }
            else if (member.IsCrossMember == "N" && !string.IsNullOrEmpty(member.AllowedMem000)) // 可看部分人
            {
                var allowedIds = member.AllowedMem000
                       .Split(',')
                       .Select(id => id.Trim())
                       .Where(id => int.TryParse(id, out _))
                       .Select(int.Parse)
                       .Cast<int?>() 
                       .ToList();

                modelQuery = modelQuery.Where(x => allowedIds.Contains(x.Mid));
            }
            #endregion

            var model = modelQuery.ToList();


            foreach (var intra in model)
            {
                var latestIntrb = _db.INTRB
                    .Where(b => b.INT999 == intra.INT000)
                    .OrderByDescending(b => b.INT005)
                    .FirstOrDefault();

                if (latestIntrb != null)
                {
                    DateTime latestDate;
                    if (DateTime.TryParse(latestIntrb.INT005, out latestDate))
                    {
                        intra.LastDate = latestDate;
                    }
                }
            }

            foreach (var intrc in model)
            {
                var latestIntrb = _db.INTRC
                    .Where(b => b.INT999 == intrc.INT000)
                    .OrderByDescending(b => b.INT001)
                    .FirstOrDefault();

                if (latestIntrb != null)
                {
                    DateTime latestDate;
                    if (DateTime.TryParse(latestIntrb.INT001, out latestDate))
                    {
                        intrc.QuoteLastDate = latestDate;
                    }
                }
            }

            if (Per == true)
            {
                Session["Per"] = true;
            }
            
            model = model.OrderByDescending(intra => intra.LastDate).ToList();

            
            var members = _db.Member.Where(x=>x.IsBusiness=="Y").ToList();
            ViewBag.Members = members;

            ViewBag.INT006List = _db.INTRA
                .GroupBy(x => x.INT006)
                .Select(x => x.Key) 
                .ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult InterviewRecordEdit(INTRA model)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }

            ViewBag.IsUpdate = false;
            if (model.INT000 != null)
            {
                model = _db.INTRA.Find(model.INT000);
                ViewBag.IsUpdate = true;
            }

            var members = _db.Member
             .Where(m => m.IsBusiness == "Y")
            .Select(m => new
            {
                Mem000 = m.Mem000,
                Mem001 = m.Mem001
            })
            .ToList();

            ViewBag.MemberList = JsonConvert.SerializeObject(members);
            var INTRD = _db.INTRD.Where(m => m.status == 0).OrderBy(m=>m.Sort).ThenByDescending(m=>m.CreateTime).ToList();
            ViewBag.INTRDs= JsonConvert.SerializeObject(INTRD);
            return View(model);
        }

        [HttpPost]
        public ActionResult InterviewRecordEdit(HttpPostedFileBase UploadedFile, INTRA model, bool IsUpdate = false)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }

            string currentLang = (string)Session["Culture"] ?? "zh-TW";
            string actionKey = string.Empty;
            if (currentLang == "zh-TW")
            {
                actionKey = IsUpdate ? "更新" : "新增";
            }
            else
            {
                actionKey = IsUpdate ? "Update" : "Add";
            }


            if (IsUpdate)
            {
                var existingDevice = _db.INTRA.Find(model.INT000);
                if (existingDevice != null)
                {
                    existingDevice.INT001 = model.INT001;
                    existingDevice.INT002 = model.INT002;
                    existingDevice.INT003 = model.INT003;
                    existingDevice.INT004 = model.INT004;
                    existingDevice.INT005 = model.INT005;
                    existingDevice.INT006 = model.INT006;
                    existingDevice.INT007 = model.INT007;
                    existingDevice.INT008 = model.INT008;
                    existingDevice.INT009 = model.INT009;
                    existingDevice.INT010 = model.INT010;
                    existingDevice.INT011 = model.INT011;
                    existingDevice.INT012 = model.INT012;
                    existingDevice.INT013 = model.INT013;
                    existingDevice.INT014 = model.INT014;
                    existingDevice.INT015 = model.INT015;
                    existingDevice.INT016 = model.INT016;
                    existingDevice.INT017 = model.INT017;
                    existingDevice.INT018 = model.INT018;
                    existingDevice.INT019 = model.INT019;
                    existingDevice.INT020 = model.INT020;
                    existingDevice.INT021 = model.INT021;
                    existingDevice.INT022 = model.INT022;
                    existingDevice.INT023 = model.INT023;
                    existingDevice.INT024 = model.INT024;
                    existingDevice.INT025 = model.INT025;
                    existingDevice.INT026 = model.INT026;
                    existingDevice.INT027 = model.INT027;
                    existingDevice.INT028 = model.INT028;
                    existingDevice.INT029 = model.INT029;
                    existingDevice.INT031 = model.INT031;

                    existingDevice.INTRD = model.INTRD;


                    existingDevice.Mid = model.Mid;
                    existingDevice.IP = Request.UserHostAddress;

                    if (UploadedFile != null && UploadedFile.ContentLength > 0)
                    {
                        var file = Request.Files[0];
                        var newFilePath = SaveUploadedFile(file, "/image/Business/UploadCard");
                        existingDevice.INT030 = newFilePath;
                    }
                    try
                    {
                        _db.SaveChanges();
                        TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang); ;

                    }
                    catch
                    {
                        TempData["SuccessMessage"] = GetFailureMessage(actionKey, currentLang);
                    }
                }
            }
            else
            {
                model.INT000 = Guid.NewGuid().ToString();
                model.IP = Request.UserHostAddress;
                model.Status = 0;
                if (UploadedFile != null && UploadedFile.ContentLength > 0)
                {
                    var file = Request.Files[0];
                    var newFilePath = SaveUploadedFile(file, "/image/Business/UploadCard");
                    model.INT030 = newFilePath;
                }
                _db.INTRA.Add(model);

                try
                {
                    _db.SaveChanges();
                    TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang);
                }
                catch
                {
                    TempData["SuccessMessage"] = GetFailureMessage(actionKey, currentLang);
                }
            }

            return RedirectToAction("InterviewRecordList");
        }
        #endregion

        #region 訪談記錄
        [HttpGet]
        public ActionResult RecordList(INTRA model)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }
            var data = _db.INTRB.Where(x => x.INT999 == model.INT000).OrderBy(x => x.Status).ThenBy(x => x.Level).ToList();
            ViewBag.INT000 = model.INT000;

            var members = _db.Member.ToList();
            ViewBag.Members = members;

            model = _db.INTRA.FirstOrDefault(x => x.INT000 == model.INT000);
            ViewBag.INTRAModel = model;
            return View(data);
        }

        [HttpGet]
        public ActionResult RecordEdit(INTRB model)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }
            ViewBag.IsUpdate = false;
            if (model.INT000 != null)
            {
                model = _db.INTRB.Find(model.INT000);
                ViewBag.IsUpdate = true;
            }
            ViewBag.INT000 = model.INT999;
            ViewBag.INTRAModel = _db.INTRA.FirstOrDefault(x => x.INT000 == model.INT999)?.INT001;

            return View(model);
        }

        [HttpPost]
        public ActionResult RecordEdit(INTRB model, bool IsUpdate = false)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }
            string currentLang = (string)Session["Culture"] ?? "zh-TW";
            string actionKey = string.Empty;
            if (currentLang == "zh-TW")
            {
                actionKey = IsUpdate ? "更新" : "新增";
            }
            else
            {
                actionKey = IsUpdate ? "Update" : "Add";
            }


            if (IsUpdate)
            {
                var existingDevice = _db.INTRB.Find(model.INT000);
                if (existingDevice != null)
                {
                    existingDevice.INT001 = model.INT001;
                    existingDevice.INT002 = model.INT002;
                    existingDevice.INT004 = model.INT004;
                    existingDevice.INT005 = model.INT005;
                    existingDevice.INT007 = model.INT007;
                    existingDevice.INT008 = model.INT008;
                    existingDevice.INT009 = model.INT009;
                    //existingDevice.INT010 = model.INT010;
                    existingDevice.INT011 = model.INT011;
                    existingDevice.Level = model.Level;


                    existingDevice.IP = Request.UserHostAddress;

                    try
                    {
                        _db.SaveChanges();
                        TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang);
                    }
                    catch
                    {
                        TempData["SuccessMessage"] = GetFailureMessage(actionKey, currentLang);
                    }
                }
            }
            else
            {
                model.INT000 = Guid.NewGuid().ToString();
                model.IP = Request.UserHostAddress;
                model.Status = 0;
                model.Mid = Convert.ToInt32(Session["Mid"]);

                _db.INTRB.Add(model);

                try
                {
                    _db.SaveChanges();
                    TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang);
                }
                catch
                {
                    TempData["SuccessMessage"] = GetFailureMessage(actionKey, currentLang);
                }
            }

            return RedirectToAction("RecordList", new { INT000 = model.INT999 });
        }

        [HttpGet]
        public ActionResult RecordAllList(INTRA model)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }
            var data = _db.INTRB.OrderBy(x => x.Status).ThenBy(x => x.Level).AsQueryable();

            var members = _db.Member.ToList();
            ViewBag.Members = members;

            #region 登入權限
            int mid = Convert.ToInt32(Session["Mid"]);
            var member = _db.Member.FirstOrDefault(x => x.Mem000 == mid);
            var intraModels = _db.INTRA.AsQueryable();

            if (member.IsCrossMember == "N" && string.IsNullOrEmpty(member.AllowedMem000)) // 只能看自己
            {
                data = from item in data
                       join intra in intraModels on item.INT999 equals intra.INT000
                       where intra.Mid == mid
                       select item;
            }
            else if (member.IsCrossMember == "N" && !string.IsNullOrEmpty(member.AllowedMem000)) // 可看部分人
            {
                var allowedIds = member.AllowedMem000
                       .Split(',')
                       .Select(id => id.Trim())
                       .Where(id => int.TryParse(id, out _))
                       .Select(int.Parse)
                       .Cast<int?>()
                       .ToList();

                data = from item in data
                       join intra in intraModels on item.INT999 equals intra.INT000
                       where allowedIds.Contains(intra.Mid)
                       select item;
            }
            #endregion
            return View("RecordList", data.ToList());
        }
        #endregion

        #region 報價紀錄
        [HttpGet]
        public ActionResult QuoteList(INTRA model)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }

            var data = _db.INTRC.Where(x => x.INT999 == model.INT000).OrderByDescending(x => x.INT001).ToList();
            ViewBag.INT000 = model.INT000;

            ViewBag.INTRAModel = _db.INTRA.ToList(); 
            var members = _db.Member.ToList();
            ViewBag.Members = members;
            return View(data);
        }

        [HttpGet]
        public ActionResult QuoteEdit(INTRC model)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }
            ViewBag.IsUpdate = false;
            if (model.INT001 == null)
            {
                model.INT001 = DateTime.Today.ToString("yyyy-MM-dd");
            }
            if (model.INT000 != null)
            {
                model = _db.INTRC.Find(model.INT000);
                ViewBag.IsUpdate = true;
            }
            ViewBag.INT000 = model.INT999;
            ViewBag.member = _db.Member.FirstOrDefault(m => m.Mem000 == model.Mid)?.Mem001;
            ViewBag.INTRAModel = _db.INTRA.FirstOrDefault(x => x.INT000 == model.INT999)?.INT001;

            return View(model);
        }

        [HttpPost]
        public ActionResult QuoteEdit(INTRC model, bool IsUpdate = false)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }
            string currentLang = (string)Session["Culture"] ?? "zh-TW";
            string actionKey = string.Empty;
            if (currentLang == "zh-TW")
            {
                actionKey = IsUpdate ? "更新" : "新增";
            }
            else
            {
                actionKey = IsUpdate ? "Update" : "Add";
            }


            if (IsUpdate)
            {
                var existingDevice = _db.INTRC.Find(model.INT000);
                if (existingDevice != null)
                {
                    existingDevice.INT001 = model.INT001;
                    existingDevice.INT002 = model.INT002;
                    existingDevice.INT003 = model.INT003;
                    existingDevice.INT004 = model.INT004;
                    existingDevice.INT005 = model.INT005;
                    existingDevice.INT007 = model.INT007;
                    existingDevice.INT008 = model.INT008;


                    existingDevice.IP = Request.UserHostAddress;

                    try
                    {
                        _db.SaveChanges();
                        TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang);
                    }
                    catch
                    {
                        TempData["SuccessMessage"] = GetFailureMessage(actionKey, currentLang);
                    }
                }
            }
            else
            {
                model.INT000 = Guid.NewGuid().ToString();
                model.IP = Request.UserHostAddress;
                model.Status = 0;
                model.Mid = Convert.ToInt32(Session["Mid"]);

                _db.INTRC.Add(model);

                try
                {
                    _db.SaveChanges();
                    TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang);
                }
                catch
                {
                    TempData["SuccessMessage"] = GetFailureMessage(actionKey, currentLang);
                }
            }

            return RedirectToAction("QuoteList", new { INT000 = model.INT999 });
        }

        [HttpGet]
        public ActionResult QuoteAllList(INTRA model, QuoteListSearch search)
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }

            var data = _db.INTRC.AsQueryable();
            var intraModels = _db.INTRA.AsQueryable();  

            ViewBag.INTRAModel = intraModels.ToList();  
            var members = _db.Member.Where(x => x.IsBusiness == "Y").ToList();

            if (search.Sales.HasValue)
            {
                data = data.Where(x => x.Mid == search.Sales);
            }

            if (!string.IsNullOrEmpty(search.Name))
            {
                data = from item in data
                       join intra in intraModels on item.INT999 equals intra.INT000
                       where intra.INT001.Contains(search.Name)
                       select item;
            }

            if (search.status.HasValue)
            {
                data = data.Where(x => x.Status == search.status);
            }

            ViewBag.Members = members;

            #region 登入權限
            int mid = Convert.ToInt32(Session["Mid"]);
            var member = _db.Member.FirstOrDefault(x => x.Mem000 == mid);

            if (member.IsCrossMember == "N" && string.IsNullOrEmpty(member.AllowedMem000)) // 只能看自己
            {
                data = from item in data
                       join intra in intraModels on item.INT999 equals intra.INT000
                       where intra.Mid == mid
                       select item;
            }
            else if (member.IsCrossMember == "N" && !string.IsNullOrEmpty(member.AllowedMem000)) // 可看部分人
            {
                var allowedIds = member.AllowedMem000
                       .Split(',')
                       .Select(id => id.Trim())
                       .Where(id => int.TryParse(id, out _))
                       .Select(int.Parse)
                       .Cast<int?>()
                       .ToList();

                data = from item in data
                       join intra in intraModels on item.INT999 equals intra.INT000
                       where allowedIds.Contains(intra.Mid)
                       select item;
            }
            #endregion
            return View("QuoteList", data.ToList());  
        }

        #endregion

        #region 下載報價單
        [HttpGet]
        public ActionResult PrintQuote(INTRC model)
        {
            model = _db.INTRC.Find(model.INT000);
            var INTRAdata = _db.INTRA.FirstOrDefault(x => x.INT000 == model.INT999);
            var member = _db.Member.FirstOrDefault(m => m.Mem000 == model.Mid);

            string currentLang = (string)Session["Culture"] ?? "zh-TW";
            string filePath = Server.MapPath(
                currentLang == "zh-TW"
                    ? ((model.INT005 ?? false)
                        ? "~/file/Business/報價單(稅).docx"
                        : "~/file/Business/報價單.docx")
                    : ((model.INT005 ?? false)
                        ? "~/file/Business/報價單-英(稅).docx"
                        : "~/file/Business/報價單-英.docx")
            );

            var data = new Dictionary<string, string>();


            var items = new List<dynamic>();
            if (!string.IsNullOrEmpty(model.INT002))
            {
                items = JsonConvert.DeserializeObject<List<dynamic>>(model.INT002);
            }

            string serialNumbers = string.Join("\\n", items.Select((item, index) => (index + 1).ToString()));
            string modelLines = string.Join("\\n", items.Select(item => item.model?.ToString() ?? ""));
            string margin1Lines = string.Join("\\n", items.Select(item => item.margin1?.ToString() ?? ""));
            string margin2Lines = string.Join("\\n", items.Select(item => item.margin2?.ToString() ?? ""));
            string unitLines = string.Join("\\n", items.Select(item => item.unit?.ToString() ?? ""));
            string quantityLines = string.Join("\\n", items.Select(item => item.quantity?.ToString() ?? ""));
            string unitPriceLines = string.Join("\\n", items.Select(item =>
            {
                decimal unitPrice = decimal.TryParse(item.unitPrice?.ToString(), out decimal p) ? p : 0;

                return (unitPrice);
            }));
            string totalLines = string.Join("\\n", items.Select(item =>
            {
                decimal quantity = decimal.TryParse(item.quantity?.ToString(), out decimal q) ? q : 0;
                decimal unitPrice = decimal.TryParse(item.unitPrice?.ToString(), out decimal p) ? p : 0;

                return (quantity * unitPrice).ToString("#,0");
            }));
            decimal totalSum = items.Sum(item =>
            {
                decimal quantity = decimal.TryParse(item.quantity?.ToString(), out decimal q) ? q : 0;
                decimal unitPrice = decimal.TryParse(item.unitPrice?.ToString(), out decimal p) ? p : 0;

                return quantity * unitPrice;
            });

            decimal tax = totalSum * 0.05m;
            decimal totalWithTax = totalSum * 1.05m;

            data = new Dictionary<string, string>
            {
                ["1"] = INTRAdata.INT001 ?? "",
                ["2"] = INTRAdata.INT008 ?? "",
                ["3"] = INTRAdata.INT004 ?? "",
                ["4"] = INTRAdata.INT005 ?? "",
                ["5"] = model.INT001 ?? "",
                ["6"] = "",
                ["7"] = member.Mem001 ?? "",
                ["8"] = model.INT004 ?? "",
                ["9"] = INTRAdata.INT026 ?? "",
                ["10"] = serialNumbers,
                ["11"] = modelLines,
                ["12"] = margin1Lines,
                ["13"] = margin2Lines,
                ["14"] = unitLines,
                ["15"] = quantityLines,
                ["16"] = unitPriceLines,
                ["17"] = totalLines,
                ["18"] = totalSum.ToString("#,0"),
                ["19"] = tax.ToString("#,0"),
                ["20"] = totalWithTax.ToString("#,0"),
                ["21"] = (model.INT003?.Replace("\r\n", "\\n").Replace("\n", "\\n")) ?? string.Empty,
            };


            byte[] template = System.IO.File.ReadAllBytes(filePath);
            byte[] resultFile = WordRender.GenerateDocx(template, data);
            string path = Server.MapPath("~/file/Business");
            string outputFilePath = System.IO.Path.Combine(path, $"Business_{model.INT000}.docx");

            // 生成 Word 文件
            System.IO.File.WriteAllBytes(outputFilePath, resultFile);

            // 生成 PDF 文件
            bool conversionResult = WordRender.ConvertDocToPdf(path, $"{path}\\Business_{model.INT000}.docx");
            if (conversionResult)
            {
                string pdfFilePath = System.IO.Path.ChangeExtension(outputFilePath, ".pdf");
                byte[] pdfBytes = System.IO.File.ReadAllBytes(pdfFilePath);

                // 刪除暫存檔案
                if (System.IO.File.Exists(pdfFilePath))
                {
                    System.IO.File.Delete(pdfFilePath); // 刪除 PDF
                }
                if (System.IO.File.Exists(outputFilePath))
                {
                    System.IO.File.Delete(outputFilePath); // 刪除 Word
                }


                return File(pdfBytes, "application/pdf");
            }
            else
            {
                ViewBag.Message = "PDF生成失敗";
                return View();
            }
        }
        #endregion

        #region 下載上傳 記錄
        [HttpPost]
        public ActionResult UploadRecord(INTRB model, HttpPostedFileBase fileUpload)
        {
            string currentLang = (string)Session["Culture"] ?? "zh-TW";
            string actionKey = currentLang == "zh-TW" ? "更新" : "Update";
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
                var fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    TempData["SuccessMessage"] = "Only jpg、jpeg、png、gif、pdf";
                    return RedirectToAction("RecordList", new { INT000 = model.INT999 });
                }

                var fileName = model.INT000.ToString() + fileExtension;

                var uploadPath = Path.Combine(Server.MapPath("~/image/Business/UploadRecord"), fileName);

                try
                {
                    fileUpload.SaveAs(uploadPath);

                    var existingDevice = _db.INTRB.Find(model.INT000);
                    if (existingDevice != null)
                    {
                        existingDevice.INT003 = fileName;
                        existingDevice.IP = Request.UserHostAddress;

                        _db.SaveChanges();
                        TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang);
                    }
                }
                catch
                {
                    TempData["SuccessMessage"] = GetFailureMessage(actionKey, currentLang);
                }
            }

            return RedirectToAction("RecordList", new { INT000 = model.INT999 });
        }

        [HttpGet]
        public ActionResult DownloadRecord(INTRB model)
        {
            var record = _db.INTRB.Find(model.INT000);

            var filePath = Path.Combine(Server.MapPath("~/image/Business/UploadRecord"), record.INT003);

            if (!System.IO.File.Exists(filePath))
            {
                TempData["SuccessMessage"] = "檔案不存在。";
                return RedirectToAction("RecordList", new { INT000 = model.INT999 });
            }

            return File(filePath, "application/pdf");
        }
        #endregion

        #region 下載上傳 報價
        [HttpPost]
        public ActionResult UploadQuote(INTRC model, HttpPostedFileBase fileUpload)
        {
            string currentLang = (string)Session["Culture"] ?? "zh-TW";
            string actionKey = currentLang == "zh-TW" ? "更新" : "Update";
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
                var fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    TempData["SuccessMessage"] = "Only jpg、jpeg、png、gif、pdf";
                    return RedirectToAction("QuoteList", new { INT000 = model.INT999 });
                }

                var fileName = model.INT000.ToString() + fileExtension;

                var uploadPath = Path.Combine(Server.MapPath("~/image/Business/UploadQuote"), fileName);

                try
                {
                    fileUpload.SaveAs(uploadPath);

                    var existingDevice = _db.INTRC.Find(model.INT000);
                    if (existingDevice != null)
                    {
                        existingDevice.INT006 = fileName;
                        existingDevice.IP = Request.UserHostAddress;

                        _db.SaveChanges();
                        TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang);
                    }
                }
                catch
                {
                    TempData["SuccessMessage"] = GetFailureMessage(actionKey, currentLang);
                }
            }

            return RedirectToAction("QuoteList", new { INT000 = model.INT999 });
        }

        [HttpGet]
        public ActionResult DownloadQuote(INTRC model)
        {
            var record = _db.INTRC.Find(model.INT000);

            var filePath = Path.Combine(Server.MapPath("~/image/Business/UploadQuote"), record.INT006);

            if (!System.IO.File.Exists(filePath))
            {
                TempData["SuccessMessage"] = "檔案不存在。";
                return RedirectToAction("RecordList", new { INT000 = model.INT999 });
            }

            return File(filePath, "application/pdf");
        }
        #endregion

        #region 下載客戶訪談記錄
        [HttpGet]
        public ActionResult PrintRecord(INTRA model, string intrb)
        {
            string currentLang = (string)Session["Culture"] ?? "zh-TW";
            string filePath = Server.MapPath(currentLang == "zh-TW" ? "~/file/Business/客戶訪談記錄.docx" : "~/file/Business/客戶訪談記錄-英.docx");

            model = _db.INTRA.Find(model.INT000);
            var intrbdata = _db.INTRB.Find(intrb);

            var member = _db.Member.Where(x => x.Mem000 == intrbdata.Mid).FirstOrDefault();

            byte[] template = System.IO.File.ReadAllBytes(filePath);
            var data = new Dictionary<string, string>
            {
                ["1"] = model.INT001,
                ["2"] = member.Mem001 ?? "",
                ["3"] = intrbdata.INT005 ?? "",
                ["4"] = "",
                ["5"] = model.INT008 ?? "",
                ["6"] = model.INT006 ?? "",
                ["7"] = model.INT007 ?? "",
                ["8"] = intrbdata.INT008 ?? "",
                ["9"] = model.INT028 ?? "",
                ["10"] = intrbdata.INT004 ?? "",
                ["11"] = intrbdata.INT009 ?? "",
            };

            byte[] resultFile = WordRender.GenerateDocx(template, data);
            string path = Server.MapPath("~/file/Business");
            string outputFilePath = System.IO.Path.Combine(path, $"Business_{model.INT000}.docx");

            // 生成 Word 文件
            System.IO.File.WriteAllBytes(outputFilePath, resultFile);

            // 生成 PDF 文件
            bool conversionResult = WordRender.ConvertDocToPdf(path, $"{path}\\Business_{model.INT000}.docx");
            if (conversionResult)
            {
                string pdfFilePath = System.IO.Path.ChangeExtension(outputFilePath, ".pdf");
                byte[] pdfBytes = System.IO.File.ReadAllBytes(pdfFilePath);

                // 刪除暫存檔案
                if (System.IO.File.Exists(pdfFilePath))
                {
                    System.IO.File.Delete(pdfFilePath); // 刪除 PDF
                }
                if (System.IO.File.Exists(outputFilePath))
                {
                    System.IO.File.Delete(outputFilePath); // 刪除 Word
                }

                return File(pdfBytes, "application/pdf");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region 主管審核 主管回覆
        public ActionResult ChangeStatusForINTRB(string id, string id2, int status)
        {
            var result = ChangeStatus<INTRB>(id, status);
            return RedirectToAction("RecordList", new { INT000 = id2 });
        }

        public ActionResult ChangeStatusForINTRC(string id, string id2, int status)
        {
            var result = ChangeStatus<INTRC>(id, status);
            return RedirectToAction("QuoteList", new { INT000 = id2 });
        }

        private bool ChangeStatus<T>(string id, int status) where T : class
        {
            string currentLang = (string)Session["Culture"] ?? "zh-TW";
            string actionKey = currentLang == "zh-TW" ? "更新" : "Update";

            var model = _db.Set<T>().Find(id);
            if (model != null)
            {
                var statusProperty = typeof(T).GetProperty("Status");
                if (statusProperty != null)
                {
                    statusProperty.SetValue(model, status);

                    _db.Entry(model).Property("Status").IsModified = true;

                    try
                    {
                        _db.SaveChanges();
                        TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang);
                        return true; // 表示成功
                    }
                    catch
                    {
                        TempData["ErrorMessage"] = GetFailureMessage(actionKey, currentLang);
                        return false;
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = currentLang == "zh-TW" ? "模型未包含 Status 屬性。" : "The model does not contain a Status property.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = currentLang == "zh-TW" ? "找不到指定的記錄。" : "The specified record was not found.";
            }

            return false;
        }

        [HttpGet]
        public ActionResult RecordReply(INTRB model)
        {
            model = _db.INTRB.Find(model.INT000);
            ViewBag.INT000 = model.INT999;
            return View(model);
        }
        [HttpPost]
        public ActionResult RecordReply(INTRB model, bool? Per = true)
        {
            string currentLang = (string)Session["Culture"] ?? "zh-TW";
            string actionKey = currentLang == "zh-TW" ? "更新" : "Update";

            var existdata = _db.INTRB.Find(model.INT000);
            if (existdata != null)
            {
                existdata.INT006 = model.INT006;

                try
                {
                    _db.SaveChanges();
                    TempData["SuccessMessage"] = GetSuccessMessage(actionKey, currentLang);
                }
                catch
                {
                    TempData["SuccessMessage"] = GetFailureMessage(actionKey, currentLang);
                }
            }
            return RedirectToAction("RecordList", new { INT000 = model.INT999 });
        }
        #endregion

        #region 語言轉換
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string lang = (string)Session["Culture"] ?? "zh-TW";
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            base.OnActionExecuting(filterContext);
        }

        public ActionResult ChangeLanguage(string lang)
        {
            Session["Culture"] = lang;
            return Redirect(Request.UrlReferrer?.ToString());
        }
        #endregion

        #region 顯示文字
        public static string GetSuccessMessage(string actionKey, string lang = "zh-TW")
        {
            if (lang == "zh-TW")
            {
                return $"{actionKey}成功！";
            }
            return $"{actionKey} Successful!";
        }

        public static string GetFailureMessage(string actionKey, string lang = "zh-TW")
        {
            if (lang == "zh-TW")
            {
                return $"{actionKey}失敗！";
            }
            return $"{actionKey} Failed!";
        }
        #endregion

        #region 儲存檔案
        private string SaveUploadedFile(HttpPostedFileBase file, string relativePath)
        {
            if (file != null && file.ContentLength > 0)
            {
                var path = Server.MapPath(relativePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fileName = Path.GetFileName(file.FileName);

                // 移除或替換檔案名稱中的特殊字元
                fileName = SanitizeFileName(fileName);

                // 生成三個隨機的英文和數字字符
                var random = new Random();
                var randomChars = new char[3];
                for (int i = 0; i < randomChars.Length; i++)
                {
                    int ascii = random.Next(0, 3);
                    if (ascii == 0)
                    {
                        randomChars[i] = (char)random.Next(65, 91); // 大寫字母
                    }
                    else if (ascii == 1)
                    {
                        randomChars[i] = (char)random.Next(97, 123); // 小寫字母
                    }
                    else
                    {
                        randomChars[i] = (char)random.Next(48, 58); // 數字
                    }
                }
                var prefix = new string(randomChars);

                var uniqueFileName = $"{prefix}_{Path.GetFileNameWithoutExtension(fileName)}{Path.GetExtension(fileName)}";

                var filePath = Path.Combine(path, uniqueFileName);

                file.SaveAs(filePath);

                return Url.Content(relativePath + "/" + uniqueFileName);
            }
            return null;
        }

        private string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return fileName;

            var invalidChars = Path.GetInvalidFileNameChars().Concat(new[] { '#', '%', '&', ' ' }).ToArray();
            foreach (var c in invalidChars)
            {
                fileName = fileName.Replace(c, '_');
            }
            return fileName;
        }
        #endregion
    }
}