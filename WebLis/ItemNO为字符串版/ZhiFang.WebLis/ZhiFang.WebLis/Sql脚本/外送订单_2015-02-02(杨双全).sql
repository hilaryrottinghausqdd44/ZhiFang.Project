
--创建订单表
CREATE TABLE [dbo].[SendOrder](
	[OrderNo] [varchar](30) NOT NULL,
	[CreateMan] [varchar](20) NULL,
	[CreateDate] [date] NULL,
	[SampleNum] [int] NULL,
	[Status] [int] NULL,
	[Note] [varchar](50) NULL,
	[LabCode] [varchar](25) NULL,
 CONSTRAINT [PK_SendOrder] PRIMARY KEY CLUSTERED 
(
	[OrderNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO




alter table barcodeform add OrderNo varchar(30)--barcodeform表中增加"订单编号"字段

alter table testitem add CheckMethodName varchar(30) --testitem中增加"检测方法"字段

alter table SendOrder add IsConfirm int default 0 --订单表增加"是否确认"

