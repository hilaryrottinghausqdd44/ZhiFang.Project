

/****** Object:  View [dbo].[GK_ToLis_DocFormView]    Script Date: 2020-12-28 11:38:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[GK_ToLis_DocFormView]
AS
SELECT  ReqDocId, ReqDocNo, MonitorType, DeptId, DeptCName, SamplerId, Sampler, SampleDate, SampleTime, dbo.GK_SampleRequestForm.RecordTypeID, 
        StatusID, dbo.GK_SampleRequestForm.CName, BarCode, SampleNo, IsAutoReceive, ReceiveFlag, ReceiveDate, ResultFlag, CreatorID, CreatorName, 
        dbo.GK_SampleRequestForm.DispOrder, dbo.GK_SampleRequestForm.Memo, dbo.GK_SampleRequestForm.DataAddTime, dbo.GK_SampleRequestForm.DataTimeStamp,
        SC_RecordType.SampleTypeCode,SC_RecordType.TestItemCode 
FROM    dbo.GK_SampleRequestForm left join  SC_RecordType on dbo.GK_SampleRequestForm.RecordTypeID=SC_RecordType.RecordTypeID



GO


