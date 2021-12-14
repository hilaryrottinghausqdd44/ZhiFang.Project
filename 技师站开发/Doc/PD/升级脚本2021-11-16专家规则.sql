
--2021-11-16 ר�ҹ���

create table dbo.LB_ExpertRule (
   LabID                bigint               not null,
   ExpertRuleID         bigint               not null,
   CName                nvarchar(400)        null,
   GName                nvarchar(200)        null,
   ResultType           int                  null,
   RuleRelation         int                  null,
   SectionID            bigint               not null,
   ItemID               bigint               null,
   ItemAlarmInfo        nvarchar(50)         null,
   AlarmLevel           int                  null,
   AlarmInfo            nvarchar(50)         null,
   ConditionName        nvarchar(200)        null,
   GenderID             bigint               null,
   EquipID              bigint               null,
   DeptID               bigint               null,
   SampleTypeID         bigint               null,
   LowAge               float                null,
   HighAge              float                null,
   AgeUnitID            bigint               null,
   bCollectTime         datetime             null,
   eCollectTime         datetime             null,
   DiagID               bigint               null,
   PhyPeriodID          bigint               null,
   CollectPartID        bigint               null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_BT_ExpertRule primary key (ExpertRuleID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ר�ҹ���ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ExpertRuleID'
go

execute sp_addextendedproperty 'MS_Description', 
   'ר�ҹ�������',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'CName'
go

execute sp_addextendedproperty 'MS_Description', 
   'ר�ҹ���������(���ڹ��򻥳�=or��ϵ)',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'GName'
go

execute sp_addextendedproperty 'MS_Description', 
   '0����������Ĭ�ϣ� 1��΢������',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ResultType'
go

execute sp_addextendedproperty 'MS_Description', 
   '0��ȫ������(and)
    1������(or)',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'RuleRelation'
go

execute sp_addextendedproperty 'MS_Description', 
   'С��ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'SectionID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ĿID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ItemID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ʾ��ʾ',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ItemAlarmInfo'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ʾ���� ö�٣�0������ 1����ʾ 2������ 3�����ؾ��� 4��Σ�� 5������',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'AlarmLevel'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ʾ��ʾ',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'AlarmInfo'
go


execute sp_addextendedproperty 'MS_Description', 
   '����˵��',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ConditionName'
go


execute sp_addextendedproperty 'MS_Description', 
   '�Ա�ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'GenderID'
go


execute sp_addextendedproperty 'MS_Description', 
   '��������ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'EquipID'
go


execute sp_addextendedproperty 'MS_Description', 
   '�ͼ����ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'DeptID'
go


execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'SampleTypeID'
go


execute sp_addextendedproperty 'MS_Description', 
   '�������',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'LowAge'
go

execute sp_addextendedproperty 'MS_Description', 
   '�������',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'HighAge'
go

execute sp_addextendedproperty 'MS_Description', 
   '���䵥λ',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'AgeUnitID'
go

execute sp_addextendedproperty 'MS_Description', 
   '������ʼʱ��',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'bCollectTime'
go


execute sp_addextendedproperty 'MS_Description', 
   '������ֹʱ��',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'eCollectTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ٴ����ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'DiagID'
go

execute sp_addextendedproperty 'MS_Description', 
   '������ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'PhyPeriodID'
go

execute sp_addextendedproperty 'MS_Description', 
   '������λID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'CollectPartID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'Comment'
go


create table dbo.LB_ExpertRuleList (
   LabID                bigint               not null,
   ExpertRuleID         bigint               not null,
   ExpertRuleListID     bigint               not null,
   RuleName             nvarchar(200)        null,
   SysRuleName          nvarchar(200)        null,
   ItemID               bigint               null,
   ItemName             varchar(40)          null,
   ValueFieldType       int                  null,
   bRelatedItemValue    bit                  null,
   bHisItemValue        bit                  null,
   bCalcItemValue       bit                  null,
   ValueType            int                  null,
   CompType             int                  null,
   CompValue            nvarchar(100)        null,
   CompFValue           float                null,
   CompFValue2          float                null,
   bValue               bit                  null,
   bLastHisComp         bit                  null,
   bLimitHisDate        bit                  null,
   LimitHisDate         int                  null,
   CalcFormula          nvarchar(300)        null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_BT_ExpertRuleList primary key (ExpertRuleListID)
         WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
   constraint FK_LB_ExpertRuleList_Rule foreign key (ExpertRuleID)
      references dbo.LB_ExpertRule (ExpertRuleID)
)
go


execute sp_addextendedproperty 'MS_Description', 
   '��ĿID',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'ItemID'
go


execute sp_addextendedproperty 'MS_Description', 
   '��Ŀ����',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'ItemName'
go


execute sp_addextendedproperty 'MS_Description', 
   'ö�٣�������� ����ֵ ���״̬��',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'ValueFieldType'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ؼ�����',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bRelatedItemValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ʷ������',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bHisItemValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bCalcItemValue'
go

execute sp_addextendedproperty 'MS_Description', 
   'ö�٣����� �ı� ����',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'ValueType'
go

execute sp_addextendedproperty 'MS_Description', 
   'ö�٣�> < = ��',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CompType'
go

execute sp_addextendedproperty 'MS_Description', 
   '�Ա�ֵ',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CompValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '�Ա���ֵ',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CompFValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '�Ա���ֵ2',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CompFValue2'
go

execute sp_addextendedproperty 'MS_Description', 
   '�Ա����ݴ���',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '���ж����һ����ʷ���',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bLastHisComp'
go

execute sp_addextendedproperty 'MS_Description', 
   '������ʷ�Ա�����',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bLimitHisDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ʷ�Ա�����',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'LimitHisDate'
go


execute sp_addextendedproperty 'MS_Description', 
   '���㹫ʽ ',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CalcFormula'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ж�����',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'DispOrder'
go





