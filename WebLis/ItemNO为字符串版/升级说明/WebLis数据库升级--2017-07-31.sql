

--1.先删除ItemPrice的FK_ITEMPRIC_TESTITEMC_TESTITEM
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ITEMPRIC_TESTITEMC_TESTITEM]') AND parent_object_id = OBJECT_ID(N'[dbo].[ItemPrice]'))
ALTER TABLE [dbo].[ItemPrice] DROP CONSTRAINT [FK_ITEMPRIC_TESTITEMC_TESTITEM];

--2.手动将ItemPrice的ItemNo修改为varchar(50)

--3.重新创建ItemPrice的FK_ITEMPRIC_TESTITEMC_TESTITEM
ALTER TABLE [dbo].[ItemPrice]  WITH NOCHECK ADD  CONSTRAINT [FK_ITEMPRIC_TESTITEMC_TESTITEM] FOREIGN KEY([ItemNo])
REFERENCES [dbo].[TestItem] ([ItemNo])
GO
ALTER TABLE [dbo].[ItemPrice] CHECK CONSTRAINT [FK_ITEMPRIC_TESTITEMC_TESTITEM]
GO

--GroupItem的PItemNo及ItemNo都修改为varchar(50)
alter table GroupItem alter column PItemNo varchar(50) not null;
alter table GroupItem alter column ItemNo varchar(50) not null;


IF COL_LENGTH('NRequestForm', 'zdy6') IS NULL ALTER TABLE NRequestForm ADD zdy6 varchar(60) null;
IF COL_LENGTH('NRequestForm', 'zdy7') IS NULL ALTER TABLE NRequestForm ADD zdy7 varchar(60) null;
IF COL_LENGTH('NRequestForm', 'zdy8') IS NULL ALTER TABLE NRequestForm ADD zdy8 varchar(60) null;
IF COL_LENGTH('NRequestForm', 'zdy10') IS NULL ALTER TABLE NRequestForm ADD zdy10 varchar(60) null;

--NRequestItem的CombiItemNo修改为varchar(50)
--alter table NRequestItem alter column CombiItemNo varchar(50) null;