



SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[GK_SampleRequestForm]  WITH CHECK ADD  CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType] FOREIGN KEY([RecordTypeID])
REFERENCES [dbo].[SC_RecordType] ([RecordTypeID])
GO

ALTER TABLE [dbo].[GK_SampleRequestForm] CHECK CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LabID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReqDocId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReqDocNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�пؼ������
1:�пؼ��;
0:���Ҽ��;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'MonitorType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DeptId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DeptCName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'SamplerId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Sampler'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'SampleDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������Id,����������¼�������ֵ�Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'RecordTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���뵥״̬Id
1�����ύ
2���Ѻ���
3���Ѽ���
4���ѷ���� 
5��������
6���ѹ鵵
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'StatusID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����������ֶΣ�ȡ������͵ĵ�һ����¼��¼��ֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'BarCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ӡ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'PrintCount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'С��������
�������Զ����պ��д' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'SampleNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ��Զ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'IsAutoReceive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ձ�־
false��δ����
true���Ѻ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReceiveFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����д��־
false�����δ��д
true������ѻ�д' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ResultFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'TesterId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'TesterName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'TestTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'TestResult'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ϸ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'BacteriaTotal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReceiveId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReceiveCName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ReceiveDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CheckId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CheckCName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'CheckDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'EvaluatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Evaluators'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'EvaluationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������־
false: δ����;
true:������;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'EvaluatorFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ж�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Judgment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�鵵��־
false: δ�鵵;
true:�ѹ鵵;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Archived'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�ʹ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Visible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ϱ�עId,����B_Dict' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteMemoId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ϱ�ע' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'ObsoleteMemo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ע' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'Memo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ʾ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʱ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ժ����������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GK_SampleRequestForm'
GO


