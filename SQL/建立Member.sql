USE CPC
GO

DROP TABLE IF EXISTS Member;

CREATE TABLE Member (
    Mem000 INT IDENTITY(1,1) PRIMARY KEY,  -- 識別碼
    Mem001 NVARCHAR(50) NOT NULL,           -- 使用者名稱
    Mem002 NVARCHAR(50) NOT NULL UNIQUE,    -- 帳號（需唯一）
    Mem003 NVARCHAR(50) NOT NULL,           -- 密碼
    Controller NVARCHAR(50) NOT NULL,       -- 對應部門的 Controller 名稱（例如：SafetyController）
    Action NVARCHAR(50) NOT NULL,           -- 對應 Controller 的 Action 名稱（例如：ViewReports）
    Permission INT,                         -- 權限 1=系統管理者 2=最高權限 3=一般使用者
    Status INT,                             -- 狀態
    CreateTime DATETIME DEFAULT GETDATE()   -- 建立時間（自動帶入目前時間）
);