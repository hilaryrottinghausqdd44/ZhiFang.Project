USE [ReaDataCenter20151224]
GO

/****** Object:  Table [dbo].[GoodsQualification]    Script Date: 2017-07-14 17:48:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GoodsQualification](
	[LabID] [bigint] NOT NULL,
	[RegisterID] [bigint] NOT NULL,
	[CenOrgID] [bigint] NULL,
	[CenOrgCName] [varchar](200) NULL,
	[CompID] [bigint] NULL,
	[CompCName] [varchar](200) NULL,
	[CName] [varchar](200) NULL,
	[RegisterNo] [varchar](200) NULL,
	[RegisterDate] [datetime] NULL,
	[RegisterInvalidDate] [datetime] NULL,
	[ClassType] [int] NULL,
	[FileName] [varchar](200) NULL,
	[FileExt] [varchar](50) NULL,
	[FileType] [varchar](200) NULL,
	[RegisterFilePath] [varchar](500) NULL,
	[DispOrder] [int] NULL,
	[Visible] [int] NULL,
	[AuthorizeCName] [varchar](50) NULL,
	[Telephone] [varchar](20) NULL,
	[Memo] [varchar](500) NULL,
	[DataAddTime] [datetime] NULL,
	[DataUpdateTime] [datetime] NULL,
	[DataTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_GoodsQualification] PRIMARY KEY CLUSTERED 
(
	[RegisterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע��֤ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'RegisterID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ҽԺ(����������Ϣ�����ID)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'CenOrgID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'CenOrgCName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ӧ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'CompID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ӧ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'CompCName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע��֤���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'RegisterNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'RegisterDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע��֤��Ч��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'RegisterInvalidDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� 1:����;2:��Ȩ��;3:��Ʒ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'ClassType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ļ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'FileType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ע���ļ�·��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'RegisterFilePath'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ʾ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�ʹ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'Visible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ȩ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'AuthorizeCName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϵ�绰' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'Telephone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ݼ���ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ݸ���ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'DataUpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʱ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����֤�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GoodsQualification'
GO


