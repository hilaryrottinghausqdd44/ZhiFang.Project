   IF COL_LENGTH('Blood_Recei', 'BReqTypeID') IS NULL ALTER TABLE Blood_Recei ADD BReqTypeID nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'AboReportDescImage') IS NULL ALTER TABLE Blood_Recei ADD AboReportDescImage image;
                

 IF COL_LENGTH('Blood_Recei', 'RhReportDescImage') IS NULL ALTER TABLE Blood_Recei ADD RhReportDescImage image;
                

 IF COL_LENGTH('Blood_Recei', 'AboReportDescDemoNoList') IS NULL ALTER TABLE Blood_Recei ADD AboReportDescDemoNoList nvarchar(50);
                

 IF COL_LENGTH('Blood_Recei', 'AboReportDescDemoNameList') IS NULL ALTER TABLE Blood_Recei ADD AboReportDescDemoNameList nvarchar(200);
                

 IF COL_LENGTH('Blood_Recei', 'RhReportDescDemoNoList') IS NULL ALTER TABLE Blood_Recei ADD RhReportDescDemoNoList nvarchar(50);
                

 IF COL_LENGTH('Blood_Recei', 'RhReportDescDemoNameList') IS NULL ALTER TABLE Blood_Recei ADD RhReportDescDemoNameList nvarchar(200);
                

 IF COL_LENGTH('Blood_Recei', 'srResult1Image') IS NULL ALTER TABLE Blood_Recei ADD srResult1Image image;
                

 IF COL_LENGTH('Blood_Recei', 'srResult2Image') IS NULL ALTER TABLE Blood_Recei ADD srResult2Image image;
                

 IF COL_LENGTH('Blood_Recei', 'srResult1DemoNoList') IS NULL ALTER TABLE Blood_Recei ADD srResult1DemoNoList nvarchar(50);
                

 IF COL_LENGTH('Blood_Recei', 'SrResult1DemoNameList') IS NULL ALTER TABLE Blood_Recei ADD SrResult1DemoNameList nvarchar(200);
                

 IF COL_LENGTH('Blood_Recei', 'SrResult2DemoNoList') IS NULL ALTER TABLE Blood_Recei ADD SrResult2DemoNoList nvarchar(50);
                

 IF COL_LENGTH('Blood_Recei', 'SrResult2DemoNameList') IS NULL ALTER TABLE Blood_Recei ADD SrResult2DemoNameList nvarchar(200);
                


 IF COL_LENGTH('Blood_Recei', 'NurseSender') IS NULL ALTER TABLE Blood_Recei ADD NurseSender nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'NurseSendTime') IS NULL ALTER TABLE Blood_Recei ADD NurseSendTime nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'InceptID') IS NULL ALTER TABLE Blood_Recei ADD InceptID nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'incepter') IS NULL ALTER TABLE Blood_Recei ADD incepter nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'incepter') IS NULL ALTER TABLE Blood_Recei ADD incepter nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'inceptTime') IS NULL ALTER TABLE Blood_Recei ADD inceptTime nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'ZDY1') IS NULL ALTER TABLE Blood_Recei ADD ZDY1 nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'ZDY2') IS NULL ALTER TABLE Blood_Recei ADD ZDY2 nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'ZDY3') IS NULL ALTER TABLE Blood_Recei ADD ZDY3 nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'ZDY4') IS NULL ALTER TABLE Blood_Recei ADD ZDY4 nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'ZDY5') IS NULL ALTER TABLE Blood_Recei ADD ZDY5 nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'ReViewAboReportDescDemo') IS NULL ALTER TABLE Blood_Recei ADD ReViewAboReportDescDemo nvarchar(100);
                

 IF COL_LENGTH('Blood_Recei', 'ReViewRhReportDescDemo') IS NULL ALTER TABLE Blood_Recei ADD ReViewRhReportDescDemo nvarchar(20);
                

 IF COL_LENGTH('Blood_Recei', 'IFlag1') IS NULL ALTER TABLE Blood_Recei ADD IFlag1 nvarchar(5);
                

 IF COL_LENGTH('Blood_Recei', 'IFlag2') IS NULL ALTER TABLE Blood_Recei ADD IFlag2 nvarchar(5);
                

 IF COL_LENGTH('Blood_Recei', 'IFlag3') IS NULL ALTER TABLE Blood_Recei ADD IFlag3 nvarchar(5);
                

 IF COL_LENGTH('Blood_Recei', 'IFlag4') IS NULL ALTER TABLE Blood_Recei ADD IFlag4 nvarchar(5);
                

 IF COL_LENGTH('Blood_Recei', 'IFlag5') IS NULL ALTER TABLE Blood_Recei ADD IFlag5 nvarchar(5);

 alter table PUser alter column ShortCode varchar(20); 

IF COL_LENGTH('Department', 'ParentID') IS NULL ALTER TABLE Department ADD ParentID int;

                