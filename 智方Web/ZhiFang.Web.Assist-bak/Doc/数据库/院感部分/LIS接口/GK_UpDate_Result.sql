
GO
/****** Object:  StoredProcedure [dbo].[GK_UpDate_Result]    Script Date: 2020-12-28 11:38:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[GK_UpDate_Result]  --更新院感结果
(
 @groupsampleformid bigint
 )         
AS  
Begin  

--先判断是否是感控培养小组，如果不是，就退出
Begin
 declare @sectionNo int;
 set @sectionNo=-999999;
 select top 1 @sectionNo=SectionNo from ME_GroupSampleForm where GroupSampleFormID =@groupsampleformid;
 if(@sectionNo<>62) return;
END

--取检验单的结果项信息
declare @ReqDocId nvarchar(50),@GSampleNo nvarchar(50),@ExaminerId nvarchar(50),@Examiner nvarchar(50),@ExamineDate nvarchar(50),@RecordDtlId nvarchar(50),@TestItemCode varchar(20),@ReportValue varchar(500),@Units nvarchar(500);

--申请主单取到的检验结果信息
declare @ReqDocId2 nvarchar(50),@SampleNo nvarchar(50),@TestResult nvarchar(500),@BacteriaTotal nvarchar(500),@CheckId nvarchar(50),@CheckCName nvarchar(50),@CheckDate nvarchar(50);

--明细记录项是否特殊项目
declare @isHasItems bit;
set @isHasItems=0;

-- 定义游标.
DECLARE c_foreign_keys CURSOR FAST_FORWARD FOR

with GK_Result as
(
select gi.ZDY1 as ReqDocId,gi.ZDY4 as GBarCode, gf.GSampleNo,gf.ExaminerId,gf.Examiner,gf.ExamineDate,gi.ResultID,gi.ZDY5 as RecordDtlId,gi.itemno as TestItemCode,gi.ReportValue,gi.Units from ME_GroupSampleForm gf left join ME_GroupSampleItem gi on gf.GroupSampleFormID=gi.GroupSampleFormID where gf.GroupSampleFormID =@groupsampleformid
)

select ReqDocId,GSampleNo,ExaminerId,Examiner,ExamineDate,RecordDtlId,TestItemCode,ReportValue,Units FROM GK_Result --查询临时表

-- 打开游标.
OPEN c_foreign_keys 

-- 取第一条记录
FETCH NEXT FROM c_foreign_keys INTO @ReqDocId,@GSampleNo,@ExaminerId,@Examiner,@ExamineDate,@RecordDtlId,@TestItemCode,@ReportValue,@Units

--循环
WHILE @@FETCH_STATUS=0
	BEGIN

	set @ReqDocId2=@ReqDocId;
	set @SampleNo=@GSampleNo;
	set @CheckId =@ExaminerId;
	set @CheckCName=@Examiner;
	set @CheckDate=@ExamineDate;

	--按申请明细主键更新申请明细记录项的检验结果
	if(@RecordDtlId<>'')
		BEGIN
			update SC_RecordDtl set ItemResult=@ReportValue+' '+ @Units where RecordDtlId=@RecordDtlId;
		END

	--如果检验明细包含以下项目：601040:菌落总数;601041:菌落总数;601042:细菌数;
	if @TestItemCode='601040' or @TestItemCode='601041' or @TestItemCode= '601042' 
		BEGIN
			set @isHasItems=1;
			set @TestResult=@ReportValue+' '+ @Units;
			set @BacteriaTotal=@ReportValue;
		END 

	-- 取下一条记录
	FETCH NEXT FROM c_foreign_keys INTO @ReqDocId,@GSampleNo,@ExaminerId,@Examiner,@ExamineDate,@RecordDtlId,@TestItemCode,@ReportValue,@Units

	--最后一条记录
	

	--如果检验单明细不包含特殊项目，取最后一个项目的检验结果
	IF @@FETCH_STATUS<>0 and @isHasItems!=1
		BEGIN
			set @TestResult=@ReportValue+' '+ @Units;
			set @BacteriaTotal=@ReportValue;
		END

	--更新主单
	if(@ReqDocId2<>'')
		BEGIN
			update GK_SampleRequestForm set StatusID=4,ResultFlag=1,SampleNo=@SampleNo,CheckId=@CheckId,CheckCName=@CheckCName,
			CheckDate=@CheckDate,TestResult=@TestResult,BacteriaTotal=@BacteriaTotal where ReqDocId=@ReqDocId2;
		END
	
	END 
	
	-- 关闭游标
	CLOSE c_foreign_keys;
	-- 释放游标.
	DEALLOCATE c_foreign_keys;

END 
