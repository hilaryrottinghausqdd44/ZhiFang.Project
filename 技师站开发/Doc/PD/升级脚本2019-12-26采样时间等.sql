
--含着就诊信息增加是否确认标记，已经确认的不允许修改   
  alter table dbo.Lis_Patient add bConfirm bit

  --增加采样时间和签收时间
  alter Table Lis_TestForm add CollectTime datetime    null
  alter Table Lis_TestForm add InceptTime datetime    null

  --增加采样时间和签收时间
  alter Table Lis_BarCodeForm add CollectTime datetime    null
  alter Table Lis_BarCodeForm add InceptTime datetime    null

  --失控审核人为bigint
  alter Table  Lis_QCdata Alter Column LoseExaminerID bigint