﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("Member")]

    public class Member
    {
        /// <summary>
        /// 識別碼，唯一的會員ID
        /// </summary>
        [Key]
        public int Mem000 { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string Mem001 { get; set; }

        /// <summary>
        /// 帳號（需唯一）
        /// </summary>
        public string Mem002 { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Mem003 { get; set; }

        /// <summary>
        /// 對應部門的 Controller 名稱（例如：SafetyController）
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 對應 Controller 的 Action 名稱（例如：ViewReports）
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 權限等級
        /// 1 = 系統管理者
        /// 2 = 最高權限
        /// 3 = 一般使用者
        /// </summary>
        public int Permission { get; set; }

        /// <summary>
        /// 會員狀態
        /// 0 = 啟用
        /// -1 = 刪除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 會員建立時間，預設為資料庫自動填入的當前時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否為業務
        /// </summary>
        public string IsBusiness { get; set; }
         /// <summary>
        /// 是否可看所有人資料
        /// </summary>
        public string IsCrossMember { get; set; }         
        /// <summary>
        /// 可看會員資料的Mem000
        /// </summary>
        public string AllowedMem000 { get; set; }
        /// <summary>
        /// 是否新增客戶資料 Y/N        
        /// 是否新增訪談記錄 Y/N        
        /// 是否新增報價資料 Y/N
        /// ex:YNY
        /// </summary>
        public string Perm_C { get; set; }
        /// <summary>
        /// 是否更新增客戶資料 Y/N        
        /// 是否更新訪談記錄 Y/N        
        /// 是否更新報價資料 Y/N
        /// ex:YNY
        /// </summary>
        public string Perm_U { get; set; }
        /// <summary>
        /// 是否刪除客戶資料 Y/N        
        /// 是否刪除訪談記錄 Y/N        
        /// 是否刪除報價資料 Y/N
        /// ex:YNY
        /// </summary>
        public string Perm_D { get; set; }
        /// <summary>
        /// 上次登入時間
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
    }
}