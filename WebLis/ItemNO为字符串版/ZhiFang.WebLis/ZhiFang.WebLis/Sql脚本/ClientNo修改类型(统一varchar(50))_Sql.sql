
alter table PGroupPrint alter column ClientNo bigint

ALTER TABLE CLIENTELE DROP CONSTRAINT  PK_CLIENTELE  --ɾ������
ALTER TABLE CLIENTELE ALTER COLUMN CLIENTNO bigint NOT NULL  --�޸�����
ALTER TABLE CLIENTELE ADD CONSTRAINT PK_CLIENTELE PRIMARY KEY CLUSTERED(CLIENTNO) --��������

alter table ClientEleArea alter column ClientNo bigint

alter table TB_CheckClientAccount alter column ClientNo bigint

alter table B_CLIENTELEControl alter column ClientNo bigint



