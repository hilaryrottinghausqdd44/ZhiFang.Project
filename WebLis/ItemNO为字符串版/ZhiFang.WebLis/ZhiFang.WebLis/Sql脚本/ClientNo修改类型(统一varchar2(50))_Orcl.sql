alter table PGroupPrint modify CLIENTNO varchar2(50);

alter table ClientEleArea modify CLIENTNO varchar2(50);

alter table TB_CheckClientAccount modify CLIENTNO varchar2(50);

alter table B_CLIENTELEControl modify CLIENTNO varchar2(50);

alter table CLIENTELE add key_NO int; --添加字段
update CLIENTELE set key_NO =CLIENTNO; --给新字段赋值 
alter table CLIENTELE drop column CLIENTNO; --删除老字段
alter table CLIENTELE add CLIENTNO varchar2(50);--重建老字段
update CLIENTELE set CLIENTNO =key_NO; --给老字段赋值
ALTER TABLE CLIENTELE ADD (CONSTRAINT CLIENTELE PRIMARY KEY(CLIENTNO)); --设置主键
alter table CLIENTELE drop column key_NO; --删除新增字段

alter table CLIENTELE add Doctor nvarchar2(2000);

--ALTER TABLE CLIENTELE DROP CONSTRAINT PK_CLIENTELE; --删除主键
--alter table CLIENTELE   modify CLIENTNO int not null;  --可为空
--update CLIENTELE set CLIENTNO='';
--alter table CLIENTELE modify CLIENTNO varchar2(50); --修改主键类型