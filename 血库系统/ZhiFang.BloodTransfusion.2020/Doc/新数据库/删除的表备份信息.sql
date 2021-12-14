USE [digitlab_Blood_2020]
GO

/****** Object:  Table [dbo].[Blood_BReqType]    Script Date: 06/28/2020 09:13:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Blood_BReqType]') AND type in (N'U'))
DROP TABLE [dbo].[Blood_BReqType]
GO


/****** Object:  Table [dbo].[Blood_BReqType]    Script Date: 06/28/2020 09:13:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Blood_BReqType](
	[LabID] [bigint] NULL,
	[BReqTypeID] [bigint] NOT NULL,
	[CName] [nvarchar](20) NULL,
	[SName] [varchar](80) NULL,
	[ShortCode] [varchar](40) NULL,
	[PinYinZiTou] [varchar](50) NULL,
	[DispOrder] [int] NULL,
	[IsUse] [bit] NULL,
	[DataAddTime] [datetime] NULL,
	[DataTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Blood_BReqTypeItem] PRIMARY KEY NONCLUSTERED 
(
	[BReqTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LabID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请类型Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'BReqTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'SName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快捷码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'ShortCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拼音字头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'PinYinZiTou'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'IsUse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BReqType'
GO




/****** Object:  Table [dbo].[Blood_UseType]    Script Date: 06/28/2020 09:16:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Blood_UseType]') AND type in (N'U'))
DROP TABLE [dbo].[Blood_UseType]
GO

/****** Object:  Table [dbo].[Blood_UseType]    Script Date: 06/28/2020 09:16:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Blood_UseType](
	[LabID] [bigint] NULL,
	[UsetypeID] [bigint] NOT NULL,
	[CName] [nvarchar](50) NULL,
	[SName] [varchar](50) NULL,
	[ShortCode] [nvarchar](20) NULL,
	[PinYinZiTou] [varchar](50) NULL,
	[BeforTime] [float] NULL,
	[BeforUnit] [nvarchar](20) NULL,
	[IsUse] [bit] NULL,
	[DispOrder] [int] NULL,
	[DataAddTime] [datetime] NULL,
	[DataTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Blood_UseType] PRIMARY KEY NONCLUSTERED 
(
	[UsetypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LabID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用血类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'UsetypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用血类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'SName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快捷码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'ShortCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拼音字头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'PinYinZiTou'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提前申请时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'BeforTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'BeforUnit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'IsUse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用血类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_UseType'
GO



/****** Object:  Table [dbo].[Blood_IceBox]    Script Date: 06/28/2020 09:17:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Blood_IceBox]') AND type in (N'U'))
DROP TABLE [dbo].[Blood_IceBox]
GO


/****** Object:  Table [dbo].[Blood_IceBox]    Script Date: 06/28/2020 09:17:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Blood_IceBox](
	[LabID] [bigint] NULL,
	[IceboxNo] [bigint] NOT NULL,
	[IceboxName] [nvarchar](50) NULL,
	[ShortCode] [nvarchar](20) NULL,
	[SName] [varchar](80) NULL,
	[PinYinZiTou] [varchar](50) NULL,
	[IsUse] [bit] NULL,
	[DispOrder] [int] NULL,
	[DataAddTime] [datetime] NULL,
	[DataTimeStamp] [timestamp] NULL,
	[Memo] [ntext] NULL,
 CONSTRAINT [PK_Blood_icebox] PRIMARY KEY CLUSTERED 
(
	[IceboxNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构实验室编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'冰箱编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'IceboxNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'冰箱名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'IceboxName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快捷码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'ShortCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'SName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拼音字头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'PinYinZiTou'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'IsUse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox', @level2type=N'COLUMN',@level2name=N'Memo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'冰箱信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_IceBox'
GO




/****** Object:  Table [dbo].[Blood_BPreWay]    Script Date: 06/28/2020 09:41:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Blood_BPreWay]') AND type in (N'U'))
DROP TABLE [dbo].[Blood_BPreWay]
GO


/****** Object:  Table [dbo].[Blood_BPreWay]    Script Date: 06/28/2020 09:41:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Blood_BPreWay](
	[LabID] [bigint] NULL,
	[BPreWayNo] [bigint] NOT NULL,
	[BPreWayName] [nvarchar](50) NULL,
	[SName] [varchar](80) NULL,
	[ShortCode] [varchar](10) NULL,
	[PinYinZiTou] [varchar](50) NULL,
	[IsUse] [bit] NULL,
	[DispOrder] [int] NULL,
	[DataAddTime] [datetime] NULL,
	[DataTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_Blood_BPreWay] PRIMARY KEY NONCLUSTERED 
(
	[BPreWayNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LabID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配血方法编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'BPreWayNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配血方法' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'BPreWayName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'SName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快捷码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'ShortCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拼音字头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'PinYinZiTou'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'IsUse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配血方法' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blood_BPreWay'
GO




