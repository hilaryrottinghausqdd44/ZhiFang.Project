----�����ǽű����£���4���ű�

---1.������

CREATE TABLE [dbo].[barCodeSeq](
	[LabCode] [varchar](50) NOT NULL,
	[LastNum] [int] NOT NULL,
	[Date] [date] NULL
) ON [PRIMARY]

GO

---2.�����ֶ�
alter table barcodeform add color varchar(20)--������"��ɫ"�ֶ�
 alter table barcodeform add ItemName varchar(200)  --����"ϸ����Ŀ����"�ֶ�
alter table barcodeform add ItemNo varchar(200) --����"ϸ����Ŀ���"�ֶ�
 alter table nrequestform add printTimes int  --����"��ӡ����"�ֶ�
 alter table nrequestform add barcode varchar(200) --����"����"�ֶ�
 alter table nrequestform add CombiItemName varchar(200) --����"�������"�ֶ�
alter table barcodeform add SampleTypeName varchar(200) --����"��������"�ֶ�


----�洢���̽ű�

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

----���������ӡ�������ñ�

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




-----�½���ɫ�ֵ��
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


---��ɫ�ֵ�� -�������ݽű�

INSERT [ItemColorDict] ([ColorName]) VALUES (N'��ɫ')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES (N'��ɫ',N'#58B681')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'��ɫ',N'#4C2953')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES (N'��ɫ',N'#000000')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'��ɫ',N'#3E3F44')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'��ɫ',N'#CC0000')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'��ɫ',N'#091853')
INSERT [ItemColorDict] ([ColorName],[ColorValue]) VALUES ( N'��ɫ',N'#E29F36')


-----�½����������ֵ����ɫ�ֵ���ձ�

CREATE TABLE [dbo].[ItemColorAndSampleTypeDetail](
	[ColorId] [int] NOT NULL,
	[SampleTypeNo] [int] NOT NULL
) ON [PRIMARY]

GO

----�������Ͷ��ձ� �������ݽű�

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
