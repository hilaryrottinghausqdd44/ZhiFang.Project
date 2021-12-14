IF COL_LENGTH('Blood_BOutForm', 'BDEndBReasonId') IS NULL ALTER TABLE Blood_BOutForm ADD BDEndBReasonId bigint;

if not exists(select * from [B_DictType] where DCId=200) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,200,N'终止输血原因',200,N'终止输血原因',1,N'2020-07-09 15:11:18',N'BDEndBReason');