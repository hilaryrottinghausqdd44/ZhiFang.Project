

if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BReqItemID bigint;

if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BPreForm')) alter table Blood_BPreForm ALTER COLUMN BReqItemID bigint;

if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_AnesthesiaMsg')) alter table Blood_AnesthesiaMsg ALTER COLUMN BReqItemID bigint;