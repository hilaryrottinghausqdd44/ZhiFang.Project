---郭海祥 创建用户科室对应表

CREATE TABLE [dbo].[EmpDeptLinks](
	[EDLID] [bigint] NOT NULL,
	[UserNo] [bigint] NULL,
	[DeptNo] [bigint] NULL,
	[UserCName] [varchar](50) NULL,
	[ShortCode] [varchar](50) NULL,
	[DeptCName] [varchar](200) NULL,
	[DataAddTime] [datetime] NULL,
	[DataUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_EmpDeptLinks] PRIMARY KEY CLUSTERED 
(
	[EDLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO