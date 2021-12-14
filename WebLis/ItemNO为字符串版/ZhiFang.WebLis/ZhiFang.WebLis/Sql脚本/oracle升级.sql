alter Table  reportformfull modify FORMNO varchar(30);
alter Table  reportformfull modify PRINTEXEC varchar(6);
alter Table  reportformfull modify PRINTTEXEC varchar(6);
alter Table  reportitemfull modify FORMNO varchar(30);
alter Table  reportitemfull modify SECTIONNAME varchar(30);
alter table reportformfull drop column WEBLISSOURCEORGNAME;

create sequence B_GenderTypeControl_ID
minvalue 1
maxvalue 999999
start with 1
increment by 1
cache 20


Create or REPLACE TRIGGER TB_GenderTypeControl_ID BEFORE
insert ON B_GenderTypeControl FOR EACH ROW
begin
select B_GenderTypeControl_ID.nextval into:New.ID from Dual;
end;