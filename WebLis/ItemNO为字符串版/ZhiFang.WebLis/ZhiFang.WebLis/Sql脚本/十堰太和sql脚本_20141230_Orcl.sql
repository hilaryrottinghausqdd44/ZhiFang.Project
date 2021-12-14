----以下是脚本更新，共4个脚本

---1.创建表
create TABLE BARCODESEQ(
  LABCODE varchar2(50) NOT NULL,
  LASTNUM number(4) NOT NULL,
  "DATE" date
) 

---2.增加字段

alter table barcodeform add color varchar(20)--中增加"颜色"字段
 alter table barcodeform add ItemName varchar(200)  --增加"细项项目名称"字段
 alter table barcodeform add ItemNo varchar(200) --增加"细项项目编号"字段
 alter table nrequestform add printTimes int  --增加"打印次数"字段
 alter table nrequestform add barcode varchar(200) --增加"条码"字段
 alter table nrequestform add CombiItemName varchar(200) --增加"组合名称"字段
alter table barcodeform add SampleTypeName varchar(200) --增加"样本名称"字段
alter table NRequestForm add price float--中增加"打印次数"字段

----存储过程脚本

create or replace procedure P_GetMaxBarCodeSeq
(
LabCode  varchar2,
OperDate varchar2,
SN  out varchar2
)
is
  returnvalue varchar2(50):='True';
  counts varchar2(150):='';
  newOperDate date:=sysdate;
begin
if (OperDate is null)
then newOperDate:=to_char(sysdate);
else
 newOperDate:=to_char(to_date(OperDate,'yyyy-mm-dd'));
 end if;

 begin
   --dbms_output.put_line(newOperDate);
    execute immediate 'select count(*) from barCodeSeq where LabCode='||LabCode||' and "DATE"='''||newOperDate||'''' into counts;
 if(counts>0) 
  then  execute immediate 'update barCodeSeq set barCodeSeq.lastNum = barCodeSeq.lastNum+1 where barCodeSeq.labcode='||LabCode||' and "DATE"='''||newOperDate||''' and rownum =1';
     if (sqlcode<>0)
       then returnvalue:='FALSE';
       execute immediate 'select substr(10000+ cast(LastNum as char(50)),-3,3) from barCodeSeq where barCodeSeq.labcode='||LabCode||' and "DATE"='''||newOperDate||''' and rownum =1 order by lastNum asc ' into SN;
       else
         execute immediate 'select substr(10000+ cast(LastNum as char(50)),-3,3) from barCodeSeq where barCodeSeq.labcode='||LabCode||' and "DATE"='''||newOperDate||''' and rownum =1 order by lastNum asc ' into SN;       
      end if;
  else
    execute immediate 'insert into barCodeSeq (labcode,lastNum,"DATE") values('||LabCode||',1,'''||newOperDate||''')';
    if sqlcode<>0
      then  returnvalue:='FALSE';
      execute immediate 'select  substr(10000+ cast(LastNum as char(50)),-3,3) a from barCodeSeq where  "DATE"='''||newOperDate||''' and barCodeSeq.labcode='||LabCode||' and rownum =1 order by lastNum asc ' into SN;
  else
 execute immediate 'select  substr(10000+ cast(LastNum as char(50)),-3,3) a from barCodeSeq where  "DATE"='''||newOperDate||''' and barCodeSeq.labcode='||LabCode||' and rownum =1 order by lastNum asc ' into SN;
   end if;
  end if;
  if upper(returnvalue) = 'TRUE'
      THEN commit;
  else
      rollback;
     end if;
 end;
 end;



----创建条码打印参数配置表
CREATE TABLE LocationbarCodePrintPamater(
  ID number(19) NOT NULL,
  ACCOUNTID varchar(30) NOT NULL,
  PARAMETER nclob NULL,
  CREATEDATETIME date NULL,
  UPDATEDATETIME date NULL,
  TIMESTAMP raw(8) NOT NULL
)

-----新建颜色字典表

CREATE TABLE ItemColorDict(
  ColorID number(19)  NOT NULL,
  ColorName varchar2(15) NULL,
  ColorValue varchar2(15) NULL
)

create sequence ITEMCOLORDICT_COLORID
minvalue 1
maxvalue 999999
start with 1
increment by 1
cache 20

CREATE OR REPLACE TRIGGER  ITEMCOLORDICT_COLORID BEFORE
insert ON ITEMCOLORDICT FOR EACH ROW
begin
select ITEMCOLORDICT.nextval into:New.COLORID from Dual;

end;

-------  性别字典表触发器
create sequence B_LAB_GENDERTYPE_GENDERID
minvalue 1
maxvalue 999999
start with 1
increment by 1
cache 20

CREATE OR REPLACE TRIGGER B_LAB_GENDERTYPE_GENDERID BEFORE
insert ON B_LAB_GENDERTYPE FOR EACH ROW
begin
select B_LAB_GENDERTYPE_GENDERID.nextval into:New.GENDERID from Dual;

end;




/*==============================================================*/
/* Table: "ItemColorAndSampleTypeDetail"                        */
/*==============================================================*/
create table ITEMCOLORANDSAMPLETYPEDETAIL
(
   COLORID            INTEGER              not null,
   SAMPLETYPENO       INTEGER              not null
)