
 IF COL_LENGTH('LB_SectionItem', 'IsTran') IS NULL  
  alter table LB_SectionItem Add IsTran int null
 