using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;


namespace ZhiFang.ReportFormQueryPrint.DBUpdate
{
    public class DBUpdate
    {
        public static string ADOConnectStr = GetADODataBaseSettings(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormQueryPrintConnectionString"));

        static Dictionary<string, string> DicVersion = GetVersionComparison();//

        static string MainAssemblyFile = "ZhiFang.ReportFormQueryPrint.dll";//可以从配置文件获取

        /// <summary>
        /// 初始化数据库版本和主程序集版本关系，key数据库版本，value主程序集版本
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetVersionComparison()
        {
            //每更新一次版本，需要手工在这里添加对应关系
            Dictionary<string, string> dicVersion = new Dictionary<string, string>();
            dicVersion.Add("1.0.0.1", "1.0.0.1");
            dicVersion.Add("1.0.0.2", "1.0.0.2");
            dicVersion.Add("1.0.0.3", "1.0.0.3");
            dicVersion.Add("1.0.0.4", "1.0.0.4");
            dicVersion.Add("1.0.0.5", "1.0.0.5");
            dicVersion.Add("1.0.0.6", "1.0.0.6");
            dicVersion.Add("1.0.0.7", "1.0.0.7");
            dicVersion.Add("1.0.0.8", "1.0.0.8");
            dicVersion.Add("1.0.0.9", "1.0.0.9");
            dicVersion.Add("1.0.0.10", "1.0.0.10");
            dicVersion.Add("1.0.0.11", "1.0.0.11");
            dicVersion.Add("1.0.0.12", "1.0.0.12");
            dicVersion.Add("1.0.0.13", "1.0.0.13");
            dicVersion.Add("1.0.0.14", "1.0.0.14");
            dicVersion.Add("1.0.0.15", "1.0.0.15");
            dicVersion.Add("1.0.0.16", "1.0.0.16");
            dicVersion.Add("1.0.0.17", "1.0.0.17");
            dicVersion.Add("1.0.0.18", "1.0.0.18");
            dicVersion.Add("1.0.0.19", "1.0.0.19");
            dicVersion.Add("1.0.2.1", "1.0.2.1");
            dicVersion.Add("1.0.2.2", "1.0.2.2");
            return dicVersion;
        }

        /// <summary>
        /// 根据databaseSettings数据库链接配置，得到ADO数据链接字符串
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        private static string GetADODataBaseSettings(string connectString)
        {
            string result = "";
            if (!string.IsNullOrEmpty(connectString))
            {
                string[] strList = connectString.Split(';');
                foreach (string tempStr in strList)
                {
                    if (!string.IsNullOrEmpty(tempStr))
                    {
                        string[] strArray = tempStr.Split('=');
                        if (strArray[0].ToUpper() == "DATA SOURCE")
                            result += "data source=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                        else if (strArray[0].ToUpper() == "INITIAL CATALOG")
                            result += "initial catalog=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                        else if (strArray[0].ToUpper() == "USER ID")
                            result += "user id=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                        else if (strArray[0].ToUpper() == "PASSWORD")
                            result += "password=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 数据库升级
        /// </summary>
        /// <param name="oldVersion"></param>
        /// <returns></returns>
        public static bool DataBaseUpdate(string oldVersion)
        {
            bool result = false;
            string updateSql = "";
            CheckBParameterIsIsExists();
            oldVersion = GetDataBaseCurVersion();
            string DBSourceType = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("DBSourceType");
            #region 1.0.0.1
            if (IsUpdateDataBase(oldVersion, "1.0.0.1"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = "declare  @a int;";
                    listSQL.Add(updateSql);
                    DataSet ds = null;
                    #region 创建表以及试图
                    updateSql = "IF COL_LENGTH('ReportForm', 'PageName') IS NULL Alter Table ReportForm add PageName varchar(10);";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('ReportForm', 'PageCount') IS NULL Alter Table ReportForm add PageCount varchar(10);";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('RequestForm', 'PageName') IS NULL Alter Table RequestForm add PageName varchar(10);";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('RequestForm', 'PageCount') IS NULL Alter Table RequestForm add PageCount varchar(10);";
                    listSQL.Add(updateSql);
                    if (!CheckDataObjectIsExists("ReportFormQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[ReportFormQueryDataSource] " +
                                    " AS " +
                                    " SELECT  (SELECT  ParaValue" +
                                    "                   FROM       dbo.SysPara" +
                                    "                   WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, rf.SectionNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(char, rf.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, rf.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), rf.ReceiveDate, 20))) AS ReportFormID, CONVERT(varchar(10), rf.ReceiveDate, " +
                                    "                   21) + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), rf.TestTypeNo) + ';' + CONVERT(varchar(30), " +
                                    "                   rf.SampleNo) AS FormNo, gt.CName AS Gender, gt.ShortCode AS GenderEName, st.CName AS SickType, " +
                                    "                   st.ShortCode AS SickEname, spt.CName AS sampletype, spt.ShortCode AS sampletypeename, ft.CName AS folk, " +
                                    "                   ft.ShortCode AS folkename, dt.CName AS Dept, dt.ShortCode AS Deptename, dct.CName AS district, " +
                                    "                   dct.ShortCode AS districtename, wt.CName AS ward, au.CName AS AgeUnit, au.ShortCode AS AgeUnitename, " +
                                    "                   tt.CName AS TestType, tt.ShortCode AS TestTypeename, dgs.CName AS diag, clt.CNAME AS client, clt.ADDRESS, " +
                                    "                   pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate, " +
                                    "                   rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName, rf.GenderNo, rf.Birthday, " +
                                    "                   rf.Age, rf.AgeUnitNo, rf.FolkNo, rf.DistrictNo, rf.WardNo, rf.Bed, rf.DeptNo, rf.Doctor, rf.ChargeNo, rf.Charge, rf.Collecter," +
                                    "                   rf.CollectDate, rf.CollectTime, rf.FormMemo, rf.TestDate, rf.TestTime, rf.Technician, rf.Operator, rf.OperDate, rf.OperTime, " +
                                    "                   rf.CheckDate, rf.CheckTime, rf.Checker, rf.SerialNo, rf.RequestSource, rf.DiagNo, rf.PrintTimes, rf.FormComment, " +
                                    "                   rf.Artificerorder, rf.Sickorder, rf.SickTypeNo, rf.Chargeflag, rf.TestDest, rf.SLable, rf.zdy1, rf.zdy2, rf.zdy3, rf.zdy4, rf.zdy5," +
                                    "                   rf.inceptdate, rf.incepttime, rf.onlinedate, rf.incepter, rf.onlinetime, rf.bmanno, rf.isReceive, rf.ReceiveMan, rf.ReceiveTime, " +
                                    "                   rf.concessionNum, rf.resultstatus, rf.testaim, rf.resultprinttimes, rf.Sender2, rf.SenderTime2, rf.paritemname, rf.clientprint, " +
                                    "                   rf.resultsend, rf.reportsend, rf.CountNodesFormSource, rf.commsendflag, rf.PrintDateTime, rf.PrintOper, rf.abnormityflag, " +
                                    "                   rf.HISDateTime, rf.allowprint, rf.UrgentState, rf.RemoveFeesReason, rf.ZDY6, rf.ZDY7, rf.ZDY8, rf.ZDY9, rf.phoneCode, " +
                                    "                   rf.IsNode, rf.PhoneNodeCount, rf.AutoNodeCount, rf.FormDesc, rf.EquipCommMemo, rf.ESampleNo, rf.EPosition, " +
                                    "                   rf.ISUsePG, rf.OperMemo, rf.FromQCL, rf.ESend, rf.IsDel, rf.EModule, rf.IsRedo, rf.EAchivPosition, rf.FenzhuDatetime, " +
                                    "                   rf.EQCLPosition, rf.EquipResult, rf.EResultToHIS, rf.NurseSender, rf.NurseSendTime, rf.NurseSendCarrier, " +
                                    "                   rf.bGetAllResults, rf.bSendWjz, rf.CollecterID, rf.PrintDateTime1, rf.PrintOper1, rf.HisDoctorId, rf.HisDoctorPhoneCode, " +
                                    "                   rf.LisDoctorNo, rf.LischeckerNo, rf.LastOperator, rf.LastOperDatetime, rf.I_Reportstatus, rf.AutoSended, rf.PatState, " +
                                    "                   rf.EquipCommSend, rf.EquipSampleNo, pg.SectionType, CASE WHEN CONVERT(varchar(50), rf.clientno) IS NULL " +
                                    "                   THEN '425' WHEN CONVERT(varchar(50), rf.clientno) = '' THEN '425' ELSE CONVERT(varchar(50), rf.clientno) " +
                                    "                   END AS clientno, dbo.GetBarCodeItemList(rf.SerialNo, '0') AS ItemName, rf.PageCount, rf.PageName, " +
                                    "                   CASE WHEN ((rf.SectionNo >= 2 AND rf.SectionNo <= 14) OR" +
                                    "                   rf.SectionNo = 41 OR" +
                                    "                   rf.SectionNo = 43) THEN rf.CheckDate ELSE rf.SenderTime2 END AS ZDY10, st.CName AS SICKTYPENAME, " +
                                    "                   tt.CName AS TestTypeName, spt.CName AS SampletypeName, '' AS SecretType, '' AS InpatientNo, '' AS PatCardNo, " +
                                    "                   au.CName AS AgeUnitName, dct.CName AS DistrictName, wt.CName AS WardName, dt.CName AS DeptName, " +
                                    "                   dgs.CName AS DiagName, '' AS LabID, '' AS MainTesterId, '' AS ActiveFlag, rf.zdy15, rf.FormComment2, " +
                                    "                   dt.code_1 AS DeptCode1" +
                                    " FROM      dbo.ReportForm AS rf LEFT OUTER JOIN" +
                                    "                   dbo.GenderType AS gt ON rf.GenderNo = gt.GenderNo LEFT OUTER JOIN" +
                                    "                   dbo.SickType AS st ON st.SickTypeNo = rf.SickTypeNo LEFT OUTER JOIN" +
                                    "                   dbo.SampleType AS spt ON spt.SampleTypeNo = rf.SampleTypeNo LEFT OUTER JOIN" +
                                    "                   dbo.FolkType AS ft ON ft.FolkNo = rf.FolkNo LEFT OUTER JOIN" +
                                    "                   dbo.Department AS dt ON dt.DeptNo = rf.DeptNo LEFT OUTER JOIN" +
                                    "                   dbo.District AS dct ON dct.DistrictNo = rf.DistrictNo LEFT OUTER JOIN" +
                                    "                   dbo.WardType AS wt ON wt.WardNo = rf.WardNo LEFT OUTER JOIN" +
                                    "                   dbo.AgeUnit AS au ON au.AgeUnitNo = rf.AgeUnitNo LEFT OUTER JOIN" +
                                    "                   dbo.TestType AS tt ON tt.TestTypeNo = rf.TestTypeNo LEFT OUTER JOIN" +
                                    "                   dbo.Diagnosis AS dgs ON dgs.DiagNo = rf.DiagNo LEFT OUTER JOIN" +
                                    "                   dbo.CLIENTELE AS clt ON clt.ClIENTNO = rf.clientno LEFT OUTER JOIN" +
                                    "                   dbo.PGroup AS pg ON pg.SectionNo = rf.SectionNo";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("ReportItemQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW[dbo].[ReportItemQueryDataSource]" +
                                    " AS " +
                                    " SELECT  CONVERT(varchar(10), ri.ReceiveDate, 21) + ';' + CONVERT(varchar(30), ri.SectionNo) + ';' + CONVERT(varchar(30), " +
                                    "                   ri.TestTypeNo) + ';' + CONVERT(varchar(30), ri.SampleNo) AS FormNo," +
                                    "                       (SELECT  ParaValue" +
                                    "                        FROM       dbo.SysPara" +
                                    "                        WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, ri.SectionNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportItemID, '__' + LTRIM(RTRIM(CONVERT(char, " +
                                    "                   ri.SectionNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportFormID, dbo.getitemrow(ri.ReceiveDate, " +
                                    "                   ri.SectionNo, ri.TestTypeNo, ri.SampleNo, t1.DispOrder) AS rowId, ri.ReceiveDate, ri.SectionNo, ri.TestTypeNo, ri.SampleNo, " +
                                    "                   ri.ParItemNo, ri.ItemNo, ri.OriginalValue, ri.ReportValue, ri.OriginalDesc, ri.ReportDesc, ri.StatusNo, ri.RefRange, ri.EquipNo, " +
                                    "                   ri.Modified, ri.ItemDate, ri.ItemTime, ri.IsMatch, CASE WHEN ((LTRIM(RTRIM(ISNULL(ri.ReportDesc, ''))) = '阳性') AND " +
                                    "                   ((t1.CName NOT LIKE '%RH%') AND (t1.CName NOT LIKE '%血型%'))) THEN 'H' ELSE ResultStatus END AS ResultStatus, " +
                                    "                   ri.HisValue, ri.HisComp, ri.isReceive, ri.SerialNoParent, ri.zdy1, ri.zdy2, ri.zdy3, ri.zdy4, ri.zdy5, ri.CountNodesItemSource, " +
                                    "                   ri.Unit AS ItemUnit, ri.PlateNo, ri.PositionNo, ri.EquipCommMemo, ri.EquipCommSend, ri.EValueState, ri.EModule, " +
                                    "                   ri.EPosition, ri.ESend, ri.IsRedo, ri.IsDel, ri.HisReceiveDate, ri.FromRedoNo, ri.I_Reportstatus, ri.ItemTestMemo, " +
                                    "                   ri.ItemDealWith, ri.I_EResult, CASE WHEN isnull(t1.Prec, 0) = 0 THEN ISNULL(CONVERT(varchar, " +
                                    "                   CAST(ReportValue AS decimal(18, 0))), '') + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 1) " +
                                    "                   = 1 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 1))), '') + ISNULL(ri.ReportDesc, '') " +
                                    "                   WHEN isnull(t1.Prec, 0) = 2 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') " +
                                    "                   + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 0) = 3 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18," +
                                    "                   3))), '') + ISNULL(ri.ReportDesc, '') ELSE ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') " +
                                    "                   + ISNULL(ri.ReportDesc, '') END AS ItemValue, t1.DispOrder, t1.CName AS ItemCname, t1.EName AS itemename, " +
                                    "                   t2.CName AS paritemname, t1.Secretgrade, t1.ShortName, t1.ShortCode, t1.OrderNo, t1.StandardCode, t1.Cuegrade, " +
                                    "                   t1.DiagMethod, '' AS EquipName, t1.Prec, t1.ItemDesc, t1.Visible, t1.EName AS TESTITEMSNAME, " +
                                    "                   t1.CName AS TESTITEMNAME" +
                                    " FROM      dbo.ReportItem AS ri LEFT OUTER JOIN" +
                                    "                   dbo.TestItem AS t1 ON t1.ItemNo = ri.ItemNo LEFT OUTER JOIN" +
                                    "                   dbo.TestItem AS t2 ON t2.ItemNo = ri.ParItemNo";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("ReportMicroQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[ReportMicroQueryDataSource]" +
                                    " AS " +
                                    " SELECT  CONVERT(varchar(10), rm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), rm.SectionNo) + ';' + CONVERT(varchar(30), " +
                                    "                   rm.TestTypeNo) + ';' + CONVERT(varchar(30), rm.SampleNo) AS ReportFormID, CONVERT(varchar(10), rm.ReceiveDate, 21) " +
                                    "                   + ';' + CONVERT(varchar(30), rm.SectionNo) + ';' + CONVERT(varchar(30), rm.TestTypeNo) + ';' + CONVERT(varchar(30), " +
                                    "                   rm.SampleNo) AS FormNo," +
                                    "                       (SELECT  ParaValue" +
                                    "                        FROM       dbo.SysPara" +
                                    "                        WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, rm.SectionNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(char, rm.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, rm.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), rm.ReceiveDate, 20))) AS Expr1, rm.ReceiveDate, rm.SectionNo, " +
                                    "                   rm.TestTypeNo, rm.SampleNo, rm.ResultNo, rm.ItemNo, rm.DescNo, rm.MicroNo, rm.MicroDesc, rm.AntiNo, rm.Suscept, " +
                                    "                   rm.SusQuan, rm.SusDesc, rm.RefRange, rm.ItemDate, rm.ItemTime, rm.ItemDesc, rm.EquipNo, rm.Modified, rm.IsMatch," +
                                    "                   rm.CheckType, rm.isReceive, rm.SerialNoParent, rm.Zdy1, rm.Zdy2, rm.Zdy3, rm.Zdy4, rm.Zdy5, rm.AntiDesc, rm.I_EResult, " +
                                    "                   rm.AntiGroupType, rm.ExpertDesc, rm.ResultState, rm.MicroResultDesc, t1.CName AS itemcname, " +
                                    "                   t1.EName AS itemename, mci.CName AS Microcname, mci.EName AS Microename, mip.CName AS DescName, " +
                                    "                   ab.CName AS AntiName, ab.EName AS Antiename, ab.ShortName AS Antishortname, ab.ShortCode AS antishortcode, " +
                                    "                   ab.AntiUnit, mci.MicroDesc AS microcountdesc, t1.OrderNo, '' AS MicroStepID, '' AS MicroStepName, '' AS ResultID, " +
                                    "                   rm.MicroDesc AS ReportValue, mci.CName AS MicroName, mci.EName AS MicroEame, '' AS EquipName, " +
                                    "                   t1.CName AS MethodName, '' AS SerumcenTration, '' AS EmictioncenTration, '' AS Microdisplayid, " +
                                    "                   rm.AntiNo AS Antidisplayid, '' AS DSTType, '' AS RPName" +
                                    " FROM      dbo.ReportMicro AS rm LEFT OUTER JOIN" +
                                    "                   dbo.TestItem AS t1 ON t1.ItemNo = rm.ItemNo LEFT OUTER JOIN" +
                                    "                   dbo.MicroItem AS mci ON mci.MicroNo = rm.MicroNo LEFT OUTER JOIN" +
                                    "                   dbo.MicroPhrase AS mip ON mip.DescNo = rm.DescNo LEFT OUTER JOIN" +
                                    "                   dbo.AntiBiotic AS ab ON ab.AntiNo = rm.AntiNo";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("ReportMarrowQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[ReportMarrowQueryDataSource]" +
                                    " AS " +
                                    " SELECT  CONVERT(varchar(10), dbo.ReportForm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), dbo.ReportForm.SectionNo) " +
                                    "                   + ';' + CONVERT(varchar(30), dbo.ReportForm.TestTypeNo) + ';' + CONVERT(varchar(30), dbo.ReportForm.SampleNo) " +
                                    "                   AS FormNo, dbo.ReportMarrow.ReceiveDate, dbo.ReportMarrow.SectionNo, dbo.ReportMarrow.TestTypeNo, " +
                                    "                   dbo.ReportMarrow.SampleNo, dbo.ReportMarrow.ParItemNo, dbo.ReportMarrow.ItemNo, dbo.ReportMarrow.BloodNum, " +
                                    "                   dbo.ReportMarrow.BloodPercent, dbo.ReportMarrow.MarrowNum, dbo.ReportMarrow.MarrowPercent, " +
                                    "                   dbo.ReportMarrow.BloodDesc, dbo.ReportMarrow.MarrowDesc, dbo.ReportMarrow.StatusNo, " +
                                    "                   dbo.ReportMarrow.RefRange, dbo.ReportMarrow.EquipNo, dbo.ReportMarrow.IsCale, dbo.ReportMarrow.Modified, " +
                                    "                   '__' + LTRIM(RTRIM(CONVERT(char, dbo.ReportMarrow.SectionNo))) + '_' + LTRIM(RTRIM(CONVERT(char, " +
                                    "                   dbo.ReportMarrow.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, dbo.ReportMarrow.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), dbo.ReportMarrow.ReceiveDate, 20))) AS ReportFormID, " +
                                    "                   dbo.ReportMarrow.ItemDate, dbo.ReportMarrow.ItemTime, dbo.ReportMarrow.IsMatch, dbo.ReportMarrow.ResultStatus, " +
                                    "                   dbo.TestItem.CName AS ItemCName, dbo.TestItem.EName AS ItemEName, TestItem_1.CName AS ParItemCName, " +
                                    "                   TestItem_1.EName AS ParItemEName, dbo.S_RequestVItem.ParItemNo AS Expr1, dbo.S_RequestVItem.ItemNo AS Expr2, " +
                                    "                   dbo.S_RequestVItem.OrgValue, dbo.S_RequestVItem.ReportValue, dbo.S_RequestVItem.OrgDesc, " +
                                    "                   dbo.S_RequestVItem.ReportDesc, dbo.S_RequestVItem.ReportText, dbo.S_RequestVItem.ReportImage, " +
                                    "                   dbo.S_RequestVItem.RefRange AS Expr3, dbo.S_RequestVItem.EquipNo AS Expr4, " +
                                    "                   dbo.S_RequestVItem.Modified AS Expr5, dbo.S_RequestVItem.ItemDate AS Expr6, " +
                                    "                   dbo.S_RequestVItem.ItemTime AS Expr7, dbo.S_RequestVItem.ResultStatus AS Expr8, dbo.S_RequestVItem.IsPrint, " +
                                    "                   dbo.S_RequestVItem.PrintOrder, dbo.S_RequestVItem_C.CItemNo," +
                                    "                       (SELECT  CName" +
                                    "                        FROM       dbo.Equipment" +
                                    "                        WHERE    (EquipNo = dbo.ReportMarrow.EquipNo)) AS EquipName, dbo.TestItem.OrderNo, " +
                                    "                   dbo.ReportForm.paritemname, dbo.TestItem.DiagMethod" +
                                    "  FROM      dbo.ReportForm INNER JOIN" +
                                    "                   dbo.ReportMarrow ON dbo.ReportForm.ReceiveDate = dbo.ReportMarrow.ReceiveDate AND " +
                                    "                   dbo.ReportForm.SectionNo = dbo.ReportMarrow.SectionNo AND " +
                                    "                   dbo.ReportForm.TestTypeNo = dbo.ReportMarrow.TestTypeNo AND " +
                                    "                   dbo.ReportForm.SampleNo = dbo.ReportMarrow.SampleNo INNER JOIN" +
                                    "                   dbo.TestItem ON dbo.ReportMarrow.ItemNo = dbo.TestItem.ItemNo INNER JOIN" +
                                    "                   dbo.TestItem AS TestItem_1 ON dbo.ReportMarrow.ParItemNo = TestItem_1.ItemNo LEFT OUTER JOIN" +
                                    "                   dbo.S_RequestVItem ON dbo.ReportMarrow.ReceiveDate = dbo.S_RequestVItem.ReceiveDate AND " +
                                    "                   dbo.ReportMarrow.SectionNo = dbo.S_RequestVItem.SectionNo AND " +
                                    "                   dbo.ReportMarrow.TestTypeNo = dbo.S_RequestVItem.TestTypeNo AND " +
                                    "                   dbo.ReportMarrow.SampleNo = dbo.S_RequestVItem.SampleNo LEFT OUTER JOIN" +
                                    "                   dbo.S_RequestVItem_C ON dbo.ReportMarrow.ReceiveDate = dbo.S_RequestVItem_C.ReceiveDate AND " +
                                    "                   dbo.ReportMarrow.SectionNo = dbo.S_RequestVItem_C.SectionNo AND " +
                                    "                   dbo.ReportMarrow.TestTypeNo = dbo.S_RequestVItem_C.TestTypeNo AND " +
                                    "                   dbo.ReportMarrow.SampleNo = dbo.S_RequestVItem_C.SampleNo";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("GetReportValue", "p"))
                    {
                        updateSql = "CREATE PROCEDURE [dbo].[GetReportValue] " +
                                    "@PatNo varchar(50), --病历号 \r\n" +
                                    "@ItemNo varchar(50), --项目号 \r\n" +
                                    "@Check varchar(50), --SectionType \r\n" +
                                    "@Where varchar(max) --where \r\n" +
                                    "--ReportForm rf" +
                                    "--ReportItem ri" +
                                    "--ReportMicro rm" +
                                    "--ReportMarrow rmm" +
                                    "--testitem     ti \r\n" +
                                    " AS " +
                                    " BEGIN " +
                                    " if @Check='item' " +
                                    "    begin " +
                                    "		exec " +
                                    " ('select isnull(ReportValue,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate,CONVERT(varchar(100), rf.checkdate, 23) Checkdate,CONVERT(varchar(100), rf.checktime, 24) checktime,ti.cname as ItemCName" +
                                    " from dbo.ReportItem ri INNER JOIN" +
                                    "		dbo.ReportForm rf ON ri.ReceiveDate = rf.ReceiveDate AND ri.SectionNo = rf.SectionNo AND " +
                                    "		ri.TestTypeNo = rf.TestTypeNo AND ri.SampleNo = rf.SampleNo LEFT OUTER JOIN    " +
                                    "         dbo.TestItem AS ti ON ti.ItemNo = ri.ItemNo" +
                                    "		where -- ReportItem.FormNo=@FormNo\r\n" +
                                    "		rf.PatNo='''+@PatNo+''' and ri.ITEMNO = '''+@ItemNo +'''' +@Where)" +
                                    "		--rf.PatNo=@PatNo and ri.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate	\r\n" +
                                    "    end" +
                                    " else if @Check='micro'" +
                                    "    begin" +
                                    "	    exec ('select isnull(SusQuan,0) as ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate,CONVERT(varchar(100), rf.checkdate, 23) Checkdate,CONVERT(varchar(100), rf.checktime, 24) checktime,ti.cname as ItemCName from dbo.ReportMicro rm INNER JOIN" +
                                    "		dbo.ReportForm rf ON rm.ReceiveDate = rf.ReceiveDate AND rm.SectionNo = rf.SectionNo AND " +
                                    "		rm.TestTypeNo = rf.TestTypeNo AND rm.SampleNo = rf.SampleNo" +
                                    "		LEFT OUTER JOIN    " +
                                    " dbo.TestItem AS ti ON ti.ItemNo = rm.ItemNo" +
                                    "		where --ReportItem.FormNo=@FormNo \r\n" +
                                    "		rf.PatNo ='''+@PatNo+''' and rm.ITEMNO ='''+ @ItemNo +'''' + @Where)" +
                                    "		--rf.PatNo=@PatNo and rm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate \r\n" +
                                    "    end" +
                                    " else if @Check='marrow'" +
                                    "	begin" +
                                    "	    exec ('select isnull(BloodPercent,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate,CONVERT(varchar(100), rf.checkdate, 23) Checkdate,CONVERT(varchar(100), rf.checktime, 24) checktime,ti.cname as ItemCName from dbo.ReportMarrow rmm INNER JOIN" +
                                    "        dbo.ReportForm rf ON rmm.ReceiveDate = rf.ReceiveDate AND rmm.SectionNo = rf.SectionNo AND " +
                                    "        rmm.TestTypeNo = rf.TestTypeNo AND rmm.SampleNo = rf.SampleNo" +
                                    "        LEFT OUTER JOIN    " +
                                    " dbo.TestItem AS ti ON ti.ItemNo = rmm.ItemNo" +
                                    "        where --ReportItem.FormNo=@FormNo \r\n" +
                                    "        rf.PatNo ='''+ @PatNo+''' and rmm.ITEMNO = '''+@ItemNo +'''' + @Where)" +
                                    "		--rf.PatNo=@PatNo and rmm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate \r\n" +
                                    "	end" +
                                    " END";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("ReportFormAllQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[ReportFormAllQueryDataSource]" +
                                    " AS " +
                                    " SELECT  CONVERT(varchar(10), rf.ReceiveDate, 21) + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), " +
                                    "                   rf.TestTypeNo) + ';' + CONVERT(varchar(30), rf.SampleNo) AS ReportFormID, CONVERT(varchar(10), rf.ReceiveDate, 21) " +
                                    "                   + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), rf.TestTypeNo) + ';' + CONVERT(varchar(30), " +
                                    "                   rf.SampleNo) AS FormNo, gt.CName AS GenderName, gt.ShortCode AS GenderEName, st.CName AS SickType, " +
                                    "                   st.ShortCode AS SickEname, spt.CName AS sampletype, spt.ShortCode AS sampletypeename, ft.CName AS folk, " +
                                    "                   ft.ShortCode AS folkename, dt.CName AS DeptName, dt.ShortCode AS Deptename, dct.CName AS district, " +
                                    "                   dct.ShortCode AS districtename, wt.CName AS ward, au.CName AS AgeUnitName, au.ShortCode AS AgeUnitename, " +
                                    "                   tt.CName AS TestType, tt.ShortCode AS TestTypeename, dgs.CName AS diag, clt.CNAME AS client, clt.ADDRESS, " +
                                    "                   pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate, " +
                                    "                   rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName, rf.GenderNo, rf.Birthday, " +
                                    "                   rf.Age, rf.AgeUnitNo, rf.FolkNo, rf.DistrictNo, rf.WardNo, rf.Bed, rf.DeptNo, rf.Doctor, rf.ChargeNo, rf.Charge, rf.Collecter," +
                                    "                   rf.CollectDate, rf.CollectTime, rf.FormMemo, rf.TestDate, rf.TestTime, rf.Technician, rf.Operator, rf.OperDate, rf.OperTime," +
                                    "                   rf.CheckDate, rf.CheckTime, rf.Checker, rf.SerialNo, rf.RequestSource, rf.DiagNo, rf.PrintTimes, rf.FormComment, " +
                                    "                   rf.Artificerorder, rf.Sickorder, rf.SickTypeNo, rf.Chargeflag, rf.TestDest, rf.SLable, rf.zdy1, rf.zdy2, rf.zdy3, rf.zdy4, rf.zdy5, " +
                                    "                   rf.inceptdate, rf.incepttime, rf.onlinedate, rf.incepter, rf.onlinetime, rf.bmanno, rf.isReceive, rf.ReceiveMan, rf.ReceiveTime, " +
                                    "                   rf.concessionNum, rf.testaim, rf.resultprinttimes, rf.Sender2, rf.SenderTime2, rf.paritemname, rf.clientprint, rf.resultsend, " +
                                    "                   rf.reportsend, rf.CountNodesFormSource, rf.commsendflag, rf.PrintDateTime, rf.PrintOper, rf.abnormityflag, " +
                                    "                   rf.HISDateTime, rf.allowprint, rf.UrgentState, rf.RemoveFeesReason, rf.ZDY6, rf.ZDY7, rf.ZDY8, rf.ZDY9, rf.ZDY10, " +
                                    "                   rf.phoneCode, rf.IsNode, rf.PhoneNodeCount, rf.AutoNodeCount, rf.FormDesc, rf.EquipCommMemo, rf.ESampleNo, " +
                                    "                   rf.EPosition, rf.ISUsePG, rf.OperMemo, rf.FromQCL, rf.ESend, rf.IsDel, rf.EModule, rf.IsRedo, rf.EAchivPosition, " +
                                    "                   rf.FenzhuDatetime, rf.EQCLPosition, rf.EquipResult, rf.EResultToHIS, rf.NurseSender, rf.NurseSendTime, " +
                                    "                   rf.NurseSendCarrier, rf.bGetAllResults, rf.bSendWjz, rf.CollecterID, rf.PrintDateTime1, rf.PrintOper1, rf.HisDoctorId, " +
                                    "                   rf.HisDoctorPhoneCode, rf.LisDoctorNo, rf.LischeckerNo, rf.LastOperator, rf.LastOperDatetime, rf.I_Reportstatus, " +
                                    "                   rf.AutoSended, rf.PatState, rf.EquipCommSend, rf.EquipSampleNo, pg.SectionType, dbo.GetBarCodeItemList(rf.SerialNo, " +
                                    "                   '0') AS DoctorItemName, rf.PageCount, rf.PageName, dbo.ReportItem.ParItemNo, dbo.ReportItem.ItemNo, " +
                                    "                   dbo.ReportItem.OriginalValue, dbo.ReportItem.ReportValue, dbo.ReportItem.OriginalDesc, dbo.ReportItem.ReportDesc, " +
                                    "                   dbo.ReportItem.RefRange, dbo.ReportItem.EquipNo, dbo.ReportItem.Modified, dbo.ReportItem.ItemDate, " +
                                    "                   dbo.ReportItem.ItemTime, dbo.ReportItem.IsMatch, dbo.ReportItem.ResultStatus, dbo.ReportItem.HisValue, " +
                                    "                   dbo.ReportItem.HisComp, dbo.ReportItem.SerialNoParent, dbo.ReportItem.CountNodesItemSource, dbo.ReportItem.Unit, " +
                                    "                   dbo.ReportItem.PlateNo, dbo.ReportItem.PositionNo, dbo.ReportItem.HisReceiveDate, dbo.ReportItem.FromRedoNo, " +
                                    "                   dbo.ReportItem.ItemTestMemo, dbo.ReportItem.ItemDealWith, dbo.ReportItem.Mergeno, dbo.ReportItem.OldParItemNo, " +
                                    "                   dbo.ReportItem.EErrorInfo, dbo.ReportItem.DilutionMultiple, dbo.TestItem.CName AS ItemCname, dbo.TestItem.EName, " +
                                    "                   dbo.TestItem.ShortName, dbo.TestItem.ShortCode" +
                                    " FROM      dbo.ReportForm AS rf INNER JOIN" +
                                    "                   dbo.ReportItem ON rf.ReceiveDate = dbo.ReportItem.ReceiveDate AND rf.SectionNo = dbo.ReportItem.SectionNo AND " +
                                    "                   rf.TestTypeNo = dbo.ReportItem.TestTypeNo AND rf.SampleNo = dbo.ReportItem.SampleNo INNER JOIN" +
                                    "                   dbo.TestItem ON dbo.ReportItem.ItemNo = dbo.TestItem.ItemNo LEFT OUTER JOIN" +
                                    "                   dbo.GenderType AS gt ON rf.GenderNo = gt.GenderNo LEFT OUTER JOIN" +
                                    "                   dbo.SickType AS st ON st.SickTypeNo = rf.SickTypeNo LEFT OUTER JOIN" +
                                    "                   dbo.SampleType AS spt ON spt.SampleTypeNo = rf.SampleTypeNo LEFT OUTER JOIN" +
                                    "                   dbo.FolkType AS ft ON ft.FolkNo = rf.FolkNo LEFT OUTER JOIN" +
                                    "                   dbo.Department AS dt ON dt.DeptNo = rf.DeptNo LEFT OUTER JOIN" +
                                    "                   dbo.District AS dct ON dct.DistrictNo = rf.DistrictNo LEFT OUTER JOIN" +
                                    "                   dbo.WardType AS wt ON wt.WardNo = rf.WardNo LEFT OUTER JOIN" +
                                    "                   dbo.AgeUnit AS au ON au.AgeUnitNo = rf.AgeUnitNo LEFT OUTER JOIN" +
                                    "                   dbo.TestType AS tt ON tt.TestTypeNo = rf.TestTypeNo LEFT OUTER JOIN" +
                                    "                   dbo.Diagnosis AS dgs ON dgs.DiagNo = rf.DiagNo LEFT OUTER JOIN" +
                                    "                   dbo.CLIENTELE AS clt ON clt.ClIENTNO = rf.clientno LEFT OUTER JOIN" +
                                    "                   dbo.PGroup AS pg ON pg.SectionNo = rf.SectionNo";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("GetReportMicroGroupFullList", "p"))
                    {
                        updateSql = "CREATE PROCEDURE [dbo].[GetReportMicroGroupFullList] \r\n" +
                                    "	-- Add the parameters for the stored procedure here \r\n" +
                                    "@ReceiveDate varchar(10), --核收时间 \r\n" +
                                    "@SectionNo varchar(50), --小组号 \r\n" +
                                    "@TestTypeNo varchar(50), --检验类型号 \r\n" +
                                    "@SampleNo varchar(50) --样本号 \r\n" +
                                    " AS " +
                                    " BEGIN " +
                                    "	-- SET NOCOUNT ON added to prevent extra result sets from  \r\n" +
                                    "	-- interfering with SELECT statements.  \r\n" +
                                    "	SET NOCOUNT ON; " +
                                    " " +
                                    "    -- Insert statements for procedure here  \r\n" +
                                    "	select * from( " +
                                    " select COUNT(rm.ReceiveDate) cn,rm.ReceiveDate, rm.ItemNo,''MicroNo,'' DescNo,''AntiNo,ti.CName itemname ,NULL MicroName,'' DescName ,NULL AntiName,NULL Suscept,NULL SusDesc,	NULL SusQuan, " +
                                    " replace(CONVERT(varchar(12) , rm.ReceiveDate, 111 ),'/','-') SJ,case WHEN rf.PATNO is null then rf.zdy3 ELSE PATNO end as PATNO,  " +
                                    " --添加英文字段 \r\n" +
                                    " ti.EName itemename ,NULL MicroEName,'' DescEName ,NULL AntiEName " +
                                    " " +
                                    " From ReportMicro rm " +
                                    " LEFT OUTER JOIN  dbo.TestItem ti ON rm.ItemNo = ti.ItemNo " +
                                    " INNER JOIN  dbo.ReportForm rf ON rf.ReceiveDate = rm.ReceiveDate AND rf.SectionNo = rm.SectionNo AND  " +
                                    "                      rf.TestTypeNo = rm.TestTypeNo AND rf.SampleNo = rm.SampleNo " +
                                    " where  " +
                                    " rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo " +
                                    " group by rm.ReceiveDate, rf.zdy3,rf.PatNo,rm.ItemNo,ti.CName,ti.EName " +
                                    " union " +
                                    " select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,'' DescNo,''AntiNo, " +
                                    " NULL itemname,mi.CName MicroName,'' DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo,  " +
                                    "--添加英文字段 \r\n" +
                                    " NULL itemename,mi.EName MicroEName ,'' DescEName ,NULL AntiEName " +
                                    " " +
                                    " From ReportMicro rm " +
                                    " LEFT OUTER JOIN dbo.MicroItem mi ON rm.MicroNo = mi.MicroNo " +
                                    " where  " +
                                    " rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.MicroNo is not null  " +
                                    " group by ReceiveDate,ItemNo,rm.MicroNo,mi.CName,mi.EName " +
                                    " union " +
                                    " " +
                                    " select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,''AntiNo,NULL itemname,NUll MicroName, " +
                                    " mp.CName DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo,  " +
                                    "--添加英文字段 \r\n" +
                                    " NULL itemename,NULL MicroEName ,mp.ShortCode DescEName ,NULL AntiEName " +
                                    " " +
                                    " From ReportMicro rm " +
                                    " left outer join dbo.MicroPhrase mp on rm.DescNo = mp.DescNo " +
                                    " where  " +
                                    " rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.DescNo is not null  " +
                                    " group by ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,mp.CName,mp.ShortCode " +
                                    " union " +
                                    " select COUNT(ReceiveDate) cn,ReceiveDate, ItemNo,MicroNo,rm.AntiNo,rm.DescNo,NULL itemname,NULL MicroName, " +
                                    " NULL DescName,ab.CName AntiName,CASE WHEN rm.Suscept = 'S' THEN '敏感' WHEN rm.Suscept = 'R' THEN '耐药' WHEN rm.Suscept = 'I' THEN '中介'   ELSE rm.Suscept END AS Suscept,rm.SusDesc,rm.SusQuan ,NULL SJ , NULL PatNo, " +
                                    "--添加英文字段 \r\n" +
                                    " NULL itemename,NULL MicroEName ,NULL DescEName ,AB.EName AntiEName " +
                                    " " +
                                    "     From ReportMicro rm LEFT OUTER JOIN dbo.AntiBiotic ab ON rm.AntiNo = ab.AntiNo  " +
                                    " where  " +
                                    " rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.MicroNo is not null and rm.AntiNo is not null " +
                                    " group by  rm.ReceiveDate,ItemNo,MicroNo,rm.AntiNo,ab.CName,ab.EName,Suscept,rm.SusDesc,rm.SusQuan,rm.DescNo " +
                                    " ) b order by ItemNo,DescNo,MicroNo,AntiNo " +
                                    " END ";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("B_ColumnsSetting", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[B_ColumnsSetting] (" +
                                    "  [OrderFlag] bit  NULL," +
                                    "  [OrderDesc] int  NULL," +
                                    "  [OrderMode] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [CTID] bigint  NOT NULL," +
                                    "  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [ShowName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Width] int  NULL," +
                                    "  [OrderNo] int  NULL," +
                                    "  [Site] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [AppType] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [DataAddTime] datetime  NULL," +
                                    "  [DataUpdateTime] datetime  NULL," +
                                    "  [IsShow] bit  NULL," +
                                    "  [ColID] bigint  NULL," +
                                    "  [ColumnName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Render] text COLLATE Chinese_PRC_CI_AS  NULL" +
                                    ")";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_ColumnsSetting] SET (LOCK_ESCALATION = TABLE);";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE[dbo].[B_ColumnsSetting] ADD CONSTRAINT[PK_B_COLUMNSSETTING] PRIMARY KEY CLUSTERED([CTID])" +
                                    "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  " +
                                    "ON [PRIMARY]";
                        listSQL.Add(updateSql);

                    }
                    if (!CheckDataObjectIsExists("B_ColumnsUnit", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[B_ColumnsUnit] (" +
                                    "  [ColID] bigint  NOT NULL," +
                                    "  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [ColumnName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Type] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Width] int  NULL," +
                                    "  [DataAddTime] datetime  NULL," +
                                    "  [DataUpdateTime] datetime  NULL," +
                                    "  [Render] text COLLATE Chinese_PRC_CI_AS  NULL)";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_ColumnsUnit] SET (LOCK_ESCALATION = TABLE);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'1', N'床号', N'Bed', NULL, N'40', NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'2', N'报告日期', N'CHECKDATE', NULL, N'150', NULL, NULL, N'function (v, meta, record, index) {" +
                                    "	            //显示审核时间 \r\n" +
                                    "							var Cdate = Shell.util.Date.toString(record.get(\"CHECKDATE\"), true);" +
                                    "							var Ctime = Shell.util.Date.toString(record.get(\"CHECKTIME\"), false);" +
                                    "							var value = '''';" +
                                    "							if(Cdate !=null){" +
                                    "								value = Cdate;" +
                                    "							}" +
                                    "							if(Ctime !=null){" +
                                    "							  var arry = Ctime.split('' '');" +
                                    "							  if(arry!=null && arry.length >1){" +
                                    "								 value +='' ''+ arry[1];" +
                                    "								}" +
                                    "							}" +
                                    "	            return value;" +
                                    "	        }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'3', N'姓名', N'CNAME', NULL, N'60', NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'4', N'检验项目', N'ItemName', NULL, N'140', NULL, NULL, N'function (value, meta, record) {if (value) meta.tdAttr = ''data-qtip=\" <b> '' + value + '' </b> \"'';         return value;	        }');";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'5', N'病历号', N'PatNo', NULL, N'60', NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'6', N'核收日期', N'RECEIVEDATE', NULL, N'80', NULL, NULL, N'function (v, meta, record, index) {" +
                                    "	 var value = record.get(\"RECEIVEDATE\").split('' '')[0];" +
                                    "	 return value;" +
                                    "}')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'7', N'打印次数', N'PRINTTIMES', NULL, N'60', NULL, NULL, N'function (v, meta, record) {" +
                                "	                var imgName = (v && v >= me.maxPrintTimes) ? \"unprint\" : \"print\"," +
                                "		                tootip = \"已经打印 < b style = ''color: red; '' > \" + v +\" </b> 次\"," +
                                "	                    value = v ? \" <b> \" + v + \" </b> \" : \"\";" +
                                "	                meta.tdAttr = ''data-qtip=\"'' + tootip + ''\"'';	        " +
                                "	                var result = '''';" +
                                "	                if(v >= 0){ " +
                                "	                    result = \" < img src = ''\" + Shell.util.Path.uiPath + \" / ReportPrint / images / \" + imgName + \".png'' /> \" + v;" +
                                "	                }	        " +
                                "	                return result;" +
                                "	            }')";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_ColumnsUnit] ADD CONSTRAINT [PK_B_COLUMNSUNIT] PRIMARY KEY CLUSTERED ([ColID])" +
                                    "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  " +
                                    "ON [PRIMARY]";
                        listSQL.Add(updateSql);

                    }
                    if (CheckDataObjectIsExists("B_Parameter", "U"))
                    {
                        DataSet dss = SqlServerHelper.QuerySql(" select * from B_Parameter ", ADOConnectStr);
                        if (dss == null || dss.Tables == null || dss.Tables[0].Rows.Count < 2)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'1', N'页面公共配置', N'allPageType', N'config', N'defaultWhere', N'', NULL, N'string', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'2', N'页面公共配置', N'allPageType', N'config', N'requestParamsArr', N'', NULL, N'stringArry', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO[dbo].[B_Parameter]  VALUES(N'3', N'页面公共配置', N'allPageType', N'config', N'hisRequestParamsArr', N'OLDSERIALNO', NULL, N'stringArry', NULL, NULL, NULL, NULL); ";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'4', N'页面公共配置', N'allPageType', N'config', N'defaultDates', N'1', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'5', N'页面公共配置', N'allPageType', N'config', N'defaultPageSize', N'50', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'6', N'页面公共配置', N'allPageType', N'config', N'hasPrint', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'7', N'页面公共配置', N'allPageType', N'config', N'A4Type', N'1', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'8', N'页面公共配置', N'allPageType', N'config', N'printType', N'A4', NULL, N'string', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'9', N'页面公共配置', N'allPageType', N'config', N'maxPrintTimes', N'100', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'10', N'页面公共配置', N'allPageType', N'config', N'mergePageCount', N'100', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'11', N'页面公共配置', N'allPageType', N'config', N'ForcedPagingField', N'', NULL, N'hash', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'12', N'页面公共配置', N'allPageType', N'config', N'openAddPrintTimes', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'13', N'页面公共配置', N'allPageType', N'config', N'checkUnprint', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'14', N'页面公共配置', N'allPageType', N'config', N'checkFilter', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'15', N'页面公共配置', N'allPageType', N'config', N'headCollapsed', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'16', N'页面公共配置', N'allPageType', N'config', N'autoSelect', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'17', N'页面公共配置', N'allPageType', N'config', N'CheckOnly', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'18', N'页面公共配置', N'allPageType', N'config', N'hasReportPage', N'true', NULL, N'bool', NULL, NULL, NULL, NULL); ";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'19', N'页面公共配置', N'allPageType', N'config', N'hasResultPage', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'20', N'页面公共配置', N'allPageType', N'config', N'defaultCheckedPage', N'1', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'21', N'页面公共配置', N'allPageType', N'config', N'hasPdfPrinter', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'22', N'页面公共配置', N'allPageType', N'config', N'pdfPrinterList', N'', NULL, N'stringArry', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'23', N'页面公共配置', N'allPageType', N'config', N'DateField', NULL, NULL, N'string', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'24', N'页面公共配置', N'allPageType', N'config', N'isListHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'66', N'查询打印页面配置', N'自助打印', N'config', N'printPageType', N'双A5', N'', N'string', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'67', N'查询打印页面配置', N'自助打印', N'config', N'printtimes', N'1', N'', N'int', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'68', N'查询打印页面配置', N'自助打印', N'config', N'selectColumn', N'PatNo', N'', N'string', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'69', N'查询打印页面配置', N'自助打印', N'config', N'lastDay', N'100', N'', N'int', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'70', N'查询打印页面配置', N'自助打印', N'config', N'tackTime', N'5', N'', N'int', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'71', N'查询打印页面配置', N'自助打印', N'config', N'showName', N'卡名', N'', N'string', N'2018-10-16 11:19:54.000', N'0', NULL, NULL);";

                        }


                    }
                    if (!CheckDataObjectIsExists("B_SearchSetting", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[B_SearchSetting] (" +
                                    "  [JsCode] text COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Type] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [SelectName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Xtype] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Mark] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Listeners] text COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [STID] bigint  NOT NULL," +
                                    "  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [ShowName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [TextWidth] int  NULL," +
                                    "  [Width] int  NULL," +
                                    "  [ShowOrderNo] int  NULL," +
                                    "  [Site] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [AppType] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [DataAddTime] datetime  NULL," +
                                    "  [DataUpdateTime] datetime  NULL," +
                                    "  [IsShow] bit  NULL," +
                                    "  [SID] bigint  NULL)";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_SearchSetting] SET (LOCK_ESCALATION = TABLE);";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_SearchSetting] ADD CONSTRAINT [PK_B_SELECTSETTING] PRIMARY KEY CLUSTERED ([STID])" +
                                    "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  " +
                                    "ON [PRIMARY]";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("B_SearchUnit", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[B_SearchUnit] (" +
                                    "  [SID] bigint  NOT NULL," +
                                    "  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [SelectName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Type] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [TextWidth] int  NULL," +
                                    "  [Width] int  NULL," +
                                    "  [DataAddTime] datetime  NULL," +
                                    "  [DataUpdateTime] datetime  NULL," +
                                    "  [Xtype] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Mark] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Listeners] text COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [JsCode] text COLLATE Chinese_PRC_CI_AS  NULL)";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_SearchUnit] SET (LOCK_ESCALATION = TABLE);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'1', N'科室过滤', N'DeptNo', N'other', N'55', N'150', NULL, NULL, N'uxCheckTrigger', NULL, N'{" +
                                    "                    check: function (p, record) {" +
                                    "                        if (record == null) {" +
                                    "                            me.items.items[1].getComponent(\"DeptNo\").setValue(\"\");" +
                                    "                            me.items.items[1].getComponent(\"ClienteleName\").setValue(\"\");" +
                                    "                           return;" +
                                    "                        }" +
                                    "                        me.items.items[1].getComponent(\"DeptNo\").setValue(\"(\" + record.get(\"DeptNo\") + \")\");" +
                                    "                       me.items.items[1].getComponent(\"ClienteleName\").setValue(record.get(\"CName\"));" +
                                    "                        p.close();" +
                                    "                    }" +
                                    "                }', N'{ type: ''search'', xtype: ''textfield'', mark: ''in'', itemId: \"DeptNo\", name: ''DeptNo'', width: 130, hidden: true } searchAndNext" +
                                    "            {" +
                                    "                fieldLabel: ''''," +
                                    "                xtype: ''uxCheckTrigger''," +
                                    "                emptyText: ''科室过滤''," +
                                    "                zIndex:1," +
                                    "                width: 150," +
                                    "                labelSeparator: ''''," +
                                    "                labelWidth: 55," +
                                    "                labelAlign: ''right''," +
                                    "                itemId: ''ClienteleName''," +
                                    "                className: ''Shell.class.dept.CheckGrid''," +
                                    "                listeners: {" +
                                    "                    check: function (p, record) {" +
                                    "                        var item = \"\";" +
                                    "                        var clientName = \"\";" +
                                    "                        var me = this.ownerCt.ownerCt;" +
                                    "						if (record == null) {" +
                                    "                            item = me.getItem(\"DeptNo\");" +
                                    "                            clientName = me.getItem(\"ClienteleName\");" +
                                    "                            item.setValue(\"\");" +
                                    "                            clientName.setValue(\"\");" +
                                    "                            return;" +
                                    "                        }						" +
                                    "                        item = me.getItem(\"DeptNo\");" +
                                    "                        clientName = me.getItem(\"ClienteleName\");" +
                                    "                        item.setValue(\"(\" + record.get(\"DeptNo\") + \")\");" +
                                    "                        clientName.setValue(record.get(\"CName\"));" +
                                    "                        p.close();" +
                                    "                    }" +
                                    "                }" +
                                    "            }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'2', N'就诊类型', N'SickTypeName', N'other', NULL, NULL, NULL, NULL, N'combobox', NULL, NULL, N'{ " +
                                    "                type: ''other'', xtype: ''checkbox'', itemId: ''checkSickType'', boxLabel: ''门诊'', width: 50, " +
                                    "                listeners:{ " +
                                    "                    change: function (m, v) { " +
                                    "					    var me = this.ownerCt.ownerCt; " +
                                    "                        var sick = me.getItem(\"mgSickType\"); " +
                                    "                        if (v) { " +
                                    "                            if (sick.getValue().length < 3 || sick.getValue() == \"\" || sick.getValue == null) { " +
                                    "                                sick.setValue(\"(''门诊'')\"); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) +\",''门诊'')\"); " +
                                    "                            } " +
                                    "                            sick.type = ''search''; " +
                                    "                       } else { " +
                                    "                            var index = sick.getValue().indexOf(\"门诊\"); " +
                                    "                           if (index == 2) { " +
                                    "                                sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6)); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3)); " +
                                    "                            } " +
                                    "                        } " +
                                    "                        if (sick.getValue() == \"\" || sick.getValue() == null || sick.getValue().length < 6) { " +
                                    "                            sick.type = ''other''; " +
                                    "                            sick.setValue(\"\"); " +
                                    "                        } " +
                                    "                    } " +
                                    "	            } " +
                                    "            }searchAndNext " +
                                    "            { type: ''search'', xtype: ''textfield'', itemId: ''mgSickType'', mark: ''in'', name: ''SickTypeName'', hidden: true }searchAndNext " +
                                    "            { " +
                                    "                type: ''other'', xtype: ''checkbox'', itemId: ''checkSickType1'', boxLabel: ''住院'', width: 50, " +
                                    "                listeners: { " +
                                    "                    change: function (m, v) { " +
                                    "					    var me = this.ownerCt.ownerCt; " +
                                    "                        var sick = me.getItem(\"mgSickType\"); " +
                                    "                        if (v) { " +
                                    "                            if (sick.getValue().length < 3 || sick.getValue() == \"\" || sick.getValue == null) { " +
                                    "                                sick.setValue(\"(''住院'')\"); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + \",''住院'')\"); " +
                                    "                            } " +
                                    "                            sick.type = ''search''; " +
                                    "                        } else { " +
                                    "                            var index = sick.getValue().indexOf(\"住院\"); " +
                                    "                            if (index == 2) { " +
                                    "                                sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6)); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3)); " +
                                    "                            } " +
                                    "                        } " +
                                    "                        if (sick.getValue() == \"\" || sick.getValue() == null || sick.getValue().length < 6) { " +
                                    "                           sick.type = ''other''; " +
                                    "                       } " +
                                    "                    } " +
                                    "                } " +
                                    "            }searchAndNext " +
                                    "            { " +
                                    "                type: ''other'', xtype: ''checkbox'', itemId: ''checkSickType2'', boxLabel: ''体检'', width: 50, " +
                                    "                listeners: { " +
                                    "                    change: function (m, v) { " +
                                    "				     	var me = this.ownerCt.ownerCt; " +
                                    "                       var sick = me.getItem(\"mgSickType\"); " +
                                    "                       if (v) { " +
                                    "                            if (sick.getValue().length < 3 || sick.getValue() == \"\" || sick.getValue == null) { " +
                                    "                               sick.setValue(\"(''体检'')\"); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + \",''体检'')\"); " +
                                    "                            } " +
                                    "                            sick.type = ''search''; " +
                                    "                        } else { " +
                                    "                            var index = sick.getValue().indexOf(\"体检\"); " +
                                    "                            if (index == 2) { " +
                                    "                                sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6)); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3)); " +
                                    "                            } " +
                                    "                        } " +
                                    "                        if (sick.getValue() == \"\" || sick.getValue() == null || sick.getValue().length < 6) { " +
                                    "                            sick.type = ''other''; " +
                                    "                       }          }           }       }') ";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'3', N'姓名', N'CNAME', N'search', N'35', N'110', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''CNAME'', fieldLabel: ''姓名'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'4', N'样本号', N'SAMPLENO', N'search', N'45', N'150', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''SAMPLENO'', fieldLabel: ''样本号'', labelWidth: 50, width: 150 }');";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'5', N'病历号', N'PATNO', N'search', N'45', N'150', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''PATNO'', fieldLabel: ''病历号'', labelWidth: 50, width: 150 }');";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'6', N'床号', N'Bed', N'search', N'35', N'150', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''Bed'', fieldLabel: ''床号'', labelWidth: 30, width: 130 }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'7', N'本周', N'本周', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'{ text: \"本周\", tooltip: \"查询本周数据\", vType:1,where: \"datediff(week, RECEIVEDATE, getdate()) = 0\" }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'8', N'本月', N'本月', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'{ text: \"本月\", tooltip: \"查询本月数据\", vType:2,where: \"datediff(month, RECEIVEDATE, getdate()) = 0\" }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'9', N'时间', N'时间', NULL, NULL, NULL, NULL, NULL, N'combobox', NULL, NULL, N'{ " +
                                    "                type: ''other'',  " +
                                    "				 xtype: ''combobox'', " +
                                    "                name: ''defaultPageSize'', " +
                                    "                itemId: ''defaultPageSize'', " +
                                    "                width: 130, " +
                                    "               displayField: ''text'',  " +
                                    "valueField: ''value'', " +
                                    "store:Ext.create(''Ext.data.Store'', { " +
                                    "	                    fields: [''text'', ''value''], " +
                                    "	                    data: [ " +
                                    "                           { text: ''审核（报告）时间'', value: ''CHECKDATE'' }, " +
                                    "                            { text: ''核收日期'', value: ''RECEIVEDATE'' }, " +
                                    "                            { text: ''采样日期'', value: ''COLLECTDATE'' }, " +
                                    "                            { text: ''签收日期'', value: ''INCEPTDATE'' }, " +
                                    "                           { text: ''检测（上机）日期'', value: ''TESTDATE'' }, " +
                                    "                            { text: ''录入（操作）日期'', value: ''OPERDATE'' } " +
                                    "	                    ] " +
                                    "	                }), " +
                                    "                value: [''CHECKDATE''], " +
                                    "                listeners: { " +
                                    "                    change: function (m, v) { " +
                                    "                        var selectDate = this.ownerCt.ownerCt.getItem(''selectdate''); " +
                                    "                        selectDate.name = v; " +
                                    "                        this.ownerCt.ownerCt.DateField = v; " +
                                    "                   } " +
                                    "                } " +
                                    "            }searchAndNext " +
                                    "		    { type: ''search'', xtype: ''uxdatearea'', itemId: ''selectdate'', name: ''CHECKDATE'', labelWidth: 0, width: 190 }') ";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_SearchUnit] ADD CONSTRAINT [PK_B_SELECTUNIT] PRIMARY KEY CLUSTERED ([SID])" +
                                    "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) " +
                                    "ON [PRIMARY]";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("RequestFormQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[RequestFormQueryDataSource]" +
                                    " AS " +
                                    "SELECT  (SELECT  ParaValue" +
                                    "                   FROM       dbo.SysPara" +
                                    "                   WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, rf.SectionNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(char, rf.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, rf.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), rf.ReceiveDate, 20))) AS ReportFormID, CONVERT(varchar(10), rf.ReceiveDate, " +
                                    "                   21) + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), rf.TestTypeNo) + ';' + CONVERT(varchar(30)," +
                                    "                   rf.SampleNo) AS FormNo, gt.CName AS Gender, gt.ShortCode AS GenderEName, st.CName AS SickType, " +
                                    "                   st.ShortCode AS SickEname, spt.CName AS sampletype, spt.ShortCode AS sampletypeename, ft.CName AS folk, " +
                                    "                   ft.ShortCode AS folkename, dt.CName AS Dept, dt.ShortCode AS Deptename, dct.CName AS district, " +
                                    "                   dct.ShortCode AS districtename, wt.CName AS ward, au.CName AS AgeUnit, au.ShortCode AS AgeUnitename, " +
                                    "                   tt.CName AS TestType, tt.ShortCode AS TestTypeename, dgs.CName, clt.CNAME AS client, clt.ADDRESS, " +
                                    "                   pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate, " +
                                    "                   rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName AS Expr1, rf.GenderNo, " +
                                    "                   rf.Birthday, rf.Age, rf.AgeUnitNo, rf.FolkNo, rf.DistrictNo, rf.WardNo, rf.Bed, rf.DeptNo, rf.Doctor, rf.ChargeNo, rf.Charge, " +
                                    "                   rf.Collecter, rf.CollectDate, rf.CollectTime, rf.FormMemo, rf.TestDate, rf.TestTime, rf.Technician, rf.Operator, rf.OperDate, " +
                                    "                   rf.OperTime, rf.CheckDate, rf.CheckTime, rf.Checker, rf.SerialNo, rf.RequestSource, rf.DiagNo, rf.PrintTimes, " +
                                    "                   rf.FormComment, rf.Artificerorder, rf.Sickorder, rf.SickTypeNo, rf.Chargeflag, rf.TestDest, rf.SLable, rf.zdy1, rf.zdy2, rf.zdy3, " +
                                    "                   rf.zdy4, rf.zdy5, rf.inceptdate, rf.incepttime, rf.onlinedate, rf.incepter, rf.onlinetime, rf.bmanno, rf.isReceive, rf.ReceiveMan," +
                                    "                   rf.ReceiveTime, rf.concessionNum, rf.resultstatus, rf.testaim, rf.resultprinttimes, rf.Sender2, rf.SenderTime2, " +
                                    "                   rf.paritemname, rf.clientprint, rf.resultsend, rf.reportsend, rf.CountNodesFormSource, rf.commsendflag, rf.PrintDateTime, " +
                                    "                   rf.PrintOper, rf.abnormityflag, rf.HISDateTime, rf.allowprint, rf.UrgentState, rf.RemoveFeesReason, rf.ZDY6, rf.ZDY7, " +
                                    "                   rf.ZDY8, rf.ZDY9, rf.phoneCode, rf.IsNode, rf.PhoneNodeCount, rf.AutoNodeCount, rf.FormDesc, rf.EquipCommMemo, " +
                                    "                   rf.ESampleNo, rf.EPosition, rf.ISUsePG, rf.OperMemo, rf.FromQCL, rf.ESend, rf.IsDel, rf.EModule, rf.IsRedo, " +
                                    "                   rf.EAchivPosition, rf.FenzhuDatetime, rf.EQCLPosition, rf.EquipResult, rf.EResultToHIS, rf.NurseSender, rf.NurseSendTime, " +
                                    "                   rf.NurseSendCarrier, rf.bGetAllResults, rf.bSendWjz, rf.CollecterID, rf.PrintDateTime1, rf.PrintOper1, rf.HisDoctorId, " +
                                    "                   rf.HisDoctorPhoneCode, rf.LisDoctorNo, rf.LischeckerNo, rf.LastOperator, rf.LastOperDatetime, rf.I_Reportstatus, " +
                                    "                   rf.AutoSended, rf.PatState, rf.EquipCommSend, rf.EquipSampleNo, pg.SectionType, CASE WHEN CONVERT(varchar(50), " +
                                    "                   rf.clientno) IS NULL THEN '425' WHEN CONVERT(varchar(50), rf.clientno) = '' THEN '425' ELSE CONVERT(varchar(50), " +
                                    "                   rf.clientno) END AS Expr2, dbo.GetBarCodeItemList(rf.SerialNo, '0') AS ItemName, rf.PageCount, rf.PageName, " +
                                    "                   CASE WHEN ((rf.SectionNo >= 2 AND rf.SectionNo <= 14) OR" +
                                    "                   rf.SectionNo = 41 OR" +
                                    "                   rf.SectionNo = 43) THEN rf.CheckDate ELSE rf.SenderTime2 END AS Expr3, st.CName AS SICKTYPENAME, rf.clientno, " +
                                    "                   pg.SuperGroupNo, pg.CName AS SuperGroupName, spt.CName AS SampletypeName" +
                                    " FROM      dbo.RequestForm AS rf LEFT OUTER JOIN " +
                                    "                   dbo.GenderType AS gt ON rf.GenderNo = gt.GenderNo LEFT OUTER JOIN" +
                                    "                   dbo.SickType AS st ON st.SickTypeNo = rf.SickTypeNo LEFT OUTER JOIN" +
                                    "                   dbo.SampleType AS spt ON spt.SampleTypeNo = rf.SampleTypeNo LEFT OUTER JOIN" +
                                    "                   dbo.FolkType AS ft ON ft.FolkNo = rf.FolkNo LEFT OUTER JOIN" +
                                    "                   dbo.Department AS dt ON dt.DeptNo = rf.DeptNo LEFT OUTER JOIN" +
                                    "                   dbo.District AS dct ON dct.DistrictNo = rf.DistrictNo LEFT OUTER JOIN" +
                                    "                   dbo.WardType AS wt ON wt.WardNo = rf.WardNo LEFT OUTER JOIN" +
                                    "                   dbo.AgeUnit AS au ON au.AgeUnitNo = rf.AgeUnitNo LEFT OUTER JOIN" +
                                    "                   dbo.TestType AS tt ON tt.TestTypeNo = rf.TestTypeNo LEFT OUTER JOIN" +
                                    "                   dbo.Diagnosis AS dgs ON dgs.DiagNo = rf.DiagNo LEFT OUTER JOIN" +
                                    "                   dbo.CLIENTELE AS clt ON clt.ClIENTNO = rf.clientno LEFT OUTER JOIN" +
                                    "                   dbo.PGroup AS pg ON pg.SectionNo = rf.SectionNo LEFT OUTER JOIN" +
                                    "                   dbo.SuperGroup AS sg ON sg.SuperGroupNo = pg.SuperGroupNo";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("RequestItemQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[RequestItemQueryDataSource]" +
                                    " AS " +
                                    "SELECT  CONVERT(varchar(10), ri.ReceiveDate, 21) + ';' + CONVERT(varchar(30), ri.SectionNo) + ';' + CONVERT(varchar(30), " +
                                    "                   ri.TestTypeNo) + ';' + CONVERT(varchar(30), ri.SampleNo) AS FormNo," +
                                    "                       (SELECT  ParaValue" +
                                    "                        FROM       dbo.SysPara" +
                                    "                        WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, ri.SectionNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportItemID, '__' + LTRIM(RTRIM(CONVERT(char, " +
                                    "                   ri.SectionNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportFormID, dbo.getitemrow(ri.ReceiveDate, " +
                                    "                   ri.SectionNo, ri.TestTypeNo, ri.SampleNo, t1.DispOrder) AS rowId, ri.ReceiveDate, ri.SectionNo, ri.TestTypeNo, ri.SampleNo, " +
                                    "                   ri.ParItemNo, ri.ItemNo, ri.OriginalValue, ri.ReportValue, ri.OriginalDesc, ri.ReportDesc, ri.StatusNo, ri.RefRange, ri.EquipNo, " +
                                    "                   ri.Modified, ri.ItemDate, ri.ItemTime, ri.IsMatch, CASE WHEN ((LTRIM(RTRIM(ISNULL(ri.ReportDesc, ''))) = '阳性') AND " +
                                    "                   ((t1.CName NOT LIKE '%RH%') AND (t1.CName NOT LIKE '%血型%'))) THEN 'H' ELSE ResultStatus END AS ResultStatus, " +
                                    "                   ri.HisValue, ri.HisComp, ri.isReceive, ri.SerialNoParent, ri.zdy1, ri.zdy2, ri.zdy3, ri.zdy4, ri.zdy5, ri.CountNodesItemSource, " +
                                    "                   ri.Unit AS ItemUnit, ri.PlateNo, ri.PositionNo, ri.EquipCommMemo, ri.EquipCommSend, ri.EValueState, ri.EModule, " +
                                    "                   ri.EPosition, ri.ESend, ri.IsRedo, ri.IsDel, ri.HisReceiveDate, ri.FromRedoNo, ri.I_Reportstatus, ri.ItemTestMemo, " +
                                    "                   ri.ItemDealWith, ri.I_EResult, CASE WHEN isnull(t1.Prec, 0) = 0 THEN ISNULL(CONVERT(varchar, " +
                                    "                   CAST(ReportValue AS decimal(18, 0))), '') + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 1) " +
                                    "                   = 1 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 1))), '') + ISNULL(ri.ReportDesc, '') " +
                                    "                   WHEN isnull(t1.Prec, 0) = 2 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') " +
                                    "                   + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 0) = 3 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, " +
                                    "                   3))), '') + ISNULL(ri.ReportDesc, '') ELSE ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') " +
                                    "                   + ISNULL(ri.ReportDesc, '') END AS ItemValue, t1.DispOrder, t1.CName AS ItemCname, t1.EName AS itemename, " +
                                    "                   t2.CName AS paritemname, t1.Secretgrade, t1.ShortName, t1.ShortCode, t1.OrderNo, t1.StandardCode, t1.Cuegrade, " +
                                    "                   t1.DiagMethod, '' AS ReportTemp" +
                                    " FROM      dbo.RequestItem AS ri LEFT OUTER JOIN" +
                                    "                   dbo.TestItem AS t1 ON t1.ItemNo = ri.ItemNo LEFT OUTER JOIN" +
                                    "                   dbo.TestItem AS t2 ON t2.ItemNo = ri.ParItemNo";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("RequestMarrowQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[RequestMarrowQueryDataSource]" +
                                    " AS " +
                                    "SELECT  CONVERT(varchar(10), dbo.RequestForm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), dbo.RequestForm.SectionNo) " +
                                    "                   + ';' + CONVERT(varchar(30), dbo.RequestForm.TestTypeNo) + ';' + CONVERT(varchar(30), dbo.RequestForm.SampleNo) " +
                                    "                   AS FormNo, dbo.RequestMarrow.ReceiveDate, dbo.RequestMarrow.SectionNo, dbo.RequestMarrow.TestTypeNo, " +
                                    "                   dbo.RequestMarrow.SampleNo, dbo.RequestMarrow.ParItemNo, dbo.RequestMarrow.ItemNo, dbo.RequestMarrow.BloodNum, " +
                                    "                   dbo.RequestMarrow.BloodPercent, dbo.RequestMarrow.MarrowNum, dbo.RequestMarrow.MarrowPercent, " +
                                    "                   dbo.RequestMarrow.BloodDesc, dbo.RequestMarrow.MarrowDesc, dbo.RequestMarrow.StatusNo, " +
                                    "                   dbo.RequestMarrow.RefRange, dbo.RequestMarrow.EquipNo, dbo.RequestMarrow.IsCale, dbo.RequestMarrow.Modified, " +
                                    "                   '__' + LTRIM(RTRIM(CONVERT(char, dbo.RequestMarrow.SectionNo))) + '_' + LTRIM(RTRIM(CONVERT(char, " +
                                    "                   dbo.RequestMarrow.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, dbo.RequestMarrow.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), dbo.RequestMarrow.ReceiveDate, 20))) AS RequestFormID, " +
                                    "                   dbo.RequestMarrow.ItemDate, dbo.RequestMarrow.ItemTime, dbo.RequestMarrow.IsMatch, dbo.RequestMarrow.ResultStatus, " +
                                    "                   dbo.TestItem.CName AS ItemCName, dbo.TestItem.EName AS ItemEName, TestItem_1.CName AS ParItemCName, " +
                                    "                   TestItem_1.EName AS ParItemEName, dbo.S_RequestVItem.ParItemNo AS Expr1, dbo.S_RequestVItem.ItemNo AS Expr2, " +
                                    "                   dbo.S_RequestVItem.OrgValue, dbo.S_RequestVItem.ReportValue, dbo.S_RequestVItem.OrgDesc, " +
                                    "                   dbo.S_RequestVItem.ReportDesc, dbo.S_RequestVItem.ReportText, dbo.S_RequestVItem.ReportImage, " +
                                    "                   dbo.S_RequestVItem.RefRange AS Expr3, dbo.S_RequestVItem.EquipNo AS Expr4, " +
                                    "                   dbo.S_RequestVItem.Modified AS Expr5, dbo.S_RequestVItem.ItemDate AS Expr6, " +
                                    "                   dbo.S_RequestVItem.ItemTime AS Expr7, dbo.S_RequestVItem.ResultStatus AS Expr8, dbo.S_RequestVItem.IsPrint, " +
                                    "                   dbo.S_RequestVItem.PrintOrder, dbo.S_RequestVItem_C.CItemNo," +
                                    "                       (SELECT  CName" +
                                    "                        FROM       dbo.Equipment" +
                                    "                        WHERE    (EquipNo = dbo.RequestMarrow.EquipNo)) AS EquipName" +
                                    " FROM      dbo.RequestForm INNER JOIN" +
                                    "                   dbo.RequestMarrow ON dbo.RequestForm.ReceiveDate = dbo.RequestMarrow.ReceiveDate AND " +
                                    "                   dbo.RequestForm.SectionNo = dbo.RequestMarrow.SectionNo AND " +
                                    "                   dbo.RequestForm.TestTypeNo = dbo.RequestMarrow.TestTypeNo AND " +
                                    "                   dbo.RequestForm.SampleNo = dbo.RequestMarrow.SampleNo INNER JOIN" +
                                    "                   dbo.TestItem ON dbo.RequestMarrow.ItemNo = dbo.TestItem.ItemNo INNER JOIN" +
                                    "                   dbo.TestItem AS TestItem_1 ON dbo.RequestMarrow.ParItemNo = TestItem_1.ItemNo LEFT OUTER JOIN" +
                                    "                   dbo.S_RequestVItem ON dbo.RequestMarrow.ReceiveDate = dbo.S_RequestVItem.ReceiveDate AND " +
                                    "                   dbo.RequestMarrow.SectionNo = dbo.S_RequestVItem.SectionNo AND " +
                                    "                   dbo.RequestMarrow.TestTypeNo = dbo.S_RequestVItem.TestTypeNo AND " +
                                    "                   dbo.RequestMarrow.SampleNo = dbo.S_RequestVItem.SampleNo LEFT OUTER JOIN" +
                                    "                   dbo.S_RequestVItem_C ON dbo.RequestMarrow.ReceiveDate = dbo.S_RequestVItem_C.ReceiveDate AND " +
                                    "                   dbo.RequestMarrow.SectionNo = dbo.S_RequestVItem_C.SectionNo AND " +
                                    "                   dbo.RequestMarrow.TestTypeNo = dbo.S_RequestVItem_C.TestTypeNo AND " +
                                    "                   dbo.RequestMarrow.SampleNo = dbo.S_RequestVItem_C.SampleNo";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("RequestMicroQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[RequestMicroQueryDataSource]" +
                                    " AS " +
                                    "SELECT  CONVERT(varchar(10), rm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), rm.SectionNo) + ';' + CONVERT(varchar(30), " +
                                    "                   rm.TestTypeNo) + ';' + CONVERT(varchar(30), rm.SampleNo) AS ReportFormID, CONVERT(varchar(10), rm.ReceiveDate, 21) " +
                                    "                   + ';' + CONVERT(varchar(30), rm.SectionNo) + ';' + CONVERT(varchar(30), rm.TestTypeNo) + ';' + CONVERT(varchar(30), " +
                                    "                   rm.SampleNo) AS FormNo," +
                                    "                       (SELECT  ParaValue" +
                                    "                        FROM       dbo.SysPara" +
                                    "                        WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, rm.SectionNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(char, rm.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, rm.SampleNo))) " +
                                    "                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), rm.ReceiveDate, 20))) AS Expr1, rm.ReceiveDate, rm.SectionNo, " +
                                    "                   rm.TestTypeNo, rm.SampleNo, rm.ResultNo, rm.ItemNo, rm.DescNo, rm.MicroNo, rm.MicroDesc, rm.AntiNo, rm.Suscept, " +
                                    "                   rm.SusQuan, rm.SusDesc, rm.RefRange, rm.ItemDate, rm.ItemTime, rm.ItemDesc, rm.EquipNo, rm.Modified, rm.IsMatch, " +
                                    "                   rm.CheckType, rm.isReceive, rm.SerialNoParent, rm.Zdy1, rm.Zdy2, rm.Zdy3, rm.Zdy4, rm.Zdy5, rm.AntiDesc, rm.I_EResult, " +
                                    "                   rm.AntiGroupType, rm.ExpertDesc, rm.ResultState, rm.MicroResultDesc, t1.CName AS itemcname, " +
                                    "                   t1.EName AS itemename, mci.CName AS Microcname, mci.EName AS Microename, mip.CName AS DescName, " +
                                    "                   ab.CName AS AntiName, ab.EName AS Antiename, ab.ShortName AS Antishortname, ab.ShortCode AS antishortcode, " +
                                    "                  ab.AntiUnit, mci.MicroDesc AS microcountdesc" +
                                    " FROM      dbo.RequestMicro AS rm LEFT OUTER JOIN" +
                                    "                   dbo.TestItem AS t1 ON t1.ItemNo = rm.ItemNo LEFT OUTER JOIN" +
                                    "                   dbo.MicroItem AS mci ON mci.MicroNo = rm.MicroNo LEFT OUTER JOIN" +
                                    "                   dbo.MicroPhrase AS mip ON mip.DescNo = rm.DescNo LEFT OUTER JOIN" +
                                    "                   dbo.AntiBiotic AS ab ON ab.AntiNo = rm.AntiNo";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("GetRequestMicroGroupFullList", "p"))
                    {
                        updateSql = "CREATE PROCEDURE [dbo].[GetRequestMicroGroupFullList]" +
                                    "	-- Add the parameters for the stored procedure here \r\n" +
                                    "@ReceiveDate varchar(10), --核收时间 \r\n" +
                                    "@SectionNo varchar(50), --小组号 \r\n" +
                                    "@TestTypeNo varchar(50), --检验类型号 \r\n" +
                                    "@SampleNo varchar(50) --样本号 \r\n" +
                                    " AS " +
                                    " BEGIN" +
                                    "	-- SET NOCOUNT ON added to prevent extra result sets from \r\n" +
                                    "	-- interfering with SELECT statements. \r\n" +
                                    "	SET NOCOUNT ON;" +
                                    "    -- Insert statements for procedure here \r\n" +
                                    "	select * from(" +
                                    " select COUNT(rm.ReceiveDate) cn,rm.ReceiveDate, rm.ItemNo,''MicroNo,'' DescNo,''AntiNo,ti.CName itemname ,NULL MicroName,'' DescName ,NULL AntiName,NULL Suscept,NULL SusDesc,	NULL SusQuan," +
                                    " replace(CONVERT(varchar(12) , rm.ReceiveDate, 111 ),'/','-') SJ,case WHEN rf.PATNO is null then rf.zdy3 ELSE PATNO end as PATNO,  " +
                                    "--添加英文字段 \r\n" +
                                    " ti.EName itemename ,NULL MicroEName,'' DescEName ,NULL AntiEName" +
                                    " From RequestMicro rm" +
                                    " LEFT OUTER JOIN  dbo.TestItem ti ON rm.ItemNo = ti.ItemNo" +
                                    " INNER JOIN  dbo.RequestForm rf ON rf.ReceiveDate = rm.ReceiveDate AND rf.SectionNo = rm.SectionNo AND " +
                                    "                      rf.TestTypeNo = rm.TestTypeNo AND rf.SampleNo = rm.SampleNo" +
                                    " where " +
                                    " rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo" +
                                    " group by rm.ReceiveDate, rf.zdy3,rf.PatNo,rm.ItemNo,ti.CName,ti.EName" +
                                    " union" +
                                    " select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,'' DescNo,''AntiNo," +
                                    " NULL itemname,mi.CName MicroName,'' DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo, " +
                                    " --添加英文字段 \r\n" +
                                    " NULL itemename,mi.EName MicroEName ,'' DescEName ,NULL AntiEName" +
                                    " From RequestMicro rm" +
                                    " LEFT OUTER JOIN dbo.MicroItem mi ON rm.MicroNo = mi.MicroNo" +
                                    " where " +
                                    " rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.MicroNo is not null " +
                                    " group by ReceiveDate,ItemNo,rm.MicroNo,mi.CName,mi.EName" +
                                    " union" +
                                    " select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,''AntiNo,NULL itemname,NUll MicroName," +
                                    " mp.CName DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo, " +
                                    "--添加英文字段 \r\n" +
                                    " NULL itemename,NULL MicroEName ,mp.ShortCode DescEName ,NULL AntiEName" +
                                    " From RequestMicro rm" +
                                    " left outer join dbo.MicroPhrase mp on rm.DescNo = mp.DescNo" +
                                    " where " +
                                    " rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.DescNo is not null " +
                                    " group by ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,mp.CName,mp.ShortCode" +
                                    " union" +
                                    " select COUNT(ReceiveDate) cn,ReceiveDate, ItemNo,MicroNo,rm.AntiNo,rm.DescNo,NULL itemname,NULL MicroName," +
                                    " NULL DescName,ab.CName AntiName,CASE WHEN rm.Suscept = 'S' THEN '敏感' WHEN rm.Suscept = 'R' THEN '耐药' WHEN rm.Suscept = 'I' THEN '中介'   ELSE rm.Suscept END AS Suscept,rm.SusDesc,rm.SusQuan ,NULL SJ , NULL PatNo, " +
                                    "--添加英文字段 \r\n" +
                                    " NULL itemename,NULL MicroEName ,NULL DescEName ,AB.EName AntiEName" +
                                    "     From RequestMicro rm LEFT OUTER JOIN dbo.AntiBiotic ab ON rm.AntiNo = ab.AntiNo " +
                                    " where " +
                                    " rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.MicroNo is not null and rm.AntiNo is not null" +
                                    " group by  rm.ReceiveDate,ItemNo,MicroNo,rm.AntiNo,ab.CName,ab.EName,Suscept,rm.SusDesc,rm.SusQuan,rm.DescNo" +
                                    ") b order by ItemNo,DescNo,MicroNo,AntiNo" +
                                    " END";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("EmpDeptLinks", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[EmpDeptLinks](" +
                                    "	[EDLID] [bigint] NOT NULL," +
                                    "	[UserNo] [bigint] NULL," +
                                    "	[DeptNo] [bigint] NULL," +
                                    "	[UserCName] [varchar](50) NULL," +
                                    "	[ShortCode] [varchar](50) NULL," +
                                    "	[DeptCName] [varchar](200) NULL," +
                                    "	[DataAddTime] [datetime] NULL," +
                                    "	[DataUpdateTime] [datetime] NULL," +
                                    " CONSTRAINT [PK_EmpDeptLinks] PRIMARY KEY CLUSTERED " +
                                    "(" +
                                    "	[EDLID] ASC" +
                                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                                    ") ON [PRIMARY]";

                        listSQL.Add(updateSql);
                        updateSql = "SET ANSI_PADDING OFF";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("RFP_ReportFormPrint_Operation", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[RFP_ReportFormPrint_Operation](" +
                                    "	[LabID] [bigint] NOT NULL," +
                                    "	[RFPOperationID] [bigint] NOT NULL," +
                                    "	[BobjectID] [bigint] NULL," +
                                    "	[ReceiveDate] [datetime] NULL," +
                                    "	[SectionNo] [int] NULL," +
                                    "	[TestTypeNo] [int] NULL," +
                                    "	[SampleNo] [varchar](20) NULL," +
                                    "	[Station] [varchar](200) NULL," +
                                    "	[EmpID] [bigint] NULL," +
                                    "	[EmpName] [varchar](20) NULL," +
                                    "	[DeptId] [bigint] NULL," +
                                    "	[DeptName] [varchar](200) NULL," +
                                    "	[Type] [bigint] NULL," +
                                    "	[TypeName] [varchar](20) NULL," +
                                    "	[BusinessModuleCode] [varchar](20) NULL," +
                                    "	[Memo] [varchar](500) NULL," +
                                    "	[DispOrder] [int] NULL," +
                                    "	[IsUse] [bit] NULL," +
                                    "	[CreatorID] [bigint] NULL," +
                                    "	[CreatorName] [varchar](50) NULL," +
                                    "	[DataAddTime] [datetime] NULL," +
                                    "	[DataUpdateTime] [datetime] NULL," +
                                    "	[DataTimeStamp] [timestamp] NULL," +
                                    " CONSTRAINT [PK_SC_OPERATION] PRIMARY KEY CLUSTERED " +
                                    "(" +
                                    "	[RFPOperationID] ASC" +
                                    ") WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                                    ") ON [PRIMARY]";

                        listSQL.Add(updateSql);
                    }
                    #endregion
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    DataSet ds = null;
                    #region 创建表以及试图
                    if (!CheckDataObjectIsExists("ReportFormQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[ReportFormQueryDataSource]" +
                                    " AS " +
                                    " SELECT" +
                                    "	ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, SectionName, TestTypeName, SampleTypeNo, SampletypeName, " +
                                    "	SecretType, PatNo, CName, InpatientNo, PatCardNo, GenderNo, GenderName, Age, AgeUnitNo, AgeUnitName, Birthday, DistrictNo, DistrictName, " +
                                    "	WardNo, WardName, Bed, DeptNo, DeptName, Doctor, SerialNo, ParitemName, Collecter, CollectDate, CollectTime, Incepter, InceptDate, InceptTime, " +
                                    "	Technician, TestDate, TestTime, Operator, OperDate, OperTime, Checker, CheckDate, CheckTime, FormComment, FormMemo, SickTypeNo, " +
                                    "	SickTypeName, DiagNo, DiagName, ClientNo, ClientName, Sender2, PrintTimes, ClientPrint, PrintOper, PrintDateTime, PrintOper1, PrintDateTime1, " +
                                    "	resultsend, reportsend, PageName, PageCount, ZDY1, ZDY2, ZDY3, ZDY4, ZDY5, ZDY6, ZDY7, ZDY8, ReportFormID AS formno, " +
                                    "	SecretType AS SectionType, LabID, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp, MainTesterId, PatientID, ExaminerId, " +
                                    "	CollectPart, ReportPublicationID AS ReportFormID, ActiveFlag, DoctorItemName AS ItemName,'' as ZDY15,'' as PinYinMa" +
                                    "  FROM dbo.ReportFormFull " +
                                    " WHERE (ActiveFlag = 1)";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("ReportItemQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[ReportItemQueryDataSource]" +
                                    " AS " +
                                    " SELECT     " +
                                    "	ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, OrderNo, ParItemNo, ItemNo, ParitemName, ItemCname, ItemEname, " +
                                    "	ReportValue, ReportDesc, ItemValue, ItemUnit, ResultStatus, RefRange, EquipNo, EquipName, DiagMethod, Prec, StandardCode, ItemDesc, " +
                                    "	SecretGrade, Visible, ZDY1, zdy2, zdy3, ItemUnit AS UNIT, ItemEname AS TESTITEMSNAME, ItemCname AS TESTITEMNAME, " +
                                    "	ReportPublicationID AS ReportFormID, LabID, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp" +
                                    " FROM dbo.ReportItemFull";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("ReportMicroQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[ReportMicroQueryDataSource]" +
                                    " AS " +
                                    " SELECT     LabID, ReportPublicationID, ReportFormID AS RFID, OrderNo, ReceiveDate, SectionNo, TestTypeNo, SampleNo, ItemNo, ItemCname, ItemEname, DescNo, " +
                                    "                      TPF3 AS DescName, MicroStepID, MicroStepName, ResultID, ReportValue, MicroNo, MicroName, MicroEame, MicroDesc, MicroResultDesc, ItemDesc, AntiNo, " +
                                    "                      AntiName, AntiEName, Suscept, SusQuan, SusDesc, AntiUnit, RefRange, ResultState, EquipNo, EquipName, AntiGroupType, MethodName, SerumcenTration," +
                                    "                      EmictioncenTration, Microdisplayid, Antidisplayid, CheckType, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp, " +
                                    "                      ReportPublicationID AS ReportFormID, DSTType, PYJDF7, ResistancePhenotypeName AS RPName" +
                                    " FROM         dbo.ReportMicroFull";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("ReportMarrowQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[ReportMarrowQueryDataSource]" +
                                    " AS " +
                                    " SELECT  ReportPublicationID AS ReportFormID, ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, OrderNo, " +
                                    "                   ParItemNo, ItemNo, ParitemName, ItemCname, ItemEname, BloodNum, BloodPercent, BloodDesc, MarrowNum, " +
                                    "                   MarrowPercent, MarrowDesc, RefRange, EquipNo, EquipName, ResultStatus, DiagMethod" +
                                    " FROM      dbo.ReportMarrowFull";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("GetReportValue", "p"))
                    {
                        updateSql = "CREATE PROCEDURE [dbo].[GetReportValue] " +
                                    "	@PatNo varchar(50), --病历号 \r\n" +
                                    "	@ItemNo varchar(50), --项目号 \r\n" +
                                    "	@Check varchar(50), --SectionType \r\n" +
                                    "	@Where varchar(max) --where \r\n" +
                                    "	--ReportFormFull rf " +
                                    "	--ReportItemFull ri " +
                                    "	--ReportMicroFull rm " +
                                    "	--ReportMarrowFull rmm \r\n" +
                                    " AS " +
                                    " BEGIN " +
                                    " if @Check='item' " +
                                    "    begin " +
                                    "		--exec ('select isnull(ReportValue,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate from dbo.ReportItemFull ri INNER JOIN " +
                                    "		--dbo.ReportFormFull rf ON ri.ReportFormID = rf.ReportFormID  " +
                                    "		--where -- ReportItem.FormNo=@FormNo " +
                                    "		--rf.PatNo='''+@PatNo+''' and ri.ITEMNO = '''+@ItemNo +'''' +@Where) " +
                                    "		--rf.PatNo=@PatNo and ri.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate \r\n" +
                                    " " +
                                    "		exec ('select isnull(ReportValue,0) ReportValue,CONVERT(varchar(10) , ReportItemFull.ReceiveDate, 111 ) ReceiveDate, " +
                                    "                       (SELECT  CONVERT(varchar(10) , ReportFormFull.CheckDate, 111 ) " +
                                    "                        FROM       dbo.ReportFormFull " +
                                    "                        WHERE    (ReportItemFull.ReportPublicationID = ReportFormFull.ReportPublicationID)) AS CheckDate, " +
                                    "                       (SELECT  CONVERT(varchar(10) , ReportFormFull.CheckTime, 108 ) " +
                                    "                        FROM       dbo.ReportFormFull " +
                                    "                        WHERE    (ReportItemFull.ReportPublicationID = ReportFormFull.ReportPublicationID)) AS CheckTime, " +
                                    "						ItemCName " +
                                    " from ReportItemFull   " +
                                    " where ReportItemFull.ReportPublicationID in (select ReportPublicationID from ReportFormFull rf where PatNo='''+@PatNo+''''+@Where+') and ITEMNO='''+@ItemNo +''' order by CheckDate,CheckTime ') " +
                                    "    end " +
                                    " else if @Check='micro' " +
                                    "    begin " +
                                    "	    print ('select isnull(SusQuan,0) as ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate from dbo.ReportMicro rm INNER JOIN " +
                                    "		dbo.ReportForm rf ON rm.ReceiveDate = rf.ReceiveDate AND rm.SectionNo = rf.SectionNo AND  " +
                                    "		rm.TestTypeNo = rf.TestTypeNo AND rm.SampleNo = rf.SampleNo " +
                                    "		where --ReportItem.FormNo=@FormNo  \r\n" +
                                    "		rf.PatNo ='''+@PatNo+''' and rm.ITEMNO ='''+ @ItemNo +'''' + @Where) " +
                                    "		--rf.PatNo=@PatNo and rm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate  \r\n" +
                                    "    end " +
                                    " else if @Check='marrow' " +
                                    "	begin " +
                                    "	    exec ('select isnull(BloodPercent,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate from dbo.ReportMarrow rmm INNER JOIN " +
                                    "        dbo.ReportForm rf ON rmm.ReceiveDate = rf.ReceiveDate AND rmm.SectionNo = rf.SectionNo AND  " +
                                    "        rmm.TestTypeNo = rf.TestTypeNo AND rmm.SampleNo = rf.SampleNo " +
                                    "        where --ReportItem.FormNo=@FormNo \r\n" +
                                    "        rf.PatNo ='''+ @PatNo+''' and rmm.ITEMNO = '''+@ItemNo +'''' + @Where) " +
                                    "		--rf.PatNo=@PatNo and rmm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate \r\n" +
                                    "	end " +
                                    " END ";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("ReportFormAllQueryDataSource", "v"))
                    {
                        updateSql = "CREATE VIEW [dbo].[ReportFormAllQueryDataSource]" +
                                    " AS " +
                                    " SELECT   dbo.ReportFormFull.LabID, dbo.ReportFormFull.ReportPublicationID AS ReportFormID, " +
                                    "                dbo.ReportFormFull.ReportFormID AS RFID, dbo.ReportFormFull.ReceiveDate, dbo.ReportFormFull.SectionNo, " +
                                    "                dbo.ReportFormFull.TestTypeNo, dbo.ReportFormFull.SampleNo, dbo.ReportFormFull.SectionName, " +
                                    "                dbo.ReportFormFull.TestTypeName, dbo.ReportFormFull.SampleTypeNo, dbo.ReportFormFull.SampletypeName, " +
                                    "                dbo.ReportFormFull.SecretType, dbo.ReportFormFull.PatientID, dbo.ReportFormFull.PatNo, dbo.ReportFormFull.CName, " +
                                    "                dbo.ReportFormFull.InpatientNo, dbo.ReportFormFull.PatCardNo, dbo.ReportFormFull.GenderNo, " +
                                    "                dbo.ReportFormFull.GenderName, dbo.ReportFormFull.Age, dbo.ReportFormFull.AgeUnitNo, " +
                                    "                dbo.ReportFormFull.AgeUnitName, dbo.ReportFormFull.Birthday, dbo.ReportFormFull.DistrictNo, " +
                                    "                dbo.ReportFormFull.DistrictName, dbo.ReportFormFull.WardNo, dbo.ReportFormFull.WardName, " +
                                    "                dbo.ReportFormFull.Bed, dbo.ReportFormFull.DeptNo, dbo.ReportFormFull.DeptName, dbo.ReportFormFull.DoctorID, " +
                                    "                dbo.ReportFormFull.Doctor, dbo.ReportFormFull.SerialNo, dbo.ReportFormFull.ParitemName AS DoctorParitemName," +
                                    "                dbo.ReportFormFull.DoctorItemName, dbo.ReportFormFull.Collecter, dbo.ReportFormFull.CollectDate, " +
                                    "               dbo.ReportFormFull.CollectTime, dbo.ReportFormFull.Incepter, dbo.ReportFormFull.InceptDate, " +
                                    "                dbo.ReportFormFull.InceptTime, dbo.ReportFormFull.MainTesterId, dbo.ReportFormFull.Technician, " +
                                    "                dbo.ReportFormFull.TestDate, dbo.ReportFormFull.TestTime, dbo.ReportFormFull.Operator, " +
                                    "                dbo.ReportFormFull.OperDate, dbo.ReportFormFull.OperTime, dbo.ReportFormFull.ExaminerId, " +
                                    "                dbo.ReportFormFull.Checker, dbo.ReportFormFull.CheckDate, dbo.ReportFormFull.CheckTime, " +
                                    "                dbo.ReportFormFull.FormComment, dbo.ReportFormFull.FormMemo, dbo.ReportFormFull.SickTypeNo, " +
                                    "                dbo.ReportFormFull.SickTypeName, dbo.ReportFormFull.DiagNo, dbo.ReportFormFull.DiagName, " +
                                    "                dbo.ReportFormFull.ClientNo, dbo.ReportFormFull.ClientName, dbo.ReportFormFull.Sender2, " +
                                    "                dbo.ReportFormFull.PrintTimes, dbo.ReportFormFull.ClientPrint, dbo.ReportFormFull.PrintOper, " +
                                    "                dbo.ReportFormFull.PrintDateTime, dbo.ReportFormFull.PrintOper1, dbo.ReportFormFull.PrintDateTime1, " +
                                    "                dbo.ReportFormFull.resultsend, dbo.ReportFormFull.reportsend, dbo.ReportFormFull.ActiveFlag, " +
                                    "                dbo.ReportFormFull.AllFlag, dbo.ReportFormFull.CollectPart, dbo.ReportFormFull.TestAim, " +
                                    "                dbo.ReportFormFull.PageName, dbo.ReportFormFull.PageCount, dbo.ReportFormFull.ReceiveTime, " +
                                    "                dbo.ReportFormFull.ZDY1, dbo.ReportFormFull.ZDY2, dbo.ReportFormFull.ZDY3, dbo.ReportFormFull.ZDY4, " +
                                    "                dbo.ReportFormFull.ZDY5, dbo.ReportFormFull.ZDY6, dbo.ReportFormFull.ZDY7, dbo.ReportFormFull.ZDY8, " +
                                    "                dbo.ReportFormFull.ZDY9, dbo.ReportFormFull.ZDY10, dbo.ReportFormFull.DataAddTime, " +
                                    "                dbo.ReportFormFull.DataUpdateTime, dbo.ReportFormFull.DataMigrationTime, dbo.ReportFormFull.DataTimeStamp, " +
                                    "                dbo.ReportFormFull.STestType, dbo.ReportFormFull.FormComment2, '' as SectionResultType, " +
                                    "                dbo.ReportItemFull.OrderNo, dbo.ReportItemFull.ParItemNo, " +
                                    "                dbo.ReportItemFull.ItemNo, dbo.ReportItemFull.ParitemName, dbo.ReportItemFull.ItemCname, " +
                                    "                dbo.ReportItemFull.ItemEname, dbo.ReportItemFull.ReportValue, dbo.ReportItemFull.ReportDesc, " +
                                    "                dbo.ReportItemFull.ItemValue, dbo.ReportItemFull.ItemUnit, dbo.ReportItemFull.ResultStatus, " +
                                    "                dbo.ReportItemFull.RefRange, dbo.ReportItemFull.EquipNo, dbo.ReportItemFull.EquipName, " +
                                    "                dbo.ReportItemFull.DiagMethod, dbo.ReportItemFull.Prec, dbo.ReportItemFull.StandardCode, " +
                                    "                dbo.ReportItemFull.ItemDesc, dbo.ReportItemFull.SecretGrade, dbo.ReportItemFull.Visible, " +
                                    "                dbo.ReportItemFull.DefaultReagent, " +
                                    "                    (SELECT   TOP (1) FilePath" +
                                    "                     FROM      dbo.PUser" +
                                    "                     WHERE   (CName = dbo.ReportFormFull.Collecter) AND (CName IS NOT NULL) AND (CName <> '')) " +
                                    "                AS CollecterImageFilePath," +
                                    "                    (SELECT   TOP (1) FilePath" +
                                    "                     FROM      dbo.PUser AS PUser_4" +
                                    "                     WHERE   (CName = dbo.ReportFormFull.Incepter) AND (CName IS NOT NULL) AND (CName <> ''))" +
                                    "                AS IncepterImageFilePath," +
                                    "                    (SELECT   TOP (1) FilePath" +
                                    "                     FROM      dbo.PUser AS PUser_3" +
                                    "                     WHERE   (CName = dbo.ReportFormFull.Technician) AND (CName IS NOT NULL) AND (CName <> '')) " +
                                    "                AS TechnicianImageFilePath," +
                                    "                    (SELECT   TOP (1) FilePath" +
                                    "                     FROM      dbo.PUser AS PUser_2" +
                                    "                     WHERE   (CName = dbo.ReportFormFull.Operator) AND (CName IS NOT NULL) AND (CName <> ''))" +
                                    "                AS OperatorImageFilePath," +
                                    "                    (SELECT   TOP (1) FilePath" +
                                    "                     FROM      dbo.PUser AS PUser_1" +
                                    "                     WHERE   (CName = dbo.ReportFormFull.Checker) AND (CName IS NOT NULL) AND (CName <> '')) " +
                                    "                AS CheckerImageFilePath" +
                                    " FROM      dbo.ReportFormFull INNER JOIN" +
                                    "                dbo.ReportItemFull ON dbo.ReportFormFull.ReportPublicationID = dbo.ReportItemFull.ReportPublicationID";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("GetReportMicroGroupFullList", "p"))
                    {
                        updateSql = "CREATE PROCEDURE [dbo].[GetReportMicroGroupFullList] " +
                                    "-- Add the parameters for the stored procedure here \r\n" +
                                    "@ReportFormID varchar(50) --报告单ID \r\n" +
                                    " AS " +
                                    " BEGIN " +
                                    "-- SET NOCOUNT ON added to prevent extra result sets from \r\n" +
                                    "-- interfering with SELECT statements. \r\n" +
                                    " SET NOCOUNT ON; " +
                                    "-- Insert statements for procedure here \r\n" +
                                    " select * from( " +
                                    " select COUNT(rm.ReceiveDate) cn,rm.ReceiveDate, rm.ItemNo,''MicroNo,''MicroDesc,'' DescNo, " +
                                    " '' AntiNo,rm.ItemCname itemname ,NULL MicroName,'' DescName ,NULL AntiName,NULL Suscept,NULL SusDesc, " +
                                    " NULL SusQuan,replace(CONVERT(varchar(12) , rm.ReceiveDate, 111 ),'/','-') SJ,rm.TPF3,rm.PYJDF7 " +
                                    "  From ReportMicroFull rm " +
                                    " INNER JOIN  dbo.ReportFormFull rf ON rf.ReportFormID = rm.ReportFormID  " +
                                    " where rm.ReportPublicationID=@ReportFormID and ItemCname is not null and ItemCname <>'' " +
                                    " group by rm.ReceiveDate,rm.ItemNo,rm.ItemCname,rm.TPF3,rm.PYJDF7 " +
                                    " union " +
                                    " select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,rm.MicroDesc,'' DescNo,''AntiNo,NULL itemname,rm.MicroName MicroName,'' DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ,NULL TPF3,NULL PYJDF7  From ReportMicroFull rm " +
                                    " where rm.ReportPublicationID=@ReportFormID AND rm.MicroNo is not null  " +
                                    " group by ReceiveDate,ItemNo,rm.MicroNo,rm.MicroName,rm.MicroDesc " +
                                    " union " +
                                    " " +
                                    " select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,''MicroDesc,rm.DescNo,''AntiNo,NULL itemname,NUll MicroName,rm.DescName DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ,NULL TPF3,NULL PYJDF7 From ReportMicroFull rm " +
                                    " where rm.ReportPublicationID=@ReportFormID AND rm.DescNo is not null and rm.DescName is not null and rm.DescName <>'' " +
                                    " group by ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,rm.DescName " +
                                    " union " +
                                    " select COUNT(ReceiveDate) cn,ReceiveDate, ItemNo,MicroNo,''MicroDesc,rm.AntiNo,rm.DescNo,NULL itemname,NULL MicroName,NULL DescName,rm.AntiName AntiName, " +
                                    " CASE WHEN rm.Suscept = 'S' THEN '敏感' WHEN rm.Suscept = 'R' THEN '耐药' WHEN rm.Suscept = 'I' THEN '中介'   ELSE rm.Suscept END AS Suscept,rm.SusDesc,rm.SusQuan ,NULL SJ,NULL TPF3,NULL PYJDF7 " +
                                    "     From ReportMicroFull rm   " +
                                    " where rm.ReportPublicationID=@ReportFormID AND rm.MicroNo is not null and rm.AntiNo is not null " +
                                    " group by  rm.ReceiveDate,ItemNo,MicroNo,rm.AntiNo,rm.AntiName,Suscept,rm.SusDesc,rm.SusQuan,rm.DescNo " +
                                    ") b order by ItemNo desc,DescNo ,MicroNo ,AntiNo  " +
                                    " " +
                                    "END ";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("B_ColumnsSetting", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[B_ColumnsSetting] (" +
                                    "  [OrderFlag] bit  NULL," +
                                    "  [OrderDesc] int  NULL," +
                                    "  [OrderMode] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [CTID] bigint  NOT NULL," +
                                    "  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [ShowName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Width] int  NULL," +
                                    "  [OrderNo] int  NULL," +
                                    "  [Site] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [AppType] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [DataAddTime] datetime  NULL," +
                                    "  [DataUpdateTime] datetime  NULL," +
                                    "  [IsShow] bit  NULL," +
                                    "  [ColID] bigint  NULL," +
                                    "  [ColumnName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Render] text COLLATE Chinese_PRC_CI_AS  NULL" +
                                    ")";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_ColumnsSetting] SET (LOCK_ESCALATION = TABLE);";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE[dbo].[B_ColumnsSetting] ADD CONSTRAINT[PK_B_COLUMNSSETTING] PRIMARY KEY CLUSTERED([CTID])" +
                                    "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  " +
                                    "ON [PRIMARY]";
                        listSQL.Add(updateSql);

                    }
                    if (!CheckDataObjectIsExists("B_ColumnsUnit", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[B_ColumnsUnit] (" +
                                    "  [ColID] bigint  NOT NULL," +
                                    "  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [ColumnName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Type] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Width] int  NULL," +
                                    "  [DataAddTime] datetime  NULL," +
                                    "  [DataUpdateTime] datetime  NULL," +
                                    "  [Render] text COLLATE Chinese_PRC_CI_AS  NULL)";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_ColumnsUnit] SET (LOCK_ESCALATION = TABLE);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'1', N'床号', N'Bed', NULL, N'40', NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'2', N'报告日期', N'CHECKDATE', NULL, N'150', NULL, NULL, N'function (v, meta, record, index) {" +
                                    "	            //显示审核时间 \r\n" +
                                    "							var Cdate = Shell.util.Date.toString(record.get(\"CHECKDATE\"), true);" +
                                    "							var Ctime = Shell.util.Date.toString(record.get(\"CHECKTIME\"), false);" +
                                    "							var value = '''';" +
                                    "							if(Cdate !=null){" +
                                    "								value = Cdate;" +
                                    "							}" +
                                    "							if(Ctime !=null){" +
                                    "							  var arry = Ctime.split('' '');" +
                                    "							  if(arry!=null && arry.length >1){" +
                                    "								 value +='' ''+ arry[1];" +
                                    "								}" +
                                    "							}" +
                                    "	            return value;" +
                                    "	        }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'3', N'姓名', N'CNAME', NULL, N'60', NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'4', N'检验项目', N'ItemName', NULL, N'140', NULL, NULL, N'function (value, meta, record) {if (value) meta.tdAttr = ''data-qtip=\" <b> '' + value + '' </b> \"'';         return value;	        }');";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'5', N'病历号', N'PatNo', NULL, N'60', NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'6', N'核收日期', N'RECEIVEDATE', NULL, N'80', NULL, NULL, N'function (v, meta, record, index) {" +
                                    "	 var value = record.get(\"RECEIVEDATE\").split('' '')[0];" +
                                    "	 return value;" +
                                    "}')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'7', N'打印次数', N'PRINTTIMES', NULL, N'60', NULL, NULL, N'function (v, meta, record) {" +
                                "	                var imgName = (v && v >= me.maxPrintTimes) ? \"unprint\" : \"print\"," +
                                "		                tootip = \"已经打印 < b style = ''color: red; '' > \" + v +\" </b> 次\"," +
                                "	                    value = v ? \" <b> \" + v + \" </b> \" : \"\";" +
                                "	                meta.tdAttr = ''data-qtip=\"'' + tootip + ''\"'';	        " +
                                "	                var result = '''';" +
                                "	                if(v >= 0){ " +
                                "	                    result = \" < img src = ''\" + Shell.util.Path.uiPath + \" / ReportPrint / images / \" + imgName + \".png'' /> \" + v;" +
                                "	                }	        " +
                                "	                return result;" +
                                "	            }')";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_ColumnsUnit] ADD CONSTRAINT [PK_B_COLUMNSUNIT] PRIMARY KEY CLUSTERED ([ColID])" +
                                    "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  " +
                                    "ON [PRIMARY]";
                        listSQL.Add(updateSql);

                    }
                    if (CheckDataObjectIsExists("B_Parameter", "U"))
                    {
                        DataSet dss = SqlServerHelper.QuerySql(" select * from B_Parameter ", ADOConnectStr);
                        if (dss == null || dss.Tables == null || dss.Tables[0].Rows.Count < 2)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'1', N'页面公共配置', N'allPageType', N'config', N'defaultWhere', N'', NULL, N'string', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'2', N'页面公共配置', N'allPageType', N'config', N'requestParamsArr', N'', NULL, N'stringArry', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO[dbo].[B_Parameter]  VALUES(N'3', N'页面公共配置', N'allPageType', N'config', N'hisRequestParamsArr', N'OLDSERIALNO', NULL, N'stringArry', NULL, NULL, NULL, NULL); ";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'4', N'页面公共配置', N'allPageType', N'config', N'defaultDates', N'1', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'5', N'页面公共配置', N'allPageType', N'config', N'defaultPageSize', N'50', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'6', N'页面公共配置', N'allPageType', N'config', N'hasPrint', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'7', N'页面公共配置', N'allPageType', N'config', N'A4Type', N'1', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'8', N'页面公共配置', N'allPageType', N'config', N'printType', N'A4', NULL, N'string', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'9', N'页面公共配置', N'allPageType', N'config', N'maxPrintTimes', N'100', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'10', N'页面公共配置', N'allPageType', N'config', N'mergePageCount', N'100', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'11', N'页面公共配置', N'allPageType', N'config', N'ForcedPagingField', N'', NULL, N'hash', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'12', N'页面公共配置', N'allPageType', N'config', N'openAddPrintTimes', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'13', N'页面公共配置', N'allPageType', N'config', N'checkUnprint', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'14', N'页面公共配置', N'allPageType', N'config', N'checkFilter', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'15', N'页面公共配置', N'allPageType', N'config', N'headCollapsed', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'16', N'页面公共配置', N'allPageType', N'config', N'autoSelect', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'17', N'页面公共配置', N'allPageType', N'config', N'CheckOnly', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'18', N'页面公共配置', N'allPageType', N'config', N'hasReportPage', N'true', NULL, N'bool', NULL, NULL, NULL, NULL); ";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'19', N'页面公共配置', N'allPageType', N'config', N'hasResultPage', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'20', N'页面公共配置', N'allPageType', N'config', N'defaultCheckedPage', N'1', NULL, N'int', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'21', N'页面公共配置', N'allPageType', N'config', N'hasPdfPrinter', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'22', N'页面公共配置', N'allPageType', N'config', N'pdfPrinterList', N'', NULL, N'stringArry', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'23', N'页面公共配置', N'allPageType', N'config', N'DateField', NULL, NULL, N'string', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'24', N'页面公共配置', N'allPageType', N'config', N'isListHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'66', N'查询打印页面配置', N'自助打印', N'config', N'printPageType', N'双A5', N'', N'string', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'67', N'查询打印页面配置', N'自助打印', N'config', N'printtimes', N'1', N'', N'int', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'68', N'查询打印页面配置', N'自助打印', N'config', N'selectColumn', N'PatNo', N'', N'string', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'69', N'查询打印页面配置', N'自助打印', N'config', N'lastDay', N'100', N'', N'int', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'70', N'查询打印页面配置', N'自助打印', N'config', N'tackTime', N'5', N'', N'int', N'2018-10-16 11:19:54.000', NULL, NULL, NULL);";
                            listSQL.Add(updateSql);
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'71', N'查询打印页面配置', N'自助打印', N'config', N'showName', N'卡名', N'', N'string', N'2018-10-16 11:19:54.000', N'0', NULL, NULL);";

                        }


                    }
                    if (!CheckDataObjectIsExists("B_SearchSetting", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[B_SearchSetting] (" +
                                    "  [JsCode] text COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Type] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [SelectName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Xtype] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Mark] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Listeners] text COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [STID] bigint  NOT NULL," +
                                    "  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [ShowName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [TextWidth] int  NULL," +
                                    "  [Width] int  NULL," +
                                    "  [ShowOrderNo] int  NULL," +
                                    "  [Site] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [AppType] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [DataAddTime] datetime  NULL," +
                                    "  [DataUpdateTime] datetime  NULL," +
                                    "  [IsShow] bit  NULL," +
                                    "  [SID] bigint  NULL)";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_SearchSetting] SET (LOCK_ESCALATION = TABLE);";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_SearchSetting] ADD CONSTRAINT [PK_B_SELECTSETTING] PRIMARY KEY CLUSTERED ([STID])" +
                                    "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  " +
                                    "ON [PRIMARY]";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("B_SearchUnit", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[B_SearchUnit] (" +
                                    "  [SID] bigint  NOT NULL," +
                                    "  [CName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [SelectName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Type] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [TextWidth] int  NULL," +
                                    "  [Width] int  NULL," +
                                    "  [DataAddTime] datetime  NULL," +
                                    "  [DataUpdateTime] datetime  NULL," +
                                    "  [Xtype] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Mark] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [Listeners] text COLLATE Chinese_PRC_CI_AS  NULL," +
                                    "  [JsCode] text COLLATE Chinese_PRC_CI_AS  NULL)";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_SearchUnit] SET (LOCK_ESCALATION = TABLE);";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'1', N'科室过滤', N'DeptNo', N'other', N'55', N'150', NULL, NULL, N'uxCheckTrigger', NULL, N'{" +
                                    "                    check: function (p, record) {" +
                                    "                        if (record == null) {" +
                                    "                            me.items.items[1].getComponent(\"DeptNo\").setValue(\"\");" +
                                    "                            me.items.items[1].getComponent(\"ClienteleName\").setValue(\"\");" +
                                    "                           return;" +
                                    "                        }" +
                                    "                        me.items.items[1].getComponent(\"DeptNo\").setValue(\"(\" + record.get(\"DeptNo\") + \")\");" +
                                    "                       me.items.items[1].getComponent(\"ClienteleName\").setValue(record.get(\"CName\"));" +
                                    "                        p.close();" +
                                    "                    }" +
                                    "                }', N'{ type: ''search'', xtype: ''textfield'', mark: ''in'', itemId: \"DeptNo\", name: ''DeptNo'', width: 130, hidden: true } searchAndNext" +
                                    "            {" +
                                    "                fieldLabel: ''''," +
                                    "                xtype: ''uxCheckTrigger''," +
                                    "                emptyText: ''科室过滤''," +
                                    "                zIndex:1," +
                                    "                width: 150," +
                                    "                labelSeparator: ''''," +
                                    "                labelWidth: 55," +
                                    "                labelAlign: ''right''," +
                                    "                itemId: ''ClienteleName''," +
                                    "                className: ''Shell.class.dept.CheckGrid''," +
                                    "                listeners: {" +
                                    "                    check: function (p, record) {" +
                                    "                        var item = \"\";" +
                                    "                        var clientName = \"\";" +
                                    "                        var me = this.ownerCt.ownerCt;" +
                                    "						if (record == null) {" +
                                    "                            item = me.getItem(\"DeptNo\");" +
                                    "                            clientName = me.getItem(\"ClienteleName\");" +
                                    "                            item.setValue(\"\");" +
                                    "                            clientName.setValue(\"\");" +
                                    "                            return;" +
                                    "                        }						" +
                                    "                        item = me.getItem(\"DeptNo\");" +
                                    "                        clientName = me.getItem(\"ClienteleName\");" +
                                    "                        item.setValue(\"(\" + record.get(\"DeptNo\") + \")\");" +
                                    "                        clientName.setValue(record.get(\"CName\"));" +
                                    "                        p.close();" +
                                    "                    }" +
                                    "                }" +
                                    "            }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'2', N'就诊类型', N'SickTypeName', N'other', NULL, NULL, NULL, NULL, N'combobox', NULL, NULL, N'{ " +
                                    "                type: ''other'', xtype: ''checkbox'', itemId: ''checkSickType'', boxLabel: ''门诊'', width: 50, " +
                                    "                listeners:{ " +
                                    "                    change: function (m, v) { " +
                                    "					    var me = this.ownerCt.ownerCt; " +
                                    "                        var sick = me.getItem(\"mgSickType\"); " +
                                    "                        if (v) { " +
                                    "                            if (sick.getValue().length < 3 || sick.getValue() == \"\" || sick.getValue == null) { " +
                                    "                                sick.setValue(\"(''门诊'')\"); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) +\",''门诊'')\"); " +
                                    "                            } " +
                                    "                            sick.type = ''search''; " +
                                    "                       } else { " +
                                    "                            var index = sick.getValue().indexOf(\"门诊\"); " +
                                    "                           if (index == 2) { " +
                                    "                                sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6)); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3)); " +
                                    "                            } " +
                                    "                        } " +
                                    "                        if (sick.getValue() == \"\" || sick.getValue() == null || sick.getValue().length < 6) { " +
                                    "                            sick.type = ''other''; " +
                                    "                            sick.setValue(\"\"); " +
                                    "                        } " +
                                    "                    } " +
                                    "	            } " +
                                    "            }searchAndNext " +
                                    "            { type: ''search'', xtype: ''textfield'', itemId: ''mgSickType'', mark: ''in'', name: ''SickTypeName'', hidden: true }searchAndNext " +
                                    "            { " +
                                    "                type: ''other'', xtype: ''checkbox'', itemId: ''checkSickType1'', boxLabel: ''住院'', width: 50, " +
                                    "                listeners: { " +
                                    "                    change: function (m, v) { " +
                                    "					    var me = this.ownerCt.ownerCt; " +
                                    "                        var sick = me.getItem(\"mgSickType\"); " +
                                    "                        if (v) { " +
                                    "                            if (sick.getValue().length < 3 || sick.getValue() == \"\" || sick.getValue == null) { " +
                                    "                                sick.setValue(\"(''住院'')\"); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + \",''住院'')\"); " +
                                    "                            } " +
                                    "                            sick.type = ''search''; " +
                                    "                        } else { " +
                                    "                            var index = sick.getValue().indexOf(\"住院\"); " +
                                    "                            if (index == 2) { " +
                                    "                                sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6)); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3)); " +
                                    "                            } " +
                                    "                        } " +
                                    "                        if (sick.getValue() == \"\" || sick.getValue() == null || sick.getValue().length < 6) { " +
                                    "                           sick.type = ''other''; " +
                                    "                       } " +
                                    "                    } " +
                                    "                } " +
                                    "            }searchAndNext " +
                                    "            { " +
                                    "                type: ''other'', xtype: ''checkbox'', itemId: ''checkSickType2'', boxLabel: ''体检'', width: 50, " +
                                    "                listeners: { " +
                                    "                    change: function (m, v) { " +
                                    "				     	var me = this.ownerCt.ownerCt; " +
                                    "                       var sick = me.getItem(\"mgSickType\"); " +
                                    "                       if (v) { " +
                                    "                            if (sick.getValue().length < 3 || sick.getValue() == \"\" || sick.getValue == null) { " +
                                    "                               sick.setValue(\"(''体检'')\"); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + \",''体检'')\"); " +
                                    "                            } " +
                                    "                            sick.type = ''search''; " +
                                    "                        } else { " +
                                    "                            var index = sick.getValue().indexOf(\"体检\"); " +
                                    "                            if (index == 2) { " +
                                    "                                sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6)); " +
                                    "                            } else { " +
                                    "                                sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3)); " +
                                    "                            } " +
                                    "                        } " +
                                    "                        if (sick.getValue() == \"\" || sick.getValue() == null || sick.getValue().length < 6) { " +
                                    "                            sick.type = ''other''; " +
                                    "                       }          }           }       }') ";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'3', N'姓名', N'CNAME', N'search', N'35', N'110', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''CNAME'', fieldLabel: ''姓名'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'4', N'样本号', N'SAMPLENO', N'search', N'45', N'150', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''SAMPLENO'', fieldLabel: ''样本号'', labelWidth: 50, width: 150 }');";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'5', N'病历号', N'PATNO', N'search', N'45', N'150', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''PATNO'', fieldLabel: ''病历号'', labelWidth: 50, width: 150 }');";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'6', N'床号', N'Bed', N'search', N'35', N'150', NULL, NULL, N'textfield', N'=         ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''Bed'', fieldLabel: ''床号'', labelWidth: 30, width: 130 }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'7', N'本周', N'本周', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'{ text: \"本周\", tooltip: \"查询本周数据\", vType:1,where: \"datediff(week, RECEIVEDATE, getdate()) = 0\" }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'8', N'本月', N'本月', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'{ text: \"本月\", tooltip: \"查询本月数据\", vType:2,where: \"datediff(month, RECEIVEDATE, getdate()) = 0\" }')";
                        listSQL.Add(updateSql);
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'9', N'时间', N'时间', NULL, NULL, NULL, NULL, NULL, N'combobox', NULL, NULL, N'{ " +
                                    "                type: ''other'',  " +
                                    "				 xtype: ''combobox'', " +
                                    "                name: ''defaultPageSize'', " +
                                    "                itemId: ''defaultPageSize'', " +
                                    "                width: 130, " +
                                    "               displayField: ''text'',  " +
                                    "valueField: ''value'', " +
                                    "store:Ext.create(''Ext.data.Store'', { " +
                                    "	                    fields: [''text'', ''value''], " +
                                    "	                    data: [ " +
                                    "                           { text: ''审核（报告）时间'', value: ''CHECKDATE'' }, " +
                                    "                            { text: ''核收日期'', value: ''RECEIVEDATE'' }, " +
                                    "                            { text: ''采样日期'', value: ''COLLECTDATE'' }, " +
                                    "                            { text: ''签收日期'', value: ''INCEPTDATE'' }, " +
                                    "                           { text: ''检测（上机）日期'', value: ''TESTDATE'' }, " +
                                    "                            { text: ''录入（操作）日期'', value: ''OPERDATE'' } " +
                                    "	                    ] " +
                                    "	                }), " +
                                    "                value: [''CHECKDATE''], " +
                                    "                listeners: { " +
                                    "                    change: function (m, v) { " +
                                    "                        var selectDate = this.ownerCt.ownerCt.getItem(''selectdate''); " +
                                    "                        selectDate.name = v; " +
                                    "                        this.ownerCt.ownerCt.DateField = v; " +
                                    "                   } " +
                                    "                } " +
                                    "            }searchAndNext " +
                                    "		    { type: ''search'', xtype: ''uxdatearea'', itemId: ''selectdate'', name: ''CHECKDATE'', labelWidth: 0, width: 190 }') ";
                        listSQL.Add(updateSql);
                        updateSql = "ALTER TABLE [dbo].[B_SearchUnit] ADD CONSTRAINT [PK_B_SELECTUNIT] PRIMARY KEY CLUSTERED ([SID])" +
                                    "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) " +
                                    "ON [PRIMARY]";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("EmpDeptLinks", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[EmpDeptLinks](" +
                                    "	[EDLID] [bigint] NOT NULL," +
                                    "	[UserNo] [bigint] NULL," +
                                    "	[DeptNo] [bigint] NULL," +
                                    "	[UserCName] [varchar](50) NULL," +
                                    "	[ShortCode] [varchar](50) NULL," +
                                    "	[DeptCName] [varchar](200) NULL," +
                                    "	[DataAddTime] [datetime] NULL," +
                                    "	[DataUpdateTime] [datetime] NULL," +
                                    " CONSTRAINT [PK_EmpDeptLinks] PRIMARY KEY CLUSTERED " +
                                    "(" +
                                    "	[EDLID] ASC" +
                                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                                    ") ON [PRIMARY]";

                        listSQL.Add(updateSql);
                        updateSql = "SET ANSI_PADDING OFF";
                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("RFP_ReportFormPrint_Operation", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[RFP_ReportFormPrint_Operation](" +
                                    "	[LabID] [bigint] NOT NULL," +
                                    "	[RFPOperationID] [bigint] NOT NULL," +
                                    "	[BobjectID] [bigint] NULL," +
                                    "	[ReceiveDate] [datetime] NULL," +
                                    "	[SectionNo] [int] NULL," +
                                    "	[TestTypeNo] [int] NULL," +
                                    "	[SampleNo] [varchar](20) NULL," +
                                    "	[Station] [varchar](200) NULL," +
                                    "	[EmpID] [bigint] NULL," +
                                    "	[EmpName] [varchar](20) NULL," +
                                    "	[DeptId] [bigint] NULL," +
                                    "	[DeptName] [varchar](200) NULL," +
                                    "	[Type] [bigint] NULL," +
                                    "	[TypeName] [varchar](20) NULL," +
                                    "	[BusinessModuleCode] [varchar](20) NULL," +
                                    "	[Memo] [varchar](500) NULL," +
                                    "	[DispOrder] [int] NULL," +
                                    "	[IsUse] [bit] NULL," +
                                    "	[CreatorID] [bigint] NULL," +
                                    "	[CreatorName] [varchar](50) NULL," +
                                    "	[DataAddTime] [datetime] NULL," +
                                    "	[DataUpdateTime] [datetime] NULL," +
                                    "	[DataTimeStamp] [timestamp] NULL," +
                                    " CONSTRAINT [PK_SC_OPERATION] PRIMARY KEY CLUSTERED " +
                                    "(" +
                                    "	[RFPOperationID] ASC" +
                                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                                    ") ON [PRIMARY]";

                        listSQL.Add(updateSql);
                    }
                    if (!CheckDataObjectIsExists("SC_Operation", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[SC_Operation](" +
                                    "	[LabID] [bigint] NULL," +
                                    "	[SCOperationID] [bigint] NOT NULL," +
                                    "	[BobjectID] [bigint] NOT NULL," +
                                    "	[Type] [bigint] NULL," +
                                    "	[Memo] [varchar](500) NULL," +
                                    "	[DispOrder] [int] NULL," +
                                    "	[IsUse] [bit] NULL," +
                                    "	[CreatorID] [bigint] NULL," +
                                    "	[CreatorName] [varchar](50) NULL," +
                                    "	[DataAddTime] [datetime] NULL," +
                                    "	[DataUpdateTime] [datetime] NULL," +
                                    "	[DataTimeStamp] [timestamp] NULL," +
                                    "	[TypeName] [varchar](50) NULL," +
                                    "	[BusinessModuleCode] [varchar](50) NULL," +
                                    " CONSTRAINT [PK_SC_OPERATION_log] PRIMARY KEY CLUSTERED " +
                                    "(" +
                                    "	[SCOperationID] ASC" +
                                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                                    ") ON [PRIMARY]";

                        listSQL.Add(updateSql);
                    }
                    #endregion

                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.1");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.1 Error");
                    return false;
                }

            }
            else {
                result = true;
            }
            #endregion
            #region 1.0.0.2
            if (IsUpdateDataBase(oldVersion, "1.0.0.2"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    
                    DataSet ds = null;
                    #region 2018/12/6-2019/2/18
                    updateSql = "declare  @a int; set @a = (select count(*) from B_Parameter where Id=25); \r\n" +
                                " if (@a <= 0)  begin \r\n" +
                                " INSERT INTO [dbo].[B_Parameter]  VALUES (N'25', N'页面公共配置', N'allPageType', N'config', N'isSeniorSetting', N'false', NULL, N'bool', N'2018-10-16 15:20:00.000', N'0', NULL, NULL); \r\n" +
                                " end \r\n" +
                                " set @a = 0 \r\n";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_SearchSetting', 'SearchType') IS NULL ALTER TABLE [dbo].[B_SearchSetting] ADD SearchType int;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_SearchSetting', 'JsCode') IS NOT NULL ALTER TABLE B_SearchSetting DROP COLUMN JsCode;";
                    listSQL.Add(updateSql);
                    updateSql = "UPDATE [dbo].[B_SearchSetting] SET SearchType=1;";
                    listSQL.Add(updateSql);
                    updateSql = "declare  @a int; set @a = (select count(*) from B_SearchUnit where SID=10); \r\n" +
                            " if (@a <= 0)  begin \r\n";
                    updateSql += "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'10', N'病区', N'DistrictName', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', N'=', NULL, N'{ type: ''search'',xtype: ''textfield'', mark: ''='', itemId: \"districtNameNo\", name: ''DistrictName'', width: 130, hidden: true } searchAndNext " +
                            "				{ " +
                            "	                fieldLabel: '' 病区:'', " +
                            "	                xtype: ''uxCheckTrigger'', " +
                            "	                emptyText: ''病区'', " +
                            "	                zIndex:1, " +
                            "	                width: 200, " +
                            "	                labelSeparator: '''', " +
                            "	                labelWidth: 70, " +
                            "	                labelAlign: ''right'', " +
                            "	                itemId: ''districtName'', " +
                            "	                className: ''Shell.class.higquery.district'', " +
                            "	                listeners: { " +
                            "	                    check: function (p, record) { " +
                            "	                        var item = \"\"; " +
                            "	                        var DistrictName = \"\"; " +
                            "							var container = this.ownerCt; " +
                            "							var toolbar = container.ownerCt;	 " +
                            "							if (record == null) {   " +
                            "								item = toolbar.getItem(\"districtNameNo\");		 " +
                            "	                            DistrictName = container.getComponent(\"districtName\"); " +
                            "								item.setValue(\"\"); " +
                            "	                            DistrictName.setValue(\"\"); " +
                            "	                            return;                  } " +
                            "	                        item = toolbar.getItem(\"districtNameNo\"); " +
                            "	                        DistrictName = container.getComponent(\"districtName\"); " +
                            "							item.setValue(record.get(\"CName\")); " +
                            "	                        DistrictName.setValue(record.get(\"CName\")); " +
                            "	                        p.close();          }        }       }') ";
                    updateSql += " end \r\n" +
                            " set @a = 0 \r\n";
                    listSQL.Add(updateSql);
                    updateSql = " declare  @a int; set @a = (select count(*) from B_SearchUnit where SID=14); \r\n" +
                                " if (@a <= 0)  begin \r\n";
                    updateSql += "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'14', N'临床诊断', N'ZDY7', N'search', N'70', N'200', NULL, NULL, N'textfield', NULL, NULL, N'{	" +
                            "	                fieldLabel: '' 临床诊断:'', " +
                            "	                xtype: ''textfield'', " +
                            "	                emptyText: ''临床诊断'', " +
                            "	                width: 200, " +
                            "	                labelWidth: 70, " +
                            "	                labelAlign: ''right'', " +
                            "	                itemId: ''zdy7'',	 " +
                            "					name: ''zdy7''		 " +
                            "            	}') ";
                    updateSql += " end \r\n" +
                                " set @a = 0 \r\n";
                    listSQL.Add(updateSql);
                    updateSql = "declare  @a int; set @a = (select count(*) from B_SearchUnit where SID=15); \r\n" +
                            " if (@a <= 0)  begin \r\n";
                    updateSql += "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'15', N'录入者', N'Operator', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N'{ type: ''search'',xtype: ''textfield'', mark: ''='', itemId: \"operatorNO\", name: ''Operator'', width: 130, hidden: true } searchAndNext " +
                            "				{ " +
                            "	                fieldLabel: '' 录入者:'', " +
                            "	                xtype: ''uxCheckTrigger'', " +
                            "	                emptyText: ''录入者'', " +
                            "	                zIndex:1, " +
                            "	                width: 200, " +
                            "	                labelSeparator: '''', " +
                            "	                labelWidth: 70, " +
                            "	                labelAlign: ''right'', " +
                            "	                itemId: ''operator'', " +
                            "	                className: ''Shell.class.higquery.operator'', " +
                            "	                listeners: { " +
                            "	                    check: function (p, record) { " +
                            "							var item = \"\"; " +
                            "	                        var Operator = \"\"; " +
                            "							var container = this.ownerCt; " +
                            "							var toolbar = container.ownerCt; " +
                            "							if (record == null) { " +
                            "								item = toolbar.getItem(\"operatorNO\"); " +
                            "	                            Operator = container.getComponent(\"operator\"); " +
                            "								item.setValue(\"\"); " +
                            "	                            Operator.setValue(\"\"); " +
                            "	                            return; " +
                            "	                       } " +
                            "						    item = toolbar.getItem(\"operatorNO\"); " +
                            "	                        Operator = container.getComponent(\"operator\"); " +
                            "							item.setValue(record.get(\"CName\")); " +
                            "	                        Operator.setValue(record.get(\"CName\")); " +
                            "	                        p.close();              }         }     }') ";
                    updateSql += " end \r\n" +
                            " set @a = 0 \r\n";
                    listSQL.Add(updateSql);
                    updateSql = "declare  @a int; set @a = (select count(*) from B_SearchUnit where SID=16); \r\n" +
                        " if (@a <= 0)  begin \r\n";
                    updateSql += "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'16', N'审核者', N'Checker', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"checkerNO\", name: ''Checker'', width: 130, hidden: true } searchAndNext	{ " +
                            "	                fieldLabel: '' 审核者:'', " +
                            "	                xtype: ''uxCheckTrigger'', " +
                            "	                emptyText: ''审核者'', " +
                            "	                zIndex:1, " +
                            "	                width: 200, " +
                            "	                labelSeparator: '''', " +
                            "	                labelWidth: 70, " +
                            "	                labelAlign: ''right'', " +
                            "	                itemId: ''checker'', " +
                            "	                className: ''Shell.class.higquery.checker'', " +
                            "	                listeners: { " +
                            "	                    check: function (p, record) { " +
                            "						    var item = \"\"; " +
                            "	                        var Checker = \"\"; " +
                            "							var container = this.ownerCt; " +
                            "							var toolbar = container.ownerCt; " +
                            "							if (record == null) { " +
                            "							    item = toolbar.getItem(\"checkerNO\"); " +
                            "	                            Checker = container.getComponent(\"checker\"); " +
                            "								item.setValue(\"\"); " +
                            "	                            Checker.setValue(\"\"); " +
                            "	                            return;	                       } " +
                            "						    item = toolbar.getItem(\"checkerNO\"); " +
                            "	                        Checker = container.getComponent(\"checker\"); " +
                            "							item.setValue(record.get(\"CName\")); " +
                            "	                        Checker.setValue(record.get(\"CName\")); " +
                            "	                        p.close();            }         }    }') ";
                    updateSql += " end \r\n" +
                            " set @a = 0 \r\n";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_ColumnsUnit', 'IsUse') IS NULL ALTER TABLE [dbo].[B_ColumnsUnit] ADD IsUse bit;";
                    listSQL.Add(updateSql);
                    updateSql = "update[dbo].[B_ColumnsUnit] set IsUse = 'True'";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_ColumnsSetting', 'IsUse') IS NULL ALTER TABLE [dbo].[B_ColumnsSetting] ADD IsUse bit;";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsSetting] set IsUse = 'True'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsSetting] set IsUse = 'False' where ColID = '2'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record, index) { " +
                                "	//显示审核时间 \r\n" +
                                "				var Cdate = Shell.util.Date.toString(record.get(\"CHECKDATE\"), true); " +
                                "				var Ctime = Shell.util.Date.toString(record.get(\"CHECKTIME\"), false); " +
                                "				var value = ''''; " +
                                "				if(Cdate !=null){ " +
                                "					value = Cdate; " +
                                "				} " +
                                "				if(Ctime !=null){ " +
                                "				  var arry = Ctime.split('' ''); " +
                                "				  if(arry!=null && arry.length >1){ " +
                                "					 value +='' ''+ arry[1]; " +
                                "					} " +
                                "				} " +
                                "	//var value = v ? Shell.util.Date.toString(v, true) : \"\"; " +
                                "	//meta.tdAttr = ''data-qtip=\" <b> '' + value + '' </b> \"''; \r\n " +
                                "	return value; " +
                                "}}',IsUse='FALSE' where COlID = 2 ";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (value, meta, record) {" +
                                "	            if (value) meta.tdAttr = ''data-qtip=\" <b> '' + value + '' </b> \"'';" +
                                "	            return value;" +
                                "	        }}' where COlID = 4";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record, index) {" +
                                "	 var value = record.get(\"RECEIVEDATE\").split('' '')[0];" +
                                "	 return value;" +
                                "}}' where COlID = 6";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record) {" +
                                "	                var imgName = (v && v >= me.maxPrintTimes) ? \"unprint\" : \"print\"," +
                                "		                tootip = \"已经打印 <b style = ''color:red;'' > \" + v + \" </b> 次\"," +
                                "	                    value = v ? \" <b> \" + v + \" </b> \" : \"\";" +
                                "" +
                                "	                meta.tdAttr = ''data-qtip=\"'' + tootip + ''\"'';" +
                                "	                " +
                                "	                var result = '''';" +
                                "	                //if(v >= 0){" +
                                "	                    //result = \" < img src = ''\" + Shell.util.Path.uiPath + \" / ReportPrint / images / \" + imgName + \".png'' /> \" + v;" +
                                "	                //} \r\n " +
                                "	                if(v > 0){" +
                                "                           meta.style=\"background-color:red\";" +
                                "	                    result = v;" +
                                "	                }else{" +
                                "				result = v;" +
                                "                       }" +
                                "	                return result;" +
                                "	            }}' where COlID = 7";
                    listSQL.Add(updateSql);
                    updateSql = "declare  @a int; set @a = (select count(*) from B_ColumnsUnit where ColID=8); \r\n" +
                    " if (@a <= 0)  begin \r\n";
                    updateSql += "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'8', N'审核日期', N'CHECKDATE', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {" +
                            "	//显示审核日期 \r\n" +
                            "				var Cdate = record.get(\"CHECKDATE\");" +
                            "				var value = '''';" +
                            "				if(Cdate !=null){" +
                            " 				  var arry = Cdate.substr(0,10);" +
                            "				  if(arry!=null && arry.length >1){" +
                            "					 value =arry;	}	}			" +
                            "	return value;" +
                            "}}', N'1')";
                    updateSql += " end \r\n" +
                        " set @a = 0 \r\n";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=9", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'9', N'审核时间', N'CHECKTIME', NULL, N'110', NULL, NULL, N' " +
                                "{renderer:function (v, meta, record, index) { " +
                                "	//显示审核时间 \r\n" +
                                "				var Ctime = record.get(\"CHECKTIME\"); " +
                                "				var value = ''''; " +
                                "				if(Ctime !=null){ " +
                                "				  var arry = Ctime.substring(Ctime.length-9); " +
                                "				  if(arry!=null && arry.length >1){ " +
                                "					 value =arry;	}	} " +
                                "	return value; " +
                                "}}', N'1') ";
                        listSQL.Add(updateSql);
                    }
                    #endregion
                    #region 2019/1/2 - 2019/2/27
                    updateSql = "delete from B_SearchSetting where SearchType = 2;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_SearchSetting', 'SearchType') IS NOT NULL ALTER TABLE B_SearchSetting DROP COLUMN SearchType;";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='doctor' where AppType='医生';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='lis' where AppType='检验之星';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='nurse' where AppType='护士';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='odp' where AppType='查询台';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='selfhelp' where AppType='自助打印';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='doctor' where AppType='医生';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='lis' where AppType='检验之星';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='nurse' where AppType='护士';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='odp' where AppType='查询台';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='selfhelp' where AppType='自助打印';";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_ColumnsSetting', 'Render') IS NOT NULL ALTER TABLE B_ColumnsSetting DROP COLUMN Render;";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=26", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'26', N'页面公共配置', N'allPageType', N'config', N'printCountSetting', N'100', NULL, N'int', N'2019-01-02 10:13:00.000', NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SICKTYPENAME'', name: ''SICKTYPENAME'', width: 130, hidden: true } searchAndNext          { " +
                                "                fieldLabel: '''', " +
                                "                xtype: ''uxCheckTrigger'', " +
                                "                emptyText: ''就诊类型'', " +
                                "                zIndex:1, " +
                                "               width: 150, " +
                                "                labelSeparator: '''', " +
                                "                labelWidth: 55, " +
                                "                labelAlign: ''right'', " +
                                "                itemId: ''SickTypeNo'', " +
                                "               className: ''Shell.class.sicktype.SickType'', " +
                                "               listeners: { " +
                                "                    check: function (p, record) { " +
                                "                        var item = \"\"; " +
                                "                        var clientName = \"\"; " +
                                "                        var me = this.ownerCt.ownerCt; " +
                                "	                    if (record == null) { " +
                                "                            item = me.getItem(\"SICKTYPENAME\"); " +
                                "                            clientName = me.getItem(\"SickTypeNo\"); " +
                                "                            item.setValue(\"\"); " +
                                "                            clientName.setValue(\"\"); " +
                                "                            return;                    }			 " +
                                "                        item = me.getItem(\"SICKTYPENAME\"); " +
                                "                        clientName = me.getItem(\"SickTypeNo\"); " +
                                "                        item.setValue(  record.get(\"CName\") ); " +
                                "                       clientName.setValue(record.get(\"CName\")); " +
                                "                       p.close(); " +
                                "                   }         }' where SID = 2 ";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=10", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'10', N'样本号', N'SampleNo', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=17", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'17', N'条码号', N'SERIALNO', N'search', N'45', N'150', NULL, NULL, N'textfield', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''SERIALNO'', fieldLabel: ''条码号'', labelWidth: 50, width: 150 }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=18", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'18', N'小组类型', N'SECTIONNO', N'search', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N' { type: ''search'', xtype: ''textfield'', mark: ''='',itemId:\"SECTIONNO\", name: ''SECTIONNO'', width: 130,hidden:true } searchAndNext  {   " +
                                    "                xtype: ''uxCheckTrigger'', " +
                                    "                emptyText: ''小组过滤'', " +
                                    "                width: 150, " +
                                    "               labelSeparator: '''', " +
                                    "                labelWidth: 55, " +
                                    "                labelAlign: ''right'', " +
                                    "                itemId: ''secname'', " +
                                    "                className: ''Shell.class.pgroup.section2'', " +
                                    "                listeners: { " +
                                    "                    check: function (p, record) { " +
                                    "                    var me=  this.ownerCt.ownerCt; " +
                                    "                    if (record == null) { " +
                                    "                    me.getItem(\"SECTIONNO\").setValue(\"\"); " +
                                    "                    me.getItem(\"secname\").setValue(\"\"); " +
                                    "                    return; " +
                                    "	                } " +
                                    "	               	me.getItem(\"SECTIONNO\").setValue(record.get(\"SectionNo\")); " +
                                    "	                me.getItem(\"secname\").setValue(record.get(\"CName\")); " +
                                    "	                p.close();         }           }      }') ";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=19", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'19', N'检验类型', N'TESTTYPENO', N'search', N'60', N'130', NULL, NULL, N'combobox', N'=', NULL, N'{ " +
                                    "			    fieldLabel: ''检验类型''," +
                                    "                type: ''other'', " +
                                    "				xtype: ''combobox''," +
                                    "               itemId: ''testType''," +
                                    "               width: 130," +
                                    "				labelWidth:60," +
                                    "                displayField: ''text'', " +
                                    "				valueField: ''value''," +
                                    "				store:Ext.create(''Ext.data.Store'', {" +
                                    "	                    fields: [''text'', ''value'']," +
                                    "	                    data: [" +
                                    "                            { text: ''常规'', value: ''1'' }," +
                                    "                            { text: ''急诊'', value: ''2'' }," +
                                    "                            { text: ''质控'', value: ''3'' }   ]             })," +
                                    "                /*alue: [''1''],*/" +
                                    "                listeners: {" +
                                    "                    change: function (m, v) {" +
                                    "                        var testTypeNo = this.ownerCt.ownerCt.getItem(''TestTypeNo'');" +
                                    "                       testTypeNo.setValue(v);       }                }" +
                                    "            }searchAndNext" +
                                    "		    { type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"TestTypeNo\", name: ''TESTTYPENO'', width: 130,hidden:true }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=20", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'20', N'收费类型', N'CHARGENO', N'search', N'60', N'130', NULL, NULL, N'combobox', N'=', NULL, N'{" +
                                    "			    fieldLabel: ''收费类型''," +
                                    "                type: ''other'', " +
                                    "				xtype: ''combobox''," +
                                    "                itemId: ''charge''," +
                                    "                width: 130," +
                                    "				labelWidth:60," +
                                    "               displayField: ''text'', " +
                                    "				valueField: ''value''," +
                                    "				store:Ext.create(''Ext.data.Store'', {" +
                                    "	                    fields: [''text'', ''value'']," +
                                    "	                    data: [" +
                                    "                            { text: ''国产'', value: ''1'' }," +
                                    "                            { text: ''进口'', value: ''2'' }       " +
                                    "	                    ]" +
                                    "	                })," +
                                    "                listeners: {" +
                                    "                    change: function (m, v) {" +
                                    "                        var cHARGENO = this.ownerCt.ownerCt.getItem(''CHARGENO'');" +
                                    "                        cHARGENO.setValue(v);                        " +
                                    "                    }" +
                                    "                }" +
                                    "           }searchAndNext" +
                                    "		    { type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"CHARGENO\", name: ''CHARGENO'', width: 130,hidden:true }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=21", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'21', N'样本类型', N'SAMPLETYPENO', N'search', N'55', N'150', NULL, NULL, N'uxCheckTrigger', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SAMPLETYPENO'', name: ''SAMPLETYPENO'', width: 130, hidden: true }searchAndNext " +
                                    "           { " +
                                    "                fieldLabel: '''', " +
                                    "                xtype: ''uxCheckTrigger'', " +
                                    "                emptyText: ''样本类型'', " +
                                    "                zIndex:1, " +
                                    "                width: 150, " +
                                    "                labelSeparator: '''', " +
                                    "                labelWidth: 55, " +
                                    "                labelAlign: ''right'', " +
                                    "                itemId: ''SampleTypeName'', " +
                                    "                className: ''Shell.class.sampletype.SampleType'', " +
                                    "                listeners: { " +
                                    "                    check: function (p, record) { " +
                                    "                        var item = \"\"; " +
                                    "                        var clientName = \"\"; " +
                                    "                        var me = this.ownerCt.ownerCt; " +
                                    "	                    if (record == null) { " +
                                    "                            item = me.getItem(\"SAMPLETYPENO\"); " +
                                    "                            clientName = me.getItem(\"SampleTypeName\"); " +
                                    "                            item.setValue(\"\"); " +
                                    "                            clientName.setValue(\"\"); " +
                                    "                            return; " +
                                    "                        } " +
                                    "                        item = me.getItem(\"SAMPLETYPENO\"); " +
                                    "                        clientName = me.getItem(\"SampleTypeName\"); " +
                                    "                        item.setValue(  record.get(\"SampleTypeNo\") ); " +
                                    "                        clientName.setValue(record.get(\"CName\")); " +
                                    "                        p.close(); " +
                                    "                    } " +
                                    "                } " +
                                    "            }') ";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SICKTYPENAME'', name: ''SICKTYPENAME'', width: 130, hidden: true } searchAndNext " +
                                "            { " +
                                "               fieldLabel: '''', " +
                                "                xtype: ''uxCheckTrigger'', " +
                                "                emptyText: ''就诊类型'', " +
                                "                zIndex:1, " +
                                "                width: 150, " +
                                "                labelSeparator: '''', " +
                                "                labelWidth: 55, " +
                                "                labelAlign: ''right'', " +
                                "                itemId: ''SickTypeNo'', " +
                                "                className: ''Shell.class.dictionaries.sicktype.SickType'', " +
                                "                listeners: { " +
                                "                    check: function (p, record) { " +
                                "                        var item = \"\"; " +
                                "                        var clientName = \"\"; " +
                                "                        var me = this.ownerCt.ownerCt; " +
                                "	                    if (record == null) { " +
                                "                            item = me.getItem(\"SICKTYPENAME\"); " +
                                "                            clientName = me.getItem(\"SickTypeNo\"); " +
                                "                            item.setValue(\"\"); " +
                                "                            clientName.setValue(\"\"); " +
                                "                            return; " +
                                "                       }						 " +
                                "                        item = me.getItem(\"SICKTYPENAME\"); " +
                                "                        clientName = me.getItem(\"SickTypeNo\"); " +
                                "                       item.setValue(  record.get(\"CName\") ); " +
                                "                        clientName.setValue(record.get(\"CName\")); " +
                                "                        p.close(); " +
                                "                    } " +
                                "                } " +
                                 "           }' where SID = 2 ";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''in'', itemId: \"DeptNo\", name: ''DeptNo'', width: 130, hidden: true } searchAndNext        {" +
                                "                fieldLabel: ''''," +
                                "                xtype: ''uxCheckTrigger''," +
                                "                emptyText: ''科室过滤''," +
                                "               zIndex:1," +
                                "                width: 150," +
                                "                labelSeparator: ''''," +
                                "                labelWidth: 55," +
                                "                labelAlign: ''right''," +
                                "               itemId: ''ClienteleName''," +
                                "                className: ''Shell.class.dictionaries.dept.CheckGrid''," +
                                "                listeners: {" +
                                "                    check: function (p, record) {" +
                                "                        var item = \"\";" +
                                "                        var clientName = \"\";" +
                                "                        var me = this.ownerCt.ownerCt;" +
                                "						if (record == null) {" +
                                "                            item = me.getItem(\"DeptNo\");" +
                                "                            clientName = me.getItem(\"ClienteleName\");" +
                                "                            item.setValue(\"\");" +
                                "                            clientName.setValue(\"\");" +
                                "                            return;                        }	" +
                                "                        item = me.getItem(\"DeptNo\");" +
                                "                        clientName = me.getItem(\"ClienteleName\");" +
                                "                        item.setValue(\"(\" + record.get(\"DeptNo\") + \")\");" +
                                "                        clientName.setValue(record.get(\"CName\"));" +
                                "                        p.close();" +
                                "                    }" +
                                "                }" +
                                "            }'	where SID = 1";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"checkerNO\", name: ''Checker'', width: 130, hidden: true } searchAndNext	{" +
                                "	                fieldLabel: '' 审核者:''," +
                                "	                xtype: ''uxCheckTrigger''," +
                                "	                emptyText: ''审核者''," +
                                "	                zIndex:1," +
                                "	                width: 200," +
                                "	                labelSeparator: ''''," +
                                "	                labelWidth: 70," +
                                "	                labelAlign: ''right''," +
                                "	                itemId: ''checker''," +
                                "	                className: ''Shell.class.dictionaries.higquery.checker''," +
                                "	                listeners: {" +
                                "	                    check: function (p, record) {" +
                                "						    var item = \"\";" +
                                "	                        var Checker = \"\";" +
                                "							var container = this.ownerCt;" +
                                "							var toolbar = container.ownerCt;" +
                                "							if (record == null) {" +
                                "							    item = toolbar.getItem(\"checkerNO\");" +
                                "	                            Checker = container.getComponent(\"checker\");" +
                                "								item.setValue(\"\");" +
                                "	                            Checker.setValue(\"\");" +
                                "	                            return;" +
                                "	                       }" +
                                "						    item = toolbar.getItem(\"checkerNO\");" +
                                "	                        Checker = container.getComponent(\"checker\");" +
                                "							item.setValue(record.get(\"CName\"));" +
                                "	                        Checker.setValue(record.get(\"CName\"));" +
                                "	                        p.close();" +
                                "	                    }" +
                                "	                }" +
                                "	            }'	where SID = 16";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"districtNameNo\", name: ''DistrictName'', width: 130, hidden: true } searchAndNext  " +
                                "				{" +
                                "	                fieldLabel: '' 病区:''," +
                                "	                xtype: ''uxCheckTrigger''," +
                                "	                emptyText: ''病区''," +
                                "	                zIndex:1," +
                                "	                width: 200," +
                                "	                labelSeparator: ''''," +
                                "	                labelWidth: 70," +
                                "	                labelAlign: ''right''," +
                                "	                itemId: ''districtName''," +
                                "	                className: ''Shell.class.dictionaries.higquery.district''," +
                                "	                listeners: {" +
                                "	                    check: function (p, record) {" +
                                "	                        var item = \"\";" +
                                "	                        var DistrictName = \"\";" +
                                "							var container = this.ownerCt;" +
                                "							var toolbar = container.ownerCt;" +
                                "							if (record == null) {  " +
                                "								item = toolbar.getItem(\"districtNameNo\");	" +
                                "	                            DistrictName = container.getComponent(\"districtName\");" +
                                "								item.setValue(\"\");" +
                                "	                            DistrictName.setValue(\"\");" +
                                "	                            return;" +
                                "	                       }" +
                                "	                        item = toolbar.getItem(\"districtNameNo\");" +
                                "	                        DistrictName = container.getComponent(\"districtName\");" +
                                "							item.setValue(record.get(\"CName\"));" +
                                "	                        DistrictName.setValue(record.get(\"CName\"));" +
                                "	                        p.close();" +
                                "	                    }" +
                                "	                }" +
                                "	            }'	where SID = 10";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"operatorNO\", name: ''Operator'', width: 130, hidden: true } searchAndNext " +
                                "				{ " +
                                "	                fieldLabel: '' 录入者:'', " +
                                "	                xtype: ''uxCheckTrigger'', " +
                                "	                emptyText: ''录入者'', " +
                                "	                zIndex:1, " +
                                "	                width: 200, " +
                                "	                labelSeparator: '''', " +
                                "	                labelWidth: 70, " +
                                "	                labelAlign: ''right'', " +
                                "	                itemId: ''operator'', " +
                                "	                className: ''Shell.class.dictionaries.higquery.operator'', " +
                                "	                listeners: { " +
                                "	                    check: function (p, record) { " +
                                "							var item = \"\"; " +
                                "	                        var Operator = \"\"; " +
                                "							var container = this.ownerCt; " +
                                "							var toolbar = container.ownerCt; " +
                                "							if (record == null) { " +
                                "								item = toolbar.getItem(\"operatorNO\"); " +
                                "	                            Operator = container.getComponent(\"operator\"); " +
                                "								item.setValue(\"\"); " +
                                "	                            Operator.setValue(\"\"); " +
                                "	                            return; " +
                                "	                       } " +
                                "						    item = toolbar.getItem(\"operatorNO\"); " +
                                "	                        Operator = container.getComponent(\"operator\"); " +
                                "							item.setValue(record.get(\"CName\")); " +
                                "	                        Operator.setValue(record.get(\"CName\")); " +
                                "	                        p.close(); " +
                                "	                    } " +
                                "	                } " +
                                "	            }'	where SID = 15 ";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = ' { type: ''search'', xtype: ''textfield'', mark: ''='',itemId:\"SECTIONNO\", name: ''SECTIONNO'', width: 130,hidden:true } searchAndNext " +
                                "            {   xtype: ''uxCheckTrigger'', " +
                                "                emptyText: ''小组过滤'', " +
                                "                width: 150, " +
                                "                labelSeparator: '''', " +
                                "                labelWidth: 55, " +
                                "                labelAlign: ''right'', " +
                                "                itemId: ''secname'', " +
                                "                className: ''Shell.class.dictionaries.pgroup.section2'', " +
                                "                listeners: { " +
                                "                    check: function (p, record) { " +
                                "                    var me=  this.ownerCt.ownerCt; " +
                                "                    if (record == null) { " +
                                "                    me.getItem(\"SECTIONNO\").setValue(\"\"); " +
                                "                    me.getItem(\"secname\").setValue(\"\"); " +
                                "                    return; " +
                                "	                } " +
                                "	               	me.getItem(\"SECTIONNO\").setValue(record.get(\"SectionNo\")); " +
                                "	                me.getItem(\"secname\").setValue(record.get(\"CName\")); " +
                                "	                p.close(); " +
                                "                    } " +
                                "                } " +
                                "            }'	where SID = 18 ";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SAMPLETYPENO'', name: ''SAMPLETYPENO'', width: 130, hidden: true }searchAndNext " +
                                "            { " +
                                "                fieldLabel: '''', " +
                                "                xtype: ''uxCheckTrigger'', " +
                                "                emptyText: ''样本类型'', " +
                                "                zIndex:1, " +
                                "                width: 150, " +
                                "                labelSeparator: '''', " +
                                "                labelWidth: 55, " +
                                "                labelAlign: ''right'', " +
                                "                itemId: ''SampleTypeName'', " +
                                "                className: ''Shell.class.dictionaries.sampletype.SampleType'', " +
                                "                listeners: { " +
                                "                    check: function (p, record) { " +
                                "                        var item = \"\"; " +
                                "                        var clientName = \"\"; " +
                                "                        var me = this.ownerCt.ownerCt; " +
                                "	                    if (record == null) { " +
                                "                            item = me.getItem(\"SAMPLETYPENO\"); " +
                                "                            clientName = me.getItem(\"SampleTypeName\"); " +
                                "                            item.setValue(\"\"); " +
                                "                            clientName.setValue(\"\"); " +
                                "                            return; " +
                                "                        } " +
                                "                        item = me.getItem(\"SAMPLETYPENO\"); " +
                                "                        clientName = me.getItem(\"SampleTypeName\"); " +
                                "                        item.setValue(  record.get(\"SampleTypeNo\") ); " +
                                "                        clientName.setValue(record.get(\"CName\")); " +
                                "                        p.close(); " +
                                "                    } " +
                                "                } " +
                                "           }'	where SID = 21 ";
                    listSQL.Add(updateSql);
                    #endregion
                    #region 2019/3/8-2019/7/1
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=27", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'27', N'页面公共配置', N'allPageType', N'config', N'isCaseSensitive', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=22", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'22', N'检验者', N'Technician',NULL, N'70', N'200', NULL, NULL, N'combobox', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"technicianNO\", name: ''Technician'', width: 130, hidden: true } searchAndNext " +
                                    "				{ " +
                                    "	                fieldLabel: ''检验者:'', " +
                                    "	                xtype: ''uxCheckTrigger'', " +
                                    "	                emptyText: ''检验者'', " +
                                    "	                zIndex:1, " +
                                    "	                width: 200, " +
                                    "	                labelSeparator: '''', " +
                                    "	                labelWidth: 70, " +
                                    "	                labelAlign: ''right'', " +
                                    "	                itemId: ''technician'', " +
                                    "	                className: ''Shell.class.dictionaries.higquery.checker'', " +
                                    "	                listeners: { " +
                                    "	                    check: function (p, record) { " +
                                    "						    var item = \"\"; " +
                                    "	                        var Technician = \"\"; " +
                                    "							var container = this.ownerCt; " +
                                    "							var toolbar = container.ownerCt; " +
                                    "							if (record == null) { " +
                                    "							    item = toolbar.getItem(\"technicianNO\"); " +
                                    "	                            Technician = container.getComponent(\"technician\"); " +
                                    "								item.setValue(\"\"); " +
                                    "	                            Technician.setValue(\"\"); " +
                                    "	                            return; " +
                                    "	                       } " +
                                    "						    item = toolbar.getItem(\"technicianNO\"); " +
                                    "	                        Technician = container.getComponent(\"technician\"); " +
                                    "							item.setValue(record.get(\"CName\")); " +
                                    "	                        Technician.setValue(record.get(\"CName\")); " +
                                    "	                        p.close(); " +
                                    "	                    } " +
                                    "	                } " +
                                    "	            }') ";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=23", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'23', N'出生日期', N'Birthday', N'other', N'60', N'170', NULL, NULL, N'textfield', N'=', NULL, N'{type: ''other'', xtype: ''datefield'',format:''Y-m-d'', mark: ''='', name: ''Birthday'', fieldLabel: ''出生日期'', labelWidth: 60, width: 150 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=24", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'24', N'性别', N'GenderName', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''GenderName'', fieldLabel: ''性别'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=25", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'25', N'病床', N'Bed', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''Bed'', fieldLabel: ''病床'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=26", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'26', N'备注', N'FormMemo', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''FormMemo'', fieldLabel: ''备注'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=27", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'27', N'大备注', N'FormComment', N'search', N'50', N'150', NULL, NULL, N'textfield', N'like ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''FormComment'', fieldLabel: ''大备注'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=28", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'28', N'检验项目', N'ItemName', N'search', N'60', N'150', NULL, NULL, N'textfield', N'like', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''ItemName'', fieldLabel: ''检验项目'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=29", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'29', N'细菌标注类型', N'ZDY4', N'search', N'80', N'180', NULL, NULL, N'textfield', N'like ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''ZDY4'', fieldLabel: ''细菌标注类型'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=30", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'30', N'病房', N'WardNo', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"WardNos\", name: ''WardNo'', width: 130, hidden: true } searchAndNext" +
                                    "				{" +
                                    "	                fieldLabel: ''病房:''," +
                                    "	                xtype: ''uxCheckTrigger''," +
                                    "	                emptyText: ''病房''," +
                                    "	                zIndex:1," +
                                    "	                width: 200," +
                                    "	                labelSeparator: ''''," +
                                    "	                labelWidth: 60," +
                                    "	                labelAlign: ''right''," +
                                    "	                itemId: ''wardName''," +
                                    "	                className: ''Shell.class.dictionaries.higquery.wardNo''," +
                                    "	                listeners: {" +
                                    "	                    check: function (p, record) {" +
                                    "						    var item = \"\";" +
                                    "	                        var wardNames = \"\";" +
                                    "							var container = this.ownerCt;" +
                                    "							var toolbar = container.ownerCt;" +
                                    "							if (record == null) {" +
                                    "							    item = toolbar.getItem(\"WardNos\");" +
                                    "	                            wardNames = container.getComponent(\"wardName\");" +
                                    "								item.setValue(\"\");" +
                                    "	                            wardNames.setValue(\"\");" +
                                    "	                            return;" +
                                    "	                       }" +
                                    "						    item = toolbar.getItem(\"WardNos\");" +
                                    "	                        wardNames = container.getComponent(\"wardName\");" +
                                    "							item.setValue(record.get(\"WardNo\"));" +
                                    "	                        wardNames.setValue(record.get(\"CName\"));" +
                                    "	                        p.close();" +
                                    "	                    }" +
                                    "	                }" +
                                    "	               }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=32", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'32', N'年龄', N'Age', N'other', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''other'', xtype: ''numberfield'', mark: ''='', name: ''Age'', fieldLabel: ''年龄'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record) {" +
                                "	                var imgName = (v && v >= me.maxPrintTimes) ? \"unprint\" : \"print\"," +
                                "		                tootip = \"已经打印 <b style = ''color:red;'' > \" + v + \" </b> 次\"," +
                                "	                    value = v ? \" <b> \" + v + \" </b> \" : \"\";" +
                                "" +
                                "	                meta.tdAttr = ''data-qtip=\"'' + tootip + ''\"'';" +
                                "	                " +
                                "	                var result = '''';" +
                                "	                //if(v >= 0){" +
                                "	                    //result = \" < img src = ''\" + Shell.util.Path.uiPath + \" / ReportPrint / images / \" + imgName + \".png'' /> \" + v;" +
                                "	                //} \r\n" +
                                "	                if(v > 0){" +
                                "                           meta.style=\"background-color:red\";" +
                                "	                    result = v;" +
                                "	                }else{" +
                                "				result = v;" +
                                "                       }" +
                                "	                return result;" +
                                "	            }}' where COlID = 7";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit]  set JsCode='{type: ''other'', " +
                                "xtype: ''datefield''," +
                                "format:''Y-m-d''," +
                                "itemId: ''selectBirthday'', " +
                                "name: ''selectBirthday'', " +
                                "fieldLabel: ''出生日期'', " +
                                "labelWidth: 60, " +
                                "width: 150," +
                                "listeners: {" +
                                "                    change: function (m, v) {" +
                                "                        var Birthday = this.ownerCt.ownerCt.getItem(''Birthday'');" +
                                "						var selectBirthday = this.ownerCt.ownerCt.getItem(''selectBirthday'');" +
                                "						var m = parseInt(v.getMonth())+1;" +
                                "						if(m<=9){" +
                                "							m = \"0\"+m;" +
                                "						}" +
                                "						var d = v.getDate();" +
                                "						if(d<=9){" +
                                "							d = \"0\"+d;" +
                                "						}" +
                                "                        Birthday.setValue(v.getFullYear()+\" - \"+m+\" - \"+d);" +
                                "                    }" +
                                "                } }" +
                                "searchAndNext" +
                                "{ type: ''search'',mark: ''='', xtype: ''textfield'',format:''Y-m-d'', itemId: ''Birthday'', name: ''Birthday'', labelWidth: 0, width: 190,hidden: true }" +
                                "' where SID = '23'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit]  set JsCode='{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''Age'', fieldLabel: ''年龄'', labelWidth: 35, width: 110 }' where SID = '32'";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=34", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'34', N'卡号', N'ZDY3', N'search', N'50', N'210', NULL, NULL, N'textfield', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='',itemId: ''ZDY3'', name: ''ZDY3'', fieldLabel: ''卡号'', labelWidth: 50, width: 210,selectOnFocus:true }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=11", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'11', N'医嘱项目名称', N'ZDY3', NULL, N'100', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where ID=28", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'28', N'页面公共配置', N'allPageType', N'config', N'listWidth', N'550', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where ID=29", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'29', N'页面公共配置', N'allPageType', N'config', N'isviewportHeader', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where ID=72", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'72', N'查询打印页面配置', N'自助打印', N'config', N'notPrintSectionNo', N'', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update B_ColumnsUnit set ColumnName = 'SAMPLENO' where ColID = 10";
                    listSQL.Add(updateSql);

                    #endregion
                    #region 2019/8/13-2020/3/11
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=73", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'73', N'查询打印页面配置', N'自助打印', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=30", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'30', N'页面公共配置', N'allPageType', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=31", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'31', N'页面公共配置', N'allPageType', N'config', N'IsbTempReport', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=32", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'32', N'页面公共配置', N'allPageType', N'config', N'IsQueryRequest', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=12", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'12', N'样本类型', N'SampletypeName', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=13", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'13', N'科室', N'DeptName', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=74", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'74', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameTop', N'13', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=75", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'75', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameLeft', N'26', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=76", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'76', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameFontSize', N'26', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=77", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'77', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameColor', N'#0e121a', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=78", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'78', N'查询打印页面配置', N'自助打印', N'config', N'printnumTop', N'14', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=79", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'79', N'查询打印页面配置', N'自助打印', N'config', N'printnumLeft', N'34', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=80", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'80', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameFontSize', N'26', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=81", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'81', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameColor', N'#0e121a', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=82", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'82', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextTop', N'16', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=83", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'83', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextLeft', N'16', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=84", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'84', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextFontSize', N'35', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=85", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'85', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextColor', N'black', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=86", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'86', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeTop', N'13', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=87", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'87', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeLeft', N'6', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=88", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'88', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeFontSize', N'26', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=89", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'89', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeColor', N'#0e121a', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=90", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'90', N'查询打印页面配置', N'自助打印', N'config', N'tabgridTop', N'66', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=91", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'91', N'查询打印页面配置', N'自助打印', N'config', N'tabgridLeft', N'8', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=32", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'92', N'查询打印页面配置', N'自助打印', N'config', N'closeprintnumTop', N'13', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=93", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'93', N'查询打印页面配置', N'自助打印', N'config', N'closeprintnumLeft', N'41', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=94", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'94', N'查询打印页面配置', N'自助打印', N'config', N'caridTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=95", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'95', N'查询打印页面配置', N'自助打印', N'config', N'caridLeft', N'15', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=96", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'96', N'查询打印页面配置', N'自助打印', N'config', N'caridWidth', N'260', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }

                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=97", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'97', N'查询打印页面配置', N'自助打印', N'config', N'openJianpanTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=98", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'98', N'查询打印页面配置', N'自助打印', N'config', N'openJianpanLeft', N'53', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=99", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'99', N'查询打印页面配置', N'自助打印', N'config', N'closeJianpanTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=100", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'100', N'查询打印页面配置', N'自助打印', N'config', N'closeJianpanLeft', N'53', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=101", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'101', N'查询打印页面配置', N'自助打印', N'config', N'closeTop', N'69', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=102", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'102', N'查询打印页面配置', N'自助打印', N'config', N'closeLeft', N'15', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=103", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'103', N'查询打印页面配置', N'自助打印', N'config', N'closeFontSize', N'16', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=104", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'104', N'查询打印页面配置', N'自助打印', N'config', N'closeColor', N'black', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=105", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'105', N'查询打印页面配置', N'自助打印', N'config', N'reportviewTop', N'70', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=106", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'106', N'查询打印页面配置', N'自助打印', N'config', N'reportviewLeft', N'28', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=107", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'107', N'查询打印页面配置', N'自助打印', N'config', N'reportviewFontSize', N'14', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=108", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'108', N'查询打印页面配置', N'自助打印', N'config', N'reportviewColor', N'black', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=109", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'109', N'查询打印页面配置', N'自助打印', N'config', N'panelTop', N'36', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=110", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'110', N'查询打印页面配置', N'自助打印', N'config', N'panelLeft', N'15', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=111", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'111', N'查询打印页面配置', N'自助打印', N'config', N'panelWidth', N'600', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=112", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'112', N'查询打印页面配置', N'自助打印', N'config', N'panelHeight', N'242', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=33", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'33', N'页面公共配置', N'allPageType', N'config', N'IsHistory', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=34", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'34', N'页面公共配置', N'allPageType', N'config', N'IsBackups', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=113", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'113', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsTop', N'66', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=114", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "  INSERT INTO [dbo].[B_Parameter]  VALUES (N'114', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsLeft', N'8', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=115", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'115', N'查询打印页面配置', N'自助打印', N'config', N'noparitemname', N'', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '34' where ParaNo = 'tabgridTop' and SName = '自助打印'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '40' where ParaNo = 'tabgridLeft' and SName = '自助打印'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '34' where ParaNo = 'tabgridTop' and SName = 'selfhelp'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '40' where ParaNo = 'tabgridLeft' and SName = 'selfhelp'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '46' where ParaNo = 'tabgridLeft' and SName = '自助打印'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '46' where ParaNo = 'tabgridLeft' and SName = 'selfhelp'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '48' where ParaNo = 'closeJianpanLeft' and SName = '自助打印'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '48' where ParaNo = 'closeJianpanLeft' and SName = 'selfhelp'";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=116", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'116', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewTop', N'50', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=117", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'117', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewLeft', N'6', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=118", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'118', N'查询打印页面配置', N'自助打印', N'config', N'defaultCondition', N'', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=14", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'14', N'重申标记', N'BRevised', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=15", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'15', N'报告类型', N'ReportType', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=35", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'35', N'身份证号', N'ZDY13', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY13'', fieldLabel: ''身份证号'', labelWidth: 35, width: 110 }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=119", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'119', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=120", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'120', N'查询打印页面配置', N'自助打印', N'config', N'printnumIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=121", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'121', N'查询打印页面配置', N'自助打印', N'config', N'closeprintnumIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=122", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'122', N'查询打印页面配置', N'自助打印', N'config', N'tabgridHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=123", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'123', N'查询打印页面配置', N'自助打印', N'config', N'tabgridHangTouFontSize', N'20', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=124", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'124', N'查询打印页面配置', N'自助打印', N'config', N'tabgridNeiRongFontSize', N'16', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=125", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'125', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=126", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'126', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=127", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'127', N'查询打印页面配置', N'自助打印', N'config', N'caridHeight', N'30', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=128", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'128', N'查询打印页面配置', N'自助打印', N'config', N'caridFontSize', N'20', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=129", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'129', N'查询打印页面配置', N'自助打印', N'config', N'caridIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=130", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'130', N'查询打印页面配置', N'自助打印', N'config', N'openJianpanIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=131", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'131', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=132", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'132', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonLeft', N'65', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=133", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'133', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonWidth', N'100', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=134", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'134', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonHeight', N'36', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=135", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'135', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonIsHidden', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=136", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'136', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsFontSize', N'20', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=137", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'137', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=138", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'138', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=139", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'139', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonLeft', N'80', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=140", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'140', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonWidth', N'100', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=141", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'141', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonHeight', N'36', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=142", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'142', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=143", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'143', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewFontSize', N'30', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=144", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'144', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=36", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'36', N'拼音码', N'PinYinMa', N'search', N'60', N'180', NULL, NULL, N'textfield', N'like ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''PinYinMa'', fieldLabel: ''姓名拼音'', labelWidth: 60, width: 180 }')";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=37", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'37', N'住院号', N'ZDY5', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY5'', fieldLabel: ''住院号'', labelWidth: 60, width: 180 }')";
                            listSQL.Add(updateSql);
                        }
                        updateSql = "IF COL_LENGTH('SectionPrint', 'IsRFGraphdataPDf') IS  NULL ALTER TABLE [dbo].[SectionPrint] ADD IsRFGraphdataPDf bit;";
                        listSQL.Add(updateSql);
                    }
                    #endregion
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    DataSet ds = null;
                    #region 2018/12/6-2019/2/18
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=25", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'25', N'页面公共配置', N'allPageType', N'config', N'isSeniorSetting', N'false', NULL, N'bool', N'2018-10-16 15:20:00.000', N'0', NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "IF COL_LENGTH('B_SearchSetting', 'SearchType') IS NULL ALTER TABLE [dbo].[B_SearchSetting] ADD SearchType int;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_SearchSetting', 'JsCode') IS NOT NULL ALTER TABLE B_SearchSetting DROP COLUMN JsCode;";
                    listSQL.Add(updateSql);
                    updateSql = "UPDATE [dbo].[B_SearchSetting] SET SearchType=1;";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=10", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'10', N'病区', N'DistrictName', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', N'=', NULL, N'{ type: ''search'',xtype: ''textfield'', mark: ''='', itemId: \"districtNameNo\", name: ''DistrictName'', width: 130, hidden: true } searchAndNext " +
                              "				{ " +
                              "	                fieldLabel: '' 病区:'', " +
                              "	                xtype: ''uxCheckTrigger'', " +
                              "	                emptyText: ''病区'', " +
                              "	                zIndex:1, " +
                              "	                width: 200, " +
                              "	                labelSeparator: '''', " +
                              "	                labelWidth: 70, " +
                              "	                labelAlign: ''right'', " +
                              "	                itemId: ''districtName'', " +
                              "	                className: ''Shell.class.higquery.district'', " +
                              "	                listeners: { " +
                              "	                    check: function (p, record) { " +
                              "	                        var item = \"\"; " +
                              "	                        var DistrictName = \"\"; " +
                              "							var container = this.ownerCt; " +
                              "							var toolbar = container.ownerCt;	 " +
                              "							if (record == null) {   " +
                              "								item = toolbar.getItem(\"districtNameNo\");		 " +
                              "	                            DistrictName = container.getComponent(\"districtName\"); " +
                              "								item.setValue(\"\"); " +
                              "	                            DistrictName.setValue(\"\"); " +
                              "	                            return;                  } " +
                              "	                        item = toolbar.getItem(\"districtNameNo\"); " +
                              "	                        DistrictName = container.getComponent(\"districtName\"); " +
                              "							item.setValue(record.get(\"CName\")); " +
                              "	                        DistrictName.setValue(record.get(\"CName\")); " +
                              "	                        p.close();          }        }       }') ";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=14", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'14', N'临床诊断', N'ZDY7', N'search', N'70', N'200', NULL, NULL, N'textfield', NULL, NULL, N'{	" +
                                "	                fieldLabel: '' 临床诊断:'', " +
                                "	                xtype: ''textfield'', " +
                                "	                emptyText: ''临床诊断'', " +
                                "	                width: 200, " +
                                "	                labelWidth: 70, " +
                                "	                labelAlign: ''right'', " +
                                "	                itemId: ''zdy7'',	 " +
                                "					name: ''zdy7''		 " +
                                "            	}') ";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=15", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'15', N'录入者', N'Operator', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N'{ type: ''search'',xtype: ''textfield'', mark: ''='', itemId: \"operatorNO\", name: ''Operator'', width: 130, hidden: true } searchAndNext " +
                                "				{ " +
                                "	                fieldLabel: '' 录入者:'', " +
                                "	                xtype: ''uxCheckTrigger'', " +
                                "	                emptyText: ''录入者'', " +
                                "	                zIndex:1, " +
                                "	                width: 200, " +
                                "	                labelSeparator: '''', " +
                                "	                labelWidth: 70, " +
                                "	                labelAlign: ''right'', " +
                                "	                itemId: ''operator'', " +
                                "	                className: ''Shell.class.higquery.operator'', " +
                                "	                listeners: { " +
                                "	                    check: function (p, record) { " +
                                "							var item = \"\"; " +
                                "	                        var Operator = \"\"; " +
                                "							var container = this.ownerCt; " +
                                "							var toolbar = container.ownerCt; " +
                                "							if (record == null) { " +
                                "								item = toolbar.getItem(\"operatorNO\"); " +
                                "	                            Operator = container.getComponent(\"operator\"); " +
                                "								item.setValue(\"\"); " +
                                "	                            Operator.setValue(\"\"); " +
                                "	                            return; " +
                                "	                       } " +
                                "						    item = toolbar.getItem(\"operatorNO\"); " +
                                "	                        Operator = container.getComponent(\"operator\"); " +
                                "							item.setValue(record.get(\"CName\")); " +
                                "	                        Operator.setValue(record.get(\"CName\")); " +
                                "	                        p.close();              }         }     }') ";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=16", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'16', N'审核者', N'Checker', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"checkerNO\", name: ''Checker'', width: 130, hidden: true } searchAndNext	{ " +
                                "	                fieldLabel: '' 审核者:'', " +
                                "	                xtype: ''uxCheckTrigger'', " +
                                "	                emptyText: ''审核者'', " +
                                "	                zIndex:1, " +
                                "	                width: 200, " +
                                "	                labelSeparator: '''', " +
                                "	                labelWidth: 70, " +
                                "	                labelAlign: ''right'', " +
                                "	                itemId: ''checker'', " +
                                "	                className: ''Shell.class.higquery.checker'', " +
                                "	                listeners: { " +
                                "	                    check: function (p, record) { " +
                                "						    var item = \"\"; " +
                                "	                        var Checker = \"\"; " +
                                "							var container = this.ownerCt; " +
                                "							var toolbar = container.ownerCt; " +
                                "							if (record == null) { " +
                                "							    item = toolbar.getItem(\"checkerNO\"); " +
                                "	                            Checker = container.getComponent(\"checker\"); " +
                                "								item.setValue(\"\"); " +
                                "	                            Checker.setValue(\"\"); " +
                                "	                            return;	                       } " +
                                "						    item = toolbar.getItem(\"checkerNO\"); " +
                                "	                        Checker = container.getComponent(\"checker\"); " +
                                "							item.setValue(record.get(\"CName\")); " +
                                "	                        Checker.setValue(record.get(\"CName\")); " +
                                "	                        p.close();            }         }    }') ";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "IF COL_LENGTH('B_ColumnsUnit', 'IsUse') IS NULL ALTER TABLE [dbo].[B_ColumnsUnit] ADD IsUse bit;";
                    listSQL.Add(updateSql);
                    updateSql = "update[dbo].[B_ColumnsUnit] set IsUse = 'True'";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_ColumnsSetting', 'IsUse') IS NULL ALTER TABLE [dbo].[B_ColumnsSetting] ADD IsUse bit;";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsSetting] set IsUse = 'True'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsSetting] set IsUse = 'False' where ColID = '2'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record, index) { " +
                                "	//显示审核时间 \r\n" +
                                "				var Cdate = Shell.util.Date.toString(record.get(\"CHECKDATE\"), true); " +
                                "				var Ctime = Shell.util.Date.toString(record.get(\"CHECKTIME\"), false); " +
                                "				var value = ''''; " +
                                "				if(Cdate !=null){ " +
                                "					value = Cdate; " +
                                "				} " +
                                "				if(Ctime !=null){ " +
                                "				  var arry = Ctime.split('' ''); " +
                                "				  if(arry!=null && arry.length >1){ " +
                                "					 value +='' ''+ arry[1]; " +
                                "					} " +
                                "				} " +
                                "	//var value = v ? Shell.util.Date.toString(v, true) : \"\"; " +
                                "	//meta.tdAttr = ''data-qtip=\" <b> '' + value + '' </b> \"''; \r\n" +
                                "	return value; " +
                                "}}',IsUse='FALSE' where COlID = 2 ";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (value, meta, record) {" +
                                "	            if (value) meta.tdAttr = ''data-qtip=\" <b> '' + value + '' </b> \"'';" +
                                "	            return value;" +
                                "	        }}' where COlID = 4";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record, index) {" +
                                "	 var value = record.get(\"RECEIVEDATE\").split('' '')[0];" +
                                "	 return value;" +
                                "}}' where COlID = 6";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record) { var imgName = (v && v >= me.maxPrintTimes) ? \"unprint\" : \"print\", tootip = \"已经打印 <b style = ''color:red; '' > \" + v + \" </b> 次\",value = v ? \" <b> \" + v + \" </b> \" : \"\";meta.tdAttr = ''data-qtip=\"'' + tootip + ''\"'';var result = '''';if(v > 0){ meta.style=\"background-color:red\"; result = v;}else{result = v;}return result;}}' where COlID = 7";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=8", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'8', N'审核日期', N'CHECKDATE', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {" +
                                "	//显示审核日期 \r\n" +
                                "				var Cdate = record.get(\"CHECKDATE\");" +
                                "				var value = '''';" +
                                "				if(Cdate !=null){" +
                                " 				  var arry = Cdate.substr(0,10);" +
                                "				  if(arry!=null && arry.length >1){" +
                                "					 value =arry;	}	}			" +
                                "	return value;" +
                                "}}', N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=9", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'9', N'审核时间', N'CHECKTIME', NULL, N'110', NULL, NULL, N' " +
                                "{renderer:function (v, meta, record, index) { " +
                                "	//显示审核时间 \r\n" +
                                "				var Ctime = record.get(\"CHECKTIME\"); " +
                                "				var value = ''''; " +
                                "				if(Ctime !=null){ " +
                                "				  var arry = Ctime.substring(Ctime.length-9); " +
                                "				  if(arry!=null && arry.length >1){ " +
                                "					 value =arry;	}	} " +
                                "	return value; " +
                                "}}', N'1') ";
                        listSQL.Add(updateSql);
                    }
                    #endregion
                    #region 2019/1/2 - 2019/2/27
                    updateSql = "delete from B_SearchSetting where SearchType = 2;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_SearchSetting', 'SearchType') IS NOT NULL ALTER TABLE B_SearchSetting DROP COLUMN SearchType;";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='doctor' where AppType='医生';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='lis' where AppType='检验之星';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='nurse' where AppType='护士';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='odp' where AppType='查询台';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_SearchSetting set AppType='selfhelp' where AppType='自助打印';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='doctor' where AppType='医生';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='lis' where AppType='检验之星';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='nurse' where AppType='护士';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='odp' where AppType='查询台';";
                    listSQL.Add(updateSql);
                    updateSql = "update B_ColumnsSetting set AppType='selfhelp' where AppType='自助打印';";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_ColumnsSetting', 'Render') IS NOT NULL ALTER TABLE B_ColumnsSetting DROP COLUMN Render;";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=26", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'26', N'页面公共配置', N'allPageType', N'config', N'printCountSetting', N'100', NULL, N'int', N'2019-01-02 10:13:00.000', NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SICKTYPENAME'', name: ''SICKTYPENAME'', width: 130, hidden: true } searchAndNext          { " +
                                "                fieldLabel: '''', " +
                                "                xtype: ''uxCheckTrigger'', " +
                                "                emptyText: ''就诊类型'', " +
                                "                zIndex:1, " +
                                "               width: 150, " +
                                "                labelSeparator: '''', " +
                                "                labelWidth: 55, " +
                                "                labelAlign: ''right'', " +
                                "                itemId: ''SickTypeNo'', " +
                                "               className: ''Shell.class.sicktype.SickType'', " +
                                "               listeners: { " +
                                "                    check: function (p, record) { " +
                                "                        var item = \"\"; " +
                                "                        var clientName = \"\"; " +
                                "                        var me = this.ownerCt.ownerCt; " +
                                "	                    if (record == null) { " +
                                "                            item = me.getItem(\"SICKTYPENAME\"); " +
                                "                            clientName = me.getItem(\"SickTypeNo\"); " +
                                "                            item.setValue(\"\"); " +
                                "                            clientName.setValue(\"\"); " +
                                "                            return;                    }			 " +
                                "                        item = me.getItem(\"SICKTYPENAME\"); " +
                                "                        clientName = me.getItem(\"SickTypeNo\"); " +
                                "                        item.setValue(  record.get(\"CName\") ); " +
                                "                       clientName.setValue(record.get(\"CName\")); " +
                                "                       p.close(); " +
                                "                   }         }' where SID = 2 ";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=10", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'10', N'样本号', N'SampleNo', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=17", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'17', N'条码号', N'SERIALNO', N'search', N'45', N'150', NULL, NULL, N'textfield', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''SERIALNO'', fieldLabel: ''条码号'', labelWidth: 50, width: 150 }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=18", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'18', N'小组类型', N'SECTIONNO', N'search', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N' { type: ''search'', xtype: ''textfield'', mark: ''='',itemId:\"SECTIONNO\", name: ''SECTIONNO'', width: 130,hidden:true } searchAndNext  {   " +
                                    "                xtype: ''uxCheckTrigger'', " +
                                    "                emptyText: ''小组过滤'', " +
                                    "                width: 150, " +
                                    "               labelSeparator: '''', " +
                                    "                labelWidth: 55, " +
                                    "                labelAlign: ''right'', " +
                                    "                itemId: ''secname'', " +
                                    "                className: ''Shell.class.pgroup.section2'', " +
                                    "                listeners: { " +
                                    "                    check: function (p, record) { " +
                                    "                    var me=  this.ownerCt.ownerCt; " +
                                    "                    if (record == null) { " +
                                    "                    me.getItem(\"SECTIONNO\").setValue(\"\"); " +
                                    "                    me.getItem(\"secname\").setValue(\"\"); " +
                                    "                    return; " +
                                    "	                } " +
                                    "	               	me.getItem(\"SECTIONNO\").setValue(record.get(\"SectionNo\")); " +
                                    "	                me.getItem(\"secname\").setValue(record.get(\"CName\")); " +
                                    "	                p.close();         }           }      }') ";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=19", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'19', N'检验类型', N'TESTTYPENO', N'search', N'60', N'130', NULL, NULL, N'combobox', N'=', NULL, N'{ " +
                                    "			    fieldLabel: ''检验类型''," +
                                    "                type: ''other'', " +
                                    "				xtype: ''combobox''," +
                                    "               itemId: ''testType''," +
                                    "               width: 130," +
                                    "				labelWidth:60," +
                                    "                displayField: ''text'', " +
                                    "				valueField: ''value''," +
                                    "				store:Ext.create(''Ext.data.Store'', {" +
                                    "	                    fields: [''text'', ''value'']," +
                                    "	                    data: [" +
                                    "                            { text: ''常规'', value: ''1'' }," +
                                    "                            { text: ''急诊'', value: ''2'' }," +
                                    "                            { text: ''质控'', value: ''3'' }   ]             })," +
                                    "                /*alue: [''1''],*/" +
                                    "                listeners: {" +
                                    "                    change: function (m, v) {" +
                                    "                        var testTypeNo = this.ownerCt.ownerCt.getItem(''TestTypeNo'');" +
                                    "                       testTypeNo.setValue(v);       }                }" +
                                    "            }searchAndNext" +
                                    "		    { type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"TestTypeNo\", name: ''TESTTYPENO'', width: 130,hidden:true }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=20", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'20', N'收费类型', N'CHARGENO', N'search', N'60', N'130', NULL, NULL, N'combobox', N'=', NULL, N'{" +
                                    "			    fieldLabel: ''收费类型''," +
                                    "                type: ''other'', " +
                                    "				xtype: ''combobox''," +
                                    "                itemId: ''charge''," +
                                    "                width: 130," +
                                    "				labelWidth:60," +
                                    "               displayField: ''text'', " +
                                    "				valueField: ''value''," +
                                    "				store:Ext.create(''Ext.data.Store'', {" +
                                    "	                    fields: [''text'', ''value'']," +
                                    "	                    data: [" +
                                    "                            { text: ''国产'', value: ''1'' }," +
                                    "                            { text: ''进口'', value: ''2'' }       " +
                                    "	                    ]" +
                                    "	                })," +
                                    "                listeners: {" +
                                    "                    change: function (m, v) {" +
                                    "                        var cHARGENO = this.ownerCt.ownerCt.getItem(''CHARGENO'');" +
                                    "                        cHARGENO.setValue(v);                        " +
                                    "                    }" +
                                    "                }" +
                                    "           }searchAndNext" +
                                    "		    { type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"CHARGENO\", name: ''CHARGENO'', width: 130,hidden:true }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=21", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'21', N'样本类型', N'SAMPLETYPENO', N'search', N'55', N'150', NULL, NULL, N'uxCheckTrigger', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SAMPLETYPENO'', name: ''SAMPLETYPENO'', width: 130, hidden: true }searchAndNext " +
                                    "           { " +
                                    "                fieldLabel: '''', " +
                                    "                xtype: ''uxCheckTrigger'', " +
                                    "                emptyText: ''样本类型'', " +
                                    "                zIndex:1, " +
                                    "                width: 150, " +
                                    "                labelSeparator: '''', " +
                                    "                labelWidth: 55, " +
                                    "                labelAlign: ''right'', " +
                                    "                itemId: ''SampleTypeName'', " +
                                    "                className: ''Shell.class.sampletype.SampleType'', " +
                                    "                listeners: { " +
                                    "                    check: function (p, record) { " +
                                    "                        var item = \"\"; " +
                                    "                        var clientName = \"\"; " +
                                    "                        var me = this.ownerCt.ownerCt; " +
                                    "	                    if (record == null) { " +
                                    "                            item = me.getItem(\"SAMPLETYPENO\"); " +
                                    "                            clientName = me.getItem(\"SampleTypeName\"); " +
                                    "                            item.setValue(\"\"); " +
                                    "                            clientName.setValue(\"\"); " +
                                    "                            return; " +
                                    "                        } " +
                                    "                        item = me.getItem(\"SAMPLETYPENO\"); " +
                                    "                        clientName = me.getItem(\"SampleTypeName\"); " +
                                    "                        item.setValue(  record.get(\"SampleTypeNo\") ); " +
                                    "                        clientName.setValue(record.get(\"CName\")); " +
                                    "                        p.close(); " +
                                    "                    } " +
                                    "                } " +
                                    "            }') ";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SICKTYPENAME'', name: ''SICKTYPENAME'', width: 130, hidden: true } searchAndNext " +
                                "            { " +
                                "               fieldLabel: '''', " +
                                "                xtype: ''uxCheckTrigger'', " +
                                "                emptyText: ''就诊类型'', " +
                                "                zIndex:1, " +
                                "                width: 150, " +
                                "                labelSeparator: '''', " +
                                "                labelWidth: 55, " +
                                "                labelAlign: ''right'', " +
                                "                itemId: ''SickTypeNo'', " +
                                "                className: ''Shell.class.dictionaries.sicktype.SickType'', " +
                                "                listeners: { " +
                                "                    check: function (p, record) { " +
                                "                        var item = \"\"; " +
                                "                        var clientName = \"\"; " +
                                "                        var me = this.ownerCt.ownerCt; " +
                                "	                    if (record == null) { " +
                                "                            item = me.getItem(\"SICKTYPENAME\"); " +
                                "                            clientName = me.getItem(\"SickTypeNo\"); " +
                                "                            item.setValue(\"\"); " +
                                "                            clientName.setValue(\"\"); " +
                                "                            return; " +
                                "                       }						 " +
                                "                        item = me.getItem(\"SICKTYPENAME\"); " +
                                "                        clientName = me.getItem(\"SickTypeNo\"); " +
                                "                       item.setValue(  record.get(\"CName\") ); " +
                                "                        clientName.setValue(record.get(\"CName\")); " +
                                "                        p.close(); " +
                                "                    } " +
                                "                } " +
                                 "           }' where SID = 2 ";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''in'', itemId: \"DeptNo\", name: ''DeptNo'', width: 130, hidden: true } searchAndNext        {" +
                                "                fieldLabel: ''''," +
                                "                xtype: ''uxCheckTrigger''," +
                                "                emptyText: ''科室过滤''," +
                                "               zIndex:1," +
                                "                width: 150," +
                                "                labelSeparator: ''''," +
                                "                labelWidth: 55," +
                                "                labelAlign: ''right''," +
                                "               itemId: ''ClienteleName''," +
                                "                className: ''Shell.class.dictionaries.dept.CheckGrid''," +
                                "                listeners: {" +
                                "                    check: function (p, record) {" +
                                "                        var item = \"\";" +
                                "                        var clientName = \"\";" +
                                "                        var me = this.ownerCt.ownerCt;" +
                                "						if (record == null) {" +
                                "                            item = me.getItem(\"DeptNo\");" +
                                "                            clientName = me.getItem(\"ClienteleName\");" +
                                "                            item.setValue(\"\");" +
                                "                            clientName.setValue(\"\");" +
                                "                            return;                        }	" +
                                "                        item = me.getItem(\"DeptNo\");" +
                                "                        clientName = me.getItem(\"ClienteleName\");" +
                                "                        item.setValue(\"(\" + record.get(\"DeptNo\") + \")\");" +
                                "                        clientName.setValue(record.get(\"CName\"));" +
                                "                        p.close();" +
                                "                    }" +
                                "                }" +
                                "            }'	where SID = 1";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"checkerNO\", name: ''Checker'', width: 130, hidden: true } searchAndNext	{" +
                                "	                fieldLabel: '' 审核者:''," +
                                "	                xtype: ''uxCheckTrigger''," +
                                "	                emptyText: ''审核者''," +
                                "	                zIndex:1," +
                                "	                width: 200," +
                                "	                labelSeparator: ''''," +
                                "	                labelWidth: 70," +
                                "	                labelAlign: ''right''," +
                                "	                itemId: ''checker''," +
                                "	                className: ''Shell.class.dictionaries.higquery.checker''," +
                                "	                listeners: {" +
                                "	                    check: function (p, record) {" +
                                "						    var item = \"\";" +
                                "	                        var Checker = \"\";" +
                                "							var container = this.ownerCt;" +
                                "							var toolbar = container.ownerCt;" +
                                "							if (record == null) {" +
                                "							    item = toolbar.getItem(\"checkerNO\");" +
                                "	                            Checker = container.getComponent(\"checker\");" +
                                "								item.setValue(\"\");" +
                                "	                            Checker.setValue(\"\");" +
                                "	                            return;" +
                                "	                       }" +
                                "						    item = toolbar.getItem(\"checkerNO\");" +
                                "	                        Checker = container.getComponent(\"checker\");" +
                                "							item.setValue(record.get(\"CName\"));" +
                                "	                        Checker.setValue(record.get(\"CName\"));" +
                                "	                        p.close();" +
                                "	                    }" +
                                "	                }" +
                                "	            }'	where SID = 16";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"districtNameNo\", name: ''DistrictName'', width: 130, hidden: true } searchAndNext  " +
                                "				{" +
                                "	                fieldLabel: '' 病区:''," +
                                "	                xtype: ''uxCheckTrigger''," +
                                "	                emptyText: ''病区''," +
                                "	                zIndex:1," +
                                "	                width: 200," +
                                "	                labelSeparator: ''''," +
                                "	                labelWidth: 70," +
                                "	                labelAlign: ''right''," +
                                "	                itemId: ''districtName''," +
                                "	                className: ''Shell.class.dictionaries.higquery.district''," +
                                "	                listeners: {" +
                                "	                    check: function (p, record) {" +
                                "	                        var item = \"\";" +
                                "	                        var DistrictName = \"\";" +
                                "							var container = this.ownerCt;" +
                                "							var toolbar = container.ownerCt;" +
                                "							if (record == null) {  " +
                                "								item = toolbar.getItem(\"districtNameNo\");	" +
                                "	                            DistrictName = container.getComponent(\"districtName\");" +
                                "								item.setValue(\"\");" +
                                "	                            DistrictName.setValue(\"\");" +
                                "	                            return;" +
                                "	                       }" +
                                "	                        item = toolbar.getItem(\"districtNameNo\");" +
                                "	                        DistrictName = container.getComponent(\"districtName\");" +
                                "							item.setValue(record.get(\"CName\"));" +
                                "	                        DistrictName.setValue(record.get(\"CName\"));" +
                                "	                        p.close();" +
                                "	                    }" +
                                "	                }" +
                                "	            }'	where SID = 10";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"operatorNO\", name: ''Operator'', width: 130, hidden: true } searchAndNext " +
                                "				{ " +
                                "	                fieldLabel: '' 录入者:'', " +
                                "	                xtype: ''uxCheckTrigger'', " +
                                "	                emptyText: ''录入者'', " +
                                "	                zIndex:1, " +
                                "	                width: 200, " +
                                "	                labelSeparator: '''', " +
                                "	                labelWidth: 70, " +
                                "	                labelAlign: ''right'', " +
                                "	                itemId: ''operator'', " +
                                "	                className: ''Shell.class.dictionaries.higquery.operator'', " +
                                "	                listeners: { " +
                                "	                    check: function (p, record) { " +
                                "							var item = \"\"; " +
                                "	                        var Operator = \"\"; " +
                                "							var container = this.ownerCt; " +
                                "							var toolbar = container.ownerCt; " +
                                "							if (record == null) { " +
                                "								item = toolbar.getItem(\"operatorNO\"); " +
                                "	                            Operator = container.getComponent(\"operator\"); " +
                                "								item.setValue(\"\"); " +
                                "	                            Operator.setValue(\"\"); " +
                                "	                            return; " +
                                "	                       } " +
                                "						    item = toolbar.getItem(\"operatorNO\"); " +
                                "	                        Operator = container.getComponent(\"operator\"); " +
                                "							item.setValue(record.get(\"CName\")); " +
                                "	                        Operator.setValue(record.get(\"CName\")); " +
                                "	                        p.close(); " +
                                "	                    } " +
                                "	                } " +
                                "	            }'	where SID = 15 ";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = ' { type: ''search'', xtype: ''textfield'', mark: ''='',itemId:\"SECTIONNO\", name: ''SECTIONNO'', width: 130,hidden:true } searchAndNext " +
                                "            {   xtype: ''uxCheckTrigger'', " +
                                "                emptyText: ''小组过滤'', " +
                                "                width: 150, " +
                                "                labelSeparator: '''', " +
                                "                labelWidth: 55, " +
                                "                labelAlign: ''right'', " +
                                "                itemId: ''secname'', " +
                                "                className: ''Shell.class.dictionaries.pgroup.section2'', " +
                                "                listeners: { " +
                                "                    check: function (p, record) { " +
                                "                    var me=  this.ownerCt.ownerCt; " +
                                "                    if (record == null) { " +
                                "                    me.getItem(\"SECTIONNO\").setValue(\"\"); " +
                                "                    me.getItem(\"secname\").setValue(\"\"); " +
                                "                    return; " +
                                "	                } " +
                                "	               	me.getItem(\"SECTIONNO\").setValue(record.get(\"SectionNo\")); " +
                                "	                me.getItem(\"secname\").setValue(record.get(\"CName\")); " +
                                "	                p.close(); " +
                                "                    } " +
                                "                } " +
                                "            }'	where SID = 18 ";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SAMPLETYPENO'', name: ''SAMPLETYPENO'', width: 130, hidden: true }searchAndNext " +
                                "            { " +
                                "                fieldLabel: '''', " +
                                "                xtype: ''uxCheckTrigger'', " +
                                "                emptyText: ''样本类型'', " +
                                "                zIndex:1, " +
                                "                width: 150, " +
                                "                labelSeparator: '''', " +
                                "                labelWidth: 55, " +
                                "                labelAlign: ''right'', " +
                                "                itemId: ''SampleTypeName'', " +
                                "                className: ''Shell.class.dictionaries.sampletype.SampleType'', " +
                                "                listeners: { " +
                                "                    check: function (p, record) { " +
                                "                        var item = \"\"; " +
                                "                        var clientName = \"\"; " +
                                "                        var me = this.ownerCt.ownerCt; " +
                                "	                    if (record == null) { " +
                                "                            item = me.getItem(\"SAMPLETYPENO\"); " +
                                "                            clientName = me.getItem(\"SampleTypeName\"); " +
                                "                            item.setValue(\"\"); " +
                                "                            clientName.setValue(\"\"); " +
                                "                            return; " +
                                "                        } " +
                                "                        item = me.getItem(\"SAMPLETYPENO\"); " +
                                "                        clientName = me.getItem(\"SampleTypeName\"); " +
                                "                        item.setValue(  record.get(\"SampleTypeNo\") ); " +
                                "                        clientName.setValue(record.get(\"CName\")); " +
                                "                        p.close(); " +
                                "                    } " +
                                "                } " +
                                "           }'	where SID = 21 ";
                    listSQL.Add(updateSql);
                    #endregion
                    #region 2019/3/8-2019/7/1
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=27", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'27', N'页面公共配置', N'allPageType', N'config', N'isCaseSensitive', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=22", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'22', N'检验者', N'Technician',NULL, N'70', N'200', NULL, NULL, N'combobox', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"technicianNO\", name: ''Technician'', width: 130, hidden: true } searchAndNext " +
                                    "				{ " +
                                    "	                fieldLabel: ''检验者:'', " +
                                    "	                xtype: ''uxCheckTrigger'', " +
                                    "	                emptyText: ''检验者'', " +
                                    "	                zIndex:1, " +
                                    "	                width: 200, " +
                                    "	                labelSeparator: '''', " +
                                    "	                labelWidth: 70, " +
                                    "	                labelAlign: ''right'', " +
                                    "	                itemId: ''technician'', " +
                                    "	                className: ''Shell.class.dictionaries.higquery.checker'', " +
                                    "	                listeners: { " +
                                    "	                    check: function (p, record) { " +
                                    "						    var item = \"\"; " +
                                    "	                        var Technician = \"\"; " +
                                    "							var container = this.ownerCt; " +
                                    "							var toolbar = container.ownerCt; " +
                                    "							if (record == null) { " +
                                    "							    item = toolbar.getItem(\"technicianNO\"); " +
                                    "	                            Technician = container.getComponent(\"technician\"); " +
                                    "								item.setValue(\"\"); " +
                                    "	                            Technician.setValue(\"\"); " +
                                    "	                            return; " +
                                    "	                       } " +
                                    "						    item = toolbar.getItem(\"technicianNO\"); " +
                                    "	                        Technician = container.getComponent(\"technician\"); " +
                                    "							item.setValue(record.get(\"CName\")); " +
                                    "	                        Technician.setValue(record.get(\"CName\")); " +
                                    "	                        p.close(); " +
                                    "	                    } " +
                                    "	                } " +
                                    "	            }') ";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=23", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'23', N'出生日期', N'Birthday', N'other', N'60', N'170', NULL, NULL, N'textfield', N'=', NULL, N'{type: ''other'', xtype: ''datefield'',format:''Y-m-d'', mark: ''='', name: ''Birthday'', fieldLabel: ''出生日期'', labelWidth: 60, width: 150 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=24", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'24', N'性别', N'GenderName', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''GenderName'', fieldLabel: ''性别'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=25", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'25', N'病床', N'Bed', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''Bed'', fieldLabel: ''病床'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=26", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'26', N'备注', N'FormMemo', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''FormMemo'', fieldLabel: ''备注'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=27", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'27', N'大备注', N'FormComment', N'search', N'50', N'150', NULL, NULL, N'textfield', N'like ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''FormComment'', fieldLabel: ''大备注'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=28", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'28', N'检验项目', N'ItemName', N'search', N'60', N'150', NULL, NULL, N'textfield', N'like', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''ItemName'', fieldLabel: ''检验项目'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=29", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'29', N'细菌标注类型', N'ZDY4', N'search', N'80', N'180', NULL, NULL, N'textfield', N'like ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''ZDY4'', fieldLabel: ''细菌标注类型'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=30", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'30', N'病房', N'WardNo', N'other', N'70', N'200', NULL, NULL, N'uxCheckTrigger', NULL, NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: \"WardNos\", name: ''WardNo'', width: 130, hidden: true } searchAndNext" +
                                    "				{" +
                                    "	                fieldLabel: ''病房:''," +
                                    "	                xtype: ''uxCheckTrigger''," +
                                    "	                emptyText: ''病房''," +
                                    "	                zIndex:1," +
                                    "	                width: 200," +
                                    "	                labelSeparator: ''''," +
                                    "	                labelWidth: 60," +
                                    "	                labelAlign: ''right''," +
                                    "	                itemId: ''wardName''," +
                                    "	                className: ''Shell.class.dictionaries.higquery.wardNo''," +
                                    "	                listeners: {" +
                                    "	                    check: function (p, record) {" +
                                    "						    var item = \"\";" +
                                    "	                        var wardNames = \"\";" +
                                    "							var container = this.ownerCt;" +
                                    "							var toolbar = container.ownerCt;" +
                                    "							if (record == null) {" +
                                    "							    item = toolbar.getItem(\"WardNos\");" +
                                    "	                            wardNames = container.getComponent(\"wardName\");" +
                                    "								item.setValue(\"\");" +
                                    "	                            wardNames.setValue(\"\");" +
                                    "	                            return;" +
                                    "	                       }" +
                                    "						    item = toolbar.getItem(\"WardNos\");" +
                                    "	                        wardNames = container.getComponent(\"wardName\");" +
                                    "							item.setValue(record.get(\"WardNo\"));" +
                                    "	                        wardNames.setValue(record.get(\"CName\"));" +
                                    "	                        p.close();" +
                                    "	                    }" +
                                    "	                }" +
                                    "	               }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=32", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'32', N'年龄', N'Age', N'other', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''other'', xtype: ''numberfield'', mark: ''='', name: ''Age'', fieldLabel: ''年龄'', labelWidth: 35, width: 110 }');";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update [dbo].[B_ColumnsUnit] set Render = '{renderer:function (v, meta, record) { var imgName = (v && v >= me.maxPrintTimes) ? \"unprint\" : \"print\", tootip = \"已经打印 <b style = ''color:red; '' > \" + v + \" </b> 次\",value = v ? \" <b> \" + v + \" </b> \" : \"\";meta.tdAttr = ''data-qtip=\"'' + tootip + ''\"'';var result = '''';if(v > 0){ meta.style=\"background-color:red\"; result = v;}else{result = v;}return result;}}' where COlID = 7";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit]  set JsCode='{type: ''other'', " +
                                "xtype: ''datefield''," +
                                "format:''Y-m-d''," +
                                "itemId: ''selectBirthday'', " +
                                "name: ''selectBirthday'', " +
                                "fieldLabel: ''出生日期'', " +
                                "labelWidth: 60, " +
                                "width: 150," +
                                "listeners: {" +
                                "                    change: function (m, v) {" +
                                "                        var Birthday = this.ownerCt.ownerCt.getItem(''Birthday'');" +
                                "						var selectBirthday = this.ownerCt.ownerCt.getItem(''selectBirthday'');" +
                                "						var m = parseInt(v.getMonth())+1;" +
                                "						if(m<=9){" +
                                "							m = \"0\"+m;" +
                                "						}" +
                                "						var d = v.getDate();" +
                                "						if(d<=9){" +
                                "							d = \"0\"+d;" +
                                "						}" +
                                "                        Birthday.setValue(v.getFullYear()+\" - \"+m+\" - \"+d);" +
                                "                    }" +
                                "                } }" +
                                "searchAndNext" +
                                "{ type: ''search'',mark: ''='', xtype: ''textfield'',format:''Y-m-d'', itemId: ''Birthday'', name: ''Birthday'', labelWidth: 0, width: 190,hidden: true }" +
                                "' where SID = '23'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_SearchUnit]  set JsCode='{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''Age'', fieldLabel: ''年龄'', labelWidth: 35, width: 110 }' where SID = '32'";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=34", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'34', N'卡号', N'ZDY3', N'search', N'50', N'210', NULL, NULL, N'textfield', N'=', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='',itemId: ''ZDY3'', name: ''ZDY3'', fieldLabel: ''卡号'', labelWidth: 50, width: 210,selectOnFocus:true }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=11", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'11', N'医嘱项目名称', N'ZDY3', NULL, N'100', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where ID=28", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'28', N'页面公共配置', N'allPageType', N'config', N'listWidth', N'550', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where ID=29", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'29', N'页面公共配置', N'allPageType', N'config', N'isviewportHeader', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where ID=72", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'72', N'查询打印页面配置', N'自助打印', N'config', N'notPrintSectionNo', N'', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update B_ColumnsUnit set ColumnName = 'SAMPLENO' where ColID = 10";
                    listSQL.Add(updateSql);

                    #endregion
                    #region 2019/8/13-2020/3/11
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=73", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'73', N'查询打印页面配置', N'自助打印', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=30", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'30', N'页面公共配置', N'allPageType', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=31", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'31', N'页面公共配置', N'allPageType', N'config', N'IsbTempReport', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=32", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'32', N'页面公共配置', N'allPageType', N'config', N'IsQueryRequest', N'false', NULL, N'bool', NULL, NULL, NULL, NULL);";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=12", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'12', N'样本类型', N'SampletypeName', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=13", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'13', N'科室', N'DeptName', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=74", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'74', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameTop', N'13', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=75", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'75', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameLeft', N'26', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=76", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'76', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameFontSize', N'26', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=77", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'77', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameColor', N'#0e121a', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=78", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'78', N'查询打印页面配置', N'自助打印', N'config', N'printnumTop', N'14', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=79", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'79', N'查询打印页面配置', N'自助打印', N'config', N'printnumLeft', N'34', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=80", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'80', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameFontSize', N'26', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=81", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'81', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameColor', N'#0e121a', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=82", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'82', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextTop', N'16', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=83", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'83', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextLeft', N'16', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=84", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'84', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextFontSize', N'35', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=85", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'85', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextColor', N'black', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=86", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'86', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeTop', N'13', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=87", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'87', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeLeft', N'6', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=88", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'88', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeFontSize', N'26', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=89", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'89', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeColor', N'#0e121a', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=90", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'90', N'查询打印页面配置', N'自助打印', N'config', N'tabgridTop', N'66', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=91", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'91', N'查询打印页面配置', N'自助打印', N'config', N'tabgridLeft', N'8', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=32", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'92', N'查询打印页面配置', N'自助打印', N'config', N'closeprintnumTop', N'13', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=93", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'93', N'查询打印页面配置', N'自助打印', N'config', N'closeprintnumLeft', N'41', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=94", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'94', N'查询打印页面配置', N'自助打印', N'config', N'caridTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=95", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'95', N'查询打印页面配置', N'自助打印', N'config', N'caridLeft', N'15', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=96", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'96', N'查询打印页面配置', N'自助打印', N'config', N'caridWidth', N'260', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }

                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=97", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'97', N'查询打印页面配置', N'自助打印', N'config', N'openJianpanTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=98", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'98', N'查询打印页面配置', N'自助打印', N'config', N'openJianpanLeft', N'53', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=99", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'99', N'查询打印页面配置', N'自助打印', N'config', N'closeJianpanTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=100", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'100', N'查询打印页面配置', N'自助打印', N'config', N'closeJianpanLeft', N'53', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=101", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'101', N'查询打印页面配置', N'自助打印', N'config', N'closeTop', N'69', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=102", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'102', N'查询打印页面配置', N'自助打印', N'config', N'closeLeft', N'15', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=103", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'103', N'查询打印页面配置', N'自助打印', N'config', N'closeFontSize', N'16', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=104", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'104', N'查询打印页面配置', N'自助打印', N'config', N'closeColor', N'black', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=105", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'105', N'查询打印页面配置', N'自助打印', N'config', N'reportviewTop', N'70', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=106", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'106', N'查询打印页面配置', N'自助打印', N'config', N'reportviewLeft', N'28', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=107", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'107', N'查询打印页面配置', N'自助打印', N'config', N'reportviewFontSize', N'14', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=108", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'108', N'查询打印页面配置', N'自助打印', N'config', N'reportviewColor', N'black', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=109", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'109', N'查询打印页面配置', N'自助打印', N'config', N'panelTop', N'36', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=110", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'110', N'查询打印页面配置', N'自助打印', N'config', N'panelLeft', N'15', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=111", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'111', N'查询打印页面配置', N'自助打印', N'config', N'panelWidth', N'600', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=112", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'112', N'查询打印页面配置', N'自助打印', N'config', N'panelHeight', N'242', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=33", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'33', N'页面公共配置', N'allPageType', N'config', N'IsHistory', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=34", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'34', N'页面公共配置', N'allPageType', N'config', N'IsBackups', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=113", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'113', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsTop', N'66', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=114", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "  INSERT INTO [dbo].[B_Parameter]  VALUES (N'114', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsLeft', N'8', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=115", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'115', N'查询打印页面配置', N'自助打印', N'config', N'noparitemname', N'', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '34' where ParaNo = 'tabgridTop' and SName = '自助打印'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '40' where ParaNo = 'tabgridLeft' and SName = '自助打印'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '34' where ParaNo = 'tabgridTop' and SName = 'selfhelp'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '40' where ParaNo = 'tabgridLeft' and SName = 'selfhelp'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '46' where ParaNo = 'tabgridLeft' and SName = '自助打印'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '46' where ParaNo = 'tabgridLeft' and SName = 'selfhelp'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '48' where ParaNo = 'closeJianpanLeft' and SName = '自助打印'";
                    listSQL.Add(updateSql);
                    updateSql = "update [dbo].[B_Parameter] set ParaValue = '48' where ParaNo = 'closeJianpanLeft' and SName = 'selfhelp'";
                    listSQL.Add(updateSql);
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=116", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'116', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewTop', N'50', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=117", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'117', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewLeft', N'6', NULL, N'int', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=118", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'118', N'查询打印页面配置', N'自助打印', N'config', N'defaultCondition', N'', NULL, N'string', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=14", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'14', N'重申标记', N'BRevised', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_ColumnsUnit where ColID=15", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'15', N'报告类型', N'ReportType', NULL, N'60', NULL, NULL, NULL, N'1')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=35", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'35', N'身份证号', N'ZDY13', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY13'', fieldLabel: ''身份证号'', labelWidth: 35, width: 110 }')";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=119", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'119', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=120", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'120', N'查询打印页面配置', N'自助打印', N'config', N'printnumIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=121", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'121', N'查询打印页面配置', N'自助打印', N'config', N'closeprintnumIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                    }
                    ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=122", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'122', N'查询打印页面配置', N'自助打印', N'config', N'tabgridHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                        listSQL.Add(updateSql);
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=123", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'123', N'查询打印页面配置', N'自助打印', N'config', N'tabgridHangTouFontSize', N'20', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=124", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'124', N'查询打印页面配置', N'自助打印', N'config', N'tabgridNeiRongFontSize', N'16', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=125", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'125', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=126", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'126', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=127", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'127', N'查询打印页面配置', N'自助打印', N'config', N'caridHeight', N'30', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=128", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'128', N'查询打印页面配置', N'自助打印', N'config', N'caridFontSize', N'20', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=129", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'129', N'查询打印页面配置', N'自助打印', N'config', N'caridIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=130", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'130', N'查询打印页面配置', N'自助打印', N'config', N'openJianpanIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=131", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'131', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=132", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'132', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonLeft', N'65', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=133", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'133', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonWidth', N'100', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=134", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'134', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonHeight', N'36', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=135", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'135', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonIsHidden', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=136", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'136', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsFontSize', N'20', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=137", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'137', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=138", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'138', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=139", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'139', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonLeft', N'80', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=140", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'140', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonWidth', N'100', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=141", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'141', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonHeight', N'36', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=142", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'142', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=143", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'143', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewFontSize', N'30', NULL, N'int', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_Parameter where Id=144", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'144', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=36", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'36', N'拼音码', N'PinYinMa', N'search', N'60', N'180', NULL, NULL, N'textfield', N'like ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''PinYinMa'', fieldLabel: ''姓名拼音'', labelWidth: 60, width: 180 }')";
                            listSQL.Add(updateSql);
                        }
                        ds = SqlServerHelper.QuerySql(" select * from B_SearchUnit where SID=37", ADOConnectStr);
                        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                        {
                            updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'37', N'住院号', N'ZDY5', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY5'', fieldLabel: ''住院号'', labelWidth: 60, width: 180 }')";
                            listSQL.Add(updateSql);
                        }
                        updateSql = "IF COL_LENGTH('SectionPrint', 'IsRFGraphdataPDf') IS  NULL ALTER TABLE [dbo].[SectionPrint] ADD IsRFGraphdataPDf bit;";
                        listSQL.Add(updateSql);
                    }
                    #endregion

                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.2");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.2 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.3
            if (IsUpdateDataBase(oldVersion, "1.0.0.3"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {                    
                    updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'35', N'页面公共配置', N'allPageType', N'config', N'IsSampleState', N'false', NULL, N'bool', N'2020-05-19 14:20:00.000', NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('SectionPrint', 'clientno') IS NULL alter table SectionPrint add clientno int";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('SectionPrint', 'IsRFGraphdataPDf') IS NULL alter table SectionPrint add IsRFGraphdataPDf bit";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'17', N'是否二审', N'Sender2', NULL, N'100', NULL, NULL, '{renderer:function (v, meta, record) { " +
                                "	                var result = ''''; " +
                                "	                if(v == null || v == \"\"){ " +
                                "                       meta.style=\"background-color:red\"; " +
                                "	                    result = \"否\"; " +
                                "	                }else{ " +
                                "						result = \"是\"; " +
                                "                    } " +
                                "	                return result; " +
                                "	            }}', N'1') ";
                    listSQL.Add(updateSql);
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter") 
                {
                    updateSql = "INSERT INTO [dbo].[B_Parameter]  VALUES (N'35', N'页面公共配置', N'allPageType', N'config', N'IsSampleState', N'false', NULL, N'bool', N'2020-05-19 14:20:00.000', NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('SectionPrint', 'clientno') IS NULL alter table SectionPrint add clientno int";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'17', N'是否二审', N'Sender2', NULL, N'100', NULL, NULL, '{renderer:function (v, meta, record) { " +
                               "	                var result = ''''; " +
                               "	                if(v == null || v == \"\"){ " +
                               "                       meta.style=\"background-color:red\"; " +
                               "	                    result = \"否\"; " +
                               "	                }else{ " +
                               "						result = \"是\"; " +
                               "                    } " +
                               "	                return result; " +
                               "	            }}', N'1') ";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.3");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.3 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.4
            if (IsUpdateDataBase(oldVersion, "1.0.0.4"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = "IF COL_LENGTH('B_Parameter', 'Id') IS NOT NULL EXEC sp_rename 'B_Parameter.[Id]', 'ParameterID' , 'COLUMN';";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'LabID') IS NULL alter table B_Parameter add LabID bigint;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'LabID') IS NOT NULL update B_Parameter set LabID = 0;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'LabID') IS NOT NULL ALTER TABLE B_Parameter ALTER COLUMN LabID bigint not null;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'NodeID') IS NULL alter table B_Parameter add NodeID bigint;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'GroupNo') IS NULL alter table B_Parameter add GroupNo bigint;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'DataAddTime') IS NULL alter table B_Parameter add DataAddTime datetime;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'DataTimeStamp') IS NULL alter table B_Parameter add DataTimeStamp timestamp;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'DispOrder') IS NULL alter table B_Parameter add DispOrder int;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'PDictId') IS NULL alter table B_Parameter add PDictId bigint;";
                    listSQL.Add(updateSql);
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = "IF COL_LENGTH('B_Parameter', 'Id') IS NOT NULL EXEC sp_rename 'B_Parameter.[Id]', 'ParameterID' , 'COLUMN';";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'LabID') IS NULL alter table B_Parameter add LabID bigint;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'LabID') IS NOT NULL update B_Parameter set LabID = 0;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'LabID') IS NOT NULL ALTER TABLE B_Parameter ALTER COLUMN LabID bigint not null;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'NodeID') IS NULL alter table B_Parameter add NodeID bigint;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'GroupNo') IS NULL alter table B_Parameter add GroupNo bigint;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'DataAddTime') IS NULL alter table B_Parameter add DataAddTime datetime;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'DataTimeStamp') IS NULL alter table B_Parameter add DataTimeStamp timestamp;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'DispOrder') IS NULL alter table B_Parameter add DispOrder int;";
                    listSQL.Add(updateSql);
                    updateSql = "IF COL_LENGTH('B_Parameter', 'PDictId') IS NULL alter table B_Parameter add PDictId bigint;";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0+//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.4");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.4 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.5
            if (IsUpdateDataBase(oldVersion, "1.0.0.5"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {                    
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'18', N'申请时间', N'OrderTime', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {	//申请时间	var Ctime = record.get(\"OrderTime\");		var value = '''';		if(Ctime !=null){  var arry = Ctime.substring(Ctime.length-9);	  if(arry!=null && arry.length >1){		 value =arry;		}}	return value;}}', N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'19', N'年龄', N'AgeDesc', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'20', N'性别', N'GenderName', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'21', N'备注', N'FormMemo', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'22', N'状态', N'State', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'23', N'申请医生', N'Doctor', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = " ALTER VIEW [dbo].[ReportFormQueryDataSource] " +
                                " AS " +
                                " SELECT  (SELECT  ParaValue " +
                                "                    FROM       dbo.SysPara " +
                                "                    WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, rf.SectionNo)))  " +
                                "                    + '_' + LTRIM(RTRIM(CONVERT(char, rf.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, rf.SampleNo)))  " +
                                "                    + '_' + LTRIM(RTRIM(CONVERT(varchar(20), rf.ReceiveDate, 20))) AS ReportFormID, CONVERT(varchar(10), rf.ReceiveDate,  " +
                                "                    21) + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), rf.TestTypeNo) + ';' + CONVERT(varchar(30),  " +
                                "                    rf.SampleNo) AS FormNo, gt.CName AS Gender, gt.ShortCode AS GenderEName, st.CName AS SickType,  " +
                                "                    st.ShortCode AS SickEname, spt.CName AS sampletype, spt.ShortCode AS sampletypeename, ft.CName AS folk,  " +
                                "                    ft.ShortCode AS folkename, dt.CName AS Dept, dt.ShortCode AS Deptename, dct.CName AS district,  " +
                                "                    dct.ShortCode AS districtename, wt.CName AS ward, au.CName AS AgeUnit, au.ShortCode AS AgeUnitename,  " +
                                "                    tt.CName AS TestType, tt.ShortCode AS TestTypeename, dgs.CName AS diag, clt.CNAME AS client, clt.ADDRESS,  " +
                                "                    pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate,  " +
                                "                    rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName, rf.GenderNo, rf.Birthday,  " +
                                "                    rf.Age, rf.AgeUnitNo, rf.FolkNo, rf.DistrictNo, rf.WardNo, rf.Bed, rf.DeptNo, rf.Doctor, rf.ChargeNo, rf.Charge, rf.Collecter,  " +
                                "                    rf.CollectDate, rf.CollectTime, rf.FormMemo, rf.TestDate, rf.TestTime, rf.Technician, rf.Operator, rf.OperDate, rf.OperTime,  " +
                                "                    rf.CheckDate, rf.CheckTime, rf.Checker, rf.SerialNo, rf.RequestSource, rf.DiagNo, rf.PrintTimes, rf.FormComment,  " +
                                "                    rf.Artificerorder, rf.Sickorder, rf.SickTypeNo, rf.Chargeflag, rf.TestDest, rf.SLable, rf.zdy1, rf.zdy2, rf.zdy3, rf.zdy4, rf.zdy5,  " +
                                "                    rf.inceptdate, rf.incepttime, rf.onlinedate, rf.incepter, rf.onlinetime, rf.bmanno, rf.isReceive, rf.ReceiveMan, rf.ReceiveTime,  " +
                                "                    rf.concessionNum, rf.resultstatus, rf.testaim, rf.resultprinttimes, rf.Sender2, rf.SenderTime2, rf.paritemname, rf.clientprint,  " +
                                "                    rf.resultsend, rf.reportsend, rf.CountNodesFormSource, rf.commsendflag, rf.PrintDateTime, rf.PrintOper, rf.abnormityflag,  " +
                                "                    rf.HISDateTime, rf.allowprint, rf.UrgentState, rf.RemoveFeesReason, rf.ZDY6, rf.ZDY7, rf.ZDY8, rf.ZDY9, rf.phoneCode,  " +
                                "                    rf.IsNode, rf.PhoneNodeCount, rf.AutoNodeCount, rf.FormDesc, rf.EquipCommMemo, rf.ESampleNo, rf.EPosition,  " +
                                "                    rf.ISUsePG, rf.OperMemo, rf.FromQCL, rf.ESend, rf.IsDel, rf.EModule, rf.IsRedo, rf.EAchivPosition, rf.FenzhuDatetime,  " +
                                "                    rf.EQCLPosition, rf.EquipResult, rf.EResultToHIS, rf.NurseSender, rf.NurseSendTime, rf.NurseSendCarrier,  " +
                                "                    rf.bGetAllResults, rf.bSendWjz, rf.CollecterID, rf.PrintDateTime1, rf.PrintOper1, rf.HisDoctorId, rf.HisDoctorPhoneCode,  " +
                                "                    rf.LisDoctorNo, rf.LischeckerNo, rf.LastOperator, rf.LastOperDatetime, rf.I_Reportstatus, rf.AutoSended, rf.PatState,  " +
                                "                    rf.EquipCommSend, rf.EquipSampleNo, pg.SectionType, CASE WHEN CONVERT(varchar(50), rf.clientno) IS NULL  " +
                                "                    THEN '425' WHEN CONVERT(varchar(50), rf.clientno) = '' THEN '425' ELSE CONVERT(varchar(50), rf.clientno)  " +
                                "                    END AS clientno, dbo.GetBarCodeItemList(rf.SerialNo, '0') AS ItemName, rf.PageCount, rf.PageName,  " +
                                "                    CASE WHEN ((rf.SectionNo >= 2 AND rf.SectionNo <= 14) OR " +
                                "                    rf.SectionNo = 41 OR " +
                                "                    rf.SectionNo = 43) THEN rf.CheckDate ELSE rf.SenderTime2 END AS ZDY10, st.CName AS SICKTYPENAME,  " +
                                "                    tt.CName AS TestTypeName, spt.CName AS SampletypeName, '' AS SecretType, '' AS InpatientNo, '' AS PatCardNo,  " +
                                "                    au.CName AS AgeUnitName, dct.CName AS DistrictName, wt.CName AS WardName, dt.CName AS DeptName,  " +
                                "                    dgs.CName AS DiagName, '' AS LabID, '' AS MainTesterId, '' AS ActiveFlag, rf.zdy15, rf.FormComment2,  " +
                                "                    dt.code_1 AS DeptCode1, NULL AS AffirmTime, NULL AS arrivetime, do.code_1 AS DoctorCode1, NULL AS OrderTime,  " +
                                "                    NULL AS AgeDesc, NULL AS State " +
                                " FROM      dbo.ReportForm AS rf LEFT OUTER JOIN " +
                                "                    dbo.GenderType AS gt ON rf.GenderNo = gt.GenderNo LEFT OUTER JOIN " +
                                "                    dbo.SickType AS st ON st.SickTypeNo = rf.SickTypeNo LEFT OUTER JOIN " +
                                "                    dbo.SampleType AS spt ON spt.SampleTypeNo = rf.SampleTypeNo LEFT OUTER JOIN " +
                                "                    dbo.FolkType AS ft ON ft.FolkNo = rf.FolkNo LEFT OUTER JOIN " +
                                "                    dbo.Department AS dt ON dt.DeptNo = rf.DeptNo LEFT OUTER JOIN " +
                                "                    dbo.District AS dct ON dct.DistrictNo = rf.DistrictNo LEFT OUTER JOIN " +
                                "                    dbo.WardType AS wt ON wt.WardNo = rf.WardNo LEFT OUTER JOIN " +
                                "                    dbo.AgeUnit AS au ON au.AgeUnitNo = rf.AgeUnitNo LEFT OUTER JOIN " +
                                "                    dbo.TestType AS tt ON tt.TestTypeNo = rf.TestTypeNo LEFT OUTER JOIN " +
                                "                    dbo.Diagnosis AS dgs ON dgs.DiagNo = rf.DiagNo LEFT OUTER JOIN " +
                                "                    dbo.CLIENTELE AS clt ON clt.ClIENTNO = rf.clientno LEFT OUTER JOIN " +
                                "                    dbo.PGroup AS pg ON pg.SectionNo = rf.SectionNo LEFT OUTER JOIN " +
                                "                    dbo.Doctor AS do ON do.CName = rf.Doctor ";
                    listSQL.Add(updateSql);
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'18', N'申请时间', N'OrderTime', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {	//申请时间	var Ctime = record.get(\"OrderTime\");		var value = '''';		if(Ctime !=null){  var arry = Ctime.substring(Ctime.length-9);	  if(arry!=null && arry.length >1){		 value =arry;		}}	return value;}}', N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'19', N'年龄', N'AgeDesc', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'20', N'性别', N'GenderName', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'21', N'备注', N'FormMemo', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'22', N'状态', N'State', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'23', N'申请医生', N'Doctor', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = " ALTER VIEW [dbo].[ReportFormQueryDataSource] " +
                                " AS " +
                                " SELECT  ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, SectionName, TestTypeName, SampleTypeNo,  " +
                                "                    SampletypeName, SecretType, PatNo, CName, InpatientNo, PatCardNo, GenderNo, GenderName, Age, AgeUnitNo,  " +
                                "                    AgeUnitName, Birthday, DistrictNo, DistrictName, WardNo, WardName, Bed, DeptNo, DeptName, Doctor, SerialNo, " +
                                "                    ParitemName, Collecter, CollectDate, CollectTime, Incepter, InceptDate, InceptTime, Technician, TestDate, TestTime,  " +
                                "                    Operator, OperDate, OperTime, Checker, CheckDate, CheckTime, FormComment, FormMemo, SickTypeNo, SickTypeName,  " +
                                "                    DiagNo, DiagName, ClientNo, ClientName, Sender2, PrintTimes, ClientPrint, PrintOper, PrintDateTime, PrintOper1,  " +
                                "                    PrintDateTime1, resultsend, reportsend, PageName, PageCount, ZDY1, ZDY2, ZDY3, ZDY4, ZDY5, ZDY6, ZDY7, ZDY8,  " +
                                "                    ReportFormID AS formno, SecretType AS SectionType, LabID, DataAddTime, DataUpdateTime, DataMigrationTime,  " +
                                "                    DataTimeStamp, MainTesterId, PatientID, ExaminerId, CollectPart, ReportPublicationID AS ReportFormID, ActiveFlag,  " +
                                "                    DoctorItemName AS ItemName, '' AS ZDY15, '' AS PinYinMa, NULL AS AffirmTime, NULL AS arrivetime, NULL  " +
                                "                    AS NurseSender, NULL AS NurseSendTime, NULL AS NurseSendCarrier, NULL AS ReceiveTime, NULL AS OrderTime,  " +
                                "                    NULL AS AgeDesc, NULL AS State " +
                                " FROM      dbo.ReportFormFull " +
                                " WHERE   (ActiveFlag = 1) ";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.5");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.5 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.6
            if (IsUpdateDataBase(oldVersion, "1.0.0.6"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'38', N'卡号', N'ZDY1', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY1'', fieldLabel: ''卡号'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'39', N'登记号', N'ZDY8', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY8'', fieldLabel: ''登记号'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'24', N'条码号', N'Serialno', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'25', N'申请单号', N'ApplicationNo', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'38', N'卡号', N'ZDY1', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY1'', fieldLabel: ''卡号'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'39', N'登记号', N'ZDY8', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY8'', fieldLabel: ''登记号'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'24', N'条码号', N'Serialno', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'25', N'申请单号', N'ApplicationNo', NULL, N'100', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.6");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.6 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.7
            if (IsUpdateDataBase(oldVersion, "1.0.0.7"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'40', N'单位', N'ZDY1', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY1'', fieldLabel: ''单位'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'26', N'核收时间', N'RECEIVETIME', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {var Ctime = record.get(\"RECEIVETIME\");var value = '''';if (Ctime != null) {	var arry = Ctime.split('' '');	if (arry != null && arry.length > 1) {		value = arry[1];	}}return value;}}', N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'27', N'检验日期', N'TestDate', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {	var Cdate = record.get(\"TestDate\");		var value = '''';	if(Cdate !=null){ 	var arry = Cdate.split('' ''); if(arry!=null && arry.length >1){	value =arry[0];	}	}	return value;}}', N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'28', N'检验时间', N'TestTime', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {		var Ctime = record.get(\"TestTime\");	var value = '''';	if(Ctime !=null){  var arry = Ctime.split('' '');	  if(arry!=null && arry.length >1){	 value =arry[1];}	}	return value;}}', N'1');";
                    listSQL.Add(updateSql);

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'40', N'单位', N'ZDY1', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY1'', fieldLabel: ''单位'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'26', N'核收时间', N'RECEIVETIME', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {var Ctime = record.get(\"RECEIVETIME\");var value = '''';if (Ctime != null) {	var arry = Ctime.split('' '');	if (arry != null && arry.length > 1) {		value = arry[1];	}}return value;}}', N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'27', N'检验日期', N'TestDate', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {	var Cdate = record.get(\"TestDate\");		var value = '''';	if(Cdate !=null){ 	var arry = Cdate.split('' ''); if(arry!=null && arry.length >1){	value =arry[0];	}	}	return value;}}', N'1');";
                    listSQL.Add(updateSql);
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'28', N'检验时间', N'TestTime', NULL, N'110', NULL, NULL, N'{renderer:function (v, meta, record, index) {		var Ctime = record.get(\"TestTime\");	var value = '''';	if(Ctime !=null){  var arry = Ctime.split('' '');	  if(arry!=null && arry.length >1){	 value =arry[1];}	}	return value;}}', N'1');";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.7");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.7 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.8
            if (IsUpdateDataBase(oldVersion, "1.0.0.8"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'36', N'页面公共配置', N'allPageType', N'config', N'MaxDownLoadNum', N'100', NULL, N'int', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'36', N'页面公共配置', N'allPageType', N'config', N'MaxDownLoadNum', N'100', NULL, N'int', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.8");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.8 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.9
            if (IsUpdateDataBase(oldVersion, "1.0.0.9"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'37', N'页面公共配置', N'allPageType', N'config', N'HistoryCompareDefaultDates ', N'90', NULL, N'int', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'37', N'页面公共配置', N'allPageType', N'config', N'HistoryCompareDefaultDates ', N'90', NULL, N'int', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.9");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.9 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.10
            if (IsUpdateDataBase(oldVersion, "1.0.0.10"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'38', N'页面公共配置', N'allPageType', N'config', N'HistoryCompareDateField ', N'CHECKDATE', NULL, N'string', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'38', N'页面公共配置', N'allPageType', N'config', N'HistoryCompareDateField ', N'CHECKDATE', NULL, N'string', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.10");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.10 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.11
            if (IsUpdateDataBase(oldVersion, "1.0.0.11"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'39', N'页面公共配置', N'allPageType', N'config', N'HistoryDefaultCollapsed', N'true', NULL, N'bool', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'39', N'页面公共配置', N'allPageType', N'config', N'HistoryDefaultCollapsed', N'true', NULL, N'bool', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.11");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.11 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.12
            if (IsUpdateDataBase(oldVersion, "1.0.0.12"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'40', N'页面公共配置', N'allPageType', N'config', N'sortFields', N'disporder,ASC', NULL, N'string', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'40', N'页面公共配置', N'allPageType', N'config', N'sortFields', N'disporder,ASC', NULL, N'string', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.12");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.12 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.13
            if (IsUpdateDataBase(oldVersion, "1.0.0.13"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'41', N'页面公共配置', N'allPageType', N'config', N'queryDateRange', N'180', NULL, N'int', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'41', N'页面公共配置', N'allPageType', N'config', N'queryDateRange', N'180', NULL, N'int', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.13");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.13 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.14
            if (IsUpdateDataBase(oldVersion, "1.0.0.14"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'42', N'页面公共配置', N'allPageType', N'config', N'NewWindowLoadIframeToPrint', N'false', NULL, N'bool', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'42', N'页面公共配置', N'allPageType', N'config', N'NewWindowLoadIframeToPrint', N'false', NULL, N'bool', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.14");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.14 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.15
            if (IsUpdateDataBase(oldVersion, "1.0.0.15"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    if (!CheckDataObjectIsExists("Proc_ReportItemQueryDataSource", "p"))
                    {
                        updateSql = "CREATE PROCEDURE [dbo].[Proc_ReportItemQueryDataSource] \r\n" +
                                    "@ReceiveDate varchar(20), --核收日期 \r\n" +
                                    "@SectionNo varchar(50), --小组编号 \r\n" +
                                    "@TestTypeNo varchar(50), --测试类型编号 \r\n" +
                                    "@SampleNo varchar(50), --样本编号 \r\n" +
                                    "@SortFields varchar(max) --排序字段 \r\n" +
                                    "--ReportItem ri" +
                                    "--testitem     t1 \r\n" +
                                    "--testitem     t2 \r\n" +
                                    " AS \r\n" +
                                    " BEGIN \r\n" +
                                    " DECLARE @SQL varchar(max) \r\n" +
                                    "set @SQL='SELECT CONVERT(varchar(10), ri.ReceiveDate, 21) +'';''+  CONVERT(varchar(30), ri.SectionNo) +'';''+  CONVERT(varchar(30),  ri.TestTypeNo)  +''; '' + CONVERT(varchar(30), ri.SampleNo) AS FormNo," +
                                    "(SELECT   ParaValue " +
                                    "FROM      dbo.SysPara " +
                                    "WHERE(ParaNo = ''1000000'') AND(NodeName = ''ALLNODE'')) + +''__'' + LTRIM(RTRIM(CONVERT(char," +
                                    "  ri.SectionNo))) +''_'' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + ''_'' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo)))" +
                                    " + ''_'' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportItemID, ''__'' + LTRIM(RTRIM(CONVERT(char, " +
                                    " ri.SectionNo))) + ''_'' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + ''_'' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo)))" +
                                    " + ''_'' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportFormID, dbo.getitemrow(ri.ReceiveDate, " +
                                    " ri.SectionNo, ri.TestTypeNo, ri.SampleNo, t1.DispOrder) AS rowId, ri.ReceiveDate, ri.SectionNo, ri.TestTypeNo, " +
                                    "     ri.SampleNo, ri.ParItemNo, ri.ItemNo, ri.OriginalValue, ri.ReportValue, ri.OriginalDesc, ri.ReportDesc, ri.StatusNo," +
                                    "    ri.RefRange, ri.EquipNo, ri.Modified, ri.ItemDate, ri.ItemTime, ri.IsMatch, " +
                                    "  CASE WHEN((LTRIM(RTRIM(ISNULL(ri.ReportDesc, ''''))) = ''阳性'') AND((t1.CName NOT LIKE '' % RH % '') AND" +
                                    "   (t1.CName NOT LIKE '' % 血型 % ''))) THEN ''H'' ELSE ResultStatus END AS ResultStatus, ri.HisValue, ri.HisComp, " +
                                    " ri.isReceive, ri.SerialNoParent, ri.zdy1, ri.zdy2, ri.zdy3, ri.zdy4, ri.zdy5, ri.CountNodesItemSource, ri.Unit AS ItemUnit, " +
                                    " ri.PlateNo, ri.PositionNo, ri.EquipCommMemo, ri.EquipCommSend, ri.EValueState, ri.EModule, ri.EPosition, ri.ESend, " +
                                    " ri.IsRedo, ri.IsDel, ri.HisReceiveDate, ri.FromRedoNo, ri.I_Reportstatus, ri.ItemTestMemo, ri.ItemDealWith, ri.I_EResult, " +
                                    " CASE WHEN isnull(t1.Prec, 0) = 0 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 0))), '''') " +
                                    " + ISNULL(ri.ReportDesc, '''') WHEN isnull(t1.Prec, 1) = 1 THEN ISNULL(CONVERT(varchar," +
                                    " CAST(ReportValue AS decimal(18, 1))), '''') +ISNULL(ri.ReportDesc, '''') WHEN isnull(t1.Prec, 0) " +
                                    "= 2 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '''') +ISNULL(ri.ReportDesc, '''')" +
                                    "WHEN isnull(t1.Prec, 0) = 3 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 3))), '''') " +
                                    " + ISNULL(ri.ReportDesc, '''') ELSE ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '''') " +
                                    " + ISNULL(ri.ReportDesc, '''') END AS ItemValue, t1.DispOrder, t1.CName AS ItemCname, t1.EName AS itemename, " +
                                    " t2.CName AS paritemname, t1.Secretgrade, t1.ShortName, t1.ShortCode, t1.OrderNo, t1.StandardCode, t1.Cuegrade, " +
                                    " t1.DiagMethod, '''' AS EquipName, t1.Prec, t1.ItemDesc, t1.Visible, t1.EName AS TESTITEMSNAME, " +
                                    " t1.CName AS TESTITEMNAME " +
                                    "FROM    dbo.ReportItem AS ri LEFT OUTER JOIN " +
                                    "dbo.TestItem AS t1 ON t1.ItemNo = ri.ItemNo LEFT OUTER JOIN " +
                                    "dbo.TestItem AS t2 ON t2.ItemNo = ri.ParItemNo' \r\n" +
                                    "set @SQL = @SQL + ' where ReceiveDate=''' + @ReceiveDate + ''' and SectionNo=' + @SectionNo + ' and TestTypeNo=' + @TestTypeNo + ' and SampleNo=''' + @SampleNo +'''' \r\n" +
                                    "if ISNULL(@SortFields, '') != '' \r\n" +
                                    "set @SQL = @SQL + ' order by ' + @SortFields \r\n" +
                                    "exec(@SQL) \r\n" +
                                    "END \r\n";
                        listSQL.Add(updateSql);
                    }

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = "CREATE PROCEDURE [dbo].[Proc_ReportItemQueryDataSource] \r\n" +
                                " @ReportFormID varchar(50),\r\n" +
                                " @SortFields varchar(max)\r\n" +
                                " AS \r\n" +
                                "BEGIN \r\n" +
                                " DECLARE @SQL varchar(max) \r\n" +
                                "set @SQL = 'SELECT   ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, OrderNo, ParItemNo, ItemNo, ParitemName, " +
                                "ItemCname, ItemEname, ReportValue, ReportDesc, ItemValue, ItemUnit, ResultStatus, RefRange, EquipNo, EquipName," +
                                " DiagMethod, Prec, StandardCode, ItemDesc, SecretGrade, Visible, ZDY1, ZDY2, ZDY3, ItemUnit AS UNIT," +
                                "ItemEname AS TESTITEMSNAME, ItemCname AS TESTITEMNAME, ReportPublicationID AS ReportFormID, LabID, " +
                                "DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp " +
                                "FROM      dbo.ReportItemFull " +
                                "where dbo.ReportItemFull.ReportPublicationID = '''+@ReportFormID+'''' \r\n" +
                                " if ISNULL(@SortFields, '') != '' \r\n" +
                                "set @SQL = @SQL + ' order by ' + @SortFields \r\n" +
                                "exec(@SQL) \r\n" +
                                "END \r\n";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.15");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.15 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.16
            if (IsUpdateDataBase(oldVersion, "1.0.0.16"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'29', N'住院号', N'ZDY5', NULL, N'110', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'43', N'页面公共配置', N'allPageType', N'config', N'IsUseClodopPrint', N'false', NULL, N'bool', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = "INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'29', N'住院号', N'ZDY5', NULL, N'110', NULL, NULL, NULL, N'1');";
                    listSQL.Add(updateSql);
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'43', N'页面公共配置', N'allPageType', N'config', N'IsUseClodopPrint', N'false', NULL, N'bool', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.16");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.16 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.17
            if (IsUpdateDataBase(oldVersion, "1.0.0.17"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'41', N'门诊号', N'ZDY2', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY2'', fieldLabel: ''门诊号'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'145', N'查询打印页面配置', N'自助打印', N'config', N'IsUseClodopPrint', N'false', NULL, N'bool', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'41', N'门诊号', N'ZDY2', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY2'', fieldLabel: ''门诊号'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    updateSql = " INSERT INTO [dbo].[B_Parameter]  VALUES (N'145', N'查询打印页面配置', N'自助打印', N'config', N'IsUseClodopPrint', N'false', NULL, N'bool', NULL, NULL, NULL, NULL,N'0',NULL, NULL, NULL, NULL, NULL, NULL);";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.17");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.17 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.18
            if (IsUpdateDataBase(oldVersion, "1.0.0.18"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    if (!CheckDataObjectIsExists("Site_Operation_Records", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[Site_Operation_Records](" +
                                    "	[LabID] [bigint] NOT NULL," +
                                    "	[SiteOperationID] [bigint] NOT NULL," +
                                    "	[SiteIP] [varchar](50) NULL," +
                                    "	[SiteHostName] [varchar](50) NULL," +
                                    "	[ServiceName] [varchar](100) NULL," +
                                    "	[EmpID] [bigint] NULL," +
                                    "	[EmpName] [varchar](20) NULL," +
                                    "	[DataAddTime] [datetime] NULL," +
                                    "	[DataUpdateTime] [datetime] NULL," +
                                    "	[DataTimeStamp] [timestamp] NULL," +
                                    " CONSTRAINT [PK_SITE_OPERATION] PRIMARY KEY CLUSTERED " +
                                    "(" +
                                    "	[SiteOperationID] ASC" +
                                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                                    ") ON [PRIMARY]";

                        listSQL.Add(updateSql);
                    }
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    if (!CheckDataObjectIsExists("Site_Operation_Records", "U"))
                    {
                        updateSql = "CREATE TABLE [dbo].[Site_Operation_Records](" +
                                    "	[LabID] [bigint] NOT NULL," +
                                    "	[SiteOperationID] [bigint] NOT NULL," +
                                    "	[SiteIP] [varchar](50) NULL," +
                                    "	[SiteHostName] [varchar](50) NULL," +
                                    "	[ServiceName] [varchar](100) NULL," +
                                    "	[EmpID] [bigint] NULL," +
                                    "	[EmpName] [varchar](20) NULL," +
                                    "	[DataAddTime] [datetime] NULL," +
                                    "	[DataUpdateTime] [datetime] NULL," +
                                    "	[DataTimeStamp] [timestamp] NULL," +
                                    " CONSTRAINT [PK_SITE_OPERATION] PRIMARY KEY CLUSTERED " +
                                    "(" +
                                    "	[SiteOperationID] ASC" +
                                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                                    ") ON [PRIMARY]";

                        listSQL.Add(updateSql);
                    }
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.18");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.18 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.0.19
            if (IsUpdateDataBase(oldVersion, "1.0.0.19"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'42', N'自定义字段10', N'ZDY10', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY10'', fieldLabel: ''自定义字段10'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    
                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'42', N'自定义字段10', N'ZDY10', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY10'', fieldLabel: ''自定义字段10'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);
                    
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.0.19");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.0.19 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.2.1
            if (IsUpdateDataBase(oldVersion, "1.0.2.1"))
            {
                DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    ExecuteUpdateSQL("insert into [B_Parameter]" +
                    " Values( " +
                    ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                    "\'数据库版本号\'," +//Name
                    "null," +//SName
                    "\'SYS\'," +//ParaType
                        "\'SYS_DBVersion\'," +//ParaNo
                        "null," +//ParaValue
                        "null," +//site
                        "\'数据库版本号\'," +//ParaDesc
                        "null," +//DataUpdateTime
                        "null," +//IsUse
                        "null," +//Shortcode
                        "null" +//PinYinZiTou
                        0 +//LabID
                        "null" +//[NodeID]
                        "null" +//[GroupNo]
                        "null" +//[DataAddTime]
                        "null" +//[DataTimeStamp]
                        "null" +//[DispOrder]
                        "null" +//[PDictId]
                        ")");
                }
                if (result)
                {
                    result = UpateCompareVersionInfo("1.0.2.1");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.2.1 Error");
                    return false;
                }
            }
            else
            {
                result = true;
            }
            #endregion
            #region 1.0.2.2
            if (IsUpdateDataBase(oldVersion, "1.0.2.2"))
            {
                List<string> listSQL = new List<string>();
                if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'43', N'自定义字段14', N'ZDY14', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY14'', fieldLabel: ''自定义字段14'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);

                }
                else if (DBSourceType == "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter")
                {
                    updateSql = "INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'43', N'自定义字段14', N'ZDY14', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY14'', fieldLabel: ''自定义字段14'', labelWidth: 60, width: 180 }');";
                    listSQL.Add(updateSql);

                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                {
                    DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
                    if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        ExecuteUpdateSQL("insert into [B_Parameter]" +
                        " Values( " +
                        ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                        "\'数据库版本号\'," +//Name
                        "null," +//SName
                        "\'SYS\'," +//ParaType
                         "\'SYS_DBVersion\'," +//ParaNo
                         "null," +//ParaValue
                         "null," +//site
                         "\'数据库版本号\'," +//ParaDesc
                         "null," +//DataUpdateTime
                         "null," +//IsUse
                         "null," +//Shortcode
                         "null" +//PinYinZiTou
                         0 +//LabID
                         "null" +//[NodeID]
                         "null" +//[GroupNo]
                         "null" +//[DataAddTime]
                         "null" +//[DataTimeStamp]
                         "null" +//[DispOrder]
                         "null" +//[PDictId]
                         ")");
                    }
                    result = UpateCompareVersionInfo("1.0.2.2");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Update 1.0.2.2 Error");
                    return false;
                }

            }
            else
            {
                result = true;
            }
            #endregion
            return result;
        }

        private static bool IsUpdateDataBase(string oldVersion, string newVersion)
        {
            bool result = false;
            //比较数据库版本号，判断是否可升级
            if (CompareDBVersion(oldVersion, newVersion))
            {
                string curAssemblyVersion = GetMainAssemblyVersion();
                if (DicVersion.Keys.Contains(newVersion))
                {
                    string mainAssemblyVersion = DicVersion[newVersion];
                    if (CompareAssemblyVersion(curAssemblyVersion, mainAssemblyVersion))
                        result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取某一程序集版本
        /// </summary>
        /// <param name="mainAssemblyFile">程序集名称</param>
        /// <returns></returns>
        private static string GetMainAssemblyVersion()
        {
            string mainPath = AppDomain.CurrentDomain.BaseDirectory;
            Assembly assembly = Assembly.LoadFile(mainPath + "\\Bin\\" + MainAssemblyFile);
            AssemblyName assemblyName = assembly.GetName();
            return assemblyName.Version.ToString();
        }

        /// <summary>
        /// 比较数据库版本
        /// </summary>
        /// <param name="oldVersion"></param>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        private static bool CompareDBVersion(string oldVersion, string newVersion)
        {
            bool result = false;
            if ((!string.IsNullOrEmpty(oldVersion.Trim())) && (!string.IsNullOrEmpty(newVersion.Trim())))
            {
                try
                {
                    string[] oldVersionList = oldVersion.Split('.');
                    string[] newVersionList = newVersion.Split('.');
                    if (oldVersionList.Length == newVersionList.Length)
                    {
                        for (int i = 0; i < newVersionList.Length; i++)
                        {
                            if (int.Parse(oldVersionList[i]) != int.Parse(newVersionList[i]))
                            {
                                if (int.Parse(oldVersionList[i]) < int.Parse(newVersionList[i]))
                                {
                                    result = true;
                                    break;
                                }
                                else
                                {
                                    result = false;
                                    break;
                                }
                            }
                        }
                    }
                    else if (oldVersionList.Length < newVersionList.Length)
                        result = true;
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("Update CompareVersion Error：" + ex.Message);
                }
            }
            else if (string.IsNullOrEmpty(oldVersion.Trim()) && (!string.IsNullOrEmpty(newVersion.Trim())))
                result = true;
            // result default false
            //else if ((!string.IsNullOrEmpty(oldVersion.Trim())) && string.IsNullOrEmpty(newVersion.Trim()))
            //    result = false;
            //else if (string.IsNullOrEmpty(oldVersion.Trim()) || string.IsNullOrEmpty(newVersion.Trim()))
            //    result = false;
            return result;
        }

        /// <summary>
        /// 比较程序集版本
        /// </summary>
        /// <param name="oldVersion"></param>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        private static bool CompareAssemblyVersion(string oldVersion, string newVersion)
        {
            bool result = false;
            if ((!string.IsNullOrEmpty(oldVersion.Trim())) && (!string.IsNullOrEmpty(newVersion.Trim())))
            {
                try
                {
                    string[] oldVersionList = oldVersion.Split('.');
                    string[] newVersionList = newVersion.Split('.');
                    if (oldVersionList.Length == newVersionList.Length)
                    {
                        int equalCount = 0;
                        for (int i = 0; i < newVersionList.Length; i++)
                        {
                            if (int.Parse(oldVersionList[i]) > int.Parse(newVersionList[i]))
                            {
                                result = true;
                                break;
                            }
                            else if (int.Parse(oldVersionList[i]) == int.Parse(newVersionList[i]))
                            {
                                equalCount++;
                            }
                        }
                        if (equalCount == oldVersionList.Length)
                            result = true;
                    }
                    else if (oldVersionList.Length < newVersionList.Length)
                        result = true;
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("Update CompareVersion Error：" + ex.Message);
                }
            }
            else if (string.IsNullOrEmpty(oldVersion.Trim()) && (!string.IsNullOrEmpty(newVersion.Trim())))
                result = true;
            // result default false
            //else if ((!string.IsNullOrEmpty(oldVersion.Trim())) && string.IsNullOrEmpty(newVersion.Trim()))
            //    result = false;
            //else if (string.IsNullOrEmpty(oldVersion.Trim()) || string.IsNullOrEmpty(newVersion.Trim()))
            //    result = false;
            if (!result)
                ZhiFang.Common.Log.Log.Info("当前主程序集版本低于配置版本，无法升级！CurAssemblyVersion：" + oldVersion
                    + "；NewAssemblyVersion：" + newVersion);
            return result;
        }

        /// <summary>
        /// 升级后更新版本相关信息
        /// </summary>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        private static bool UpateCompareVersionInfo(string newVersion)
        {
            string updateSql = " update B_Parameter set ParaValue=\'" + newVersion + "\'" + " where ParaType=\'SYS\' and ParaNo=\'SYS_DBVersion\'";
            return ExecuteUpdateSQL(updateSql);
        }

        /// <summary>
        /// 获取数据库当前版本
        /// </summary>
        /// <returns></returns>
        public static string GetDataBaseCurVersion()
        {
            string curVersion = "1.0.0.0";
            DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                curVersion = ds.Tables[0].Rows[0][0].ToString();
            }
            return curVersion;
        }

        /// <summary>
        /// 检查数据库对象是否存在（表-U、视图-V、存储过程-P、触发器-TR、标量函数-FN等）
        /// </summary>
        /// <param name="objectname"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        private static bool CheckDataObjectIsExists(string objectname, string objectType)
        {
            string objectID = "";
            DataSet ds = SqlServerHelper.QuerySql("select object_id(\'" + objectname + "\', \'" + objectType + "\') as ObjectID", ADOConnectStr);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objectID = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return false;
            }
            if (objectID != null && objectID != "")
            {
                ZhiFang.Common.Log.Log.Debug("objectID:" + objectID);
            }
            else
            {
                return false;
            }
            return (!string.IsNullOrEmpty(objectID));
        }

        /// <summary>
        /// 检查参数表是否存在，不存在则创建
        /// </summary>
        private static void CheckBParameterIsIsExists()
        {
            string updateSql = "";
            if (!CheckDataObjectIsExists("B_Parameter", "U"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " CREATE TABLE [dbo].[B_Parameter]( " +
                            " [Id] [bigint]  NOT NULL, " +
                            " [Name]            [varchar](50) COLLATE Chinese_PRC_CI_AS NULL, " +
                            " [SName]           [varchar](50) COLLATE Chinese_PRC_CI_AS NULL, " +
                            " [ParaType]        [varchar](50) COLLATE Chinese_PRC_CI_AS NULL, " +
                            " [ParaNo]          [varchar](50) COLLATE Chinese_PRC_CI_AS NULL, " +
                            " [ParaValue]       [varchar](50) COLLATE Chinese_PRC_CI_AS NULL, " +
                            " [Site]            [varchar](50) COLLATE Chinese_PRC_CI_AS NULL, " +
                            " [ParaDesc]        [varchar](50) COLLATE Chinese_PRC_CI_AS NULL, " +
                            " [DataUpdateTime]  [datetime]        NULL, "                        +
                            " [IsUse]           [bit]        NULL, "                            +
                            " [Shortcode]       [varchar](50) COLLATE Chinese_PRC_CI_AS NULL, " +
                            " [PinYinZiTou]     [varchar](50) COLLATE Chinese_PRC_CI_AS NULL )" ;

                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE [dbo].[B_Parameter] SET (LOCK_ESCALATION = TABLE)";                
                listSQL.Add(updateSql);

                updateSql = "EXEC sp_addextendedproperty 'MS_Description', N'数据更新时间','SCHEMA', N'dbo','TABLE', N'B_Parameter','COLUMN', N'DataUpdateTime'";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE [dbo].[B_Parameter] ADD CONSTRAINT [PK_B_PARAMETER] PRIMARY KEY CLUSTERED ([Id])" +
                            "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  " +
                            "ON [PRIMARY]";
                listSQL.Add(updateSql);
                ExecuteUpdateSQL(listSQL);

                ExecuteUpdateSQL("insert into [B_Parameter]" +
                    " Values( " +                  
                    ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDLong().ToString() + "," +//ID
                    "\'数据库版本号\'," +//Name
                    "null," +//SName
                    "\'SYS\'," +//ParaType
                     "\'SYS_DBVersion\'," +//ParaNo
                     "null," +//ParaValue
                     "null," +//site
                     "\'数据库版本号\'," +//ParaDesc
                     "null," +//DataUpdateTime
                     "null," +//IsUse
                     "null," +//Shortcode
                     "null" +//PinYinZiTou
                     ")");
            }
        }

        private static bool ExecuteUpdateSQL(string sql)
        {
            return SqlServerHelper.ExecuteSql(sql, ADOConnectStr);
        }

        private static bool ExecuteUpdateSQL(List<string> listSQL)
        {
            return SqlServerHelper.ExecuteSqlList(listSQL, ADOConnectStr);
        }
    }
}
