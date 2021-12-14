--郭海祥 2019-6-17 
--增加显示列
--增加全局配置 List列表宽度、是否显示页面Header
--增加报告打印操作日志表

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'11', N'医嘱项目名称', N'ZDY3', NULL, N'100', NULL, NULL, NULL, N'1')
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'28', N'页面公共配置', N'allPageType', N'config', N'listWidth', N'550', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'29', N'页面公共配置', N'allPageType', N'config', N'isviewportHeader', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO


CREATE TABLE [dbo].[RFP_ReportFormPrint_Operation](
	[LabID] [bigint] NOT NULL,
	[RFPOperationID] [bigint] NOT NULL,
	[BobjectID] [bigint] NULL,
	[ReceiveDate] [datetime] NULL,
	[SectionNo] [int] NULL,
	[TestTypeNo] [int] NULL,
	[SampleNo] [varchar](20) NULL,
	[Station] [varchar](200) NULL,
	[EmpID] [bigint] NULL,
	[EmpName] [varchar](20) NULL,
	[DeptId] [bigint] NULL,
	[DeptName] [varchar](200) NULL,
	[Type] [bigint] NULL,
	[TypeName] [varchar](20) NULL,
	[BusinessModuleCode] [varchar](20) NULL,
	[Memo] [varchar](500) NULL,
	[DispOrder] [int] NULL,
	[IsUse] [bit] NULL,
	[CreatorID] [bigint] NULL,
	[CreatorName] [varchar](50) NULL,
	[DataAddTime] [datetime] NULL,
	[DataUpdateTime] [datetime] NULL,
	[DataTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_SC_OPERATION] PRIMARY KEY CLUSTERED 
(
	[RFPOperationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
