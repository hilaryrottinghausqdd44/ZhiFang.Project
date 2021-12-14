

 IF COL_LENGTH('Blood_BReqForm', 'HasAllergy') IS NULL ALTER TABLE Blood_BReqForm ADD HasAllergy bit; 

update Blood_BReqForm set HasAllergy=0 where HasAllergy is null; 

IF COL_LENGTH('Blood_BTestItem', 'IsPreTrransfusionEvaluationItem') IS NULL ALTER TABLE Blood_BTestItem ADD IsPreTrransfusionEvaluationItem bit; 

update Blood_BTestItem set IsPreTrransfusionEvaluationItem=0 where IsPreTrransfusionEvaluationItem is null; 

IF COL_LENGTH('Blood_BReqFormResult', 'IsPreTrransfusionEvaluationItem') IS NULL ALTER TABLE Blood_BReqFormResult ADD IsPreTrransfusionEvaluationItem bit; 

update Blood_BReqFormResult set IsPreTrransfusionEvaluationItem=0 where IsPreTrransfusionEvaluationItem is null; 

 if exists(select 1 from sysobjects where id = object_id('B_UserUIConfig') and type = 'U') drop table B_UserUIConfig create table B_UserUIConfig ( LabID bigint null, UserUIID bigint not null, UserUIKey varchar(100) null, UserUIName varchar(100) null, TemplateTypeID bigint null, TemplateTypeCName varchar(100) null, UITypeID bigint null, UITypeName varchar(100) null, ModuleId bigint null, EmpID bigint null, IsDefault bit null, Comment ntext collate Chinese_PRC_CI_AS null, DispOrder int null, IsUse bit null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_B_USERUICONFIG primary key (UserUIID)); 


