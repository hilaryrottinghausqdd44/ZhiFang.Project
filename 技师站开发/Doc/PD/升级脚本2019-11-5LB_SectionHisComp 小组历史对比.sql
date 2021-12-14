create table LB_SectionHisComp (
   LabID                bigint               not null,
   SectionHisCompID     bigint               not null,
   SectionID            bigint               null,
   HisCompSectionID     bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SectionHisComp primary key (SectionHisCompID),
   constraint FK_SectionHisComp_HisCompSectionID foreign key (SectionID)
      references LB_Section (SectionID),
   constraint FK_SectionHisComp_SectionID foreign key (HisCompSectionID)
      references LB_Section (SectionID)
)
go