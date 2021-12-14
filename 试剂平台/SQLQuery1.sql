USE [ReaDataCenter20151224]
GO

/****** Object:  Table [dbo].[Rea_CenOrg]    Script Date: 2017-09-12 9:57:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Rea_CenOrg](
	[LabID] [dbo].[D_ʵ����ID] NULL,
	[OrgID] [bigint] NOT NULL,
	[OrgNo] [int] NOT NULL,
	[OrgType] [int] NOT NULL,
	[PlatformOrgID] [bigint] NULL,
	[PlatformOrgNo] [int] NULL,
	[CName] [varchar](100) NOT NULL,
	[EName] [varchar](100) NULL,
	[ServerIP] [varchar](100) NULL,
	[ServerPort] [varchar](10) NULL,
	[ShortCode] [varchar](50) NULL,
	[Address] [varchar](100) NULL,
	[Contact] [varchar](100) NULL,
	[Tel] [varchar](500) NULL,
	[Tel1] [varchar](500) NULL,
	[HotTel] [varchar](500) NULL,
	[HotTel1] [varchar](500) NULL,
	[Fox] [varchar](500) NULL,
	[Email] [varchar](50) NULL,
	[WebAddress] [varchar](100) NULL,
	[Memo] [varchar](100) NULL,
	[DispOrder] [int] NULL,
	[Visible] [int] NULL,
	[ZX1] [varchar](100) NULL,
	[ZX2] [varchar](100) NULL,
	[ZX3] [varchar](100) NULL,
	[DataUpdateTime] [datetime] NULL,
	[DataAddTime] [datetime] NULL,
 CONSTRAINT [PK_Rea_CenOrg] PRIMARY KEY CLUSTERED 
(
	[OrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Rea_CenOrg] UNIQUE NONCLUSTERED 
(
	[OrgNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'OrgID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'OrgNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'OrgType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ƽ̨����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'PlatformOrgID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ƽ̨��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'PlatformOrgNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ӣ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'EName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'ServerIP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������˿�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'ServerPort'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'ShortCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'Address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϵ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'Contact'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�绰' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'Tel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�绰1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'Tel1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ߵ绰' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'HotTel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ߵ绰1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'HotTel1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'Fox'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'Email'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'WebAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ע' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'Memo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ʾ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�ʹ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'Visible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ר��1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'ZX1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ר��2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'ZX2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ר��3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg', @level2type=N'COLUMN',@level2name=N'ZX3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_CenOrg'
GO

