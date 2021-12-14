

 ---�������� BmsCenOrderDoc
if Not Exists(Select * from SysColumns where [Name]= 'Sender' and ID = (Select [ID] from SysObjects where Name = 'BmsCenOrderDoc'))
begin 
  Alter Table BmsCenOrderDoc Add Sender varchar(50) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ͻ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenOrderDoc', @level2type=N'COLUMN',@level2name=N'Sender'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'SendTime' and ID = (Select [ID] from SysObjects where Name = 'BmsCenOrderDoc'))
begin 
  Alter Table BmsCenOrderDoc Add SendTime datetime 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ͻ�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenOrderDoc', @level2type=N'COLUMN',@level2name=N'SendTime'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'IsThirdFlag' and ID = (Select [ID] from SysObjects where Name = 'BmsCenOrderDoc'))
begin 
  Alter Table BmsCenOrderDoc Add IsThirdFlag int 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�д�������ϵͳ��־' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenOrderDoc', @level2type=N'COLUMN',@level2name=N'IsThirdFlag'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'DeleteFlag' and ID = (Select [ID] from SysObjects where Name = 'BmsCenOrderDoc'))
begin 
  Alter Table BmsCenOrderDoc Add DeleteFlag int 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ɾ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenOrderDoc', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
end
GO

 ---��������ϸ�� BmsCenOrderDtl
if Not Exists(Select * from SysColumns where [Name]= 'SumTotal' and ID = (Select [ID] from SysObjects where Name = 'BmsCenOrderDtl'))
begin 
  Alter Table BmsCenOrderDtl Add SumTotal float 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ܼƽ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenOrderDtl', @level2type=N'COLUMN',@level2name=N'SumTotal'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'DeleteFlag' and ID = (Select [ID] from SysObjects where Name = 'BmsCenOrderDtl'))
begin 
  Alter Table BmsCenOrderDtl Add DeleteFlag int 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ɾ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenOrderDtl', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'Memo' and ID = (Select [ID] from SysObjects where Name = 'BmsCenOrderDtl'))
begin 
  Alter Table BmsCenOrderDtl Add Memo varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ע' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenOrderDtl', @level2type=N'COLUMN',@level2name=N'Memo'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'LabMemo' and ID = (Select [ID] from SysObjects where Name = 'BmsCenOrderDtl'))
begin 
  Alter Table BmsCenOrderDtl Add LabMemo varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������ע' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenOrderDtl', @level2type=N'COLUMN',@level2name=N'LabMemo'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'CompMemo' and ID = (Select [ID] from SysObjects where Name = 'BmsCenOrderDtl'))
begin 
  Alter Table BmsCenOrderDtl Add CompMemo varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������ע' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenOrderDtl', @level2type=N'COLUMN',@level2name=N'CompMemo'
end
GO
 ---�������� BmsCenSaleDoc
if Not Exists(Select * from SysColumns where [Name]= 'LabAddress' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add LabAddress varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ���ҵ�ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'LabAddress'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'LabContact' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add  LabContact	varchar(100)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ������ϵ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'LabContact'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'LabTel' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add LabTel varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ���ҵ绰' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'LabTel'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'LabHotTel' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add LabHotTel varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ�������ߵ绰' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'LabHotTel'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'LabFox' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add LabFox varchar(500) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ���Ҵ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'LabFox'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'LabEmail' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add LabEmail varchar(50) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'LabEmail'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'LabWebAddress' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add LabWebAddress varchar(100) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ������ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'LabWebAddress'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'CompAddress' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add CompAddress varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����̵�ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'CompAddress'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'CompContact' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add  CompContact varchar(100) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������ϵ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'CompContact'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'CompTel' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add CompTel varchar(500) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����̵绰' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'CompTel'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'CompHotTel' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add CompHotTel varchar(500) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������ߵ绰' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'CompHotTel'
end																	
GO

if Not Exists(Select * from SysColumns where [Name]= 'CompFox' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add CompFox varchar(500) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����̴���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'CompFox'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'CompEmail' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add CompEmail varchar(50) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'CompEmail'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'CompWebAddress' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add CompWebAddress varchar(100) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'CompWebAddress'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'CheckerID' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add CheckerID bigint 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'CheckerID'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'Checker' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add Checker varchar(100) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'Checker'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'CheckTime' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add CheckTime Datetime
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'CheckTime'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'Sender' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add Sender varchar(100)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ͻ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'Sender'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'Receiver' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add Receiver varchar(100)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ǩ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'Receiver'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'InvoiceReceiver' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add InvoiceReceiver varchar(100) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊǩ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'InvoiceReceiver'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'ReceiveTime' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add ReceiveTime datetime
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ǩ��ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'ReceiveTime'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'SendOutTime' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add SendOutTime datetime
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'SendOutTime'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'DeleteFlag' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add DeleteFlag int 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ɾ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'SecAccepterTime' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDoc'))
begin 
  Alter Table BmsCenSaleDoc Add SecAccepterTime Datetime
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDoc', @level2type=N'COLUMN',@level2name=N'SecAccepterTime'
end
GO


 ---��������ϸ�� BmsCenSaleDtl
if Not Exists(Select * from SysColumns where [Name]= 'BarCodeMgr' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDtl'))
begin 
  Alter Table BmsCenSaleDtl Add BarCodeMgr int default 0
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDtl', @level2type=N'COLUMN',@level2name=N'BarCodeMgr'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'ApproveDocNo' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDtl'))
begin 
  Alter Table BmsCenSaleDtl Add ApproveDocNo varchar(200)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��׼�ĺ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDtl', @level2type=N'COLUMN',@level2name=N'ApproveDocNo'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'StorageType' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDtl'))
begin 
  Alter Table BmsCenSaleDtl Add StorageType varchar(200)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDtl', @level2type=N'COLUMN',@level2name=N'StorageType'
end
GO
if Not Exists(Select * from SysColumns where [Name]= 'DeleteFlag' and ID = (Select [ID] from SysObjects where Name = 'BmsCenSaleDtl'))
begin 
  Alter Table BmsCenSaleDtl Add DeleteFlag int 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ɾ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BmsCenSaleDtl', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
end
GO


---��Ʒ���Լ���Goods
if Not Exists(Select * from SysColumns where [Name]= 'BarCodeMgr' and ID = (Select [ID] from SysObjects where Name = 'Goods'))
begin 
  Alter Table Goods Add BarCodeMgr int default 0
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'BarCodeMgr'
end
GO


if Not Exists(Select * from SysColumns where [Name]= 'IsRegister' and ID = (Select [ID] from SysObjects where Name = 'Goods'))
begin 
  Alter Table Goods Add IsRegister int default 0
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ���ע��֤' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'IsRegister'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'IsPrintBarCode' and ID = (Select [ID] from SysObjects where Name = 'Goods'))
begin 
  Alter Table Goods Add IsPrintBarCode int default 0
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ��ӡ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'IsPrintBarCode'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'SuitableType' and ID = (Select [ID] from SysObjects where Name = 'Goods'))
begin 
  Alter Table Goods Add SuitableType varchar(50)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���û���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'SuitableType'
end
GO

---������ CenOrg
if Not Exists(Select * from SysColumns where [Name]= 'Tel1' and ID = (Select [ID] from SysObjects where Name = 'CenOrg'))
begin 
  Alter Table CenOrg Add Tel1 varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�绰1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CenOrg', @level2type=N'COLUMN',@level2name=N'Tel1'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'HotTel' and ID = (Select [ID] from SysObjects where Name = 'CenOrg'))
begin 
  Alter Table CenOrg Add HotTel varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ߵ绰' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CenOrg', @level2type=N'COLUMN',@level2name=N'HotTel'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'HotTel1' and ID = (Select [ID] from SysObjects where Name = 'CenOrg'))
begin 
  Alter Table CenOrg Add HotTel1 varchar(500)
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ߵ绰1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CenOrg', @level2type=N'COLUMN',@level2name=N'HotTel1'
end
GO

if Not Exists(Select * from SysColumns where [Name]= 'DataAddTime' and ID = (Select [ID] from SysObjects where Name = 'CenOrg'))
begin 
  Alter Table CenOrg Add DataAddTime datetime
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CenOrg', @level2type=N'COLUMN',@level2name=N'DataAddTime'
end
GO

---������ϵ�� CenOrgCondition
if Not Exists(Select * from SysColumns where [Name]= 'CustomerAccount' and ID = (Select [ID] from SysObjects where Name = 'CenOrgCondition'))
begin 
  Alter Table CenOrgCondition Add CustomerAccount varchar(100) 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ�����ڹ�Ӧ��ϵͳ�е��˻�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CenOrgCondition', @level2type=N'COLUMN',@level2name=N'CustomerAccount'
end
GO