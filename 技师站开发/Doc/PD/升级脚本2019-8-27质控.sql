/*==============================================================*/
/* Table: LB_QCMaterial                                         */
/*==============================================================*/
create table dbo.LB_QCMaterial (
   LabID                bigint               not null,
   QCMatID              bigint               not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   EquipID              bigint               not null,
   EquipModule          nvarchar(50)         null,
   ConcLevel            nvarchar(20)         null,
   MarkID               nvarchar(50)         null,
   MatType              int                  not null,
   MicroID              bigint               null,
   QCGroup              nvarchar(50)         null,
   QCWithGroup          nvarchar(50)         null,
   IsQCWithMain         bit                  null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   EndDate              datetime             null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCMaterial primary key (QCMatID)
         WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
   constraint FK_LB_QCMAT_EQUIP foreign key (EquipID)
      references dbo.LB_Equip (EquipID)
)
go


execute sp_addextendedproperty 'MS_Description', 
   'ʵ����ID',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'LabID'
go



execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���ID',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'QCMatID'
go



execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'CName'
go



execute sp_addextendedproperty 'MS_Description', 
   'Ӣ������',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'EName'
go



execute sp_addextendedproperty 'MS_Description', 
   '���',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'SName'
go



execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'EquipID'
go


execute sp_addextendedproperty 'MS_Description', 
   '����ģ��',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'EquipModule'
go



execute sp_addextendedproperty 'MS_Description', 
   'Ũ��ˮƽ',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'ConcLevel'
go



execute sp_addextendedproperty 'MS_Description', 
   'ͨѶ�ʿر�ʶ',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'MarkID'
go



execute sp_addextendedproperty 'MS_Description', 
   '�ʿ�����0�������ʿ� 1��΢�����ʿ� ',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'MatType'
go


execute sp_addextendedproperty 'MS_Description', 
   '΢����ID',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'MicroID'
go


execute sp_addextendedproperty 'MS_Description', 
   '�ʿط���',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'QCGroup'
go



execute sp_addextendedproperty 'MS_Description', 
   '��ͬ��������',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'QCWithGroup'
go


execute sp_addextendedproperty 'MS_Description', 
   '���������ο�',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'IsQCWithMain'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'UseCode'
go


execute sp_addextendedproperty 'MS_Description', 
   '��׼����',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'StandCode'
go


execute sp_addextendedproperty 'MS_Description', 
   '�����̱�׼����',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'DeveCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '�����',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'Shortcode'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ƴ����ͷ',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'PinYinZiTou'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'Comment'
go


execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ�ʹ��',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'IsUse'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ʾ����',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'DispOrder'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ֹ����',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'EndDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'DataAddTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '���ݸ���ʱ��',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'DataUpdateTime'
go


execute sp_addextendedproperty 'MS_Description', 
   'ʱ���',
   'user', 'dbo', 'table', 'LB_QCMaterial', 'column', 'DataTimeStamp'
go

/*==============================================================*/
/* Table: LB_QCItem                                             */
/*==============================================================*/
create table dbo.LB_QCItem (
   LabID                bigint               not null,
   QCItemID             bigint               not null,
   QCMatID              bigint               null,
   ItemID               bigint               null,
   AntiID               bigint               null,
   IsLog                bit                  null,
   SDCV                 float                null,
   QCDataType           int                  not null,
   QCDataTypeName       nvarchar(20)         null,
   TestDateInterval     float                null,
   iLostTimes           int                  null,
   Comment              nvarchar(500)        null,
   UserID               bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCItem primary key (QCItemID)
         WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
   constraint FK_LB_QCItem_QCMAT foreign key (QCMatID)
      references dbo.LB_QCMaterial (QCMatID),
   constraint FK_LB_QCItem_Item foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ʵ����ID',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'LabID'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���ĿID',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'QCItemID'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���ID ',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'QCMatID'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ĿID',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'ItemID'
go

execute sp_addextendedproperty 'MS_Description', 
   '������ID',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'AntiID'
go

execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ����ֵ',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'IsLog'
go

execute sp_addextendedproperty 'MS_Description', 
   '�趨CV',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'SDCV'
go


execute sp_addextendedproperty 'MS_Description', 
   '0����ֵ��׼� 1������ 2��ֵ��Χ',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'QCDataType'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���������',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'QCDataTypeName'
go


execute sp_addextendedproperty 'MS_Description', 
   '�ʿ����ڼ��',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'TestDateInterval'
go

execute sp_addextendedproperty 'MS_Description', 
   '���ʧ�ش���',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'iLostTimes'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'Comment'
go

execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'UserID'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'DataAddTime'
go


execute sp_addextendedproperty 'MS_Description', 
   '���ݸ���ʱ��',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'DataUpdateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   'ʱ���',
   'user', 'dbo', 'table', 'LB_QCItem', 'column', 'DataTimeStamp'
go

/*==============================================================*/
/* Table: LB_QCMatTime                                          */
/*==============================================================*/
create table dbo.LB_QCMatTime (
   LabID                bigint               not null,
   QCMatTimeID          bigint               not null,
   QCMatID              bigint               null,
   LotNo                nvarchar(50)         null,
   Manu                 nvarchar(50)         null,
   Begindate            datetime             null,
   EndDate              datetime             null,
   NotUseDate           datetime             null,
   CanUseDateInfo       nvarchar(50)         null,
   ManuQCInfo           nvarchar(200)        null,
   ManuQCStoreInfo      nvarchar(500)        null,
   QCDesc               nvarchar(500)        null,
   Comment              nvarchar(500)        null,
   UserID               bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCMaterialTime primary key (QCMatTimeID)
         WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
   constraint FK_LB_QCMATTime_QCMAT foreign key (QCMatID)
      references dbo.LB_QCMaterial (QCMatID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ʵ����ID',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'LabID'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���ʱЧID',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'QCMatTimeID'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���ID',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'QCMatID'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'LotNo'
go


execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'Manu'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ʼ����',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'Begindate'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ֹ����',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'EndDate'
go

execute sp_addextendedproperty 'MS_Description', 
   'ʧЧ����',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'NotUseDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '��Ч������',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'CanUseDateInfo'
go

execute sp_addextendedproperty 'MS_Description', 
   '�����ʿ�������',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'ManuQCInfo'
go

execute sp_addextendedproperty 'MS_Description', 
   '�洢˵��',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'ManuQCStoreInfo'
go


execute sp_addextendedproperty 'MS_Description', 
   '�ʿ�ʱЧ����',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'QCDesc'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'Comment'
go

execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'UserID'
go


execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'DataAddTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '���ݸ���ʱ��',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'DataUpdateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   'ʱ���',
   'user', 'dbo', 'table', 'LB_QCMatTime', 'column', 'DataTimeStamp'
go

/*==============================================================*/
/* Table: LB_QCItemTime                                         */
/*==============================================================*/
create table dbo.LB_QCItemTime (
   LabID                bigint               not null,
   QCItemTimeID         bigint               not null,
   QCItemID             bigint               not null,
   StartDate            datetime             not null,
   EndDate              datetime             null,
   QCMatTimeID          bigint               null,
   Target               float                null,
   SD                   float                null,
   CCV                  float                null,
   HValue               float                null,
   LValue               float                null,
   HHValue              float                null,
   LLValue              float                null,
   DescAll              nvarchar(500)        null,
   SpecialType          int                  null,
   iIndex               int                  identity,
   ManuQCRange          nvarchar(200)        null,
   ManuQCInfo           nvarchar(200)        null,
   QCItemTimeDesc       nvarchar(200)        null,
   Comment              nvarchar(500)        null,
   DiagMethod           nvarchar(50)         null,
   Unit                 nvarchar(50)         null,
   TestDesc             nvarchar(50)         null,
   QCWithInfo           nvarchar(10)         null,
   QCWithRange          float                null,
   UserID               bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCItemTime primary key (QCItemTimeID)
         WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY],
   constraint FK_LB_QCItemTime_QCMATTime foreign key (QCMatTimeID)
      references dbo.LB_QCMatTime (QCMatTimeID),
   constraint FK_LB_QCItemTime_QCItem foreign key (QCItemID)
      references dbo.LB_QCItem (QCItemID)
)
go


execute sp_addextendedproperty 'MS_Description', 
   'ʵ����ID',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'LabID'
go


execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���ĿʱЧID',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'QCItemTimeID'
go


execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���ĿID',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'QCItemID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ʼ����',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'StartDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ֹ����',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'EndDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���ʱЧID',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'QCMatTimeID'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ֵ',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'Target'
go


execute sp_addextendedproperty 'MS_Description', 
   '��׼��',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'SD'
go


execute sp_addextendedproperty 'MS_Description', 
   'CCV',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'CCV'
go


execute sp_addextendedproperty 'MS_Description', 
   '�����',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'HValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '�����',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'LValue'
go


execute sp_addextendedproperty 'MS_Description', 
   'ʧ�ظ�',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'HHValue'
go

execute sp_addextendedproperty 'MS_Description', 
   'ʧ�ص�',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'LLValue'
go


execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'DescAll'
go

execute sp_addextendedproperty 'MS_Description', 
   '���⴦��',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'SpecialType'
go


execute sp_addextendedproperty 'MS_Description', 
   '˳��',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'iIndex'
go


execute sp_addextendedproperty 'MS_Description', 
   '�����ʿط�Χ����',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'ManuQCRange'
go

execute sp_addextendedproperty 'MS_Description', 
   '�����ʿ���Ŀ��Ϣ',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'ManuQCInfo'
go


execute sp_addextendedproperty 'MS_Description', 
   '�ʿ���ĿʱЧ˵��',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'QCItemTimeDesc'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'Comment'
go



execute sp_addextendedproperty 'MS_Description', 
   '���鷽��',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'DiagMethod'
go


execute sp_addextendedproperty 'MS_Description', 
   '��λ',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'Unit'
go


execute sp_addextendedproperty 'MS_Description', 
   '����˵��',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'TestDesc'
go

execute sp_addextendedproperty 'MS_Description', 
   '�����ʿ�����',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'QCWithInfo'
go


execute sp_addextendedproperty 'MS_Description', 
   '��ֵ������Χ',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'QCWithRange'
go


execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'UserID'
go


execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'DataAddTime'
go


execute sp_addextendedproperty 'MS_Description', 
   '���ݸ���ʱ��',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'DataUpdateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   'ʱ���',
   'user', 'dbo', 'table', 'LB_QCItemTime', 'column', 'DataTimeStamp'
go