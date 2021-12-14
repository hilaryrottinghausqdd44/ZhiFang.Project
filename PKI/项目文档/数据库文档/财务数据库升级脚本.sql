
USE [Lis_Finance_New]
GO


IF OBJECT_ID ('dbo.SampleSendPlace') IS NULL

begin
    --委托单位基础表
	CREATE TABLE [dbo].[SampleSendPlace](
		[SendPlaceNo] [bigint] NOT NULL,
		[CName] [varchar](100) NULL,
		[EName] [varchar](40) NULL,
		[ShortCode] [varchar](40) NULL,
		[IsUse] [int] NULL CONSTRAINT [DF_SampleSendPlace_IsUse]  DEFAULT ((1)),
		[LinkMan] [varchar](40) NULL,
		[PhoneNum] [varchar](40) NULL,
		[Address] [varchar](40) NULL,
		[MailNo] [varchar](40) NULL,
		[Email] [varchar](40) NULL,
		[Principal] [varchar](40) NULL,
		[PhoneNum1] [varchar](40) NULL,
		[ClientType] [int] NULL,
		[RoMark] [varchar](200) NULL,
	 CONSTRAINT [PK_SampleSendPlace] PRIMARY KEY CLUSTERED 
	(
		[SendPlaceNo] ASC
	) ON [PRIMARY]
	) ON [PRIMARY]
end
GO


--IF OBJECT_ID ('dbo.D_ContractPrice', 'U') IS Not NULL
--begin
--  DROP TABLE dbo.D_ContractPrice
--end
--GO

--IF OBJECT_ID ('D_UnitDealerRelation', 'U') IS Not NULL
--begin
--  DROP TABLE dbo.D_UnitDealerRelation
--end
--GO

IF OBJECT_ID ('[D_UnitDealerRelation]') IS NULL
begin

	CREATE TABLE [dbo].[D_UnitDealerRelation](
		[LabID] [bigint] NOT NULL,
		[UDRelationID] [bigint] NOT NULL,
		[DealerID] [bigint] NULL,
		[SLabID] [bigint] NULL,
		[DeptID] [bigint] NULL,
		[BillingUnitID] [bigint] NULL,
		[ItemID] [bigint] NULL,
		[BillingUnitType] [varchar](10) NULL,
		[CoopLevel] [int] NULL,
		[BeginDate] [datetime] NULL,
		[EndDate] [datetime] NULL,
		[Explain] [varchar](40) NULL,
		[Comment] [ntext] NULL,
		[AddUser] [varchar](50) NULL,
		[AddUserID] [bigint] NULL,
		[AddTime] [datetime] NULL,
		[ConfirmUser] [varchar](50) NULL,
		[ConfirmUserID] [bigint] NULL,
		[ConfirmTime] [datetime] NULL,
		[DataAddTime] [datetime] NULL,
		[DataUpdateTime] [datetime] NULL,
		[DataTimeStamp] [timestamp] NULL,
	 CONSTRAINT [PK_D_UNITDEALERRELATION] PRIMARY KEY CLUSTERED 
	(
		[UDRelationID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


	ALTER TABLE [dbo].[D_UnitDealerRelation]  WITH CHECK ADD  CONSTRAINT [FK_D_UNITDE_REFERENCE_B_BILLIN] FOREIGN KEY([BillingUnitID])
	REFERENCES [dbo].[B_BillingUnit] ([BillingUnitID])


	ALTER TABLE [dbo].[D_UnitDealerRelation] CHECK CONSTRAINT [FK_D_UNITDE_REFERENCE_B_BILLIN]


	ALTER TABLE [dbo].[D_UnitDealerRelation]  WITH CHECK ADD  CONSTRAINT [FK_D_UNITDE_REFERENCE_B_DEALER] FOREIGN KEY([DealerID])
	REFERENCES [dbo].[B_Dealer] ([DealerID])


	ALTER TABLE [dbo].[D_UnitDealerRelation] CHECK CONSTRAINT [FK_D_UNITDE_REFERENCE_B_DEALER]


	ALTER TABLE [dbo].[D_UnitDealerRelation]  WITH CHECK ADD  CONSTRAINT [FK_D_UNITDE_REFERENCE_B_DEPT] FOREIGN KEY([DeptID])
	REFERENCES [dbo].[B_Dept] ([DeptID])


	ALTER TABLE [dbo].[D_UnitDealerRelation] CHECK CONSTRAINT [FK_D_UNITDE_REFERENCE_B_DEPT]


	ALTER TABLE [dbo].[D_UnitDealerRelation]  WITH CHECK ADD  CONSTRAINT [FK_D_UNITDE_REFERENCE_B_LABORA] FOREIGN KEY([SLabID])
	REFERENCES [dbo].[B_Laboratory] ([SLabID])


	ALTER TABLE [dbo].[D_UnitDealerRelation] CHECK CONSTRAINT [FK_D_UNITDE_REFERENCE_B_LABORA]


	ALTER TABLE [dbo].[D_UnitDealerRelation]  WITH CHECK ADD  CONSTRAINT [FK_D_UNITDE_REFERENCE_B_TESTIT] FOREIGN KEY([ItemID])
	REFERENCES [dbo].[B_TestItem] ([ItemID])


	ALTER TABLE [dbo].[D_UnitDealerRelation] CHECK CONSTRAINT [FK_D_UNITDE_REFERENCE_B_TESTIT]


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实验室ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'LabID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关系ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'UDRelationID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经销商ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'DealerID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送检单位ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'SLabID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'DeptID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开票方ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'BillingUnitID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'ItemID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开票方类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'BillingUnitType'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合作级别1送检单位2单位科室' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'CoopLevel'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'BeginDate'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'EndDate'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'Explain'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'Comment'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同录入人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'AddUser'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同录入人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'AddUserID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同录入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'AddTime'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同确认人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'ConfirmUser'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同确认人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'ConfirmUserID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同确认时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'ConfirmTime'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'DataAddTime'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'DataUpdateTime'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送检单位与经销商关系' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_UnitDealerRelation'


end
GO

IF OBJECT_ID ('[D_ContractPrice]') IS NULL
begin
	
	CREATE TABLE [dbo].[D_ContractPrice](
		[LabID] [bigint] NOT NULL,
		[ContractPriceID] [bigint] NOT NULL,
		[DealerID] [bigint] NULL,
		[SLabID] [bigint] NULL,
		[ItemID] [bigint] NULL,
		[BeginDate] [datetime] NULL,
		[EndDate] [datetime] NULL,
		[ContractType] [int] NULL,
		[IsStepPrice] [int] NULL,
		[SampleCount] [int] NULL,
		[StepPrice] [float] NULL,
		[StepPriceMemo] [varchar](200) NULL,
		[ContractNo] [varchar](100) NULL,
		[Explain] [varchar](40) NULL,
		[Comment] [ntext] NULL,
		[AddUser] [varchar](50) NULL,
		[AddUserID] [bigint] NULL,
		[AddTime] [datetime] NULL,
		[ConfirmUser] [varchar](50) NULL,
		[ConfirmUserID] [bigint] NULL,
		[ConfirmTime] [datetime] NULL,
		[DataAddTime] [datetime] NULL,
		[DataUpdateTime] [datetime] NULL,
		[DataTimeStamp] [timestamp] NULL,
	 CONSTRAINT [PK_D_CONTRACTPRICE] PRIMARY KEY CLUSTERED 
	(
		[ContractPriceID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



	ALTER TABLE [dbo].[D_ContractPrice]  WITH CHECK ADD  CONSTRAINT [FK_D_CONTRA_REFERENCE_B_DEALER] FOREIGN KEY([DealerID])
	REFERENCES [dbo].[B_Dealer] ([DealerID])


	ALTER TABLE [dbo].[D_ContractPrice] CHECK CONSTRAINT [FK_D_CONTRA_REFERENCE_B_DEALER]


	ALTER TABLE [dbo].[D_ContractPrice]  WITH CHECK ADD  CONSTRAINT [FK_D_CONTRA_REFERENCE_B_LABORA] FOREIGN KEY([SLabID])
	REFERENCES [dbo].[B_Laboratory] ([SLabID])


	ALTER TABLE [dbo].[D_ContractPrice] CHECK CONSTRAINT [FK_D_CONTRA_REFERENCE_B_LABORA]


	ALTER TABLE [dbo].[D_ContractPrice]  WITH CHECK ADD  CONSTRAINT [FK_D_CONTRA_REFERENCE_B_TESTIT] FOREIGN KEY([ItemID])
	REFERENCES [dbo].[B_TestItem] ([ItemID])


	ALTER TABLE [dbo].[D_ContractPrice] CHECK CONSTRAINT [FK_D_CONTRA_REFERENCE_B_TESTIT]


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实验室ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'LabID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同价格ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'ContractPriceID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经销商ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'DealerID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送检单位ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'SLabID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'ItemID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'BeginDate'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'EndDate'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'ContractType'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否阶梯价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'IsStepPrice'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样本量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'SampleCount'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'阶梯价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'StepPrice'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'阶梯价格说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'StepPriceMemo'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'ContractNo'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'Explain'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'Comment'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'AddUser'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同录入人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'AddUserID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同录入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'AddTime'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同确认人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'ConfirmUser'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同确认人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'ConfirmUserID'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同确认时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'ConfirmTime'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'DataAddTime'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'DataUpdateTime'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'


	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'D_ContractPrice'


end
GO


if not Exists(Select * from SysColumns where [Name]= 'IsGetPrice' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  IsGetPrice [Integer]
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'匹配合同' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'IsGetPrice'
end 
go

if not Exists(Select * from SysColumns where [Name]= 'GetPriceUser' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  GetPriceUser VarChar(50)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'匹配合同人员' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'GetPriceUser'
end 
go

if not Exists(Select * from SysColumns where [Name]= 'GetPriceTime' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  GetPriceTime DateTime
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'匹配合同时间' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'GetPriceTime'
end 
go

if not Exists(Select * from SysColumns where [Name]= 'SampleState' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  SampleState VarChar(50)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样本项目状态' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'SampleState'
end 
go		   

if not Exists(Select * from SysColumns where [Name]= 'LockBatchNumber' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  LockBatchNumber VarChar(50)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对账批号' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'LockBatchNumber'
end 
go

if Exists(Select * from SysColumns where [Name]= 'IsFreeType' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem alter column IsFreeType VarChar(50)
end 
go

if not Exists(Select * from SysColumns where [Name]= 'SendPlaceName' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  SendPlaceName VarChar(100)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'委托单位名称' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'SendPlaceName'
end 
go

if not Exists(Select * from SysColumns where [Name]= 'SendPlaceNo' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  SendPlaceNo Bigint
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'委托单位' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'SendPlaceNo'
end 
go

if not Exists(Select * from SysColumns where [Name]= 'IsReview' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  IsReview int  default 0
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'复查样本标志' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'IsReview'
end 
go

if not Exists(Select * from SysColumns where [Name]= 'ItemCommission' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  ItemCommission float
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目佣金' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'ItemCommission'
end 
go

if not Exists(Select * from SysColumns where [Name]= 'IsFinanceLocked' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  IsFinanceLocked int default 0
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'财务锁定标志' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'IsFinanceLocked'
end 
go

if not Exists(Select * from SysColumns where [Name]= 'DealerType' and ID = (Select [ID] from SysObjects where Name = 'B_Dealer')) 
begin
  alter table B_Dealer add  DealerType int
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经销商类型' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'B_Dealer', @level2type=N'COLUMN',@level2name=N'DealerType'
end 
go

if not Exists(Select * from SysColumns where [Name]= 'DeleteFlag' and ID = (Select [ID] from SysObjects where Name = 'NRequestItem')) 
begin
  alter table NRequestItem add  DeleteFlag int default 0
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', 
       @level1type=N'TABLE',@level1name=N'NRequestItem', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
end 
go





