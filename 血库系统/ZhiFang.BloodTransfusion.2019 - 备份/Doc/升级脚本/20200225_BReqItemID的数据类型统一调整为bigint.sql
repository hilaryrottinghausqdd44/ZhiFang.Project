
/***
	һ.���ݿ������ű�����;
	BReqItemID����������ͳһ����Ϊbigint;
	1.Blood_BOutItem(������ϸ)��BReqItemIDΪnvarchar(20);
	2.Blood_BPreForm(��Ѫ����)��BReqItemIDΪnvarchar(20);
	3.Blood_AnesthesiaMsg(������Ϣ�ֵ�)��BReqItemIDΪint;
*/

--1.Blood_BOutItem(������ϸ)��BReqItemID��ԭ����nvarchar(20)����Ϊbigint;
if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BReqItemID bigint;

--2.Blood_BPreForm(��Ѫ����)��BReqItemID��ԭ����nvarchar(20)����Ϊbigint;
if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BPreForm')) alter table Blood_BPreForm ALTER COLUMN BReqItemID bigint;

--2.Blood_BPreForm(������Ϣ�ֵ�)��BReqItemID��ԭ����int����Ϊbigint;
if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_AnesthesiaMsg')) alter table Blood_AnesthesiaMsg ALTER COLUMN BReqItemID bigint;