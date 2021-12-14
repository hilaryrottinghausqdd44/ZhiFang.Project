create table LB_Right (
   LabID                bigint               not null,
   LisRightID           bigint               not null,
   EmpID                bigint               null,
   RoleID               bigint               null,
   SectionID            bigint               null,
   Operator             nvarchar(50)         null,
   OperatorID           bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_Right primary key (LisRightID) ON [PRIMARY],
   constraint FK_LB_RIGHT_Section foreign key (SectionID)
      references LB_Section (SectionID)
)
go