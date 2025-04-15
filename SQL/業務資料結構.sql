USE [CPC]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 2025/4/15 上午 08:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[ID] [uniqueidentifier] NOT NULL,--識別碼
	[SourceID] [uniqueidentifier] NOT NULL,--使用者名稱
	[FileName] [nvarchar](255) NOT NULL,--檔案名稱
	[ServerPath] [nvarchar](255) NOT NULL,--路徑
	[FileSize] [int] NOT NULL,--檔案大小
	[Extension] [nvarchar](10) NOT NULL,--副檔名
	[CreateTime] [datetime] NOT NULL,--建立時間
	[SourceDB] [nvarchar](10) NULL,--DB來源
	[IP] [nvarchar](20) NULL,--IP
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INTRA]    Script Date: 2025/4/15 上午 08:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INTRA](
	[INT000] [varchar](36) NOT NULL,--識別碼
	[INT001] [nvarchar](50) NOT NULL,--客戶名稱
	[INT002] [varchar](2) NULL,--型態別
	[INT003] [nvarchar](250) NULL,--地址
	[INT004] [nvarchar](100) NULL,--電話
	[INT005] [nvarchar](100) NULL,--傳真
	[INT006] [nvarchar](50) NULL,--國家
	[INT007] [nvarchar](100) NULL,--區域
	[INT008] [nvarchar](50) NULL,--接洽人1
	[INT009] [nvarchar](50) NULL,--職稱1
	[INT010] [nvarchar](100) NULL,--分機1
	[INT011] [nvarchar](50) NULL,--接洽人2
	[INT012] [nvarchar](50) NULL,--職稱2
	[INT013] [nvarchar](100) NULL,--分機2
	[INT014] [nvarchar](50) NULL,--接洽人3
	[INT015] [nvarchar](50) NULL,--職稱3
	[INT016] [nvarchar](100) NULL,--分機3
	[INT017] [text] NULL,--業務範圍
	[IP] [nvarchar](20) NULL,--IP
	[status] [int] NULL,--狀態
	[CreateTime] [datetime] NULL,--建立時間
	[Mid] [int] NULL,--會員ID
	[INT018] [nvarchar](100) NULL,--信用額度
	[INT019] [nvarchar](100) NULL,--成立時間
	[INT020] [nvarchar](100) NULL,--公司網站
	[INT021] [nvarchar](100) NULL,--公司營業額
	[INT022] [nvarchar](100) NULL,--年營業額
	[INT023] [nvarchar](100) NULL,--員工人數
	[INT024] [nvarchar](100) NULL,--統編
	[INT025] [nvarchar](100) NULL,--公司負責人
	[INT026] [nvarchar](100) NULL,--付款條件
	[INT027] [nvarchar](100) NULL,--客戶Email
	[INT028] [nvarchar](500) NULL,--CPC相關產品現況（儲存為 JSON 字串）
	[INT029] [int] NULL,--部門
	[INT030] [nvarchar](250) NULL,--名片上傳
	[INTRD] [nvarchar](36) NULL,--客戶狀態/來源 (與INTRD的INT000)關聯
	[INT031] [nvarchar](10) NULL,--幣別
	[INT032] [bit] NULL,--旗標
PRIMARY KEY CLUSTERED 
(
	[INT000] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INTRB]    Script Date: 2025/4/15 上午 08:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INTRB](
	[INT000] [varchar](36) NOT NULL,--識別碼
	[INT001] [nvarchar](250) NULL,--主題
	[INT002] [int] NULL,--訪談記錄別  
	[INT003] [nvarchar](150) NULL,--檔案網址
	[INT004] [text] NULL,--內容
	[INT005] [varchar](20) NULL,--訪談時間
	[INT006] [text] NULL,--主管回覆欄
	[INT007] [nvarchar](250) NULL,--專案名稱
	[INT999] [varchar](36) NOT NULL,--
	[IP] [nvarchar](20) NULL,--
	[status] [int] NULL,--
	[CreateTime] [datetime] NULL,--
	[Level] [int] NULL,--
	[Mid] [int] NULL,--會員ID
	[INT008] [text] NULL,--拜訪目的
	[INT009] [text] NULL,--後續步驟
	[INT010] [int] NULL,--部門
	[INT011] [nvarchar](100) NULL,--子公司
	[INT012] [nvarchar](250) NULL,--圖片上傳
	[INT013] [bit] NULL,--是否有旗子
	[INT014] [bit] NULL,--旗標
	[INT015] [nvarchar](100) NULL,--負責業務(for展覽)
	[INT016] [nvarchar](100) NULL,--填表人(for展覽)
	[INT017] [nvarchar](100) NULL,--客戶姓名(for展覽)
	[INT018] [nvarchar](100) NULL,--Email(for展覽)
	[INT019] [nvarchar](100) NULL,--電話(for展覽)
	[INT020] [nvarchar](250) NULL,--網址(for展覽)
	[INT021] [nvarchar](250) NULL,--地址(for展覽)
	[INT022] [nvarchar](100) NULL,--興趣產品(for展覽)
PRIMARY KEY CLUSTERED 
(
	[INT000] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INTRC]    Script Date: 2025/4/15 上午 08:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INTRC](
	[INT000] [varchar](36) NOT NULL,--識別碼
	[INT001] [nvarchar](20) NULL,--報價時間
	[INT002] [nvarchar](max) NULL,--報價明細（JSON 格式）
	[Mid] [int] NULL,--業務人員 ID
	[INT999] [varchar](36) NOT NULL,--INTRA-INT000 關聯欄位。
	[IP] [nvarchar](20) NULL,--客戶端 IP 位址。
	[status] [int] NULL,--訂單狀態
	[CreateTime] [datetime] NULL,--建立時間
	[INT003] [nvarchar](200) NULL,--備註
	[INT004] [nvarchar](50) NULL,--交期
	[INT005] [bit] NULL,--是否稅額
	[INT006] [nvarchar](150) NULL,--檔案網址
	[INT007] [nvarchar](max) NULL,--備註2
	[INT008] [nvarchar](100) NULL,--子公司客戶
	[INT009] [varchar](4) NULL,--單別(ERP)
	[INT010] [varchar](11) NULL,--單號(ERP)
	[INT011] [varchar](12) NULL,--編號
PRIMARY KEY CLUSTERED 
(
	[INT000] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INTRD]    Script Date: 2025/4/15 上午 08:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INTRD](
	[INT000] [uniqueidentifier] NOT NULL,--識別碼
	[INT001] [nvarchar](100) NOT NULL,--名稱
	[INT002] [nvarchar](100) NOT NULL,--國家
	[INT003] [nvarchar](100) NULL,--區間
	[status] [int] NULL,--狀態
	[CreateTime] [datetime] NULL,--建立時間
	[Sort] [int] NULL,--排序
PRIMARY KEY CLUSTERED 
(
	[INT000] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INTRE]    Script Date: 2025/4/15 上午 08:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INTRE](
	[INT000] [varchar](36) NOT NULL,--識別碼
	[INT001] [nvarchar](20) NULL,--追蹤日期
	[INT002] [nvarchar](max) NULL,--內容
	[INT003] [int] NULL,--聯絡方式
	[INT004] [nvarchar](max) NULL,--後續步驟
	[IP] [nvarchar](20) NULL,--IP
	[status] [int] NULL,--狀態
	[CreateTime] [datetime] NULL,-- 建立時間
	[INT999] [varchar](36) NULL,--與 INTRB 表的關聯欄位
	[Mid] [int] NULL,--會員ID
	[INT005] [bit] NULL,--記錄旗標
PRIMARY KEY CLUSTERED 
(
	[INT000] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 2025/4/15 上午 08:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[Mem000] [int] IDENTITY(1,1) NOT NULL,--識別碼
	[Mem001] [nvarchar](50) NOT NULL,--使用者名稱
	[Mem002] [nvarchar](50) NOT NULL,--帳號（需唯一）
	[Mem003] [nvarchar](50) NOT NULL,--密碼
	[Controller] [nvarchar](50) NOT NULL,--對應部門的 Controller 名稱
	[Action] [nvarchar](50) NOT NULL,--對應 Controller 的 Action 名稱
	[Permission] [int] NULL,--權限等級
	[Status] [int] NULL,--會員狀態
	[CreateTime] [datetime] NULL,--建立時間
	[IsBusiness] [varchar](1) NULL,--是否為業務
	[IsCrossMember] [nvarchar](1) NULL,--是否可看所有人資料
	[AllowedMem000] [nvarchar](200) NULL,--可看會員資料的Mem000
	[Perm_C] [nvarchar](10) NULL,--是否新增
	[Perm_U] [nvarchar](10) NULL,--是否更新
	[Perm_D] [nvarchar](10) NULL,--是否刪除訪
	[LastLoginTime] [datetime] NULL,--上次登入時間
PRIMARY KEY CLUSTERED 
(
	[Mem000] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Mem002] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Files] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Files] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[INTRA] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[INTRB] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[INTRC] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[INTRD] ADD  DEFAULT (newid()) FOR [INT000]
GO
ALTER TABLE [dbo].[INTRD] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[INTRE] ADD  DEFAULT (newid()) FOR [INT000]
GO
ALTER TABLE [dbo].[INTRE] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Member] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
