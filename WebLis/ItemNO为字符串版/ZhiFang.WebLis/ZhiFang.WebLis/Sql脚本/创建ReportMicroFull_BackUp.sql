USE [weblis_dajia20140522]
GO

/****** Object:  Table [dbo].[ReportMicroFull_BackUp]    Script Date: 05/04/2015 11:49:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ReportMicroFull_BackUp](
	[ReportFormID] [varchar](50) NOT NULL,
	[ReportItemID] [int] NOT NULL,
	[ResultNo] [int] NOT NULL,
	[ItemNo] [int] NULL,
	[ItemName] [varchar](200) NULL,
	[DescNo] [int] NULL,
	[DescName] [varchar](250) NULL,
	[MicroNo] [int] NULL,
	[MicroDesc] [varchar](200) NULL,
	[MicroName] [varchar](200) NULL,
	[AntiNo] [int] NULL,
	[AntiName] [varchar](200) NULL,
	[Suscept] [varchar](50) NULL,
	[SusQuan] [float] NULL,
	[RefRange] [varchar](200) NULL,
	[SusDesc] [varchar](50) NULL,
	[AntiUnit] [varchar](50) NULL,
	[ItemDate] [datetime] NULL,
	[ItemTime] [datetime] NULL,
	[ItemDesc] [varchar](200) NULL,
	[EquipNo] [int] NULL,
	[Modified] [int] NULL,
	[IsMatch] [int] NULL,
	[CheckType] [int] NULL,
	[SerialNo] [varchar](30) NULL,
	[FormNo] [int] NOT NULL,
	[isReceive] [int] NULL,
	[microCountDesc] [varchar](200) NULL,
	[mresulttType] [int] NULL,
	[itemename] [varchar](40) NULL,
	[Microename] [varchar](40) NULL,
	[Antiename] [varchar](40) NULL,
	[Antishortname] [varchar](10) NULL,
	[antishortcode] [varchar](10) NULL,
	[Barcode] [varchar](20) NULL,
	[ReceiveDate] [datetime] NULL,
	[SectionNo] [int] NULL,
	[TestTypeNo] [int] NULL,
	[SampleNo] [varchar](20) NULL,
	[EquipName] [varchar](40) NULL,
	[ReportMicroIndexID] [bigint] NOT NULL,
	[ReportFormIndexID] [bigint] NULL,
 CONSTRAINT [PK_ReportMicroFull_BackUp] PRIMARY KEY CLUSTERED 
(
	[ReportMicroIndexID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ReportMicroFull_BackUp] ADD  DEFAULT ((0)) FOR [ReportMicroIndexID]
GO


