USE [ExplorerBeyond]
GO

DELETE FROM [dbo].[Suppliers];
GO

-- ----------------------------
-- Records of Suppliers
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Suppliers] ON
GO

INSERT INTO [dbo].[Suppliers] ([SupplierID], [SupplierName], [SupplierUrl], [IsActive], [IsAutoRefreshActive], [AutoRefreshInMinutes], [ClassTransferList]) VALUES (N'1', N'Best Rentals', N'https://1click.lv/GitHub/XTask4/data/AvailableOffers.json', N'1', N'0', N'60', N'[
	{"OldName":"uniqueId","NewName":"SupplierOfferID"},
	{"OldName":"rentalCost","NewName":"RentCost"},
	{"OldName":"rentalCostCurrency","NewName":"RentCurrency"},
	{"OldName":"vehicle","NewName":"CarDesc"},
	{"OldName":"sipp","NewName":"CarID"},
	{"OldName":"imageLink","NewName":"CarImage"},
	{"OldName":"logo","NewName":"CarLogoImage"}
]
')
GO

INSERT INTO [dbo].[Suppliers] ([SupplierID], [SupplierName], [SupplierUrl], [IsActive], [IsAutoRefreshActive], [AutoRefreshInMinutes], [ClassTransferList]) VALUES (N'2', N'Northern Rentals', N'https://1click.lv/GitHub/XTask4/data/GetRates.json', N'1', N'0', N'60', N'[
	{"OldName":"id","NewName":"SupplierOfferID"},
	{"OldName":"price","NewName":"RentCost"},
	{"OldName":"currency","NewName":"RentCurrency"},
	{"OldName":"vehicleName","NewName":"CarDesc"},
	{"OldName":"sippCode","NewName":"CarID"},
	{"OldName":"image","NewName":"CarImage"},
	{"OldName":"supplierLogo","NewName":"CarLogoImage"}
]')
GO

INSERT INTO [dbo].[Suppliers] ([SupplierID], [SupplierName], [SupplierUrl], [IsActive], [IsAutoRefreshActive], [AutoRefreshInMinutes], [ClassTransferList]) VALUES (N'3', N'South Rentals', N'https://1click.lv/GitHub/XTask4/data/Quotes.json', N'1', N'0', N'60', N'[
	{"OldName":"quoteNumber","NewName":"SupplierOfferID"},
	{"OldName":"price","NewName":"RentCost"},
	{"OldName":"currency","NewName":"RentCurrency"},
	{"OldName":"vehicleName","NewName":"CarDesc"},
	{"OldName":"acrissCode","NewName":"CarID"},
	{"OldName":"imageLink","NewName":"CarImage"},
	{"OldName":"logoLink","NewName":"CarLogoImage"}
]
')
GO

SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO


-- ----------------------------
-- Auto increment value for Suppliers
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Suppliers]', RESEED, 3)
GO
