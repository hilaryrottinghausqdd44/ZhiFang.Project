USE [weblis_dajia20140522]
GO

/****** Object:  Table [dbo].[ReportMarrowFull_BackUp]    Script Date: 05/04/2015 11:49:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ReportMarrowFull_BackUp](
	[ReportFormID] [varchar](50) NOT NULL,
	[ReportItemID] [int] NOT NULL,
	[ParItemNo] [int] NOT NULL,
	[ItemNo] [int] NOT NULL,
	[BloodNum] [int] NULL,
	[BloodPercent] [float] NULL,
	[MarrowNum] [int] NULL,
	[MarrowPercent] [float] NULL,
	[BloodDesc] [nvarchar](1000) NULL,
	[MarrowDesc] [nvarchar](1000) NULL,
	[StatusNo] [int] NULL,
	[RefRange] [varchar](30) NULL,
	[EquipNo] [int] NULL,
	[IsCale] [int] NULL,
	[Modified] [int] NULL,
	[ItemDate] [datetime] NULL,
	[ItemTime] [datetime] NULL,
	[IsMatch] [int] NULL,
	[ResultStatus] [varchar](10) NULL,
	[FormNo] [varchar](150) NOT NULL,
	[CItemNo] [int] NULL,
	[ReportText] [text] NULL,
	[OrgValue] [float] NULL,
	[OrgDesc] [varchar](400) NULL,
	[IsPrint] [int] NULL,
	[PrintOrder] [int] NULL,
	[itemname] [varchar](50) NULL,
	[ValueTypeNo] [int] NULL,
	[ReportValue] [float] NULL,
	[ReportDesc] [varchar](400) NULL,
	[ReportImage] [image] NULL,
	[Barcode] [varchar](20) NULL,
	[EquipName] [varchar](40) NULL,
	[ReceiveDate] [datetime] NULL,
	[SectionNo] [int] NULL,
	[ItemCName] [varchar](40) NULL,
	[ItemEName] [varchar](40) NULL,
	[ParItemCName] [varchar](40) NULL,
	[ParItemEName] [varchar](20) NULL,
	[TestTypeNo] [int] NULL,
	[SampleNo] [int] NULL,
	[ReportMarrowIndexID] [bigint] IDENTITY(1,1) NOT NULL,
	[ReportFormIndexID] [bigint] NULL,
 CONSTRAINT [PK_ReportMarrowFull_BackUp] PRIMARY KEY CLUSTERED 
(
	[ReportMarrowIndexID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


