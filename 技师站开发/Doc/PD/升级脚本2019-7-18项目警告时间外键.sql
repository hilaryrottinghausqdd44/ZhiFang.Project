--��Ŀ����ʱ����ĿID���
ALTER TABLE dbo.LB_ItemTimeW  WITH CHECK ADD  CONSTRAINT FK_LB_ITEMTimeW_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
GO
