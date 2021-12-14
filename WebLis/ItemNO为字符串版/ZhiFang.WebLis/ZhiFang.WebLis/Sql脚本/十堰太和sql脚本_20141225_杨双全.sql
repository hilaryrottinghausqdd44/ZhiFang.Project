----以下是脚本更新，共4个脚本

---1.创建表

CREATE TABLE [dbo].[barCodeSeq](
	[LabCode] [varchar](50) NOT NULL,
	[LastNum] [int] NOT NULL,
	[Date] [date] NULL
) ON [PRIMARY]

GO

---2.增加字段
alter table barcodeform add color varchar(20)--中增加"颜色"字段
 alter table barcodeform add ItemName varchar(200)  --增加"细项项目名称"字段
alter table barcodeform add ItemNo varchar(200) --增加"细项项目编号"字段
 alter table nrequestform add printTimes int  --增加"打印次数"字段
 alter table nrequestform add barcode varchar(200) --增加"条码"字段
 alter table nrequestform add CombiItemName varchar(200) --增加"组合名称"字段
alter table barcodeform add SampleTypeName varchar(200) --增加"样本名称"字段


----存储过程脚本

CREATE procedure [dbo].[P_GetMaxBarCodeSeq]
@LabCode  varchar(50),
@OperDate datetime,
@SN varchar(10) output

as
 declare  
 @returnvalue  varchar(10)

 set @returnvalue='True'
if(@OperDate is null)
set @OperDate= convert(varchar(10),getDate(),111)
else
set @OperDate=CAST(@OperDate as varchar(10))


begin tran
begin
	if((select count(*) from dbo.barCodeSeq where [LabCode]=@LabCode and [Date]=@OperDate)>0)
	 begin
		 update dbo.barCodeSeq  set dbo.barCodeSeq.lastNum= dbo.barCodeSeq.lastNum +1 where dbo.barCodeSeq.labcode=@LabCode and [Date]=@OperDate
		 if @@error<>0
		   set @returnvalue='False'
	   select @SN = right('000'+ cast(LastNum  as varchar) ,3)  from dbo.barCodeSeq where dbo.barCodeSeq.labcode=@LabCode and [Date]=@OperDate
	 end
	else
	begin
	  insert into dbo.barCodeSeq  (labcode,lastNum,[date]) values(@LabCode,1,@OperDate)
	  if @@error<>0
		   set @returnvalue='False'
	  select @SN = right('000'+ cast(LastNum as varchar) ,3)  from dbo.barCodeSeq where dbo.barCodeSeq.labcode=@LabCode and [Date]=@OperDate
	 end
end

if upper(@returnvalue)='TRUE'
  commit
else
  rollback

----创建条码打印参数配置表

CREATE TABLE [dbo].[LocationbarCodePrintPamater](
	[Id] [bigint] NOT NULL,
	[AccountId] [varchar](30) NOT NULL,
	[ParaMeter] [ntext] NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_LocationbarCodePrintPamater] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO




-----新建颜色字典表
CREATE TABLE [dbo].[ItemColorDict](
	[ColorID] [int] IDENTITY(1,1) NOT NULL,
	[ColorName] [varchar](15) NULL,
	[ColorValue] [varchar](15) NULL,
 CONSTRAINT [PK_ColorDict] PRIMARY KEY CLUSTERED 
(
	[ColorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


---颜色字典表 -生成数据脚本

INSERT [ItemColorDict] ([ColorName]) VALUES (N'无色')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES (N'绿色',N'#58B681')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'紫色',N'#4C2953')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES (N'黑色',N'#000000')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'灰色',N'#3E3F44')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'红色',N'#CC0000')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'蓝色',N'#091853')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'黄色',N'#E29F36')


-----新建样本类型字典和颜色字典对照表

CREATE TABLE [dbo].[ItemColorAndSampleTypeDetail](
	[ColorId] [int] NOT NULL,
	[SampleTypeNo] [int] NOT NULL
) ON [PRIMARY]

GO

----样本类型对照表 生成数据脚本

INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 2,6)
INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 2,7)
INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 3,6)
INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 3,7)
INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 4,1)
INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 4,7)
INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 5,6)
INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 6,1)
INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 7,6)
INSERT [ItemColorAndSampleTypeDetail] ([ColorId],[SampleTypeNo]) VALUES ( 8,1)
