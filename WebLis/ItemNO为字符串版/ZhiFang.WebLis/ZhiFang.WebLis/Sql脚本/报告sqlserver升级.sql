--ReportFormfull-------------------------------------------- ReportFormIndexID
--ReportItemFull-------------------------------------------- ReportItemIndexID
--ReportMarrowFull-------------------------------------------- ReportMarrowIndexID
--ReportMicroFull-------------------------------------------- ReportMicroIndexID

alter table ReportFormFull add ReportFormIndexID bigint default 0 not null
declare @fid int
set @fid = 0
update ReportFormfull set ReportFormIndexID=@fid,@fid=@fid+1
alter table ReportFormFull add constraint ReportFormIndexID primary key (ReportFormIndexID)

alter table ReportItemFull add ReportItemIndexID bigint default 0 not null
declare @iid int
set @iid = 0
update ReportItemFull set ReportItemIndexID=@iid,@iid=@iid+1
alter table ReportItemFull add constraint ReportItemIndexID primary key (ReportItemIndexID)


alter table ReportMicroFull add ReportMicroIndexID bigint default 0 not null
declare @mid int
set @mid = 0
update ReportMicroFull set ReportMicroIndexID=@mid,@mid=@mid+1
alter table ReportMicroFull add constraint ReportMicroIndexID primary key (ReportMicroIndexID)


alter table ReportMarrowFull add ReportMarrowIndexID bigint default 0 not null
declare @mmid int
set @mmid = 0
update ReportMarrowFull set ReportMarrowIndexID=@mmid,@mmid=@mmid+1
alter table ReportMarrowFull add constraint ReportMarrowIndexID primary key (ReportMarrowIndexID)
