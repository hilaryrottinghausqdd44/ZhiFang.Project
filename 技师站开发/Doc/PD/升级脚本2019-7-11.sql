if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ItemCalc')
            and   type = 'U')
   drop table LB_ItemCalc
go

/*==============================================================*/
/* Table: LB_ItemCalc                                           */
/*==============================================================*/
create table LB_ItemCalc (
   ItemClacID           bigint               not null,
   ItemID               bigint               null,
   CalcItemID           bigint               null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemCalc primary key (ItemClacID),
   constraint FK_LB_ITEMCALC_FK_ITEMCA_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID),
   constraint FK_LB_ITEMCalc_CalcItemID foreign key (CalcItemID)
      references dbo.LB_Item (ItemID)
)
go
