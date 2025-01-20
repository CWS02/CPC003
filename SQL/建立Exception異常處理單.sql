USE CPC
GO
CREATE TABLE ExceptionForm (
    EXC001 NVARCHAR(30),                      -- 單號
    EXC002 DATETIME,                          -- 日期
    EXC003 NVARCHAR(10),                      -- 單位
    EXC004 NVARCHAR(10),                      -- 單位其他
    EXC005 NVARCHAR(10),                      -- 異常類別
    EXC006 NVARCHAR(30),                      -- 主題
    EXC007 TEXT,                              -- 異常問題之描述
    EXC008 TEXT,                              -- 緊急處理方式
    EXC009 TEXT,                              -- 原因分析
    EXC010 TEXT,                              -- 再發防止對策
    EXC011 TEXT,                              -- 其他單位需配合執行事項
    status int,                              -- 狀態 0=未審核 1=已審核 2=刪除
    IP NVARCHAR(45) NULL,                     -- IP
    CreateTime DATETIME DEFAULT GETDATE(),    -- MSSQL自動生成時間
    PRIMARY KEY (EXC001)                      -- 設定 EXC000 為主鍵
);
