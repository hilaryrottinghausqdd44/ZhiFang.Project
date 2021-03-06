
--增加定制时间查询项
INSERT [dbo].[B_SearchUnit] ([SID], [CName], [SelectName], [Type], [TextWidth], [Width], [DataAddTime], [DataUpdateTime], [Xtype], [Mark], [Listeners], [JsCode]) VALUES (1000, N'时间', N'时间', NULL, NULL, NULL, NULL, NULL, N'combobox', NULL, NULL, N'{
                type: ''other'', 
								xtype: ''combobox'',
                name: ''defaultPageSize'',
                itemId: ''defaultPageSize'',
                width: 130,
                displayField: ''text'', 
valueField: ''value'',
store:Ext.create(''Ext.data.Store'', {
	                    fields: [''text'', ''value''],
	                    data: [
                            { text: ''采样日期'', value: ''COLLECTDATE'' },
                            { text: ''录入（操作）日期'', value: ''OPERDATE'' }
	                    ]
	                }),
                value: [''OPERDATE''],
                listeners: {
                    change: function (m, v) {
                        var selectDate = this.ownerCt.ownerCt.getItem(''selectdate'');
                        selectDate.name = v;
                        this.ownerCt.ownerCt.DateField = v;
                    }
                }
            }searchAndNext
		    { type: ''search'', xtype: ''uxdatearea'', itemId: ''selectdate'', name: ''OPERDATE'', labelWidth: 0, width: 190 }
')

GO


--修改查询ReportFormFull表存储过程
ALTER VIEW [dbo].[ReportFormQueryDataSource]
AS
SELECT  ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, SectionName, TestTypeName, SampleTypeNo, 
                   SampletypeName, SecretType, PatNo, CName, InpatientNo, PatCardNo, GenderNo, GenderName, Age, AgeUnitNo, 
                   AgeUnitName, Birthday, DistrictNo, DistrictName, WardNo, WardName, Bed, DeptNo, DeptName, Doctor, SerialNo, 
                   ParitemName, Collecter, CollectDate, CollectTime, Incepter, InceptDate, InceptTime, Technician, TestDate, TestTime, 
                   Operator, OperDate, OperTime, Checker, CheckDate, CheckTime, FormComment, FormMemo, SickTypeNo, SickTypeName, 
                   DiagNo, DiagName, ClientNo, ClientName, Sender2, PrintTimes, ClientPrint, PrintOper, PrintDateTime, PrintOper1, 
                   PrintDateTime1, resultsend, reportsend, PageName, PageCount, ZDY1, ZDY2, ZDY3, ZDY4, ZDY5, ZDY6, ZDY7, ZDY8, 
                   ReportFormID AS formno, SecretType AS SectionType, LabID, DataAddTime, DataUpdateTime, DataMigrationTime, 
                   DataTimeStamp, MainTesterId, PatientID, ExaminerId, CollectPart, ReportPublicationID AS ReportFormID, ActiveFlag, 
                   DoctorItemName AS ItemName, '' AS ZDY15, '' AS PinYinMa, NULL AS AffirmTime, NULL AS arrivetime, NULL 
                   AS NurseSender, NULL AS NurseSendTime, NULL AS NurseSendCarrier, NULL AS ReceiveTime, NULL AS OrderTime, 
                   NULL AS AgeDesc, NULL AS State,null as ReportStatus
FROM      dbo.ReportFormFull
WHERE   (ActiveFlag = 1)

GO

---修改微生物存储过程
ALTER VIEW [dbo].[ReportMicroQueryDataSource]
AS


select * from
(
SELECT     LabID, ReportPublicationID, ReportFormID AS RFID, OrderNo, ReceiveDate, SectionNo, TestTypeNo, SampleNo, ItemNo, ItemCname, ItemEname, DescNo, 
                      TPF3 AS DescName, MicroStepID, MicroStepName, ResultID, ReportValue, MicroNo, MicroName, MicroEame, MicroDesc, MicroResultDesc, ItemDesc, AntiNo, 
                      AntiName, AntiEName, Suscept, SusQuan, SusDesc, AntiUnit, RefRange, ResultState, EquipNo, EquipName, AntiGroupType, MethodName, SerumcenTration, 
                      EmictioncenTration, Microdisplayid, Antidisplayid, CheckType, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp, 
                      ReportPublicationID AS ReportFormID, DSTType, PYJDF7, ResistancePhenotypeName AS RPName,
					  (select TPF3 From ReportMicroFull rm where rm.ReportPublicationID = ReportMicroFull.ReportPublicationID and TPF9 is null and TPF3 is not Null and MicroNo is NULL) as TPF3,TPF9,
					  STUFF((SELECT ','+CONVERT(VARCHAR(100), MicroName, 111) FROM ReportMicroFull aa WHERE  aa.ReportPublicationID = ReportMicroFull.ReportPublicationID Group by MicroName FOR XML PATH('')),1,1,'') as DrugAllergyResult
					  ,pAntiGroupType,AntiNote,comment
FROM         dbo.ReportMicroFull where  TPF3 is NULL and TPF9 is not null  and MicroNo is NULL  

union all

SELECT     LabID, ReportPublicationID, ReportFormID AS RFID, OrderNo, ReceiveDate, SectionNo, TestTypeNo, SampleNo, ItemNo, ItemCname, ItemEname, DescNo, 
                      TPF3 AS DescName, MicroStepID, MicroStepName, ResultID, ReportValue, MicroNo, MicroName, MicroEame, MicroDesc, MicroResultDesc, ItemDesc, AntiNo, 
                      AntiName, AntiEName, Suscept, SusQuan, SusDesc, AntiUnit, RefRange, ResultState, EquipNo, EquipName, AntiGroupType, MethodName, SerumcenTration, 
                      EmictioncenTration, Microdisplayid, Antidisplayid, CheckType, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp, 
                      ReportPublicationID AS ReportFormID, DSTType, PYJDF7, ResistancePhenotypeName AS RPName,
					  TPF3, TPF9,
					  STUFF((SELECT ','+CONVERT(VARCHAR(100), MicroName, 111) FROM ReportMicroFull aa WHERE  aa.ReportPublicationID = ReportMicroFull.ReportPublicationID Group by MicroName FOR XML PATH('')),1,1,'') as DrugAllergyResult
					  ,pAntiGroupType,AntiNote,comment
FROM         dbo.ReportMicroFull where  TPF3 is not NULL and TPF9 is  null  and MicroNo is NULL  
union all

SELECT     LabID, ReportPublicationID, ReportFormID AS RFID, OrderNo, ReceiveDate, SectionNo, TestTypeNo, SampleNo, ItemNo, ItemCname, ItemEname, DescNo, 
                      TPF3 AS DescName, MicroStepID, MicroStepName, ResultID, ReportValue, MicroNo, MicroName, MicroEame, MicroDesc, MicroResultDesc, ItemDesc, AntiNo, 
                      AntiName, AntiEName, Suscept, SusQuan, SusDesc, AntiUnit, RefRange, ResultState, EquipNo, EquipName, AntiGroupType, MethodName, SerumcenTration, 
                      EmictioncenTration, Microdisplayid, Antidisplayid, CheckType, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp, 
                      ReportPublicationID AS ReportFormID, DSTType, PYJDF7, ResistancePhenotypeName AS RPName,
					  TPF3,TPF9, null as DrugAllergyResult,pAntiGroupType,AntiNote,comment
FROM         dbo.ReportMicroFull where  TPF3 is NULL and TPF9 is NULL and MicroNo is not NULL and AntiNo is null 

union all


SELECT     LabID, ReportPublicationID, ReportFormID AS RFID, OrderNo, ReceiveDate, SectionNo, TestTypeNo, SampleNo, ItemNo, ItemCname, ItemEname, DescNo, 
                      TPF3 AS DescName, MicroStepID, MicroStepName, ResultID, ReportValue, MicroNo, MicroName, MicroEame, MicroDesc, MicroResultDesc, ItemDesc, AntiNo, 
                      AntiName, AntiEName, Suscept, SusQuan, SusDesc, AntiUnit, RefRange, ResultState, EquipNo, EquipName, AntiGroupType, MethodName, SerumcenTration, 
                      EmictioncenTration, Microdisplayid, Antidisplayid, CheckType, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp, 
                      ReportPublicationID AS ReportFormID, DSTType, PYJDF7, ResistancePhenotypeName AS RPName,
					  TPF3,TPF9, STUFF((SELECT ','+CONVERT(VARCHAR(100), MicroName, 111) FROM ReportMicroFull aa WHERE  aa.ReportPublicationID = ReportMicroFull.ReportPublicationID Group by MicroName FOR XML PATH('')),1,1,'') as DrugAllergyResult,pAntiGroupType,AntiNote,comment
FROM         dbo.ReportMicroFull where  TPF3 is NULL and TPF9 is NULL and MicroNo is not NULL and AntiNo is Not null 

)ReportMicroFull

GO



