USE [ExplorerBeyond]
GO

/****** Object:  Table [dbo].[Offers]    Script Date: 10/26/2023 10:24:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Offers](
	[UEID] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[SupplierOfferID] [varchar](25) NOT NULL,
	[RentCost] [money] NOT NULL,
	[RentCurrency] [varchar](5) NOT NULL,
	[CarDesc] [varchar](100) NOT NULL,
	[CarID] [varchar](25) NOT NULL,
	[CarLogoImage] [nvarchar](250) NULL,
	[CarImage] [nvarchar](250) NULL,
	[EntryDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Offers] PRIMARY KEY CLUSTERED 
(
	[UEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Offers] ADD  CONSTRAINT [DF_Table_1_SuppliedID]  DEFAULT ((0)) FOR [SupplierID]
GO

ALTER TABLE [dbo].[Offers] ADD  CONSTRAINT [DF_Offers_EntryDateTime]  DEFAULT (getdate()) FOR [EntryDateTime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'; 0 -- Does not provided' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Offers', @level2type=N'COLUMN',@level2name=N'SupplierID'
GO

