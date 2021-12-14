



SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[GK_SampleRequestForm]  WITH CHECK ADD  CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType] FOREIGN KEY([RecordTypeID])
REFERENCES [dbo].[SC_RecordType] ([RecordTypeID])
GO

ALTER TABLE [dbo].[GK_SampleRequestForm] CHECK CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LabID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请主单主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReqDocId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请主单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReqDocNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'感控监测类型
1:感控监测;
0:科室监测;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'MonitorType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DeptId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采样人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DeptCName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'SamplerId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采样人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Sampler'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采样日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'SampleDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'监测类型Id,关联公共记录项类型字典Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'RecordTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请单状态Id
1：已提交
2：已核收
3：已检验
4：已返结果 
5：已评价
6：已归档
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'StatusID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名，冗余字段，取监测类型的第一个记录项录入值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'条码号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'BarCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'条码打印次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'PrintCount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'小组样本号
按科室自动核收后回写' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'SampleNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否自动核收' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'IsAutoReceive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'核收标志
false：未核收
true：已核收' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReceiveFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结果回写标志
false：结果未回写
true：结果已回写' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ResultFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检验者Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'TesterId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检验者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'TesterName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检验日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'TestTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检验结果' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'TestResult'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'细菌总数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'BacteriaTotal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'核收人Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReceiveId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'核收人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReceiveCName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'核收日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReceiveDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CheckId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CheckCName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CheckDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评估者名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'EvaluatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评估者名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Evaluators'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评估日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'EvaluationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评估标志
false: 未评估;
true:已评估;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'EvaluatorFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评估判定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Judgment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'归档标志
false: 未归档;
true:已归档;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Archived'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Visible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废人Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废备注Id,关联B_Dict' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteMemoId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteMemo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Memo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'院感样本申请主单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm'
GO


