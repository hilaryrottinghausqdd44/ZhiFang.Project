USE [weblis_dajia20140522]
GO

/****** Object:  Table [dbo].[ReportFormFull_BackUp]    Script Date: 05/04/2015 11:44:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ReportFormFull_BackUp](
	[ReportFormID] [varchar](100) NOT NULL,
	[CLIENTNO] [varchar](50) NULL,
	[CNAME] [varchar](50) NULL,
	[AGEUNITNAME] [varchar](50) NULL,
	[GENDERNAME] [varchar](50) NULL,
	[DEPTNAME] [varchar](50) NULL,
	[DOCTORNAME] [varchar](50) NULL,
	[DISTRICTNAME] [varchar](50) NULL,
	[WARDNAME] [varchar](50) NULL,
	[FOLKNAME] [varchar](50) NULL,
	[SICKTYPENAME] [varchar](50) NULL,
	[SAMPLETYPENAME] [varchar](50) NULL,
	[SECTIONNAME] [varchar](50) NULL,
	[TESTTYPENAME] [varchar](40) NULL,
	[RECEIVEDATE] [datetime] NOT NULL,
	[SECTIONNO] [varchar](50) NOT NULL,
	[TESTTYPENO] [varchar](50) NOT NULL,
	[SAMPLENO] [varchar](150) NULL,
	[STATUSNO] [int] NULL,
	[SAMPLETYPENO] [int] NULL,
	[PATNO] [varchar](50) NULL,
	[GENDERNO] [int] NULL,
	[BIRTHDAY] [datetime] NULL,
	[AGE] [varchar](50) NULL,
	[AGEUNITNO] [int] NULL,
	[FOLKNO] [varchar](50) NULL,
	[DISTRICTNO] [varchar](50) NULL,
	[WARDNO] [varchar](50) NULL,
	[BED] [varchar](50) NULL,
	[DEPTNO] [int] NULL,
	[DOCTOR] [varchar](50) NULL,
	[CHARGENO] [varchar](50) NULL,
	[CHARGE] [varchar](50) NULL,
	[COLLECTER] [varchar](50) NULL,
	[COLLECTDATE] [datetime] NULL,
	[COLLECTTIME] [datetime] NULL,
	[FORMMEMO] [varchar](1000) NULL,
	[TECHNICIAN] [varchar](50) NULL,
	[TESTDATE] [datetime] NULL,
	[TESTTIME] [datetime] NULL,
	[OPERATOR] [varchar](50) NULL,
	[OPERDATE] [datetime] NULL,
	[OPERTIME] [datetime] NULL,
	[CHECKER] [varchar](50) NULL,
	[PRINTTIMES] [int] NULL,
	[resultfile] [varchar](100) NULL,
	[CHECKDATE] [datetime] NULL,
	[CHECKTIME] [datetime] NULL,
	[SERIALNO] [varchar](50) NULL,
	[REQUESTSOURCE] [varchar](50) NULL,
	[DIAGNO] [varchar](50) NULL,
	[SICKTYPENO] [varchar](50) NULL,
	[FORMCOMMENT] [varchar](2000) NULL,
	[ARTIFICERORDER] [varchar](50) NULL,
	[SICKORDER] [varchar](50) NULL,
	[SICKTYPE] [varchar](50) NULL,
	[CHARGEFLAG] [varchar](50) NULL,
	[TESTDEST] [varchar](50) NULL,
	[SLABLE] [varchar](50) NULL,
	[ZDY1] [varchar](50) NULL,
	[ZDY2] [varchar](50) NULL,
	[ZDY3] [varchar](50) NULL,
	[ZDY4] [varchar](50) NULL,
	[ZDY5] [varchar](50) NULL,
	[INCEPTDATE] [datetime] NULL,
	[INCEPTTIME] [datetime] NULL,
	[INCEPTER] [varchar](50) NULL,
	[ONLINEDATE] [datetime] NULL,
	[ONLINETIME] [datetime] NULL,
	[BMANNO] [varchar](50) NULL,
	[FILETYPE] [varchar](50) NULL,
	[JPGFILE] [varchar](500) NULL,
	[PDFFILE] [varchar](500) NULL,
	[FORMNO] [varchar](50) NULL,
	[CHILDTABLENAME] [varchar](100) NULL,
	[PRINTEXEC] [char](4) NOT NULL,
	[LABCENTER] [varchar](50) NULL,
	[CheckName] [varchar](50) NULL,
	[CheckNo] [varchar](50) NULL,
	[CLIENTNAME] [varchar](100) NULL,
	[BARCODE] [nvarchar](50) NULL,
	[PRINTDATETIME] [nvarchar](50) NULL,
	[PRINTTEXEC] [char](4) NOT NULL,
	[UploadDate] [datetime] NULL,
	[isdown] [varchar](2) NULL,
	[SECTIONTYPE] [varchar](50) NULL,
	[SECTIONSHORTNAME] [varchar](50) NULL,
	[SECTIONSHORTCODE] [varchar](50) NULL,
	[DIAGNOSE] [varchar](50) NULL,
	[OldSerialno] [varchar](50) NULL,
	[AreaSendFlag] [int] NOT NULL,
	[AreaSendTime] [datetime] NULL,
	[GenderEname] [varchar](10) NULL,
	[SickEname] [varchar](10) NULL,
	[sampletypeename] [varchar](10) NULL,
	[folkename] [varchar](10) NULL,
	[Deptename] [varchar](10) NULL,
	[districtename] [varchar](10) NULL,
	[AgeUnitename] [varchar](10) NULL,
	[TestType] [varchar](20) NULL,
	[TestTypeename] [varchar](10) NULL,
	[diag] [varchar](40) NULL,
	[clientstyle] [varchar](40) NULL,
	[ClientReportTitle] [varchar](50) NULL,
	[ADDRESS] [varchar](40) NULL,
	[czdy1] [varchar](100) NULL,
	[czdy2] [varchar](100) NULL,
	[czdy3] [varchar](100) NULL,
	[czdy4] [varchar](100) NULL,
	[czdy5] [varchar](100) NULL,
	[czdy6] [varchar](100) NULL,
	[Poperator] [image] NULL,
	[PNOperator] [image] NULL,
	[PSender2] [image] NULL,
	[NOperDate] [datetime] NULL,
	[NOPERTIME] [varchar](102) NULL,
	[clientzdy3] [varchar](102) NULL,
	[ZDY6] [varchar](50) NULL,
	[ZDY7] [varchar](50) NULL,
	[ZDY8] [varchar](50) NULL,
	[ZDY9] [varchar](50) NULL,
	[ZDY10] [varchar](50) NULL,
	[clientename] [varchar](40) NULL,
	[clientcode] [varchar](40) NULL,
	[sectiondesc] [varchar](250) NULL,
	[Ptechnician] [image] NULL,
	[Pincepter] [image] NULL,
	[receivetime] [datetime] NULL,
	[CollectPart] [varchar](50) NULL,
	[WebLisOrgID] [varchar](50) NULL,
	[WebLisSourceOrgID] [varchar](50) NULL,
	[TelNo] [varchar](40) NULL,
	[resultstatus] [varchar](10) NULL,
	[WebLisOrgName] [varchar](150) NULL,
	[WebLisSourceOrgName] [varchar](50) NULL,
	[StatusType] [varchar](50) NULL,
	[PersonID] [varchar](20) NULL,
	[ReportFormIndexID] [bigint] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'ReportFormID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ͼ쵥λ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'CLIENTNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'CNAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���䵥λ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'AGEUNITNAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ա�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'GENDERNAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'DEPTNAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ҽ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'DOCTORNAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'DISTRICTNAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'WARDNAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'FOLKNAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'SICKTYPENAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'SAMPLETYPENAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'С������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'SECTIONNAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'TESTTYPENAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'RECEIVEDATE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'С����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'SECTIONNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'TESTTYPENO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'SAMPLENO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'״̬���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'STATUSNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'SAMPLETYPENO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'PATNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ա����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'GENDERNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'BIRTHDAY'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'AGE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���䵥λ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'AGEUNITNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'FOLKNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'DISTRICTNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'WARDNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'BED'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���Ҵ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'DEPTNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ҽ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'DOCTOR'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�շ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'CHARGENO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'CHARGE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'COLLECTER'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'COLLECTDATE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'COLLECTTIME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ע' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'FORMMEMO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���鼼ʦ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'TECHNICIAN'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'TESTDATE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'TESTTIME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'OPERATOR'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'OPERDATE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'OPERTIME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'CHECKER'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ӡ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'PRINTTIMES'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���浥�ļ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'resultfile'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'CHECKDATE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'CHECKTIME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ҽ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'SERIALNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Դ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'REQUESTSOURCE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'DIAGNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'SICKTYPENO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���鵥����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'FORMCOMMENT'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=NULL , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'SICKTYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�շѱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'CHARGEFLAG'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����Ŀ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'TESTDEST'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Զ���1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'ZDY1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Զ���2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'ZDY2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Զ���3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'ZDY3'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Զ���4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'ZDY4'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Զ���5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'ZDY5'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ǩ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'INCEPTDATE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ǩ��ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'INCEPTTIME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ǩ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'INCEPTER'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ϻ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'ONLINEDATE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ϻ�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'ONLINETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ҵ��Ա' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'BMANNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ļ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'FILETYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ͼƬ�����ļ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'JPGFILE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PDF�����ļ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'PDFFILE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ʱ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'FORMNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ͼ쵥λ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'CHILDTABLENAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ӡ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'PRINTEXEC'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʵ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportFormFull_BackUp', @level2type=N'COLUMN',@level2name=N'LABCENTER'
GO

ALTER TABLE [dbo].[ReportFormFull_BackUp] ADD  CONSTRAINT [DF_ReportFormFull_BackUp1_PrintTimes]  DEFAULT ((0)) FOR [PRINTTIMES]
GO

ALTER TABLE [dbo].[ReportFormFull_BackUp] ADD  CONSTRAINT [DF_ReportFormFull_BackUp_PRINTEXEC]  DEFAULT ('��ӡ') FOR [PRINTEXEC]
GO

ALTER TABLE [dbo].[ReportFormFull_BackUp] ADD  CONSTRAINT [DF_ReportFormFull_BackUp_PRINTTEXEC]  DEFAULT ('�ͻ�') FOR [PRINTTEXEC]
GO

ALTER TABLE [dbo].[ReportFormFull_BackUp] ADD  CONSTRAINT [DF_ReportFormFull_BackUp_UploadDate]  DEFAULT (getdate()) FOR [UploadDate]
GO

ALTER TABLE [dbo].[ReportFormFull_BackUp] ADD  CONSTRAINT [DF_ReportFormFull_BackUp_isdown]  DEFAULT ((0)) FOR [isdown]
GO

ALTER TABLE [dbo].[ReportFormFull_BackUp] ADD  CONSTRAINT [DF__ReportFor__AreaS__31F75A1E]  DEFAULT ((0)) FOR [AreaSendFlag]
GO

ALTER TABLE [dbo].[ReportFormFull_BackUp] ADD  CONSTRAINT [DF__ReportFor__Repor__29820FAE]  DEFAULT ((0)) FOR [ReportFormIndexID]
GO


