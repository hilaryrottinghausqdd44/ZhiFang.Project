--ReportFormfull-------------------------------------------- ReportFormIndexID
--ReportItemFull-------------------------------------------- ReportItemIndexID
--ReportMarrowFull-------------------------------------------- ReportMarrowIndexID
--ReportMicroFull-------------------------------------------- ReportMicroIndexID

alter table ReportFormFull add ReportFormIndexID number(19)
alter table ReportItemFull add ReportItemIndexID number(19)
alter table ReportMicroFull add ReportMicroIndexID number(19)
alter table ReportMarrowFull add ReportMarrowIndexID number(19)


update ReportFormFull set ReportFormIndexID =rownum
update ReportItemFull set ReportItemIndexID =rownum
update ReportMicroFull set ReportMicroIndexID =rownum
update ReportMarrowFull set ReportMarrowIndexID =rownum


alter table Reportformfull add constraint pk_ReportForm primary key(ReportFormIndexID); 
alter table ReportItemFull add constraint pk_ReportItem primary key(ReportItemIndexID); 
alter table ReportMicroFull add constraint pk_ReportMicro primary key(ReportMicroIndexID); 
alter table ReportMarrowFull add constraint pk_ReportMarrow primary key(ReportMarrowIndexID); 
