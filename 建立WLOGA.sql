use CPC
go
drop table WLOGA
CREATE TABLE WLOGA (
    LOG000 UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,  -- 唯一碼
    LOG001 DATETIME NOT NULL,      -- 日期
    LOG002 NVARCHAR(MAX),         -- 工作內容
    LOG003 NVARCHAR(MAX),         -- 工作問題
    LOG004 NVARCHAR(MAX),         -- 備註
    LOG005 NVARCHAR(MAX),         -- 明日工作計畫
    LOG006 NVARCHAR(MAX),         -- 共同完成人員
    LOG007 BIT NOT NULL,          -- 是否完成
    LOG008 NVARCHAR(MAX),         -- 部門
    LOG009 int,                   -- 權限
    IP nvarchar(20),              -- IP
    Mid VARCHAR(10),              -- 與CMSMV-MV001關聯
    Status INT NOT NULL,          -- 狀態 (0=正常, -1=刪除)
    CreaTime DATETIME DEFAULT GETDATE(), -- 建立時間，DB自動帶入當前時間
);
CREATE TABLE WLOGB (
    LOG000 UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,  -- 唯一碼
    LOG001 NVARCHAR(50),          -- 專案名稱
    LOG002 NVARCHAR(MAX),         -- 部門代號
    IP nvarchar(20),              -- IP
    Mid VARCHAR(10),              -- 與CMSMV-MV001關聯
    Status INT NOT NULL,          -- 狀態 (0=正常, -1=刪除)
    CreaTime DATETIME DEFAULT GETDATE(), -- 建立時間，DB自動帶入當前時間
);