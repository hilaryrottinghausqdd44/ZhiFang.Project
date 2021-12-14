if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ParItemSplit')
            and   type = 'U')
   drop table LB_ParItemSplit
go

/*==============================================================*/
/* Table: LB_ParItemSplit                                       */
/*==============================================================*/
create table LB_ParItemSplit (
   ParItemSplitID       bigint               not null,
   ParItemID            bigint               not null,
   ItemID               bigint               not null,
   SamplingGroupID      bigint               not null,
   NewBarCode           int                  not null,
   IsAutoUnion          bit                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_PARITEMSPLIT primary key (ParItemSplitID),
   constraint FK_LB_PARIT_REFERENCE_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID),
   constraint FK_LB_PARIT_REFERENCE_LB_SAMPL foreign key (SamplingGroupID)
      references LB_SamplingGroup (SamplingGroupID),
   constraint FK_LB_PARIT_REFERENCE_LB_PARITEM foreign key (ParItemID)
      references dbo.LB_Item (ItemID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_ParItemSplit') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_ParItemSplit' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '组合项目拆分', 
   'user', @CurrentUser, 'table', 'LB_ParItemSplit'
go
