
alter table PGroupPrint alter column ClientNo bigint

ALTER TABLE CLIENTELE DROP CONSTRAINT  PK_CLIENTELE  --删除主键
ALTER TABLE CLIENTELE ALTER COLUMN CLIENTNO bigint NOT NULL  --修改类型
ALTER TABLE CLIENTELE ADD CONSTRAINT PK_CLIENTELE PRIMARY KEY CLUSTERED(CLIENTNO) --设置主键

alter table ClientEleArea alter column ClientNo bigint

alter table TB_CheckClientAccount alter column ClientNo bigint

alter table B_CLIENTELEControl alter column ClientNo bigint



