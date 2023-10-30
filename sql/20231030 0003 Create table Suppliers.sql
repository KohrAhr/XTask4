USE [ExplorerBeyond]
GO

-- ----------------------------
-- Table structure for Suppliers
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Suppliers]') AND type IN ('U'))
	DROP TABLE [dbo].[Suppliers]
GO

CREATE TABLE [dbo].[Suppliers] (
  [SupplierID] int  IDENTITY(1,1) NOT NULL,
  [SupplierName] nvarchar(50) COLLATE Cyrillic_General_CI_AS  NOT NULL,
  [SupplierUrl] varchar(250) COLLATE Cyrillic_General_CI_AS  NOT NULL,
  [IsActive] bit DEFAULT 0 NOT NULL,
  [IsAutoRefreshActive] bit DEFAULT 0 NOT NULL,
  [AutoRefreshInMinutes] int DEFAULT 0 NOT NULL,
  [ClassTransferList] text COLLATE Cyrillic_General_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Suppliers] SET (LOCK_ESCALATION = TABLE)
GO

-- ----------------------------
-- Primary Key structure for table Suppliers
-- ----------------------------
ALTER TABLE [dbo].[Suppliers] ADD CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED ([SupplierID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

