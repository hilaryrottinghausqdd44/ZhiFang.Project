---郭海祥 2019-10-22
---增加全局配置和日志表
INSERT INTO [dbo].[B_Parameter]  VALUES (N'33', N'页面公共配置', N'allPageType', N'config', N'IsHistory', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'34', N'页面公共配置', N'allPageType', N'config', N'IsBackups', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

CREATE TABLE [dbo].[SC_Operation](
	[LabID] [bigint] NULL,
	[SCOperationID] [bigint] NOT NULL,
	[BobjectID] [bigint] NOT NULL,
	[Type] [bigint] NULL,
	[Memo] [varchar](500) NULL,
	[DispOrder] [int] NULL,
	[IsUse] [bit] NULL,
	[CreatorID] [bigint] NULL,
	[CreatorName] [varchar](50) NULL,
	[DataAddTime] [datetime] NULL,
	[DataUpdateTime] [datetime] NULL,
	[DataTimeStamp] [timestamp] NULL,
	[TypeName] [varchar](50) NULL,
	[BusinessModuleCode] [varchar](50) NULL,
 CONSTRAINT [PK_SC_OPERATION_log] PRIMARY KEY CLUSTERED 
(
	[SCOperationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



---试图Reportformquerydatasource增加字段zdy15
ALTER VIEW [dbo].[ReportFormQueryDataSource]
AS
SELECT
	ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, SectionName, TestTypeName, SampleTypeNo, SampletypeName, 
	SecretType, PatNo, CName, InpatientNo, PatCardNo, GenderNo, GenderName, Age, AgeUnitNo, AgeUnitName, Birthday, DistrictNo, DistrictName, 
	WardNo, WardName, Bed, DeptNo, DeptName, Doctor, SerialNo, ParitemName, Collecter, CollectDate, CollectTime, Incepter, InceptDate, InceptTime, 
	Technician, TestDate, TestTime, Operator, OperDate, OperTime, Checker, CheckDate, CheckTime, FormComment, FormMemo, SickTypeNo, 
	SickTypeName, DiagNo, DiagName, ClientNo, ClientName, Sender2, PrintTimes, ClientPrint, PrintOper, PrintDateTime, PrintOper1, PrintDateTime1, 
	resultsend, reportsend, PageName, PageCount, ZDY1, ZDY2, ZDY3, ZDY4, ZDY5, ZDY6, ZDY7, ZDY8, ReportFormID AS formno, 
	SecretType AS SectionType, LabID, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp, MainTesterId, PatientID, ExaminerId, 
	CollectPart, ReportPublicationID AS ReportFormID, ActiveFlag, DoctorItemName AS ItemName,'' as ZDY15
FROM dbo.ReportFormFull
WHERE (ActiveFlag = 1)

GO