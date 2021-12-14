USE [ReaDataCenter20151224]
GO

/****** Object:  Table [dbo].[GoodsRegister]    Script Date: 2017-05-19 16:03:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GoodsRegister](
	[LabID] [bigint] NOT NULL,
	[RegisterID] [bigint] NOT NULL,
	[CenOrgID] [bigint] NULL,
	[CenOrgNo] [varchar](50) NULL,
	[GoodsNo] [varchar](50) NULL,
	[CName] [varchar](200) NULL,
	[EName] [varchar](200) NULL,
	[ShortCode] [varchar](100) NULL,
	[GoodsLotNo] [varchar](200) NULL,
	[RegisterNo] [varchar](200) NULL,
	[RegisterDate] [datetime] NULL,
	[RegisterInvalidDate] [datetime] NULL,
	[RegisterFilePath] [varchar](500) NULL,
	[DispOrder] [int] NULL,
	[Visible] [int] NULL,
	[ZX1] [varchar](100) NULL,
	[ZX2] [varchar](100) NULL,
	[ZX3] [varchar](100) NULL,
	[EmpID] [bigint] NULL,
	[EmpName] [varchar](50) NULL,
	[DataAddTime] [datetime] NULL,
	[DataUpdateTime] [datetime] NULL,
	[DataTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_GoodsRegister] PRIMARY KEY CLUSTERED 
(
	[RegisterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实验室ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注册证ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构ID(关联机构信息表)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'CenOrgID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'CenOrgNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'GoodsNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品中文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品英文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'EName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'ShortCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品批号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'GoodsLotNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注册证编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注册日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注册证有效期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterInvalidDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注册文件路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterFilePath'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'Visible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'专项1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'ZX1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'专项2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'ZX2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'专项3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'ZX3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'EmpID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'EmpName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据加入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'DataUpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品注册证表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister'
GO


