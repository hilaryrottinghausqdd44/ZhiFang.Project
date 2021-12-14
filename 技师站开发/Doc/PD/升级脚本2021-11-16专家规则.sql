
--2021-11-16 专家规则

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
   '专家规则ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ExpertRuleID'
go

execute sp_addextendedproperty 'MS_Description', 
   '专家规则名称',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'CName'
go

execute sp_addextendedproperty 'MS_Description', 
   '专家规则组名称(组内规则互斥=or关系)',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'GName'
go

execute sp_addextendedproperty 'MS_Description', 
   '0：常规结果（默认） 1：微生物结果',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ResultType'
go

execute sp_addextendedproperty 'MS_Description', 
   '0：全部满足(and)
    1：互斥(or)',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'RuleRelation'
go

execute sp_addextendedproperty 'MS_Description', 
   '小组ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'SectionID'
go

execute sp_addextendedproperty 'MS_Description', 
   '项目ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ItemID'
go

execute sp_addextendedproperty 'MS_Description', 
   '警示提示',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ItemAlarmInfo'
go


execute sp_addextendedproperty 'MS_Description', 
   '警示级别 枚举：0：正常 1：警示 2：警告 3：严重警告 4：危急 5：错误',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'AlarmLevel'
go


execute sp_addextendedproperty 'MS_Description', 
   '警示提示',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'AlarmInfo'
go


execute sp_addextendedproperty 'MS_Description', 
   '条件说明',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'ConditionName'
go


execute sp_addextendedproperty 'MS_Description', 
   '性别ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'GenderID'
go


execute sp_addextendedproperty 'MS_Description', 
   '检验仪器ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'EquipID'
go


execute sp_addextendedproperty 'MS_Description', 
   '送检科室ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'DeptID'
go


execute sp_addextendedproperty 'MS_Description', 
   '样本类型',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'SampleTypeID'
go


execute sp_addextendedproperty 'MS_Description', 
   '年龄低限',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'LowAge'
go

execute sp_addextendedproperty 'MS_Description', 
   '年龄高限',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'HighAge'
go

execute sp_addextendedproperty 'MS_Description', 
   '年龄单位',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'AgeUnitID'
go

execute sp_addextendedproperty 'MS_Description', 
   '采样开始时间',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'bCollectTime'
go


execute sp_addextendedproperty 'MS_Description', 
   '采样截止时间',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'eCollectTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '临床诊断ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'DiagID'
go

execute sp_addextendedproperty 'MS_Description', 
   '生理期ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'PhyPeriodID'
go

execute sp_addextendedproperty 'MS_Description', 
   '采样部位ID',
   'user', 'dbo', 'table', 'LB_ExpertRule', 'column', 'CollectPartID'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
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
   '项目ID',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'ItemID'
go


execute sp_addextendedproperty 'MS_Description', 
   '项目名称',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'ItemName'
go


execute sp_addextendedproperty 'MS_Description', 
   '枚举，定量结果 报告值 结果状态等',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'ValueFieldType'
go


execute sp_addextendedproperty 'MS_Description', 
   '相关检验结果',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bRelatedItemValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '历史检验结果',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bHisItemValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '计算结果',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bCalcItemValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '枚举，定量 文本 存在',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'ValueType'
go

execute sp_addextendedproperty 'MS_Description', 
   '枚举，> < = 等',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CompType'
go

execute sp_addextendedproperty 'MS_Description', 
   '对比值',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CompValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '对比数值',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CompFValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '对比数值2',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CompFValue2'
go

execute sp_addextendedproperty 'MS_Description', 
   '对比内容存在',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '仅判断最近一次历史结果',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bLastHisComp'
go

execute sp_addextendedproperty 'MS_Description', 
   '限制历史对比日期',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'bLimitHisDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '历史对比日期',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'LimitHisDate'
go


execute sp_addextendedproperty 'MS_Description', 
   '计算公式 ',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'CalcFormula'
go

execute sp_addextendedproperty 'MS_Description', 
   '判定次序',
   'user', 'dbo', 'table', 'LB_ExpertRuleList', 'column', 'DispOrder'
go





