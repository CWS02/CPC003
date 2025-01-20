CREATE TABLE DEVTA (
    DEA000 NVARCHAR(40) NOT NULL,  -- 隨機碼 (主鍵)
    DEA001 NVARCHAR(30) NOT NULL,  -- 設備名稱
    DEA002 NVARCHAR(15),           --項目
    DEA003 SMALLINT,               -- 廠別
    DEA004 NVARCHAR(50),           -- 照片網址
    DEA005 NVARCHAR(150),          -- 設備規格
    DEA006 NVARCHAR(30),           -- 保養廠商
    DEA007 NVARCHAR(150),          -- 備註
    DEA008 NVARCHAR(20),           -- IP
    DEA009 NVARCHAR(20),           -- 創建日期
    DEA010 SMALLINT,               -- 是否啟用
    PRIMARY KEY (DEA000)           -- 設定 DEA000 為主鍵
);
CREATE TABLE DEVTB (
    DEB000 NVARCHAR(40) NOT NULL,  -- 隨機碼 (可以重複)
    DEB001 NVARCHAR(20),           -- 檢驗項目
    DEB002 smallint,			   -- 每季年月
    DEB003 NVARCHAR(150),           -- 備註
    DEB004 NVARCHAR(20),           -- IP
    DEB005 NVARCHAR(20),           -- 創建日期
    DEB006 SMALLINT,               -- 是否啟用
	DEB999 NVARCHAR(40) NOT NULL
);
CREATE TABLE DEVTC (
    DEC000 NVARCHAR(40) NOT NULL,  -- 隨機碼 (可以重複)
    DEC001 NVARCHAR(10),           -- 檢驗時間
	DEC002 NVARCHAR(100),          -- 檔案網址
	DEC003 NVARCHAR(150),		   -- 備註
    DEC004 NVARCHAR(20),           -- IP
	DEC999 NVARCHAR(40) NOT NULL
);

CREATE TABLE DEVTD (
    DED000 NVARCHAR(40) NOT NULL,  -- 隨機碼 (可以重複)
    DED001 NVARCHAR(20),           -- 維修開始時間
    DED002 NVARCHAR(20),           -- 維修結束時間
	DED003 NVARCHAR(100),          -- 檔案網址
	DED004 NVARCHAR(150),		   -- 備註
    DED005 NVARCHAR(20),           -- IP
    DED006 NVARCHAR(20),           -- 創建日期
	DED999 NVARCHAR(40) NOT NULL   -- 連接DEA000
);
