alter table PGroupPrint modify CLIENTNO varchar2(50);

alter table ClientEleArea modify CLIENTNO varchar2(50);

alter table TB_CheckClientAccount modify CLIENTNO varchar2(50);

alter table B_CLIENTELEControl modify CLIENTNO varchar2(50);

alter table CLIENTELE add key_NO int; --����ֶ�
update CLIENTELE set key_NO =CLIENTNO; --�����ֶθ�ֵ 
alter table CLIENTELE drop column CLIENTNO; --ɾ�����ֶ�
alter table CLIENTELE add CLIENTNO varchar2(50);--�ؽ����ֶ�
update CLIENTELE set CLIENTNO =key_NO; --�����ֶθ�ֵ
ALTER TABLE CLIENTELE ADD (CONSTRAINT CLIENTELE PRIMARY KEY(CLIENTNO)); --��������
alter table CLIENTELE drop column key_NO; --ɾ�������ֶ�

alter table CLIENTELE add Doctor nvarchar2(2000);

--ALTER TABLE CLIENTELE DROP CONSTRAINT PK_CLIENTELE; --ɾ������
--alter table CLIENTELE   modify CLIENTNO int not null;  --��Ϊ��
--update CLIENTELE set CLIENTNO='';
--alter table CLIENTELE modify CLIENTNO varchar2(50); --�޸���������