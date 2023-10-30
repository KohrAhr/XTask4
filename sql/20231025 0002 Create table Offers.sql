-- ----------------------------
-- Table structure for Offers
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Offers]') AND type IN ('U'))
	DROP TABLE [dbo].[Offers]
GO

CREATE TABLE [dbo].[Offers] (
  [UEID] bigint  IDENTITY(1,1) NOT NULL,
  [SupplierID] int DEFAULT 0 NOT NULL,
  [SupplierOfferID] varchar(25) COLLATE Cyrillic_General_CI_AS  NOT NULL,
  [RentCost] money  NOT NULL,
  [RentCurrency] varchar(5) COLLATE Cyrillic_General_CI_AS  NOT NULL,
  [CarDesc] varchar(100) COLLATE Cyrillic_General_CI_AS  NOT NULL,
  [CarID] varchar(25) COLLATE Cyrillic_General_CI_AS  NOT NULL,
  [CarLogoImage] nvarchar(250) COLLATE Cyrillic_General_CI_AS  NULL,
  [CarImage] nvarchar(250) COLLATE Cyrillic_General_CI_AS  NULL,
  [EntryDateTime] datetime DEFAULT getdate() NOT NULL
)
GO

ALTER TABLE [dbo].[Offers] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'; 0 -- Does not provided',
'SCHEMA', N'dbo',
'TABLE', N'Offers',
'COLUMN', N'SupplierID'
GO


-- ----------------------------
-- Auto increment value for Offers
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Offers]', RESEED, 1620)
GO


-- ----------------------------
-- Primary Key structure for table Offers
-- ----------------------------
ALTER TABLE [dbo].[Offers] ADD CONSTRAINT [PK_Offers] PRIMARY KEY CLUSTERED ([UEID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

