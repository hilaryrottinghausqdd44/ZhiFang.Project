if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SamplingGroup')
            and   type = 'U')
   drop table LB_SamplingGroup
go

/*==============================================================*/
/* Table: LB_SamplingGroup                                      */
/*==============================================================*/
create table LB_SamplingGroup (
   SamplingGroupID      bigint               not null,
   CName                nvarchar(50)         null,
   SampleTypeID         bigint               null,
   CuveteID             bigint               null,
   SuperGroupID         bigint               null,
   SName                nvarchar(50)         null,
   SCode                nvarchar(50)         null,
   Destination          nvarchar(50)         null,
   Synopsis             nvarchar(50)         null,
   PrintCount           int                  null,
   AffixTubeFlag        nvarchar(50)         null,
   PrepInfo             nvarchar(300)        null,
   VirtualNo            int                  null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SAMPLINGGROUP primary key (SamplingGroupID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_SamplingGroup') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_SamplingGroup' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '采样组', 
   'user', @CurrentUser, 'table', 'LB_SamplingGroup'
go
