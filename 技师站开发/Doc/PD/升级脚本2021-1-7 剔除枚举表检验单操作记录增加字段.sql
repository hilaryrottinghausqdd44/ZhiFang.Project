

--���鵥��Ϣ�������״̬
IF COL_LENGTH('Lis_TestForm', 'FormInfoStatus') IS NULL  
  alter table Lis_TestForm Add FormInfoStatus int null
go

execute sp_addextendedproperty 'MS_Description', '���鵥��Ϣ�������״̬', 'SCHEMA', 'dbo', 'table', 'Lis_TestForm', 'column', 'FormInfoStatus'
go


--������¼���������ݱ���
 IF COL_LENGTH('Lis_Operate', 'DataInfo') IS NULL  
  alter table Lis_Operate Add DataInfo ntext null
 go

 execute sp_addextendedproperty 'MS_Description', '������Ϣ����', 'SCHEMA', 'dbo', 'table', 'Lis_Operate', 'column', 'DataInfo'
go

--�����������������
 IF COL_LENGTH('B_Para', 'ParaMainClassCode') IS NULL  
  alter table B_Para Add ParaMainClassCode nvarchar(100) null
 go

 execute sp_addextendedproperty 'MS_Description', '�����������', 'SCHEMA', 'dbo', 'table', 'B_Para', 'column', 'ParaMainClassCode'
go

--������������������
 IF COL_LENGTH('B_Para', 'ParaMainClassName') IS NULL  
  alter table B_Para Add ParaMainClassName nvarchar(200) null
 go

 execute sp_addextendedproperty 'MS_Description', '������������', 'SCHEMA', 'dbo', 'table', 'B_Para', 'column', 'ParaMainClassName'
go



  --ɾ��������Ϊö�����ͣ����õı�

  --ɾ��LB_BaseType������������
    drop table LB_BaseType
	go
  --ɾ��LB_AgeUnit���䵥λ
    drop table LB_AgeUnit
	go
  --ɾ��LB_Gender�Ա�
    drop table LB_Gender
	go

	--PD�ļ����޴˱���
	drop table B_SectionPara
    go

    drop table B_ParaGroup
    go

    drop table B_ParaGroupItem
    go

    drop table B_HostTypePara
    go

  --ɾ�����û�������Ա���õı�

  --ɾ��LB_Client�ͼ쵥λ
      drop table LB_Client
	  go
  --ɾ��LB_Dept�ͼ����
      drop table LB_Dept
	  go
  --ɾ��LB_DiseaseArea����
      drop table LB_DiseaseArea
	  go
  --ɾ��LB_DiseaseRoom����
      drop table LB_DiseaseRoom
	  go
  --ɾ��LB_Doctorҽ��
      drop table LB_Doctor
	  go
  --ɾ��LB_ExecDeptִ�п���
      drop table LB_ExecDept
	  go
  --ɾ��LB_SuperSection�������
      drop table LB_SuperSection
	  go