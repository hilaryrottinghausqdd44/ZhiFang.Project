
--����������
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




alter table barcodeform add OrderNo varchar(30)--barcodeform��������"�������"�ֶ�

alter table testitem add CheckMethodName varchar(30) --testitem������"��ⷽ��"�ֶ�

alter table SendOrder add IsConfirm int default 0 --����������"�Ƿ�ȷ��"

