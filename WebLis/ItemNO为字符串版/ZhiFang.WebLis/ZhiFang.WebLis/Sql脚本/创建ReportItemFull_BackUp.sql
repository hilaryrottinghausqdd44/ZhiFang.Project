USE [weblis_dajia20140522]
GO

/****** Object:  Table [dbo].[ReportItemFull_BackUp]    Script Date: 05/04/2015 11:48:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ReportItemFull_BackUp](
	[ItemRanNum] [bigint] IDENTITY(1,1) NOT NULL,
	[ReportFormID] [varchar](50) NOT NULL,
	[ReportItemID] [int] NOT NULL,
	[TESTITEMNAME] [varchar](200) NULL,
	[TESTITEMSNAME] [varchar](200) NULL,
	[RECEIVEDATE] [datetime] NULL,
	[SECTIONNO] [varchar](50) NULL,
	[TESTTYPENO] [varchar](50) NULL,
	[SAMPLENO] [varchar](50) NULL,
	[PARITEMNO] [varchar](50) NULL,
	[ITEMNO] [varchar](50) NULL,
	[ORIGINALVALUE] [varchar](500) NULL,
	[REPORTVALUE] [varchar](500) NULL,
	[ORIGINALDESC] [varchar](500) NULL,
	[REPORTDESC] [varchar](500) NULL,
	[STATUSNO] [varchar](50) NULL,
	[EQUIPNO] [varchar](50) NULL,
	[MODIFIED] [varchar](50) NULL,
	[REFRANGE] [varchar](500) NULL,
	[ITEMDATE] [datetime] NULL,
	[ITEMTIME] [datetime] NULL,
	[ISMATCH] [varchar](50) NULL,
	[RESULTSTATUS] [varchar](50) NULL,
	[TESTITEMDATETIME] [datetime] NULL,
	[REPORTVALUEALL] [varchar](200) NULL,
	[PARITEMNAME] [varchar](500) NULL,
	[PARITEMSNAME] [varchar](500) NULL,
	[DISPORDER] [varchar](50) NULL,
	[ITEMORDER] [varchar](50) NULL,
	[UNIT] [varchar](50) NULL,
	[SERIALNO] [varchar](50) NULL,
	[ZDY1] [varchar](50) NULL,
	[ZDY2] [varchar](50) NULL,
	[ZDY3] [varchar](50) NULL,
	[ZDY4] [varchar](50) NULL,
	[ZDY5] [varchar](50) NULL,
	[HISORDERNO] [varchar](100) NULL,
	[FORMNO] [varchar](150) NOT NULL,
	[TECHNICIAN] [nvarchar](100) NULL,
	[OLDSERIALNO] [nvarchar](50) NULL,
	[PREC] [varchar](50) NULL,
	[itemunit] [varchar](40) NULL,
	[itemename] [varchar](40) NULL,
	[secretgrade] [int] NULL,
	[shortname] [varchar](40) NULL,
	[shortcode] [varchar](40) NULL,
	[cuegrade] [int] NULL,
	[ZDY6] [varchar](50) NULL,
	[ZDY7] [varchar](50) NULL,
	[ZDY8] [varchar](50) NULL,
	[ZDY9] [varchar](50) NULL,
	[ZDY10] [varchar](50) NULL,
	[curritemredo] [int] NULL,
	[Barcode] [varchar](20) NULL,
	[SectionName] [varchar](20) NULL,
	[EquipName] [varchar](40) NULL,
	[CheckType] [int] NULL,
	[CheckTypeName] [varchar](40) NULL,
	[ReportItemIndexID] [bigint] NOT NULL,
	[ReportFormIndexID] [bigint] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ReportItemFull_BackUp] ADD  DEFAULT ((0)) FOR [ReportItemIndexID]
GO


