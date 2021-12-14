
IF OBJECT_ID ('dbo.View_NRequestItem', 'V') IS Not NULL
begin
  DROP VIEW dbo.View_NRequestItem
end
GO

CREATE VIEW [dbo].[View_NRequestItem]
AS
SELECT     
    NRI.NRequestItemNo, NRI.NRequestFormNo, NRI.BarCodeFormNo, BF.BarCode, 
	BF.CollectDate, BF.CollectTime, NRF.OperDate, NRF.Operator, NRI.ParItemNo, 
	NRI.ItemPrice, NRI.TransitFlag, NRI.ModifyFlag, NRI.ModifyDate, 
	NRI.zdy1, NRI.zdy2, NRI.zdy3, NRI.zdy4, NRI.zdy5, NRI.OldSerialNo, 
	NRI.SectionNo1 AS SectionNo, NRI.SectionName, NRI.TestDate, NRI.TestDater, 
	NRI.SenderTime1, NRI.SenderTime1er, NRI.SenderTime2, NRI.SenderTime2er, 
	NRI.CheckDate, NRI.CheckDater, NRI.ReceiveDate, NRI.ReceiveDater, NRI.DeleteFlag, 
	NRF.SerialNo, dbo.F_GetBarCodeItemState(NRI.BarCodeFormNo)as SampleState,
	SSP.SendPlaceNo, SSP.CName 
FROM  dbo.NRequestItem AS NRI 
INNER JOIN dbo.NRequestForm AS NRF ON NRI.NRequestFormNo = NRF.NRequestFormNo 
INNER JOIN dbo.BarCodeForm AS BF ON NRI.BarCodeFormNo = BF.BarCodeFormNo
LEFT  JOIN dbo.SampleSendBarCode SSB ON NRI.BarCodeFormNo = SSB.BarCodeFormNo
LEFT  JOIN SampleSendList SSLT ON SSB.SampleSendNo = SSLT.SampleSendNo
LEFT  JOIN dbo.SampleSendPlace SSP ON SSLT.SendPlaceNo = SSP.SendPlaceNo
WHERE (NRI.DeleteFlag = 0 or NRI.DeleteFlag = 2)

GO




