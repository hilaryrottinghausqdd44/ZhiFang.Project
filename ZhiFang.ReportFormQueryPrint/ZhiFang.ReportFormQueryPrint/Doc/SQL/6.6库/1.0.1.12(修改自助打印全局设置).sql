--郭海祥 2019-11-13
--修改自助打印全局设置  
  
  
  
  update [dbo].[B_Parameter] set ParaValue = '46' where ParaNo = 'tabgridLeft' and SName = '自助打印'
  update [dbo].[B_Parameter] set ParaValue = '46' where ParaNo = 'tabgridLeft' and SName = 'selfhelp'
  
  update [dbo].[B_Parameter] set ParaValue = '48' where ParaNo = 'closeJianpanLeft' and SName = '自助打印'
  update [dbo].[B_Parameter] set ParaValue = '48' where ParaNo = 'closeJianpanLeft' and SName = 'selfhelp'