if exists (select 1
            from  sysobjects
           where  id = object_id('LB_OrderModel')
            and   type = 'U')
   drop table LB_OrderModel
go

/*==============================================================*/
/* Table: LB_OrderModel                                         */
/*==============================================================*/
create table LB_OrderModel (
   LabID                bigint               not null,
   OrderModelID         bigint               not null,
   POrderModelID        bigint               null,
   CName                nvarchar(100)        null,
   DeptID               bigint               null,
   UserID               bigint               null,
   SName                nvarchar(100)        null,
   SCode                nvarchar(100)        null,
   OrderModelDesc       nvarchar(500)        null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ORDERMODEL primary key (OrderModelID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_OrderModel') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_OrderModel' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '医嘱模板', 
   'user', @CurrentUser, 'table', 'LB_OrderModel'
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_OrderModelItem')
            and   type = 'U')
   drop table LB_OrderModelItem
go

/*==============================================================*/
/* Table: LB_OrderModelItem                                     */
/*==============================================================*/
create table LB_OrderModelItem (
   LabID                bigint               not null,
   OrderModelItemID     bigint               not null,
   OrderModelID         bigint               not null,
   ItemID               bigint               not null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ORDERMODELITEM primary key (OrderModelItemID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_OrderModelItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_OrderModelItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '医嘱模板明细', 
   'user', @CurrentUser, 'table', 'LB_OrderModelItem'
go
