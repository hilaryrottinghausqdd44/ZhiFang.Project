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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע��֤ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID(����������Ϣ��)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'CenOrgID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'CenOrgNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʒ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'GoodsNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʒ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ƷӢ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'EName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʒ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'ShortCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʒ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'GoodsLotNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע��֤���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע��֤��Ч��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterInvalidDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע���ļ�·��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'RegisterFilePath'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ʾ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�ʹ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'Visible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ר��1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'ZX1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ר��2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'ZX2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ר��3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'ZX3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'EmpID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'EmpName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ݼ���ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ݸ���ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'DataUpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʱ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʒע��֤��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsRegister'
GO


