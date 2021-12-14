if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SamplingItem')
            and   type = 'U')
   drop table LB_SamplingItem
go

/*==============================================================*/
/* Table: LB_SamplingItem                                       */
/*==============================================================*/
create table LB_SamplingItem (
   SamplingItemID       bigint               not null,
   SamplingGroupID      bigint               null,
   ItemID               bigint               null,
   IsDefault            bit                  null,
   MinItemCount         int                  null,
   MustItem             int                  null,
   VirtualItemNo        int                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SAMPLINGITEM primary key (SamplingItemID),
   constraint FK_LB_SAMPL_REFERENCE_LB_SAMPL foreign key (SamplingGroupID)
      references LB_SamplingGroup (SamplingGroupID),
   constraint FK_LB_SAMPL_REFERENCE_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_SamplingItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_SamplingItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '采样组项目', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem'
go
