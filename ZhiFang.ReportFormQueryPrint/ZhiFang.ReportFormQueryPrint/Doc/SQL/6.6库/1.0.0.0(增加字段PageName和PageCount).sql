---郭海祥 2019-10-22
---增加表字段PageName,PageCount


print '增加ReportForm表字段PageName,PageCount'
    
if not exists(select * from syscolumns where id=object_id('ReportForm') and name='PageName') 
begin
    Alter Table ReportForm add PageName varchar(10)
end

if not exists(select * from syscolumns where id=object_id('ReportForm') and name='PageCount')
 begin
	Alter Table ReportForm add PageCount varchar(10)
 end
go

print '增加RequestForm表字段PageName,PageCount'
    
if not exists(select * from syscolumns where id=object_id('RequestForm') and name='PageName') 
begin
    Alter Table RequestForm add PageName varchar(10)
end

if not exists(select * from syscolumns where id=object_id('RequestForm') and name='PageCount')
 begin
	Alter Table RequestForm add PageCount varchar(10)
 end
go

print '增加BackupRForm表字段PageName,PageCount'
    
if not exists(select * from syscolumns where id=object_id('BackupRForm') and name='PageName') 
begin
    Alter Table BackupRForm add PageName varchar(10)
end

if not exists(select * from syscolumns where id=object_id('BackupRForm') and name='PageCount')
 begin
	Alter Table BackupRForm add PageCount varchar(10)
 end
go