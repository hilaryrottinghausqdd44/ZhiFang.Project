
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.Entity.WebAssist;
using ZhiFang.DAO.SQL.WebAssist.Lis;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.DAO.SQL.WebAssist
{
    /// <summary>
    /// 数据访问类:MEGroupSampleItemDao_SQL
    /// </summary>
    public partial class MEGroupSampleItemDao_SQL : IDMEGroupSampleItemDao_SQL
    {
        public MEGroupSampleItemDao_SQL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Insert(MEGroupSampleItem model)
        {
            return AddByParameter(model);
        }
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public bool AddBySql(MEGroupSampleItem model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.LabID != null)
            {
                strSql1.Append("LabID,");
                strSql2.Append("" + model.LabID + ",");
            }
            if (model.Id != null)
            {
                strSql1.Append("ResultID,");
                strSql2.Append("" + model.Id + ",");
            }
            if (model.NItemID != null)
            {
                strSql1.Append("NItemID,");
                strSql2.Append("" + model.NItemID + ",");
            }
            if (model.TestType != null)
            {
                strSql1.Append("TestType,");
                strSql2.Append("" + model.TestType + ",");
            }
            if (model.ParentResultID != null)
            {
                strSql1.Append("ParentResultID,");
                strSql2.Append("" + model.ParentResultID + ",");
            }
            if (model.ImmResultID != null)
            {
                strSql1.Append("ImmResultID,");
                strSql2.Append("" + model.ImmResultID + ",");
            }
            if (model.EquipResultID != null)
            {
                strSql1.Append("EquipResultID,");
                strSql2.Append("" + model.EquipResultID + ",");
            }
            if (model.PItemNo != null)
            {
                strSql1.Append("PItemNo,");
                strSql2.Append("" + model.PItemNo + ",");
            }
            if (model.ItemNo != null)
            {
                strSql1.Append("ItemNo,");
                strSql2.Append("" + model.ItemNo + ",");
            }
            if (model.GroupSampleFormID != null)
            {
                strSql1.Append("GroupSampleFormID,");
                strSql2.Append("" + model.GroupSampleFormID + ",");
            }
            if (model.ReCheckFormID != null)
            {
                strSql1.Append("ReCheckFormID,");
                strSql2.Append("" + model.ReCheckFormID + ",");
            }
            if (model.OriglValue != null)
            {
                strSql1.Append("OriglValue,");
                strSql2.Append("'" + model.OriglValue + "',");
            }
            if (model.ValueType != null)
            {
                strSql1.Append("ValueType,");
                strSql2.Append("" + model.ValueType + ",");
            }
            if (model.ReportValue != null)
            {
                strSql1.Append("ReportValue,");
                strSql2.Append("'" + model.ReportValue + "',");
            }
            if (model.ResultStatus != null)
            {
                strSql1.Append("ResultStatus,");
                strSql2.Append("'" + model.ResultStatus + "',");
            }
            if (model.QuanValue != null)
            {
                strSql1.Append("QuanValue,");
                strSql2.Append("" + model.QuanValue + ",");
            }
            if (model.QuanValue2 != null)
            {
                strSql1.Append("QuanValue2,");
                strSql2.Append("" + model.QuanValue2 + ",");
            }
            if (model.QuanValue3 != null)
            {
                strSql1.Append("QuanValue3,");
                strSql2.Append("" + model.QuanValue3 + ",");
            }
            if (model.QuanValueComparison != null)
            {
                strSql1.Append("QuanValueComparison,");
                strSql2.Append("'" + model.QuanValueComparison + "',");
            }
            if (model.Units != null)
            {
                strSql1.Append("Units,");
                strSql2.Append("'" + model.Units + "',");
            }
            if (model.RefRange != null)
            {
                strSql1.Append("RefRange,");
                strSql2.Append("'" + model.RefRange + "',");
            }
            if (model.PreValue != null)
            {
                strSql1.Append("PreValue,");
                strSql2.Append("'" + model.PreValue + "',");
            }
            if (model.PreValueComp != null)
            {
                strSql1.Append("PreValueComp,");
                strSql2.Append("'" + model.PreValueComp + "',");
            }
            if (model.PreCompStatus != null)
            {
                strSql1.Append("PreCompStatus,");
                strSql2.Append("'" + model.PreCompStatus + "',");
            }
            if (model.TestMethod != null)
            {
                strSql1.Append("TestMethod,");
                strSql2.Append("'" + model.TestMethod + "',");
            }
            if (model.AlarmLevel != null)
            {
                strSql1.Append("AlarmLevel,");
                strSql2.Append("" + model.AlarmLevel + ",");
            }
            if (model.ResultLinkType != null)
            {
                strSql1.Append("ResultLinkType,");
                strSql2.Append("" + model.ResultLinkType + ",");
            }
            if (model.ResultLink != null)
            {
                strSql1.Append("ResultLink,");
                strSql2.Append("'" + model.ResultLink + "',");
            }
            if (model.SimpleResultLink != null)
            {
                strSql1.Append("SimpleResultLink,");
                strSql2.Append("'" + model.SimpleResultLink + "',");
            }
            if (model.ResultComment != null)
            {
                strSql1.Append("ResultComment,");
                strSql2.Append("'" + model.ResultComment + "',");
            }
            if (model.DeleteFlag != null)
            {
                strSql1.Append("DeleteFlag,");
                strSql2.Append("" + (model.DeleteFlag ? 1 : 0) + ",");
            }
            if (model.IsDuplicate != null)
            {
                strSql1.Append("IsDuplicate,");
                strSql2.Append("" + (model.IsDuplicate ? 1 : 0) + ",");
            }
            if (model.IsHandEditStatus != null)
            {
                strSql1.Append("IsHandEditStatus,");
                strSql2.Append("" + model.IsHandEditStatus + ",");
            }
            if (model.TestFlag != null)
            {
                strSql1.Append("TestFlag,");
                strSql2.Append("" + model.TestFlag + ",");
            }
            if (model.CheckFlag != null)
            {
                strSql1.Append("CheckFlag,");
                strSql2.Append("" + model.CheckFlag + ",");
            }
            if (model.ReportFlag != null)
            {
                strSql1.Append("ReportFlag,");
                strSql2.Append("" + model.ReportFlag + ",");
            }
            if (model.IsExcess != null)
            {
                strSql1.Append("IsExcess,");
                strSql2.Append("" + (model.IsExcess ? 1 : 0) + ",");
            }
            if (model.IZDY1 != null)
            {
                strSql1.Append("I_ZDY1,");
                strSql2.Append("'" + model.IZDY1 + "',");
            }
            if (model.IZDY2 != null)
            {
                strSql1.Append("I_ZDY2,");
                strSql2.Append("'" + model.IZDY2 + "',");
            }
            if (model.IZDY3 != null)
            {
                strSql1.Append("I_ZDY3,");
                strSql2.Append("'" + model.IZDY3 + "',");
            }
            if (model.IZDY4 != null)
            {
                strSql1.Append("I_ZDY4,");
                strSql2.Append("'" + model.IZDY4 + "',");
            }
            if (model.IZDY5 != null)
            {
                strSql1.Append("I_ZDY5,");
                strSql2.Append("'" + model.IZDY5 + "',");
            }
            if (model.CommState != null)
            {
                strSql1.Append("CommState,");
                strSql2.Append("" + model.CommState + ",");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.DataAddTime != null)
            {
                strSql1.Append("DataAddTime,");
                strSql2.Append("'" + model.DataAddTime + "',");
            }
            if (model.DataUpdateTime != null)
            {
                strSql1.Append("DataUpdateTime,");
                strSql2.Append("'" + model.DataUpdateTime + "',");
            }
            if (model.ZDY1 != null)
            {
                strSql1.Append("ZDY1,");
                strSql2.Append("'" + model.ZDY1 + "',");
            }
            if (model.ZDY2 != null)
            {
                strSql1.Append("ZDY2,");
                strSql2.Append("'" + model.ZDY2 + "',");
            }
            if (model.ZDY3 != null)
            {
                strSql1.Append("ZDY3,");
                strSql2.Append("'" + model.ZDY3 + "',");
            }
            if (model.ZDY4 != null)
            {
                strSql1.Append("ZDY4,");
                strSql2.Append("'" + model.ZDY4 + "',");
            }
            if (model.ZDY5 != null)
            {
                strSql1.Append("ZDY5,");
                strSql2.Append("'" + model.ZDY5 + "',");
            }
            if (model.PlateNo != null)
            {
                strSql1.Append("PlateNo,");
                strSql2.Append("" + model.PlateNo + ",");
            }
            if (model.PositionNo != null)
            {
                strSql1.Append("PositionNo,");
                strSql2.Append("" + model.PositionNo + ",");
            }
            if (model.EquipNo != null)
            {
                strSql1.Append("EquipNo,");
                strSql2.Append("" + model.EquipNo + ",");
            }
            if (model.IDevelop != null)
            {
                strSql1.Append("iDevelop,");
                strSql2.Append("" + model.IDevelop + ",");
            }
            if (model.UserNo != null)
            {
                strSql1.Append("UserNo,");
                strSql2.Append("" + model.UserNo + ",");
            }
            if (model.Operator != null)
            {
                strSql1.Append("Operator,");
                strSql2.Append("'" + model.Operator + "',");
            }
            if (model.GTestDate != null)
            {
                strSql1.Append("GTestDate,");
                strSql2.Append("'" + model.GTestDate + "',");
            }
            if (model.EReportValue != null)
            {
                strSql1.Append("EReportValue,");
                strSql2.Append("'" + model.EReportValue + "',");
            }
            if (model.PReportValue != null)
            {
                strSql1.Append("PReportValue,");
                strSql2.Append("'" + model.PReportValue + "',");
            }
            if (model.ELValue != null)
            {
                strSql1.Append("ELValue,");
                strSql2.Append("'" + model.ELValue + "',");
            }
            if (model.EHValue != null)
            {
                strSql1.Append("EHValue,");
                strSql2.Append("'" + model.EHValue + "',");
            }
            if (model.Conclusion != null)
            {
                strSql1.Append("Conclusion,");
                strSql2.Append("'" + model.Conclusion + "',");
            }
            if (model.ReportValue2 != null)
            {
                strSql1.Append("ReportValue2,");
                strSql2.Append("'" + model.ReportValue2 + "',");
            }
            if (model.ReportValue3 != null)
            {
                strSql1.Append("ReportValue3,");
                strSql2.Append("'" + model.ReportValue3 + "',");
            }
            if (model.PreResultID != null)
            {
                strSql1.Append("PreResultID,");
                strSql2.Append("" + model.PreResultID + ",");
            }
            if (model.PreGTestDate != null)
            {
                strSql1.Append("PreGTestDate,");
                strSql2.Append("'" + model.PreGTestDate + "',");
            }
            if (model.IsMatch != null)
            {
                strSql1.Append("IsMatch,");
                strSql2.Append("" + model.IsMatch + ",");
            }
            if (model.AlarmInfo != null)
            {
                strSql1.Append("AlarmInfo,");
                strSql2.Append("'" + model.AlarmInfo + "',");
            }
            if (model.HisResultCount != null)
            {
                strSql1.Append("HisResultCount,");
                strSql2.Append("" + model.HisResultCount + ",");
            }
            if (model.ResultAddTime != null)
            {
                strSql1.Append("ResultAddTime,");
                strSql2.Append("'" + model.ResultAddTime + "',");
            }
            if (model.IItemSource != null)
            {
                strSql1.Append("iItemSource,");
                strSql2.Append("" + model.IItemSource + ",");
            }
            if (model.PreResultID2 != null)
            {
                strSql1.Append("PreResultID2,");
                strSql2.Append("" + model.PreResultID2 + ",");
            }
            if (model.PreGTestDate2 != null)
            {
                strSql1.Append("PreGTestDate2,");
                strSql2.Append("'" + model.PreGTestDate2 + "',");
            }
            if (model.PreValue2 != null)
            {
                strSql1.Append("PreValue2,");
                strSql2.Append("'" + model.PreValue2 + "',");
            }
            if (model.PreResultID3 != null)
            {
                strSql1.Append("PreResultID3,");
                strSql2.Append("" + model.PreResultID3 + ",");
            }
            if (model.PreGTestDate3 != null)
            {
                strSql1.Append("PreGTestDate3,");
                strSql2.Append("'" + model.PreGTestDate3 + "',");
            }
            if (model.PreValue3 != null)
            {
                strSql1.Append("PreValue3,");
                strSql2.Append("'" + model.PreValue3 + "',");
            }
            if (model.ItemResultInfo != null)
            {
                strSql1.Append("ItemResultInfo,");
                strSql2.Append("" + model.ItemResultInfo + ",");
            }
            if (model.ItemDiagMethod != null)
            {
                strSql1.Append("ItemDiagMethod,");
                strSql2.Append("'" + model.ItemDiagMethod + "',");
            }
            if (model.IAutoUnion != null)
            {
                strSql1.Append("iAutoUnion,");
                strSql2.Append("" + model.IAutoUnion + ",");
            }
            if (model.AutoUnionSNo != null)
            {
                strSql1.Append("AutoUnionSNo,");
                strSql2.Append("'" + model.AutoUnionSNo + "',");
            }
            if (model.IUnionCount != null)
            {
                strSql1.Append("iUnionCount,");
                strSql2.Append("" + model.IUnionCount + ",");
            }
            if (model.INeedUnionCount != null)
            {
                strSql1.Append("iNeedUnionCount,");
                strSql2.Append("" + model.INeedUnionCount + ",");
            }
            if (model.NPItemNo != null)
            {
                strSql1.Append("NPItemNo,");
                strSql2.Append("" + model.NPItemNo + ",");
            }
            if (model.ESend != null)
            {
                strSql1.Append("ESend,");
                strSql2.Append("'" + model.ESend + "',");
            }
            if (model.DispReportValue != null)
            {
                strSql1.Append("DispReportValue,");
                strSql2.Append("'" + model.DispReportValue + "',");
            }
            if (model.QuanValue1 != null)
            {
                strSql1.Append("QuanValue1,");
                strSql2.Append("" + model.QuanValue1 + ",");
            }
            if (model.QuanValue4 != null)
            {
                strSql1.Append("QuanValue4,");
                strSql2.Append("" + model.QuanValue4 + ",");
            }
            if (model.DescValue1 != null)
            {
                strSql1.Append("DescValue1,");
                strSql2.Append("'" + model.DescValue1 + "',");
            }
            if (model.DescValue2 != null)
            {
                strSql1.Append("DescValue2,");
                strSql2.Append("'" + model.DescValue2 + "',");
            }
            if (model.IPositiveCard != null)
            {
                strSql1.Append("iPositiveCard,");
                strSql2.Append("" + model.IPositiveCard + ",");
            }
            if (model.RedoFlag != null)
            {
                strSql1.Append("RedoFlag,");
                strSql2.Append("" + model.RedoFlag + ",");
            }
            if (model.RedoReason != null)
            {
                strSql1.Append("RedoReason,");
                strSql2.Append("'" + model.RedoReason + "',");
            }
            if (model.EResultStatus != null)
            {
                strSql1.Append("EResultStatus,");
                strSql2.Append("'" + model.EResultStatus + "',");
            }
            if (model.EResultAlarm != null)
            {
                strSql1.Append("EResultAlarm,");
                strSql2.Append("'" + model.EResultAlarm + "',");
            }
            if (model.IEResultAlarm != null)
            {
                strSql1.Append("iEResultAlarm,");
                strSql2.Append("" + model.IEResultAlarm + ",");
            }
            if (model.ReportValue4 != null)
            {
                strSql1.Append("ReportValue4,");
                strSql2.Append("'" + model.ReportValue4 + "',");
            }
            if (model.BReportPrint != null)
            {
                strSql1.Append("bReportPrint,");
                strSql2.Append("" + (model.BReportPrint ? 1 : 0) + ",");
            }
            if (model.RedoValue != null)
            {
                strSql1.Append("RedoValue,");
                strSql2.Append("'" + model.RedoValue + "',");
            }
            strSql.Append("insert into ME_GroupSampleItem(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            bool resultBool = DbHelperSQL.ExecuteSql(DbHelperSQL.ConnectionString, strSql.ToString());
            return resultBool;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddByParameter(MEGroupSampleItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ME_GroupSampleItem(");
            strSql.Append("LabID,ResultID,NItemID,TestType,ParentResultID,ImmResultID,EquipResultID,PItemNo,ItemNo,GroupSampleFormID,ReCheckFormID,OriglValue,ValueType,ReportValue,ResultStatus,QuanValue,QuanValue2,QuanValue3,QuanValueComparison,Units,RefRange,PreValue,PreValueComp,PreCompStatus,TestMethod,AlarmLevel,ResultLinkType,ResultLink,SimpleResultLink,ResultComment,DeleteFlag,IsDuplicate,IsHandEditStatus,TestFlag,CheckFlag,ReportFlag,IsExcess,I_ZDY1,I_ZDY2,I_ZDY3,I_ZDY4,I_ZDY5,CommState,DispOrder,DataAddTime,DataUpdateTime,RedoValue,ZDY1,ZDY2,ZDY3,ZDY4,ZDY5,PlateNo,PositionNo,EquipNo,iDevelop,UserNo,Operator,GTestDate,EReportValue,PReportValue,ELValue,EHValue,Conclusion,ReportValue2,ReportValue3,PreResultID,PreGTestDate,IsMatch,AlarmInfo,HisResultCount,ResultAddTime,iItemSource,PreResultID2,PreGTestDate2,PreValue2,PreResultID3,PreGTestDate3,PreValue3,ItemResultInfo,ItemDiagMethod,iAutoUnion,AutoUnionSNo,iUnionCount,iNeedUnionCount,NPItemNo,ESend,DispReportValue,QuanValue1,QuanValue4,DescValue1,DescValue2,iPositiveCard,RedoFlag,RedoReason,EResultStatus,EResultAlarm,iEResultAlarm,ReportValue4,bReportPrint)");
            strSql.Append(" values (");
            strSql.Append("@LabID,@ResultID,@NItemID,@TestType,@ParentResultID,@ImmResultID,@EquipResultID,@PItemNo,@ItemNo,@GroupSampleFormID,@ReCheckFormID,@OriglValue,@ValueType,@ReportValue,@ResultStatus,@QuanValue,@QuanValue2,@QuanValue3,@QuanValueComparison,@Units,@RefRange,@PreValue,@PreValueComp,@PreCompStatus,@TestMethod,@AlarmLevel,@ResultLinkType,@ResultLink,@SimpleResultLink,@ResultComment,@DeleteFlag,@IsDuplicate,@IsHandEditStatus,@TestFlag,@CheckFlag,@ReportFlag,@IsExcess,@I_ZDY1,@I_ZDY2,@I_ZDY3,@I_ZDY4,@I_ZDY5,@CommState,@DispOrder,@DataAddTime,@DataUpdateTime,@RedoValue,@ZDY1,@ZDY2,@ZDY3,@ZDY4,@ZDY5,@PlateNo,@PositionNo,@EquipNo,@iDevelop,@UserNo,@Operator,@GTestDate,@EReportValue,@PReportValue,@ELValue,@EHValue,@Conclusion,@ReportValue2,@ReportValue3,@PreResultID,@PreGTestDate,@IsMatch,@AlarmInfo,@HisResultCount,@ResultAddTime,@iItemSource,@PreResultID2,@PreGTestDate2,@PreValue2,@PreResultID3,@PreGTestDate3,@PreValue3,@ItemResultInfo,@ItemDiagMethod,@iAutoUnion,@AutoUnionSNo,@iUnionCount,@iNeedUnionCount,@NPItemNo,@ESend,@DispReportValue,@QuanValue1,@QuanValue4,@DescValue1,@DescValue2,@iPositiveCard,@RedoFlag,@RedoReason,@EResultStatus,@EResultAlarm,@iEResultAlarm,@ReportValue4,@bReportPrint)");
            SqlParameter[] parameters = {
                    new SqlParameter("@LabID", SqlDbType.BigInt,8),
                    new SqlParameter("@ResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@NItemID", SqlDbType.BigInt,8),
                    new SqlParameter("@TestType", SqlDbType.Int,4),
                    new SqlParameter("@ParentResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@ImmResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@EquipResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@PItemNo", SqlDbType.Int,4),
                    new SqlParameter("@ItemNo", SqlDbType.Int,4),
                    new SqlParameter("@GroupSampleFormID", SqlDbType.BigInt,8),
                    new SqlParameter("@ReCheckFormID", SqlDbType.BigInt,8),
                    new SqlParameter("@OriglValue", SqlDbType.VarChar,40),
                    new SqlParameter("@ValueType", SqlDbType.Int,4),
                    new SqlParameter("@ReportValue", SqlDbType.VarChar,300),
                    new SqlParameter("@ResultStatus", SqlDbType.VarChar,10),
                    new SqlParameter("@QuanValue", SqlDbType.Float,8),
                    new SqlParameter("@QuanValue2", SqlDbType.Float,8),
                    new SqlParameter("@QuanValue3", SqlDbType.Float,8),
                    new SqlParameter("@QuanValueComparison", SqlDbType.VarChar,50),
                    new SqlParameter("@Units", SqlDbType.VarChar,50),
                    new SqlParameter("@RefRange", SqlDbType.VarChar,400),
                    new SqlParameter("@PreValue", SqlDbType.VarChar,300),
                    new SqlParameter("@PreValueComp", SqlDbType.VarChar,50),
                    new SqlParameter("@PreCompStatus", SqlDbType.VarChar,20),
                    new SqlParameter("@TestMethod", SqlDbType.VarChar,50),
                    new SqlParameter("@AlarmLevel", SqlDbType.Int,4),
                    new SqlParameter("@ResultLinkType", SqlDbType.Int,4),
                    new SqlParameter("@ResultLink", SqlDbType.VarChar,100),
                    new SqlParameter("@SimpleResultLink", SqlDbType.VarChar,100),
                    new SqlParameter("@ResultComment", SqlDbType.NText),
                    new SqlParameter("@DeleteFlag", SqlDbType.Bit,1),
                    new SqlParameter("@IsDuplicate", SqlDbType.Bit,1),
                    new SqlParameter("@IsHandEditStatus", SqlDbType.Int,4),
                    new SqlParameter("@TestFlag", SqlDbType.Int,4),
                    new SqlParameter("@CheckFlag", SqlDbType.Int,4),
                    new SqlParameter("@ReportFlag", SqlDbType.Int,4),
                    new SqlParameter("@IsExcess", SqlDbType.Bit,1),
                    new SqlParameter("@I_ZDY1", SqlDbType.VarChar,200),
                    new SqlParameter("@I_ZDY2", SqlDbType.VarChar,200),
                    new SqlParameter("@I_ZDY3", SqlDbType.VarChar,200),
                    new SqlParameter("@I_ZDY4", SqlDbType.VarChar,200),
                    new SqlParameter("@I_ZDY5", SqlDbType.VarChar,200),
                    new SqlParameter("@CommState", SqlDbType.Int,4),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@DataAddTime", SqlDbType.DateTime),
                    new SqlParameter("@DataUpdateTime", SqlDbType.DateTime),

                    new SqlParameter("@RedoValue", SqlDbType.VarChar,100),
                    //new SqlParameter("@DataTimeStamp", SqlDbType.Timestamp,8),

                    new SqlParameter("@ZDY1", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY2", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY3", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY4", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY5", SqlDbType.VarChar,50),
                    new SqlParameter("@PlateNo", SqlDbType.Int,4),
                    new SqlParameter("@PositionNo", SqlDbType.Int,4),
                    new SqlParameter("@EquipNo", SqlDbType.Int,4),
                    new SqlParameter("@iDevelop", SqlDbType.Int,4),
                    new SqlParameter("@UserNo", SqlDbType.Int,4),
                    new SqlParameter("@Operator", SqlDbType.VarChar,20),
                    new SqlParameter("@GTestDate", SqlDbType.DateTime),
                    new SqlParameter("@EReportValue", SqlDbType.VarChar,300),
                    new SqlParameter("@PReportValue", SqlDbType.VarChar,300),
                    new SqlParameter("@ELValue", SqlDbType.VarChar,20),
                    new SqlParameter("@EHValue", SqlDbType.VarChar,20),
                    new SqlParameter("@Conclusion", SqlDbType.VarChar,300),
                    new SqlParameter("@ReportValue2", SqlDbType.VarChar,300),
                    new SqlParameter("@ReportValue3", SqlDbType.VarChar,300),
                    new SqlParameter("@PreResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@PreGTestDate", SqlDbType.DateTime),
                    new SqlParameter("@IsMatch", SqlDbType.Int,4),
                    new SqlParameter("@AlarmInfo", SqlDbType.VarChar,50),
                    new SqlParameter("@HisResultCount", SqlDbType.Int,4),
                    new SqlParameter("@ResultAddTime", SqlDbType.DateTime),
                    new SqlParameter("@iItemSource", SqlDbType.Int,4),
                    new SqlParameter("@PreResultID2", SqlDbType.BigInt,8),
                    new SqlParameter("@PreGTestDate2", SqlDbType.DateTime),
                    new SqlParameter("@PreValue2", SqlDbType.VarChar,300),
                    new SqlParameter("@PreResultID3", SqlDbType.BigInt,8),
                    new SqlParameter("@PreGTestDate3", SqlDbType.DateTime),
                    new SqlParameter("@PreValue3", SqlDbType.VarChar,300),
                    new SqlParameter("@ItemResultInfo", SqlDbType.Image),
                    new SqlParameter("@ItemDiagMethod", SqlDbType.VarChar,40),
                    new SqlParameter("@iAutoUnion", SqlDbType.Int,4),
                    new SqlParameter("@AutoUnionSNo", SqlDbType.VarChar,60),
                    new SqlParameter("@iUnionCount", SqlDbType.Int,4),
                    new SqlParameter("@iNeedUnionCount", SqlDbType.Int,4),
                    new SqlParameter("@NPItemNo", SqlDbType.Int,4),
                    new SqlParameter("@ESend", SqlDbType.VarChar,1000),
                    new SqlParameter("@DispReportValue", SqlDbType.VarChar,2000),
                    new SqlParameter("@QuanValue1", SqlDbType.Float,8),
                    new SqlParameter("@QuanValue4", SqlDbType.Float,8),
                    new SqlParameter("@DescValue1", SqlDbType.VarChar,300),
                    new SqlParameter("@DescValue2", SqlDbType.VarChar,300),
                    new SqlParameter("@iPositiveCard", SqlDbType.Int,4),
                    new SqlParameter("@RedoFlag", SqlDbType.Int,4),
                    new SqlParameter("@RedoReason", SqlDbType.VarChar,300),
                    new SqlParameter("@EResultStatus", SqlDbType.VarChar,50),
                    new SqlParameter("@EResultAlarm", SqlDbType.VarChar,50),
                    new SqlParameter("@iEResultAlarm", SqlDbType.Int,4),
                    new SqlParameter("@ReportValue4", SqlDbType.VarChar,300),
                    new SqlParameter("@bReportPrint", SqlDbType.Bit,1)
                    };

            #region parameters
            parameters[0].Value = model.LabID;
            parameters[1].Value = model.Id;
            parameters[2].Value = model.NItemID;
            parameters[3].Value = model.TestType;
            parameters[4].Value = model.ParentResultID;
            parameters[5].Value = model.ImmResultID;
            parameters[6].Value = model.EquipResultID;
            parameters[7].Value = model.PItemNo;
            parameters[8].Value = model.ItemNo;
            parameters[9].Value = model.GroupSampleFormID;
            parameters[10].Value = model.ReCheckFormID;
            parameters[11].Value = model.OriglValue;
            parameters[12].Value = model.ValueType;
            parameters[13].Value = model.ReportValue;
            parameters[14].Value = model.ResultStatus;
            parameters[15].Value = model.QuanValue;
            parameters[16].Value = model.QuanValue2;
            parameters[17].Value = model.QuanValue3;
            parameters[18].Value = model.QuanValueComparison;
            parameters[19].Value = model.Units;
            parameters[20].Value = model.RefRange;
            parameters[21].Value = model.PreValue;
            parameters[22].Value = model.PreValueComp;
            parameters[23].Value = model.PreCompStatus;
            parameters[24].Value = model.TestMethod;
            parameters[25].Value = model.AlarmLevel;
            parameters[26].Value = model.ResultLinkType;
            parameters[27].Value = model.ResultLink;
            parameters[28].Value = model.SimpleResultLink;
            parameters[29].Value = model.ResultComment;
            parameters[30].Value = model.DeleteFlag;
            parameters[31].Value = model.IsDuplicate;
            parameters[32].Value = model.IsHandEditStatus;
            parameters[33].Value = model.TestFlag;
            parameters[34].Value = model.CheckFlag;
            parameters[35].Value = model.ReportFlag;
            parameters[36].Value = model.IsExcess;
            parameters[37].Value = model.IZDY1;
            parameters[38].Value = model.IZDY2;
            parameters[39].Value = model.IZDY3;
            parameters[40].Value = model.IZDY4;
            parameters[41].Value = model.IZDY5;
            parameters[42].Value = model.CommState;
            parameters[43].Value = model.DispOrder;
            parameters[44].Value = model.DataAddTime;
            parameters[45].Value = model.DataUpdateTime;

            parameters[46].Value = model.RedoValue;

            parameters[47].Value = model.ZDY1;
            parameters[48].Value = model.ZDY2;
            parameters[49].Value = model.ZDY3;
            parameters[50].Value = model.ZDY4;
            parameters[51].Value = model.ZDY5;
            parameters[52].Value = model.PlateNo;
            parameters[53].Value = model.PositionNo;
            parameters[54].Value = model.EquipNo;
            parameters[55].Value = model.IDevelop;
            parameters[56].Value = model.UserNo;
            parameters[57].Value = model.Operator;
            parameters[58].Value = model.GTestDate;
            parameters[59].Value = model.EReportValue;
            parameters[60].Value = model.PReportValue;
            parameters[61].Value = model.ELValue;
            parameters[62].Value = model.EHValue;
            parameters[63].Value = model.Conclusion;
            parameters[64].Value = model.ReportValue2;
            parameters[65].Value = model.ReportValue3;
            parameters[66].Value = model.PreResultID;
            parameters[67].Value = model.PreGTestDate;
            parameters[68].Value = model.IsMatch;
            parameters[69].Value = model.AlarmInfo;
            parameters[70].Value = model.HisResultCount;
            parameters[71].Value = model.ResultAddTime;
            parameters[72].Value = model.IItemSource;
            parameters[73].Value = model.PreResultID2;
            parameters[74].Value = model.PreGTestDate2;
            parameters[75].Value = model.PreValue2;
            parameters[76].Value = model.PreResultID3;
            parameters[77].Value = model.PreGTestDate3;
            parameters[78].Value = model.PreValue3;
            parameters[79].Value = model.ItemResultInfo;
            parameters[80].Value = model.ItemDiagMethod;
            parameters[81].Value = model.IAutoUnion;
            parameters[82].Value = model.AutoUnionSNo;
            parameters[83].Value = model.IUnionCount;
            parameters[84].Value = model.INeedUnionCount;
            parameters[85].Value = model.NPItemNo;
            parameters[86].Value = model.ESend;
            parameters[87].Value = model.DispReportValue;
            parameters[88].Value = model.QuanValue1;
            parameters[89].Value = model.QuanValue4;
            parameters[90].Value = model.DescValue1;
            parameters[91].Value = model.DescValue2;
            parameters[92].Value = model.IPositiveCard;
            parameters[93].Value = model.RedoFlag;
            parameters[94].Value = model.RedoReason;
            parameters[95].Value = model.EResultStatus;
            parameters[96].Value = model.EResultAlarm;
            parameters[97].Value = model.IEResultAlarm;
            parameters[98].Value = model.ReportValue4;
            parameters[99].Value = model.BReportPrint;
          
            #endregion

            int rows = 0;
            rows = DbHelperSQL.ExecuteSql(DbHelperSQL.ConnectionString, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(MEGroupSampleItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ME_GroupSampleItem set ");
            strSql.Append("LabID=@LabID,");
            strSql.Append("NItemID=@NItemID,");
            strSql.Append("TestType=@TestType,");
            strSql.Append("ParentResultID=@ParentResultID,");
            strSql.Append("ImmResultID=@ImmResultID,");
            strSql.Append("EquipResultID=@EquipResultID,");
            strSql.Append("ReCheckFormID=@ReCheckFormID,");
            strSql.Append("OriglValue=@OriglValue,");
            strSql.Append("ValueType=@ValueType,");
            strSql.Append("ResultStatus=@ResultStatus,");
            strSql.Append("QuanValue2=@QuanValue2,");
            strSql.Append("QuanValue3=@QuanValue3,");
            strSql.Append("QuanValueComparison=@QuanValueComparison,");
            strSql.Append("Units=@Units,");
            strSql.Append("RefRange=@RefRange,");
            strSql.Append("PreValue=@PreValue,");
            strSql.Append("PreValueComp=@PreValueComp,");
            strSql.Append("PreCompStatus=@PreCompStatus,");
            strSql.Append("TestMethod=@TestMethod,");
            strSql.Append("AlarmLevel=@AlarmLevel,");
            strSql.Append("ResultLinkType=@ResultLinkType,");
            strSql.Append("ResultLink=@ResultLink,");
            strSql.Append("SimpleResultLink=@SimpleResultLink,");
            strSql.Append("ResultComment=@ResultComment,");
            strSql.Append("DeleteFlag=@DeleteFlag,");
            strSql.Append("IsDuplicate=@IsDuplicate,");
            strSql.Append("IsHandEditStatus=@IsHandEditStatus,");
            strSql.Append("TestFlag=@TestFlag,");
            strSql.Append("CheckFlag=@CheckFlag,");
            strSql.Append("ReportFlag=@ReportFlag,");
            strSql.Append("IsExcess=@IsExcess,");
            strSql.Append("I_ZDY1=@I_ZDY1,");
            strSql.Append("I_ZDY2=@I_ZDY2,");
            strSql.Append("I_ZDY3=@I_ZDY3,");
            strSql.Append("I_ZDY4=@I_ZDY4,");
            strSql.Append("I_ZDY5=@I_ZDY5,");
            strSql.Append("CommState=@CommState,");
            strSql.Append("DispOrder=@DispOrder,");
            strSql.Append("DataAddTime=@DataAddTime,");
            strSql.Append("DataUpdateTime=@DataUpdateTime,");
            strSql.Append("ZDY1=@ZDY1,");
            strSql.Append("ZDY2=@ZDY2,");
            strSql.Append("ZDY3=@ZDY3,");
            strSql.Append("ZDY4=@ZDY4,");
            strSql.Append("ZDY5=@ZDY5,");
            strSql.Append("PlateNo=@PlateNo,");
            strSql.Append("PositionNo=@PositionNo,");
            strSql.Append("EquipNo=@EquipNo,");
            strSql.Append("iDevelop=@iDevelop,");
            strSql.Append("UserNo=@UserNo,");
            strSql.Append("Operator=@Operator,");
            strSql.Append("EReportValue=@EReportValue,");
            strSql.Append("PReportValue=@PReportValue,");
            strSql.Append("ELValue=@ELValue,");
            strSql.Append("EHValue=@EHValue,");
            strSql.Append("Conclusion=@Conclusion,");
            strSql.Append("ReportValue2=@ReportValue2,");
            strSql.Append("ReportValue3=@ReportValue3,");
            strSql.Append("PreResultID=@PreResultID,");
            strSql.Append("PreGTestDate=@PreGTestDate,");
            strSql.Append("IsMatch=@IsMatch,");
            strSql.Append("AlarmInfo=@AlarmInfo,");
            strSql.Append("HisResultCount=@HisResultCount,");
            strSql.Append("ResultAddTime=@ResultAddTime,");
            strSql.Append("iItemSource=@iItemSource,");
            strSql.Append("PreResultID2=@PreResultID2,");
            strSql.Append("PreGTestDate2=@PreGTestDate2,");
            strSql.Append("PreValue2=@PreValue2,");
            strSql.Append("PreResultID3=@PreResultID3,");
            strSql.Append("PreGTestDate3=@PreGTestDate3,");
            strSql.Append("PreValue3=@PreValue3,");
            strSql.Append("ItemResultInfo=@ItemResultInfo,");
            strSql.Append("ItemDiagMethod=@ItemDiagMethod,");
            strSql.Append("iAutoUnion=@iAutoUnion,");
            strSql.Append("AutoUnionSNo=@AutoUnionSNo,");
            strSql.Append("iUnionCount=@iUnionCount,");
            strSql.Append("iNeedUnionCount=@iNeedUnionCount,");
            strSql.Append("NPItemNo=@NPItemNo,");
            strSql.Append("ESend=@ESend,");
            strSql.Append("DispReportValue=@DispReportValue,");
            strSql.Append("QuanValue1=@QuanValue1,");
            strSql.Append("QuanValue4=@QuanValue4,");
            strSql.Append("DescValue1=@DescValue1,");
            strSql.Append("DescValue2=@DescValue2,");
            strSql.Append("iPositiveCard=@iPositiveCard,");
            strSql.Append("RedoFlag=@RedoFlag,");
            strSql.Append("RedoReason=@RedoReason,");
            strSql.Append("EResultStatus=@EResultStatus,");
            strSql.Append("EResultAlarm=@EResultAlarm,");
            strSql.Append("iEResultAlarm=@iEResultAlarm,");
            strSql.Append("ReportValue4=@ReportValue4,");
            strSql.Append("bReportPrint=@bReportPrint,");
            strSql.Append("RedoValue=@RedoValue");
            strSql.Append(" where ResultID=@ResultID and PItemNo=@PItemNo and ItemNo=@ItemNo and GroupSampleFormID=@GroupSampleFormID and ReportValue=@ReportValue and QuanValue=@QuanValue and GTestDate=@GTestDate ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LabID", SqlDbType.BigInt,8),
                    new SqlParameter("@NItemID", SqlDbType.BigInt,8),
                    new SqlParameter("@TestType", SqlDbType.Int,4),
                    new SqlParameter("@ParentResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@ImmResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@EquipResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@ReCheckFormID", SqlDbType.BigInt,8),
                    new SqlParameter("@OriglValue", SqlDbType.VarChar,40),
                    new SqlParameter("@ValueType", SqlDbType.Int,4),
                    new SqlParameter("@ResultStatus", SqlDbType.VarChar,10),
                    new SqlParameter("@QuanValue2", SqlDbType.Float,8),
                    new SqlParameter("@QuanValue3", SqlDbType.Float,8),
                    new SqlParameter("@QuanValueComparison", SqlDbType.VarChar,50),
                    new SqlParameter("@Units", SqlDbType.VarChar,50),
                    new SqlParameter("@RefRange", SqlDbType.VarChar,400),
                    new SqlParameter("@PreValue", SqlDbType.VarChar,300),
                    new SqlParameter("@PreValueComp", SqlDbType.VarChar,50),
                    new SqlParameter("@PreCompStatus", SqlDbType.VarChar,20),
                    new SqlParameter("@TestMethod", SqlDbType.VarChar,50),
                    new SqlParameter("@AlarmLevel", SqlDbType.Int,4),
                    new SqlParameter("@ResultLinkType", SqlDbType.Int,4),
                    new SqlParameter("@ResultLink", SqlDbType.VarChar,100),
                    new SqlParameter("@SimpleResultLink", SqlDbType.VarChar,100),
                    new SqlParameter("@ResultComment", SqlDbType.NText),
                    new SqlParameter("@DeleteFlag", SqlDbType.Bit,1),
                    new SqlParameter("@IsDuplicate", SqlDbType.Bit,1),
                    new SqlParameter("@IsHandEditStatus", SqlDbType.Int,4),
                    new SqlParameter("@TestFlag", SqlDbType.Int,4),
                    new SqlParameter("@CheckFlag", SqlDbType.Int,4),
                    new SqlParameter("@ReportFlag", SqlDbType.Int,4),
                    new SqlParameter("@IsExcess", SqlDbType.Bit,1),
                    new SqlParameter("@I_ZDY1", SqlDbType.VarChar,200),
                    new SqlParameter("@I_ZDY2", SqlDbType.VarChar,200),
                    new SqlParameter("@I_ZDY3", SqlDbType.VarChar,200),
                    new SqlParameter("@I_ZDY4", SqlDbType.VarChar,200),
                    new SqlParameter("@I_ZDY5", SqlDbType.VarChar,200),
                    new SqlParameter("@CommState", SqlDbType.Int,4),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
                    new SqlParameter("@DataAddTime", SqlDbType.DateTime),
                    new SqlParameter("@DataUpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@ZDY1", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY2", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY3", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY4", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY5", SqlDbType.VarChar,50),
                    new SqlParameter("@PlateNo", SqlDbType.Int,4),
                    new SqlParameter("@PositionNo", SqlDbType.Int,4),
                    new SqlParameter("@EquipNo", SqlDbType.Int,4),
                    new SqlParameter("@iDevelop", SqlDbType.Int,4),
                    new SqlParameter("@UserNo", SqlDbType.Int,4),
                    new SqlParameter("@Operator", SqlDbType.VarChar,20),
                    new SqlParameter("@EReportValue", SqlDbType.VarChar,300),
                    new SqlParameter("@PReportValue", SqlDbType.VarChar,300),
                    new SqlParameter("@ELValue", SqlDbType.VarChar,20),
                    new SqlParameter("@EHValue", SqlDbType.VarChar,20),
                    new SqlParameter("@Conclusion", SqlDbType.VarChar,300),
                    new SqlParameter("@ReportValue2", SqlDbType.VarChar,300),
                    new SqlParameter("@ReportValue3", SqlDbType.VarChar,300),
                    new SqlParameter("@PreResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@PreGTestDate", SqlDbType.DateTime),
                    new SqlParameter("@IsMatch", SqlDbType.Int,4),
                    new SqlParameter("@AlarmInfo", SqlDbType.VarChar,50),
                    new SqlParameter("@HisResultCount", SqlDbType.Int,4),
                    new SqlParameter("@ResultAddTime", SqlDbType.DateTime),
                    new SqlParameter("@iItemSource", SqlDbType.Int,4),
                    new SqlParameter("@PreResultID2", SqlDbType.BigInt,8),
                    new SqlParameter("@PreGTestDate2", SqlDbType.DateTime),
                    new SqlParameter("@PreValue2", SqlDbType.VarChar,300),
                    new SqlParameter("@PreResultID3", SqlDbType.BigInt,8),
                    new SqlParameter("@PreGTestDate3", SqlDbType.DateTime),
                    new SqlParameter("@PreValue3", SqlDbType.VarChar,300),
                    new SqlParameter("@ItemResultInfo", SqlDbType.Image),
                    new SqlParameter("@ItemDiagMethod", SqlDbType.VarChar,40),
                    new SqlParameter("@iAutoUnion", SqlDbType.Int,4),
                    new SqlParameter("@AutoUnionSNo", SqlDbType.VarChar,60),
                    new SqlParameter("@iUnionCount", SqlDbType.Int,4),
                    new SqlParameter("@iNeedUnionCount", SqlDbType.Int,4),
                    new SqlParameter("@NPItemNo", SqlDbType.Int,4),
                    new SqlParameter("@ESend", SqlDbType.VarChar,1000),
                    new SqlParameter("@DispReportValue", SqlDbType.VarChar,2000),
                    new SqlParameter("@QuanValue1", SqlDbType.Float,8),
                    new SqlParameter("@QuanValue4", SqlDbType.Float,8),
                    new SqlParameter("@DescValue1", SqlDbType.VarChar,300),
                    new SqlParameter("@DescValue2", SqlDbType.VarChar,300),
                    new SqlParameter("@iPositiveCard", SqlDbType.Int,4),
                    new SqlParameter("@RedoFlag", SqlDbType.Int,4),
                    new SqlParameter("@RedoReason", SqlDbType.VarChar,300),
                    new SqlParameter("@EResultStatus", SqlDbType.VarChar,50),
                    new SqlParameter("@EResultAlarm", SqlDbType.VarChar,50),
                    new SqlParameter("@iEResultAlarm", SqlDbType.Int,4),
                    new SqlParameter("@ReportValue4", SqlDbType.VarChar,300),
                    new SqlParameter("@bReportPrint", SqlDbType.Bit,1),
                    new SqlParameter("@RedoValue", SqlDbType.VarChar,100),
                    new SqlParameter("@ResultID", SqlDbType.BigInt,8),
                    new SqlParameter("@PItemNo", SqlDbType.Int,4),
                    new SqlParameter("@ItemNo", SqlDbType.Int,4),
                    new SqlParameter("@GroupSampleFormID", SqlDbType.BigInt,8),
                    new SqlParameter("@ReportValue", SqlDbType.VarChar,300),
                    new SqlParameter("@QuanValue", SqlDbType.Float,8),
                    new SqlParameter("@GTestDate", SqlDbType.DateTime)};
            parameters[0].Value = model.LabID;
            parameters[1].Value = model.NItemID;
            parameters[2].Value = model.TestType;
            parameters[3].Value = model.ParentResultID;
            parameters[4].Value = model.ImmResultID;
            parameters[5].Value = model.EquipResultID;
            parameters[6].Value = model.ReCheckFormID;
            parameters[7].Value = model.OriglValue;
            parameters[8].Value = model.ValueType;
            parameters[9].Value = model.ResultStatus;
            parameters[10].Value = model.QuanValue2;
            parameters[11].Value = model.QuanValue3;
            parameters[12].Value = model.QuanValueComparison;
            parameters[13].Value = model.Units;
            parameters[14].Value = model.RefRange;
            parameters[15].Value = model.PreValue;
            parameters[16].Value = model.PreValueComp;
            parameters[17].Value = model.PreCompStatus;
            parameters[18].Value = model.TestMethod;
            parameters[19].Value = model.AlarmLevel;
            parameters[20].Value = model.ResultLinkType;
            parameters[21].Value = model.ResultLink;
            parameters[22].Value = model.SimpleResultLink;
            parameters[23].Value = model.ResultComment;
            parameters[24].Value = model.DeleteFlag;
            parameters[25].Value = model.IsDuplicate;
            parameters[26].Value = model.IsHandEditStatus;
            parameters[27].Value = model.TestFlag;
            parameters[28].Value = model.CheckFlag;
            parameters[29].Value = model.ReportFlag;
            parameters[30].Value = model.IsExcess;
            parameters[31].Value = model.IZDY1;
            parameters[32].Value = model.IZDY2;
            parameters[33].Value = model.IZDY3;
            parameters[34].Value = model.IZDY4;
            parameters[35].Value = model.IZDY5;
            parameters[36].Value = model.CommState;
            parameters[37].Value = model.DispOrder;
            parameters[38].Value = model.DataAddTime;
            parameters[39].Value = model.DataUpdateTime;
            parameters[40].Value = model.ZDY1;
            parameters[41].Value = model.ZDY2;
            parameters[42].Value = model.ZDY3;
            parameters[43].Value = model.ZDY4;
            parameters[44].Value = model.ZDY5;
            parameters[45].Value = model.PlateNo;
            parameters[46].Value = model.PositionNo;
            parameters[47].Value = model.EquipNo;
            parameters[48].Value = model.IDevelop;
            parameters[49].Value = model.UserNo;
            parameters[50].Value = model.Operator;
            parameters[51].Value = model.EReportValue;
            parameters[52].Value = model.PReportValue;
            parameters[53].Value = model.ELValue;
            parameters[54].Value = model.EHValue;
            parameters[55].Value = model.Conclusion;
            parameters[56].Value = model.ReportValue2;
            parameters[57].Value = model.ReportValue3;
            parameters[58].Value = model.PreResultID;
            parameters[59].Value = model.PreGTestDate;
            parameters[60].Value = model.IsMatch;
            parameters[61].Value = model.AlarmInfo;
            parameters[62].Value = model.HisResultCount;
            parameters[63].Value = model.ResultAddTime;
            parameters[64].Value = model.IItemSource;
            parameters[65].Value = model.PreResultID2;
            parameters[66].Value = model.PreGTestDate2;
            parameters[67].Value = model.PreValue2;
            parameters[68].Value = model.PreResultID3;
            parameters[69].Value = model.PreGTestDate3;
            parameters[70].Value = model.PreValue3;
            parameters[71].Value = model.ItemResultInfo;
            parameters[72].Value = model.ItemDiagMethod;
            parameters[73].Value = model.IAutoUnion;
            parameters[74].Value = model.AutoUnionSNo;
            parameters[75].Value = model.IUnionCount;
            parameters[76].Value = model.INeedUnionCount;
            parameters[77].Value = model.NPItemNo;
            parameters[78].Value = model.ESend;
            parameters[79].Value = model.DispReportValue;
            parameters[80].Value = model.QuanValue1;
            parameters[81].Value = model.QuanValue4;
            parameters[82].Value = model.DescValue1;
            parameters[83].Value = model.DescValue2;
            parameters[84].Value = model.IPositiveCard;
            parameters[85].Value = model.RedoFlag;
            parameters[86].Value = model.RedoReason;
            parameters[87].Value = model.EResultStatus;
            parameters[88].Value = model.EResultAlarm;
            parameters[89].Value = model.IEResultAlarm;
            parameters[90].Value = model.ReportValue4;
            parameters[91].Value = model.BReportPrint;
            parameters[92].Value = model.RedoValue;
            parameters[93].Value = model.Id;
            parameters[94].Value = model.PItemNo;
            parameters[95].Value = model.ItemNo;
            parameters[96].Value = model.GroupSampleFormID;
            parameters[97].Value = model.ReportValue;
            parameters[98].Value = model.QuanValue;
            parameters[99].Value = model.GTestDate;

            int rows = 0;
            //rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion  ExtensionMethod
    }
}

