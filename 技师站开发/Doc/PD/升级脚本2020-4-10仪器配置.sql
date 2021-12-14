  alter Table LB_Equip Alter Column CommInfo  ntext  null
 if not exists ( select * from LB_Equip where not  CommSys is Null )  alter Table LB_Equip Drop  Column  CommSys
  alter Table LB_Equip Add   CommSys ntext  null