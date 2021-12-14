/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2019-05-27 19:36:18                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LB_EquipItem') and o.name = 'FK_ITEM_EQU_EP_B_EQU')
alter table LB_EquipItem
   drop constraint FK_ITEM_EQU_EP_B_EQU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LB_ItemCalc') and o.name = 'FK_LB_ITEMCALC_FK_ITEMCA_LB_ITEM')
alter table LB_ItemCalc
   drop constraint FK_LB_ITEMCALC_FK_ITEMCA_LB_ITEM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LB_ItemCalcFormula') and o.name = 'FK_LB_ITEMC_FK_ITEMCA_LB_ITEM')
alter table LB_ItemCalcFormula
   drop constraint FK_LB_ITEMC_FK_ITEMCA_LB_ITEM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.LB_ItemGroup') and o.name = 'FK_ITEM_ITEMCON_ITEM_GROUP')
alter table dbo.LB_ItemGroup
   drop constraint FK_ITEM_ITEMCON_ITEM_GROUP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.LB_ItemGroup') and o.name = 'FK_ITEM_ITEMCON_ITEM')
alter table dbo.LB_ItemGroup
   drop constraint FK_ITEM_ITEMCON_ITEM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LB_Section') and o.name = 'FK_GM_GROUP_SPECIALTY')
alter table LB_Section
   drop constraint FK_GM_GROUP_SPECIALTY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('B_AgeUnit')
            and   type = 'U')
   drop table B_AgeUnit
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_Client')
            and   type = 'U')
   drop table dbo.B_Client
go

if exists (select 1
            from  sysobjects
           where  id = object_id('B_Diag')
            and   type = 'U')
   drop table B_Diag
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_DiseaseArea')
            and   type = 'U')
   drop table dbo.B_DiseaseArea
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_DiseaseRoom')
            and   type = 'U')
   drop table dbo.B_DiseaseRoom
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_BaseType')
            and   type = 'U')
   drop table dbo.LB_BaseType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_Dept')
            and   type = 'U')
   drop table dbo.LB_Dept
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_Destination')
            and   type = 'U')
   drop table LB_Destination
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_Doctor')
            and   type = 'U')
   drop table dbo.LB_Doctor
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_Equip')
            and   type = 'U')
   drop table dbo.LB_Equip
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_EquipItem')
            and   type = 'U')
   drop table LB_EquipItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_ExecDept')
            and   type = 'U')
   drop table dbo.LB_ExecDept
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_Gender')
            and   type = 'U')
   drop table LB_Gender
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_Item')
            and   type = 'U')
   drop table dbo.LB_Item
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ItemCalc')
            and   type = 'U')
   drop table LB_ItemCalc
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ItemCalcFormula')
            and   type = 'U')
   drop table LB_ItemCalcFormula
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_ItemComp')
            and   type = 'U')
   drop table dbo.LB_ItemComp
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_ItemGroup')
            and   type = 'U')
   drop table dbo.LB_ItemGroup
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ItemRange')
            and   type = 'U')
   drop table LB_ItemRange
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ItemRangeExp')
            and   type = 'U')
   drop table LB_ItemRangeExp
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_ItemTimeW')
            and   type = 'U')
   drop table dbo.LB_ItemTimeW
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_Phrase')
            and   type = 'U')
   drop table dbo.LB_Phrase
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SampleType')
            and   type = 'U')
   drop table LB_SampleType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_Section')
            and   type = 'U')
   drop table LB_Section
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SectionItem')
            and   type = 'U')
   drop table LB_SectionItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SickType')
            and   type = 'U')
   drop table LB_SickType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_Specialty')
            and   type = 'U')
   drop table LB_Specialty
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_SuperSection')
            and   type = 'U')
   drop table dbo.LB_SuperSection
go

/*==============================================================*/
/* Table: LB_AgeUnit                                             */
/*==============================================================*/
create table LB_AgeUnit (
   AgeUnitID            bigint               not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_AGEUNIT primary key nonclustered (AgeUnitID)
)
go

/*==============================================================*/
/* Table: LB_Client                                              */
/*==============================================================*/
create table dbo.LB_Client (
   ClientID             bigint               not null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   ParentID             bigint               null,
   LevelNum             int                  null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   Tel                  nvarchar(50)         null,
   Fax                  nvarchar(50)         null,
   ZipCode              nvarchar(50)         null,
   Address              nvarchar(100)        null,
   Contact              nvarchar(50)         null,
   ParentOrg            nvarchar(50)         null,
   OrgType              nvarchar(50)         not null,
   OrgCode              nvarchar(50)         null,
   constraint PK_LB_Client primary key (ClientID)
         on "PRIMARY"
)
go

/*==============================================================*/
/* Table: LB_Diag                                                */
/*==============================================================*/
create table LB_Diag (
   DiagID               bigint               not null,
   CName                nvarchar(500)        null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_DIAG primary key (DiagID)
)
go

/*==============================================================*/
/* Table: LB_DiseaseArea                                         */
/*==============================================================*/
create table dbo.LB_DiseaseArea (
   LabID                bigint               not null,
   DiseaseAreaID        bigint               not null,
   ParentID             bigint               null,
   LevelNum             int                  null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   Tel                  nvarchar(50)         null,
   Fax                  nvarchar(50)         null,
   ZipCode              nvarchar(50)         null,
   Address              nvarchar(100)        null,
   Contact              nvarchar(50)         null,
   ParentOrg            nvarchar(50)         null,
   OrgType              nvarchar(50)         not null,
   OrgCode              nvarchar(50)         null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_DiseaseArea primary key (DiseaseAreaID)
         on "PRIMARY"
)
go

/*==============================================================*/
/* Table: LB_DiseaseRoom                                         */
/*==============================================================*/
create table dbo.LB_DiseaseRoom (
   DiseaseRoomID        bigint               not null,
   ParentID             bigint               null,
   LevelNum             int                  null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   Tel                  nvarchar(50)         null,
   Fax                  nvarchar(50)         null,
   ZipCode              nvarchar(50)         null,
   Address              nvarchar(100)        null,
   Contact              nvarchar(50)         null,
   ParentOrg            nvarchar(50)         null,
   OrgType              nvarchar(50)         not null,
   OrgCode              nvarchar(50)         null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_DiseaseRoom primary key (DiseaseRoomID)
         on "PRIMARY"
)
go

/*==============================================================*/
/* Table: LB_BaseType                                           */
/*==============================================================*/
create table dbo.LB_BaseType (
   BaseTypeID           bigint               not null,
   TypeGroup            nvarchar(50)         null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   iColor               int                  null,
   sColor               nvarchar(50)         null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_BaseType primary key nonclustered (BaseTypeID)
)
go

/*==============================================================*/
/* Table: LB_Dept                                               */
/*==============================================================*/
create table dbo.LB_Dept (
   DeptID               bigint               not null,
   ParentID             bigint               null,
   LevelNum             int                  null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   Tel                  nvarchar(50)         null,
   Fax                  nvarchar(50)         null,
   ZipCode              nvarchar(50)         null,
   Address              nvarchar(100)        null,
   Contact              nvarchar(50)         null,
   ParentOrg            nvarchar(50)         null,
   OrgType              nvarchar(50)         not null,
   OrgCode              nvarchar(50)         null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_Dept primary key (DeptID)
         on "PRIMARY"
)
go

/*==============================================================*/
/* Table: LB_Destination                                        */
/*==============================================================*/
create table LB_Destination (
   DestinationID        bigint               not null,
   DestinationNo        nvarchar(50)         null,
   CName                nvarchar(500)        null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_DESTINATION primary key (DestinationID)
)
go

/*==============================================================*/
/* Table: LB_Doctor                                             */
/*==============================================================*/
create table dbo.LB_Doctor (
   DoctorID             bigint               not null,
   NameL                nvarchar(30)         not null,
   NameF                nvarchar(30)         not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   Birthday             datetime             null,
   Email                nvarchar(50)         null,
   MobileTel            nvarchar(50)         null,
   OfficeTel            float                null,
   ExtTel               float                null,
   HomeTel              float                null,
   Tel                  nvarchar(50)         null,
   Address              nvarchar(250)        null,
   ZipCode              nvarchar(50)         null,
   IsEnabled            int                  not null,
   PicFile              image                null,
   IdNumber             nvarchar(50)         null,
   GraduateSchool       varchar(200)         null,
   EduBackground        varchar(200)         null,
   Training             varchar(200)         null,
   PersonalHomePage     varchar(200)         null,
   JobResume            varchar(200)         null,
   Family               varchar(200)         null,
   EntryTime            datetime             null,
   ContinuingEducation  nvarchar(500)        null,
   WageChange           nvarchar(500)        null,
   LaborContract        nvarchar(200)        null,
   Nationality          nvarchar(200)        null,
   ProfessionalQualifications nvarchar(200)        null,
   AwardandCertificates nvarchar(200)        null,
   JobDuty              nvarchar(200)        null,
   SignatureImage       image                null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_Doctor primary key (DoctorID)
         on "PRIMARY"
)
go

/*==============================================================*/
/* Table: LB_Equip                                              */
/*==============================================================*/
create table dbo.LB_Equip (
   EquipID              bigint               not null,
   SectionID            bigint               null,
   Computer             varchar(20)          null,
   ComProgram           varchar(20)          null,
   Doubleflag           int                  null,
   EquipType            int                  null,
   EquipResultType      int                  null,
   LicenceKey           varchar(30)          null,
   LicenceType          varchar(25)          null,
   SQH                  varchar(4)           null,
   SNo                  int                  null,
   LicenceDate          datetime             null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   constraint PK_LB_Equip primary key nonclustered (EquipID)
         on "PRIMARY"
)
go

/*==============================================================*/
/* Table: LB_EquipItem                                          */
/*==============================================================*/
create table LB_EquipItem (
   EquipItemID          bigint               not null,
   EquipID              bigint               null,
   Channel              char(50)             null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DispOrderComm        int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_EQUIPITEM primary key nonclustered (EquipItemID),
   constraint FK_ITEM_EQU_EP_B_EQU foreign key (EquipID)
      references dbo.LB_Equip (EquipID)
)
go

/*==============================================================*/
/* Table: LB_ExecDept                                           */
/*==============================================================*/
create table dbo.LB_ExecDept (
   ExecDeptID           bigint               not null,
   ParentID             bigint               null,
   LevelNum             int                  null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   Tel                  nvarchar(50)         null,
   Fax                  nvarchar(50)         null,
   ZipCode              nvarchar(50)         null,
   Address              nvarchar(100)        null,
   Contact              nvarchar(50)         null,
   ParentOrg            nvarchar(50)         null,
   OrgType              nvarchar(50)         not null,
   OrgCode              nvarchar(50)         null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ExecDept primary key (ExecDeptID)
         on "PRIMARY"
)
go

/*==============================================================*/
/* Table: LB_Gender                                             */
/*==============================================================*/
create table LB_Gender (
   GenderID             bigint               not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_Gender primary key (GenderID)
)
go

/*==============================================================*/
/* Table: LB_Item                                               */
/*==============================================================*/
create table dbo.LB_Item (
   ItemID               bigint               not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   ItemType             int                  null,
   DiagMethod           nvarchar(50)         null,
   Unit                 nvarchar(50)         null,
   RefRange             nvarchar(500)        null,
   ValueType            int                  null,
   SamplingRequire      ntext                null,
   ClinicalInfo         ntext                null,
   ItemCharge           decimal              null,
   Prec                 int                  null,
   IsOrderItem          bit                  null,
   GroupType            int                  null,
   IsSampleItem         bit                  null,
   IsCalcItem           bit                  null,
   IsChargeItem         bit                  null,
   IsUnionItem          bit                  null,
   IsPrint              bit                  null,
   IsPartItem           bit                  null,
   SecretFlag           int                  null,
   HisCompType          int                  null,
   HisCompH             float                null,
   HisCompHH            float                null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ITEM primary key nonclustered (ItemID)
)
go

/*==============================================================*/
/* Table: LB_ItemCalc                                           */
/*==============================================================*/
create table LB_ItemCalc (
   CalcItemID           bigint               not null,
   ItemID               bigint               null,
   Calc_ItemID          bigint               null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemCalc primary key (CalcItemID),
   constraint FK_LB_ITEMCALC_FK_ITEMCA_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go

/*==============================================================*/
/* Table: LB_ItemCalcFormula                                    */
/*==============================================================*/
create table LB_ItemCalcFormula (
   CalcFormulaID        bigint               not null,
   ItemID               bigint               null,
   FormulaCondition     nvarchar(300)        null,
   CalcFormula          nvarchar(300)        null,
   HAge                 float                null,
   LAge                 float                null,
   AgeUnitID            bigint               null,
   GenderID             bigint               null,
   UWeight              float                null,
   LWeight              float                null,
   SampleTypeID         bigint               null,
   SectionID            bigint               null,
   IsUse                bit                  null,
   IsDefault            bit                  null,
   CalcType             int                  null,
   IsKeepInvalid        bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemCalcFormula primary key (CalcFormulaID),
   constraint FK_LB_ITEMC_FK_ITEMCA_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go

/*==============================================================*/
/* Table: LB_ItemComp                                           */
/*==============================================================*/
create table dbo.LB_ItemComp (
   ItemCompID           bigint               not null,
   CompType             nvarchar(50)         null,
   CompType2            nvarchar(50)         null,
   LisItemID            nvarchar(50)         null,
   SourceID             nvarchar(50)         null,
   SourceCode           nvarchar(50)         null,
   SourceCode2          nvarchar(50)         null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   UserID               bigint               not null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemComp primary key nonclustered (ItemCompID)
)
go

/*==============================================================*/
/* Table: LB_ItemGroup                                          */
/*==============================================================*/
create table dbo.LB_ItemGroup (
   ItemConID            bigint               not null,
   GroupItemID          bigint               null,
   ItemID               bigint               null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemGroup primary key (ItemConID),
   constraint FK_ITEM_ITEMCON_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID),
   constraint FK_ITEM_ITEMCON_ITEM_GROUP foreign key (GroupItemID)
      references dbo.LB_Item (ItemID)
)
go

/*==============================================================*/
/* Table: LB_ItemRange                                          */
/*==============================================================*/
create table LB_ItemRange (
   ItemRangeID          bigint               not null,
   ItemID               bigint               null,
   ConditionName        nvarchar(100)        null,
   GenderID             bigint               not null,
   LowAge               float                null,
   HighAge              float                null,
   bCollectTime         datetime             null,
   eCollectTime         datetime             null,
   IsDefault            bit                  null,
   LValueComp           nvarchar(10)         collate Chinese_PRC_CI_AS null,
   LValue               float                null,
   HValueComp           nvarchar(10)         collate Chinese_PRC_CI_AS null,
   HValue               float                null,
   RefRange             nvarchar(400)        collate Chinese_PRC_CI_AS null,
   LLValueComp          nvarchar(10)         collate Chinese_PRC_CI_AS null,
   LLValue              float                null,
   HHValueComp          nvarchar(10)         collate Chinese_PRC_CI_AS null,
   HHValue              float                null,
   DiagMethod           nvarchar(50)         null,
   Unit                 nvarchar(50)         null,
   ManualSort           int                  null,
   DispOrder            int                  null,
   RedoLValueComp       nvarchar(10)         collate Chinese_PRC_CI_AS null,
   RedoLValue           float                null,
   RedoHValueComp       nvarchar(10)         collate Chinese_PRC_CI_AS null,
   RedoHValue           float                null,
   InvalidLValueComp    nvarchar(10)         collate Chinese_PRC_CI_AS null,
   InvalidLValue        float                null,
   InvalidHValueComp    nvarchar(10)         collate Chinese_PRC_CI_AS null,
   InvalidHValue        float                null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemRange primary key (ItemRangeID),
   constraint FK_LB_ITEMR_FK_ITEMRA_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go

/*==============================================================*/
/* Table: LB_ItemRangeExp                                       */
/*==============================================================*/
create table LB_ItemRangeExp (
   ItemRangeExpID       bigint               not null,
   ItemID               bigint               not null,
   JudgeType            int                  null,
   JudgeValue           nvarchar(300)        null,
   ResultStatus         nvarchar(20)         null,
   ResultReport         nvarchar(500)        null,
   IsAddReport          bit                  null,
   expReport            int                  null,
   expComment           nvarchar(500)        null,
   AlarmColor           int                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemRangeExp primary key (ItemRangeExpID)
)
go

/*==============================================================*/
/* Table: LB_ItemTimeW                                          */
/*==============================================================*/
create table dbo.LB_ItemTimeW (
   ItemTimeWID          bigint               not null,
   TimeWType            nvarchar(50)         null,
   ItemID               bigint               null,
   SampleTypeID         bigint               null,
   SickTypeID           bigint               null,
   SectionID            bigint               null,
   TimeWValue           int                  null,
   TimeWDesc            nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   UserID               bigint               not null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemTimeW primary key nonclustered (ItemTimeWID)
)
go

/*==============================================================*/
/* Table: LB_Phrase                                             */
/*==============================================================*/
create table dbo.LB_Phrase (
   PhraseID             bigint               not null,
   TypeName             nvarchar(50)         null,
   ObjectType           int                  null,
   ObjectID             bigint               not null,
   SampleTypeID         bigint               null,
   CName                nvarchar(500)        null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_Phrase primary key nonclustered (PhraseID)
)
go

/*==============================================================*/
/* Table: LB_SampleType                                         */
/*==============================================================*/
create table LB_SampleType (
   SampleTypeID         bigint               not null,
   ParSampleTypeID      bigint               null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SampleType primary key (SampleTypeID)
)
go

/*==============================================================*/
/* Table: LB_Specialty                                          */
/*==============================================================*/
create table LB_Specialty (
   SpecialtyID          bigint               not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_Specialty primary key (SpecialtyID)
)
go

/*==============================================================*/
/* Table: LB_Section                                            */
/*==============================================================*/
create table LB_Section (
   SectionID            bigint               not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   ExecDeptID           bigint               not null,
   SuperSectionID       bigint               not null,
   SpecialtyID          bigint               null,
   IsVirtualGroup       bit                  null,
   SectionFun           nvarchar(50)         null,
   ProDLL               nvarchar(200)        null,
   IsImage              bit                  null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_Section primary key (SectionID),
   constraint FK_GM_GROUP_SPECIALTY foreign key (SpecialtyID)
      references LB_Specialty (SpecialtyID)
)
go

/*==============================================================*/
/* Table: LB_SectionItem                                        */
/*==============================================================*/
create table LB_SectionItem (
   SectionItemID        bigint               not null,
   ItemID               bigint               null,
   SectionID            bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   IsDefult             bit                  null,
   DefultValue          nvarchar(50)         null,
   GroupItemID          bigint               null,
   EquipID              bigint               null,
   LabID                bigint               not null,
   DataTimeStamp        timestamp            null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   constraint PK_LB_SectionItem primary key (SectionItemID),
   constraint FK_GM_GROUPITEM_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID),
   constraint FK_GM_GROUPITEM_GROUP foreign key (SectionID)
      references LB_Section (SectionID)
)
go

/*==============================================================*/
/* Table: LB_SickType                                           */
/*==============================================================*/
create table LB_SickType (
   SickTypeID           bigint               not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SickType primary key (SickTypeID)
)
go

/*==============================================================*/
/* Table: LB_SuperSection                                       */
/*==============================================================*/
create table dbo.LB_SuperSection (
   SuperGroupID         bigint               not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SuperSection primary key (SuperGroupID)
         on "PRIMARY"
)
go

