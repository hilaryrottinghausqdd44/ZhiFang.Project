
--PUser添加所属医生Id字段;医生帐号关联医生表信息
IF COL_LENGTH('PUser', 'DoctorNo') IS NULL ALTER TABLE PUser ADD DoctorNo int;

--添加部门人员关系表
 IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentUser]') AND type in (N'U')) CREATE TABLE [dbo].[DepartmentUser]( [LabID] [bigint] NULL, [DeptEmpID] [bigint] NOT NULL, [DeptNo] [int] NULL, [UserNo] [int] NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_DepartmentUser] PRIMARY KEY CLUSTERED ( [DeptEmpID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; 

IF COL_LENGTH('Doctor', 'DispOrder') IS NULL ALTER TABLE Doctor ADD DispOrder int;

IF COL_LENGTH('Doctor', 'DataAddTime') IS NULL ALTER TABLE Doctor ADD DataAddTime datetime;

update PUser set DispOrder=UserNo; 

update Doctor set DispOrder=DoctorNo;

update Department set DispOrder=DeptNo;


IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_Template]') AND type in(N'U')) CREATE TABLE [dbo].[B_Template]( [LabID] [bigint] NULL, [TemplateID] [bigint] NOT NULL, [CName] [varchar](100) NULL, [TypeID] [bigint] NULL, [TypeName] [varchar](50) NULL, [FilePath] [varchar](500) NULL, [FileExt] [varchar](50) NULL, [ContentType] [varchar](100) NULL, [FileSize] [float] NULL, [SName] [varchar](80) NULL, [Shortcode] [varchar](80) NULL, [PinYinZiTou] [varchar](50) NULL, [DispOrder] [int] NULL, [Comment] [ntext] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [IsDefault] [bit] NULL, [FileName] [varchar](100) NULL, [ExcelRuleInfo] [ntext] NULL, [TemplateType] [varchar](40) NULL, CONSTRAINT [PK_B_TEMPLATE] PRIMARY KEY CLUSTERED( [TemplateID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];