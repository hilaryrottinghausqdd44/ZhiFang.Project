
 IF COL_LENGTH('LB_Equip', 'EquipNo') IS NULL  
  alter table LB_Equip Add EquipNo int null

IF COL_LENGTH('LB_Section', 'SectionNo') IS NULL  
  alter table LB_Section Add SectionNo int null

IF COL_LENGTH('LB_Item', 'ItemNo') IS NULL  
  alter table LB_Item Add ItemNo int null

IF COL_LENGTH('LB_SampleType', 'SampleTypeNo') IS NULL  
  alter table LB_SampleType Add SampleTypeNo int null

IF COL_LENGTH('LB_QCMaterial', 'QCMatNo') IS NULL  
  alter table LB_QCMaterial Add QCMatNo int null

IF COL_LENGTH('LB_QCItem', 'QCItemNo') IS NULL  
  alter table LB_QCItem Add QCItemNo int null
