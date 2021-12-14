CREATE VIEW [dbo].[ReportFormAllQueryDataSource]
AS
SELECT   dbo.ReportFormFull.LabID, dbo.ReportFormFull.ReportPublicationID AS ReportFormID, 
                dbo.ReportFormFull.ReportFormID AS RFID, dbo.ReportFormFull.ReceiveDate, dbo.ReportFormFull.SectionNo, 
                dbo.ReportFormFull.TestTypeNo, dbo.ReportFormFull.SampleNo, dbo.ReportFormFull.SectionName, 
                dbo.ReportFormFull.TestTypeName, dbo.ReportFormFull.SampleTypeNo, dbo.ReportFormFull.SampletypeName, 
                dbo.ReportFormFull.SecretType, dbo.ReportFormFull.PatientID, dbo.ReportFormFull.PatNo, dbo.ReportFormFull.CName, 
                dbo.ReportFormFull.InpatientNo, dbo.ReportFormFull.PatCardNo, dbo.ReportFormFull.GenderNo, 
                dbo.ReportFormFull.GenderName, dbo.ReportFormFull.Age, dbo.ReportFormFull.AgeUnitNo, 
                dbo.ReportFormFull.AgeUnitName, dbo.ReportFormFull.Birthday, dbo.ReportFormFull.DistrictNo, 
                dbo.ReportFormFull.DistrictName, dbo.ReportFormFull.WardNo, dbo.ReportFormFull.WardName, 
                dbo.ReportFormFull.Bed, dbo.ReportFormFull.DeptNo, dbo.ReportFormFull.DeptName, dbo.ReportFormFull.DoctorID, 
                dbo.ReportFormFull.Doctor, dbo.ReportFormFull.SerialNo, dbo.ReportFormFull.ParitemName AS DoctorParitemName, 
                dbo.ReportFormFull.DoctorItemName, dbo.ReportFormFull.Collecter, dbo.ReportFormFull.CollectDate, 
                dbo.ReportFormFull.CollectTime, dbo.ReportFormFull.Incepter, dbo.ReportFormFull.InceptDate, 
                dbo.ReportFormFull.InceptTime, dbo.ReportFormFull.MainTesterId, dbo.ReportFormFull.Technician, 
                dbo.ReportFormFull.TestDate, dbo.ReportFormFull.TestTime, dbo.ReportFormFull.Operator, 
                dbo.ReportFormFull.OperDate, dbo.ReportFormFull.OperTime, dbo.ReportFormFull.ExaminerId, 
                dbo.ReportFormFull.Checker, dbo.ReportFormFull.CheckDate, dbo.ReportFormFull.CheckTime, 
                dbo.ReportFormFull.FormComment, dbo.ReportFormFull.FormMemo, dbo.ReportFormFull.SickTypeNo, 
                dbo.ReportFormFull.SickTypeName, dbo.ReportFormFull.DiagNo, dbo.ReportFormFull.DiagName, 
                dbo.ReportFormFull.ClientNo, dbo.ReportFormFull.ClientName, dbo.ReportFormFull.Sender2, 
                dbo.ReportFormFull.PrintTimes, dbo.ReportFormFull.ClientPrint, dbo.ReportFormFull.PrintOper, 
                dbo.ReportFormFull.PrintDateTime, dbo.ReportFormFull.PrintOper1, dbo.ReportFormFull.PrintDateTime1, 
                dbo.ReportFormFull.resultsend, dbo.ReportFormFull.reportsend, dbo.ReportFormFull.ActiveFlag, 
                dbo.ReportFormFull.AllFlag, dbo.ReportFormFull.CollectPart, dbo.ReportFormFull.TestAim, 
                dbo.ReportFormFull.PageName, dbo.ReportFormFull.PageCount, dbo.ReportFormFull.ReceiveTime, 
                dbo.ReportFormFull.ZDY1, dbo.ReportFormFull.ZDY2, dbo.ReportFormFull.ZDY3, dbo.ReportFormFull.ZDY4, 
                dbo.ReportFormFull.ZDY5, dbo.ReportFormFull.ZDY6, dbo.ReportFormFull.ZDY7, dbo.ReportFormFull.ZDY8, 
                dbo.ReportFormFull.ZDY9, dbo.ReportFormFull.ZDY10, dbo.ReportFormFull.DataAddTime, 
                dbo.ReportFormFull.DataUpdateTime, dbo.ReportFormFull.DataMigrationTime, dbo.ReportFormFull.DataTimeStamp, 
                dbo.ReportFormFull.STestType, dbo.ReportFormFull.FormComment2, dbo.ReportFormFull.SectionResultType, 
                dbo.ReportFormFull.Sendertime2, dbo.ReportItemFull.OrderNo, dbo.ReportItemFull.ParItemNo, 
                dbo.ReportItemFull.ItemNo, dbo.ReportItemFull.ParitemName, dbo.ReportItemFull.ItemCname, 
                dbo.ReportItemFull.ItemEname, dbo.ReportItemFull.ReportValue, dbo.ReportItemFull.ReportDesc, 
                dbo.ReportItemFull.ItemValue, dbo.ReportItemFull.ItemUnit, dbo.ReportItemFull.ResultStatus, 
                dbo.ReportItemFull.RefRange, dbo.ReportItemFull.EquipNo, dbo.ReportItemFull.EquipName, 
                dbo.ReportItemFull.DiagMethod, dbo.ReportItemFull.Prec, dbo.ReportItemFull.StandardCode, 
                dbo.ReportItemFull.ItemDesc, dbo.ReportItemFull.SecretGrade, dbo.ReportItemFull.Visible, 
                dbo.ReportItemFull.DefaultReagent, dbo.ReportItemFull.ResultStatusDesc,
                    (SELECT   TOP (1) FilePath
                     FROM      dbo.PUser
                     WHERE   (CName = dbo.ReportFormFull.Collecter) AND (CName IS NOT NULL) AND (CName <> '')) 
                AS CollecterImageFilePath,
                    (SELECT   TOP (1) FilePath
                     FROM      dbo.PUser AS PUser_4
                     WHERE   (CName = dbo.ReportFormFull.Incepter) AND (CName IS NOT NULL) AND (CName <> '')) 
                AS IncepterImageFilePath,
                    (SELECT   TOP (1) FilePath
                     FROM      dbo.PUser AS PUser_3
                     WHERE   (CName = dbo.ReportFormFull.Technician) AND (CName IS NOT NULL) AND (CName <> '')) 
                AS TechnicianImageFilePath,
                    (SELECT   TOP (1) FilePath
                     FROM      dbo.PUser AS PUser_2
                     WHERE   (CName = dbo.ReportFormFull.Operator) AND (CName IS NOT NULL) AND (CName <> '')) 
                AS OperatorImageFilePath,
                    (SELECT   TOP (1) FilePath
                     FROM      dbo.PUser AS PUser_1
                     WHERE   (CName = dbo.ReportFormFull.Checker) AND (CName IS NOT NULL) AND (CName <> '')) 
                AS CheckerImageFilePath
FROM      dbo.ReportFormFull INNER JOIN
                dbo.ReportItemFull ON dbo.ReportFormFull.ReportPublicationID = dbo.ReportItemFull.ReportPublicationID




