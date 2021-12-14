--USE [digitlab_bs]
GO

/****** Object:  View [dbo].[GK_ToLis_ItemsFormView]    Script Date: 2020-12-28 15:48:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[GK_ToLis_ItemsFormView]
AS
SELECT dbo.SC_RecordDtl.LabID,dbo.SC_RecordDtl.BObjectID,dbo.GK_SampleRequestForm.ReqDocNo,dbo.SC_RecordDtl.ContentTypeID,dbo.SC_RecordDtl.RecordTypeID,dbo.SC_RecordItemLink.TestItemCode,dbo.GK_SampleRequestForm.BarCode,dbo.GK_SampleRequestForm.ReqDocId,dbo.SC_RecordDtl.RecordDtlId, dbo.SC_RecordDtl.RecordDtlNo, dbo.SC_RecordDtl.RecordTypeItemID,dbo.SC_RecordDtl.DispOrder, dbo.SC_RecordDtl.DataAddTime, dbo.SC_RecordDtl.DataTimeStamp,dbo.SC_RecordDtl.NumberItemResult,
 --透析液及透析用水的检验项目结果值处理
 case when SC_RecordDtl.RecordTypeItemID =120010 then '0'
 else SC_RecordDtl.ItemResult
     end as 'ItemResult',dbo.SC_RecordDtl.Memo
FROM  dbo.SC_RecordDtl LEFT  JOIN dbo.GK_SampleRequestForm ON dbo.SC_RecordDtl.BObjectID = dbo.GK_SampleRequestForm.ReqDocId
 LEFT JOIN dbo.SC_RecordItemLink ON dbo.SC_RecordDtl.RecordTypeItemID = dbo.SC_RecordItemLink.RecordTypeItemID 
and dbo.SC_RecordDtl.RecordTypeID = dbo.SC_RecordItemLink.RecordTypeID where LEN(dbo.SC_RecordDtl.TestItemCode)>0 and SC_RecordItemLink.TestItemCode is not null;




GO


