using CPC02.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPC02.Controllers
{
    public class CommonController : Controller
    {
        CPCContext _db=new CPCContext();
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
                var DepartmentP = Session["DepartmentP"]?.ToString();
                var Permission = Session["Permission"];
                model.LOG009 = Permission != null ? Convert.ToInt32(Permission) : (int?)null;
                model.Mid = mid;
                model.LOG008 = DepartmentP;

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

        #region 管理部門記錄
        [HttpGet]
        public ActionResult ManageLogList()
        {
            if (Session["Mid"] == null)
            {
                return RedirectToAction("Login", "Member");
            }

            var departmentP = Session["DepartmentP"]?.ToString();

            int permission = 0;
            if (Session["Permission"] != null)
            {
                int.TryParse(Session["Permission"].ToString(), out permission);
            }

            var model = _erpcpc.WLOGA.AsQueryable();
            model = model.Where(x => permission >= x.LOG009);

            if (!string.IsNullOrEmpty(departmentP))
            {
                model = model.Where(x => departmentP.Contains(x.LOG008));
            }

            return View("WorkLogList", model.ToList()); 
        }
        #endregion

        #region  檔案多筆上傳
        [HttpGet]
        public ActionResult MultipleFiles(Files model,string name = null ,string layout = null)
        {
            var data=_db.Files.Where(x=>x.SourceID==model.SourceID).ToList();
            ViewBag.SourceID = model.SourceID;
            ViewBag.SourceDB = model.SourceDB;
            ViewBag.name = name;
            ViewBag.Layout = string.IsNullOrEmpty(layout) ? "~/Views/Shared/_LayoutCommon.cshtml" : layout;
            return View(data);
        }

        [HttpPost]
        public ActionResult MultipleFiles(IEnumerable<HttpPostedFileBase> files,Files model, string name = null, string layout = null)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".JPG", ".png", ".gif", ".pdf",".doc",".docx",".xlsx" };
            string currentLang = (string)Session["Culture"] ?? "zh-TW";

            var data = _db.Files.Where(x => x.SourceID == model.SourceID).ToList();
            ViewBag.SourceID = model.SourceID;
            ViewBag.SourceDB = model.SourceDB;
            ViewBag.name = name;
            ViewBag.Layout = string.IsNullOrEmpty(layout) ? "~/Views/Shared/_LayoutCommon.cshtml" : layout;

            if (files == null || !files.Any(f => f != null && f.ContentLength > 0))
            {
                ViewBag.Message = currentLang == "zh-TW" ? "未接收到任何檔案" : "No files received.";
                return View(data);
            }

            string uploadFolder = Server.MapPath("~/image/upload");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            List<Files> fileList = new List<Files>();

            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        string originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string uniqueFileName = $"{originalFileName}_{Guid.NewGuid().ToString("N").Substring(0, 8)}{extension}";
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);

                        if (!allowedExtensions.Contains(extension))
                        {
                            ViewBag.Message = currentLang == "zh-TW"
                                                   ? $"檔案 {file.FileName} 格式不被允許，請上傳 {string.Join(", ", allowedExtensions)} 格式檔案。"
                                                   : $"File {file.FileName} format is not allowed. Please upload files with the following formats: {string.Join(", ", allowedExtensions)}."; 
                            return View(data);
                        }
                        // 🚨 檔案大小檢查（限制 5MB）
                        if (file.ContentLength > 5 * 1024 * 1024) // 5MB 限制
                        {
                            ViewBag.Message = currentLang == "zh-TW"
                                ? $"檔案 {file.FileName} 大小超過 5MB，請上傳小於 5MB 的檔案。"
                                : $"File {file.FileName} exceeds the size limit of 5MB. Please upload files smaller than 5MB.";
                            return View(data);
                        }

                        file.SaveAs(filePath);

                        var fileRecord = new Files
                        {
                            SourceID = model.SourceID,
                            FileName = fileName,
                            ServerPath = "/image/upload/" + uniqueFileName,
                            FileSize = file.ContentLength,
                            Extension = extension,
                            SourceDB= model.SourceDB,
                            IP= Request.UserHostAddress
                        };

                        fileList.Add(fileRecord);
                        _db.Files.Add(fileRecord);
                    }
                }

                _db.SaveChanges();
                ViewBag.Message = currentLang == "zh-TW"
                            ? $"{fileList.Count} 個檔案上傳成功！"
                            : $"{fileList.Count} files have been uploaded successfully!";
            }
            data = _db.Files.Where(x => x.SourceID == model.SourceID).ToList();
            return View(data);
        }

        [HttpPost]
        public ActionResult DeleteFile(Guid id, string layout = null)
        {
            var fileRecord = _db.Files.SingleOrDefault(f => f.ID == id);
            if (fileRecord != null)
            {
                string filePath = Server.MapPath(fileRecord.ServerPath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _db.Files.Remove(fileRecord);
                _db.SaveChanges();

                TempData["Message"] = "檔案刪除成功！";
            }
            else
            {
                TempData["Message"] = "找不到該檔案！";
            }

            return RedirectToAction("MultipleFiles", new { SourceID = fileRecord.SourceID, layout = layout });
        }


        #endregion
    }
}