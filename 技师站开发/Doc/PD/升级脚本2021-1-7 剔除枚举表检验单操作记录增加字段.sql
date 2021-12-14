

--检验单信息基本完成状态
IF COL_LENGTH('Lis_TestForm', 'FormInfoStatus') IS NULL  
  alter table Lis_TestForm Add FormInfoStatus int null
go

execute sp_addextendedproperty 'MS_Description', '检验单信息基本完成状态', 'SCHEMA', 'dbo', 'table', 'Lis_TestForm', 'column', 'FormInfoStatus'
go


--操作记录表，操作数据备份
 IF COL_LENGTH('Lis_Operate', 'DataInfo') IS NULL  
  alter table Lis_Operate Add DataInfo ntext null
 go

 execute sp_addextendedproperty 'MS_Description', '数据信息备份', 'SCHEMA', 'dbo', 'table', 'Lis_Operate', 'column', 'DataInfo'
go

--参数表，参数大类代码
 IF COL_LENGTH('B_Para', 'ParaMainClassCode') IS NULL  
  alter table B_Para Add ParaMainClassCode nvarchar(100) null
 go

 execute sp_addextendedproperty 'MS_Description', '参数大类代码', 'SCHEMA', 'dbo', 'table', 'B_Para', 'column', 'ParaMainClassCode'
go

--参数表，参数大类名称
 IF COL_LENGTH('B_Para', 'ParaMainClassName') IS NULL  
  alter table B_Para Add ParaMainClassName nvarchar(200) null
 go

 execute sp_addextendedproperty 'MS_Description', '参数大类名称', 'SCHEMA', 'dbo', 'table', 'B_Para', 'column', 'ParaMainClassName'
go



  --删除调整过为枚举类型，不用的表

  --删除LB_BaseType基础数据类型
    drop table LB_BaseType
	go
  --删除LB_AgeUnit年龄单位
    drop table LB_AgeUnit
	go
  --删除LB_Gender性别
    drop table LB_Gender
	go

	--PD文件中无此表定义
	drop table B_SectionPara
    go

    drop table B_ParaGroup
    go

    drop table B_ParaGroupItem
    go

    drop table B_HostTypePara
    go

  --删除采用机构和人员不用的表

  --删除LB_Client送检单位
      drop table LB_Client
	  go
  --删除LB_Dept送检科室
      drop table LB_Dept
	  go
  --删除LB_DiseaseArea病区
      drop table LB_DiseaseArea
	  go
  --删除LB_DiseaseRoom病房
      drop table LB_DiseaseRoom
	  go
  --删除LB_Doctor医生
      drop table LB_Doctor
	  go
  --删除LB_ExecDept执行科室
      drop table LB_ExecDept
	  go
  --删除LB_SuperSection检验大组
      drop table LB_SuperSection
	  go