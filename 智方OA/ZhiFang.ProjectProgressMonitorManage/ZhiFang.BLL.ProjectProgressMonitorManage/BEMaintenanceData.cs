using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.IBLL.RBAC;
using ZhiFang.ProjectProgressMonitorManage.Common;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BEMaintenanceData : BaseBLL<EMaintenanceData>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBEMaintenanceData
    {
        IBEReportData IBEReportData { get; set; }
        IBETemplet IBETemplet { get; set; }
        IBETempletEmp IBETempletEmp { get; set; }
        IBETempletRes IBETempletRes { get; set; }
        IBEAttachment IBEAttachment { get; set; }
        IBHREmployee IBHREmployee { get; set; }
        IBBParameter IBBParameter { get; set; }
        IBEParameter IBEParameter { get; set; }

        string[] arrayES = new string[] { "E", "E1", "S", "S1" };
        string[] arrayNumber = new string[] { "R", "I", "L", "F" };
        string[] arrayText = new string[] { "C", "CL", "M", "O", "RP", "RM", "OP", "OM" };
        string[] arrayDate = new string[] { "DT", "D", "T", "ODT", "OD", "OT", "RDT", "RD", "RT" };
        string[] arrayExcelCol = new string[] {
            "A", "B", "C", "D", "E", "F", "G",
            "H", "I" ,"J", "K", "L", "M", "N",
            "O", "P", "Q", "R" ,"S", "T",
            "U", "V", "W", "X", "Y", "Z",
            "AA", "AB", "AC", "AD", "AE", "AF", "AG",
            "AH", "AI" ,"AJ", "AK", "AL", "AM", "AN",
            "AO", "AP", "AQ", "AR" ,"AS", "AT",
            "AU", "AV", "AW", "AX", "AY", "AZ"
        };

        public BaseResultDataValue AddEMaintenanceData(long templetID, string itemDate, string templetBatNo, IList<EMaintenanceData> entityList, ref IList<EMaintenanceData> resultList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                //ZhiFang.Common.Log.Log.Info("Begin Save Data");
                if (entityList != null && entityList.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(templetBatNo))
                    {
                        templetBatNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString();
                        //templetBatNo = QueryTempletBatNo(templetID, itemDate, itemDate);
                        //if (string.IsNullOrWhiteSpace(templetBatNo))
                        //    templetBatNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString();
                    }
                    IList<EMaintenanceData> dataTypeList = entityList.Where(p => p.TempletDataType == 1).ToList();
                    if (dataTypeList != null && dataTypeList.Count > 0)
                    {
                        foreach (EMaintenanceData EMD in dataTypeList)
                        {
                            IList<EMaintenanceData> dataList = entityList.Where(p => p.TempletTypeCode == EMD.TempletTypeCode).ToList();
                            AddEMaintenanceData(templetID, itemDate, EMD.TempletTypeCode, templetBatNo, dataList, ref resultList);
                        }
                    }
                    else
                        AddEMaintenanceData(templetID, itemDate, "", templetBatNo, entityList, ref resultList);
                }
                //ZhiFang.Common.Log.Log.Info("End Save Data");
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "新增质量登记数据错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;

        }

        public BaseResultDataValue AddEMaintenanceData(long templetID, string itemDate, string typeCode, string templetBatNo, IList<EMaintenanceData> entityList, ref IList<EMaintenanceData> resultList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                //ZhiFang.Common.Log.Log.Info("Begin Save Data");
                if (entityList != null && entityList.Count > 0)
                {
                    string operater = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    string batchNumber = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString();
                    if (string.IsNullOrWhiteSpace(templetBatNo))
                    {
                        templetBatNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString();
                        //templetBatNo = QueryTempletBatNo(templetID, itemDate, itemDate);
                        //if (string.IsNullOrWhiteSpace(templetBatNo))
                        //    templetBatNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString();
                    }
                    EMaintenanceData dataTypeOne = null;
                    int result = 0;
                    int addCount = 0;
                    int editCount = 0;
                    int delCount = 0;
                    foreach (EMaintenanceData entity in entityList)
                    {
                        if (!string.IsNullOrWhiteSpace(entity.TempletTypeCode))
                            typeCode = entity.TempletTypeCode;
                        else if (!string.IsNullOrWhiteSpace(typeCode))
                            entity.TempletTypeCode = typeCode;

                        if (entity.TempletDataType == 1)
                        {
                            dataTypeOne = entity;
                            continue;
                        }
                        result = _AddEMaintenanceData(entity, typeCode, batchNumber, templetBatNo, operater, ref resultList);
                        if (result == 1)
                            addCount++;//记录新增质量记录项的数量
                        else if (result == 2)
                            editCount++;//记录修改质量记录项的数量
                        else if (result == 3)
                        {
                            editCount++;
                            delCount++;//记录删除质量记录项的数量
                        }
                    }//foreach
                    if (addCount > 0) //如果有新增的质量记录项，则增加公共质量记录项
                        _AddEMaintenanceData(dataTypeOne, typeCode, batchNumber, templetBatNo, operater, ref resultList);
                    else if (delCount > 0 && delCount >= editCount)//如果有删除的质量记录项数量，且删除的数量等于数据库中现存的数量，则删除公共质量记录项
                    {
                        EMaintenanceData ema = this.Get(dataTypeOne.Id);
                        if (ema != null)
                        {
                            this.Entity = ema;
                            this.Remove();
                        }
                    }
                    else if (editCount > 0)
                        _AddEMaintenanceData(dataTypeOne, typeCode, batchNumber, templetBatNo, operater, ref resultList);

                }//if
                //ZhiFang.Common.Log.Log.Info("End Save Data");
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "新增质量登记数据错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;

        }

        private int _AddEMaintenanceData(EMaintenanceData entity, string typeCode, string batchNumber, string templetBatNo, string operater, ref IList<EMaintenanceData> resultList)
        {
            int result = 0;
            EMaintenanceData ema = this.Get(entity.Id);
            if (ema == null)
            {
                if (string.IsNullOrWhiteSpace(entity.BatchNumber))
                    entity.BatchNumber = batchNumber;
                if (string.IsNullOrWhiteSpace(entity.TempletBatNo))
                    entity.TempletBatNo = templetBatNo;
                entity.Operater = operater;
                if (entity.OperateTime == null)
                    entity.OperateTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.DataAddTime = DateTime.Now;
                if (entity.ItemDate == null)
                    entity.ItemDate = DateTime.Today;
                this.Entity = entity;
                if (_JudgeEMaintenanceData(this.Entity) && (this.Add()))
                {
                    result = 1;
                    resultList.Add(this.Entity);
                }
            }
            else
            {
                ema.Operater = operater;
                if (ema.ItemDate == null)
                    ema.ItemDate = DateTime.Today;
                ema.DataUpdateTime = DateTime.Now;
                ema.ItemIsExcute = entity.ItemIsExcute;
                ema.ItemMemo = entity.ItemMemo;
                ema.ItemResult = entity.ItemResult;
                ema.ItemStatus = entity.ItemStatus;
                this.Entity = ema;
                if (_JudgeEMaintenanceData(ema))
                {
                    if (this.Edit())
                    {
                        result = 2;
                        resultList.Add(this.Entity);
                    }
                }
                else
                {
                    if (this.Remove())
                        result = 3;
                }
            }
            return result;
        }

        private bool _JudgeEMaintenanceData(EMaintenanceData ema)
        {
            bool result = false;
            if (ema != null && ema.TempletDataType == 2)
            {
                if (arrayES.Contains(ema.ItemDataType))
                    result = (string.IsNullOrWhiteSpace(ema.ItemResult) || ema.ItemResult.Trim() == "0");
                else
                    result = (string.IsNullOrWhiteSpace(ema.ItemResult));
            }
            return (!result);
        }

        public BaseResultDataValue JudgeTempletIsFillData(long templetID, string typeCode, string templetDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            bool tempBool = ((!string.IsNullOrEmpty(typeCode)) && (typeCode.ToUpper() == "MD"));
            if (!tempBool)
            {
                ETemplet templet = IBETemplet.Get(templetID);
                tempBool = (templet.CheckType == 1);
            }
            if (tempBool)
            {
                string para = IBEParameter.QueryPara("QualityRecord", "IsJudgeDayBeforeData");
                if (para == "1")
                {
                    DateTime tempDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(templetDate))
                        tempDate = DateTime.Parse(templetDate).AddDays(-1);
                    string strDate = tempDate.ToString("yyyy-MM-dd");
                    IList<EMaintenanceData> listEMaintenanceData = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                        "and emaintenancedata.TempletDataType=2 and emaintenancedata.TempletTypeCode=\'" + typeCode + "\' and emaintenancedata.ItemDate=\'" + strDate + "\'");
                    if ((listEMaintenanceData == null) || (listEMaintenanceData.Count == 0))
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "此页签数据项前一天未做数据登记，请补入数据！";
                    }
                }
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue JudgeTempletIsFillData(long templetID, string curDate, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.ResultDataValue = "0";
            IList<EMaintenanceData> listEMaintenanceData = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                " and emaintenancedata.ItemDate>=\'" + beginDate + "\' and emaintenancedata.ItemDate<=\'" + endDate + "\'");
            baseResultDataValue.success = ((listEMaintenanceData != null) && (listEMaintenanceData.Count > 0));
            if (baseResultDataValue.success)
            {
                baseResultDataValue.ResultDataValue = "1";
                listEMaintenanceData = listEMaintenanceData.Where(p => p.ItemDate == DateTime.Parse(curDate)).ToList();
                if ((listEMaintenanceData != null) && (listEMaintenanceData.Count > 0))
                    baseResultDataValue.ResultDataValue = "2";
            }
            return baseResultDataValue;
        }
        public string QueryTempletBatNo(long templetID, string beginDate, string endDate)
        {
            string templetBatNo = "";
            IList<EMaintenanceData> listEMaintenanceData = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                " and emaintenancedata.ItemDate>=\'" + beginDate + "\' and emaintenancedata.ItemDate<=\'" + endDate + "\'" +
                " and emaintenancedata.TempletBatNo is not null");
            if ((listEMaintenanceData != null) && (listEMaintenanceData.Count > 0))
            {
                templetBatNo = listEMaintenanceData[0].TempletBatNo;
            }
            return templetBatNo;
        }

        public EntityList<EMaintenanceData> SearchEMaintenanceDataByTypeCode(long templetID, DateTime itemDate, string typeCode, string templetBatNo, int isLoadBeforeData)
        {
            EntityList<EMaintenanceData> entityList = new EntityList<EMaintenanceData>();
            string templetTypeCode = typeCode.ToUpper();
            IList<EMaintenanceData> listEMaintenanceData = null;
            string whereHQL = "";
            if (!string.IsNullOrWhiteSpace(templetTypeCode))
                whereHQL = whereHQL + " and emaintenancedata.TempletTypeCode=\'" + templetTypeCode + "\'";
            if (!string.IsNullOrWhiteSpace(templetBatNo))
                whereHQL = whereHQL + " and emaintenancedata.TempletBatNo=\'" + templetBatNo + "\'";
            if (templetTypeCode == "MW")
            {
                DateTime firstMMDay = new DateTime(itemDate.Year, itemDate.Month, 1);
                int dayMonth = DateTime.DaysInMonth(itemDate.Year, itemDate.Month);
                DateTime lastMMDay = new DateTime(itemDate.Year, itemDate.Month, dayMonth);
                DateTime firstMWDay = DateTime.Now;
                DateTime lastMWDay = DateTime.Now;
                if (isLoadBeforeData == 1)
                {
                    itemDate = itemDate.AddDays(-7);
                    _GetWeekBeginAndEndDate(itemDate, ref firstMWDay, ref lastMWDay);
                }
                else
                {
                    _GetWeekBeginAndEndDate(itemDate, ref firstMWDay, ref lastMWDay);
                    if (firstMWDay < firstMMDay)
                        firstMWDay = firstMMDay;
                    if (lastMWDay > lastMMDay)
                        lastMWDay = lastMMDay;
                }
                listEMaintenanceData = _GetEMaintenanceDataByTypeCode(templetID, templetTypeCode, templetBatNo, firstMWDay, lastMWDay);
            }
            else if (templetTypeCode == "MM")
            {
                if (isLoadBeforeData == 1)
                    itemDate = itemDate.AddMonths(-1);
                DateTime firstDay = new DateTime(itemDate.Year, itemDate.Month, 1);
                int dayMonth = DateTime.DaysInMonth(itemDate.Year, itemDate.Month);
                DateTime lastDay = new DateTime(itemDate.Year, itemDate.Month, dayMonth);
                listEMaintenanceData = _GetEMaintenanceDataByTypeCode(templetID, templetTypeCode, templetBatNo, firstDay, lastDay);
            }
            else if (templetTypeCode == "SP")
            {
                if (isLoadBeforeData == 1)
                {
                    DateTime firstDay = new DateTime(itemDate.Year, itemDate.Month, 1);
                    int dayMonth = DateTime.DaysInMonth(itemDate.Year, itemDate.Month);
                    DateTime lastDay = new DateTime(itemDate.Year, itemDate.Month, dayMonth);
                    listEMaintenanceData = _GetEMaintenanceDataByTypeCode(templetID, templetTypeCode, templetBatNo, firstDay, lastDay);
                }
                else
                {
                    listEMaintenanceData = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                        " and emaintenancedata.ItemDate=\'" + itemDate.ToString("yyyy-MM-dd") + "\'" + whereHQL);
                }
            }
            else
            {
                if (isLoadBeforeData == 1)
                    itemDate = itemDate.AddDays(-1);
                listEMaintenanceData = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                    " and emaintenancedata.ItemDate=\'" + itemDate.ToString("yyyy-MM-dd") + "\'" + whereHQL);
                if ((listEMaintenanceData == null || listEMaintenanceData.Count == 0) && (isLoadBeforeData == 1))
                {
                    int days = 7;
                    string para = IBEParameter.QueryPara("QualityRecord", "LoadDataDays");
                    if (!string.IsNullOrWhiteSpace(para))
                        int.TryParse(para, out days);
                    if (days <= 0)
                        days = 7;
                    for (int i = 1; i <= days; i++)//向前查找7天的数据
                    {
                        listEMaintenanceData = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                            " and emaintenancedata.ItemDate=\'" + itemDate.AddDays(i * -1).ToString("yyyy-MM-dd") + "\'" + whereHQL);
                        if (listEMaintenanceData != null && listEMaintenanceData.Count > 0)
                            break;
                    }
                }
            }
            if (listEMaintenanceData != null)
            {
                entityList.count = listEMaintenanceData.Count;
                entityList.list = listEMaintenanceData;
            }
            return entityList;
        }

        public EntityList<EMaintenanceData> LoadSaveEMaintenanceData(long templetID, DateTime itemDate, string typeCode, string templetBatNo)
        {
            EntityList<EMaintenanceData> entityList = new EntityList<EMaintenanceData>();
            string templetTypeCode = typeCode.ToUpper();
            IList<EMaintenanceData> listEMaintenanceData = null;
            string whereHQL = "";
            if (!string.IsNullOrWhiteSpace(templetTypeCode))
                whereHQL = whereHQL + " and emaintenancedata.TempletTypeCode=\'" + templetTypeCode + "\'";
            if (!string.IsNullOrWhiteSpace(templetBatNo))
                whereHQL = whereHQL + " and emaintenancedata.TempletBatNo=\'" + templetBatNo + "\'";
            if (templetTypeCode == "MW")
            {
                DateTime firstMMDay = new DateTime(itemDate.Year, itemDate.Month, 1);
                int dayMonth = DateTime.DaysInMonth(itemDate.Year, itemDate.Month);
                DateTime lastMMDay = new DateTime(itemDate.Year, itemDate.Month, dayMonth);
                DateTime firstMWDay = DateTime.Now;
                DateTime lastMWDay = DateTime.Now;

                itemDate = itemDate.AddDays(-7);
                _GetWeekBeginAndEndDate(itemDate, ref firstMWDay, ref lastMWDay);

                listEMaintenanceData = _GetEMaintenanceDataByTypeCode(templetID, templetTypeCode, templetBatNo, firstMWDay, lastMWDay);
            }
            else if (templetTypeCode == "MM")
            {

                itemDate = itemDate.AddMonths(-1);
                DateTime firstDay = new DateTime(itemDate.Year, itemDate.Month, 1);
                int dayMonth = DateTime.DaysInMonth(itemDate.Year, itemDate.Month);
                DateTime lastDay = new DateTime(itemDate.Year, itemDate.Month, dayMonth);
                listEMaintenanceData = _GetEMaintenanceDataByTypeCode(templetID, templetTypeCode, templetBatNo, firstDay, lastDay);
            }
            else if (templetTypeCode == "SP")
            {

                DateTime firstDay = new DateTime(itemDate.Year, itemDate.Month, 1);
                int dayMonth = DateTime.DaysInMonth(itemDate.Year, itemDate.Month);
                DateTime lastDay = new DateTime(itemDate.Year, itemDate.Month, dayMonth);
                listEMaintenanceData = _GetEMaintenanceDataByTypeCode(templetID, templetTypeCode, templetBatNo, firstDay, lastDay);

            }
            else
            {
                itemDate = itemDate.AddDays(-1);
                listEMaintenanceData = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                    " and emaintenancedata.ItemDate=\'" + itemDate.ToString("yyyy-MM-dd") + "\'" + whereHQL);
                if ((listEMaintenanceData == null || listEMaintenanceData.Count == 0))
                {
                    for (int i = 1; i <= 7; i++)//向前查找7天的数据
                    {
                        listEMaintenanceData = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                            " and emaintenancedata.ItemDate=\'" + itemDate.AddDays(i * -1).ToString("yyyy-MM-dd") + "\'" + whereHQL);
                        if (listEMaintenanceData != null && listEMaintenanceData.Count > 0)
                            break;
                    }
                }
            }
            if (listEMaintenanceData != null)
            {
                entityList.count = listEMaintenanceData.Count;
                entityList.list = listEMaintenanceData;
            }
            return entityList;
        }

        private IList<EMaintenanceData> _GetEMaintenanceDataByTypeCode(long templetID, string templetTypeCode, string templetBatNo, DateTime firstDay, DateTime lastDay)
        {
            string strHQL = "emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                " and emaintenancedata.ItemDate>=\'" + firstDay.ToString("yyyy-MM-dd") + "\'" +
                " and emaintenancedata.ItemDate<=\'" + lastDay.ToString("yyyy-MM-dd") + "\'";
            if (!string.IsNullOrWhiteSpace(templetTypeCode))
                strHQL = strHQL + " and emaintenancedata.TempletTypeCode=\'" + templetTypeCode + "\'";
            if (!string.IsNullOrWhiteSpace(templetBatNo))
                strHQL = strHQL + " and emaintenancedata.TempletBatNo=\'" + templetBatNo + "\'";
            IList<EMaintenanceData> listEMaintenanceData = this.SearchListByHQL(strHQL);
            if ((listEMaintenanceData != null) && (listEMaintenanceData.Count > 0))
            {
                var lisItem = from EMaintenanceData in listEMaintenanceData
                              group EMaintenanceData by
                                  new
                                  {
                                      ItemDate = EMaintenanceData.ItemDate
                                  } into g
                              select new
                              {
                                  ItemDate = g.Key.ItemDate
                              };
                IList<DateTime> listItemDate = new List<DateTime>();
                foreach (var item in lisItem)
                    listItemDate.Add((DateTime)item.ItemDate);
                listItemDate = listItemDate.OrderBy(p => p).ToList();
                listEMaintenanceData = listEMaintenanceData.Where(p => p.ItemDate == listItemDate[listItemDate.Count - 1]).ToList();
            }
            return listEMaintenanceData;
        }

        public BaseResultDataValue GroupMaintenanceDataTB(long templetID, string typeCode, string beginDate, string endDate, int isLoadBeforeData)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ETemplet templet = IBETemplet.Get(templetID);
            if (templet == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法根据模板ID获取模板信息";
                ZhiFang.Common.Log.Log.Info("无法根据模板ID获取模板信息,模板ID：" + templetID.ToString());
                return baseResultDataValue;
            }
            string TempletFillStruct = templet.TempletFillStruct;
            if (string.IsNullOrEmpty(TempletFillStruct))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "模板的填充结构信息为空";
                ZhiFang.Common.Log.Log.Info("模板的填充结构信息为空,模板ID：" + templetID.ToString());
                return baseResultDataValue;
            }
            //2|0&TB|标本复检记录本&TB1|日期|d&d|d1|v
            StringBuilder strSQL = new StringBuilder();
            IList<string> listTBItem = new List<string>();
            IList<string> listTempletFillStruct = TempletFillStruct.Split(',').ToList();
            foreach (string strChild in listTempletFillStruct)
            {
                IList<string> listChild = strChild.Split('&').ToList();
                if (listChild != null && listChild.Count > 2 && listChild[1].Substring(0, 2).ToUpper() == typeCode.ToUpper())
                {
                    IList<string> list = listChild[2].Split('|').ToList();
                    if (list != null && list.Count > 1 && list[0].Length > 2 && list[0].Substring(0, 2).ToUpper() == typeCode.ToUpper())
                    {
                        if (!string.IsNullOrEmpty(list[1]) && listTBItem.IndexOf(list[0]) < 0)
                        {
                            listTBItem.Add(list[0]);
                            strSQL.Append(",Max(case when TempletItemCode=\'" + list[0] + "\' then ItemResult else null end) as \'" + list[1] + "\'");
                        }
                    }
                }
            }
            string sql = strSQL.ToString();
            if (!string.IsNullOrEmpty(sql))
            {
                bool isExistData = false;
                if (isLoadBeforeData == 1)
                {
                    int days = 7;
                    string para = IBEParameter.QueryPara("QualityRecord", "LoadDataDays");
                    if (!string.IsNullOrWhiteSpace(para))
                        int.TryParse(para, out days);
                    if (days <= 0)
                        days = 7;
                    for (int i = 1; i <= days; i++)//向前查找7天的数据
                    {

                        string b_date = DateTime.Parse(beginDate).AddDays(i * -1).ToString("yyyy-MM-dd");
                        string e_date = DateTime.Parse(b_date).AddDays(1).ToString("yyyy-MM-dd");
                        baseResultDataValue = _GetMaintenanceDataTB(templetID, typeCode, b_date, e_date, sql, ref isExistData);
                        if (isExistData)
                            break;
                    }
                }
                else
                    baseResultDataValue = _GetMaintenanceDataTB(templetID, typeCode, beginDate, endDate, sql, ref isExistData);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue _GetMaintenanceDataTB(long templetID, string typeCode, string beginDate, string endDate, string sql, ref bool isExistData)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IDataParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@TempletID", templetID.ToString());
            parameters[1] = new SqlParameter("@TypeCode", typeCode.ToUpper());
            parameters[2] = new SqlParameter("@BatchNumber", "");
            parameters[3] = new SqlParameter("@SQLPara", sql);
            parameters[4] = new SqlParameter("@BeginDate", beginDate);
            parameters[5] = new SqlParameter("@EndDate", endDate);
            DataSet ds = ZhiFang.DBUpdate.PM.SqlServerHelper.ExecuteProcedure("P_GetMaintenanceDataTB", parameters, ZhiFang.DBUpdate.PM.DBUpdate.ADOConnectStr);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                isExistData = true;
                string jsonData = ZhiFang.Common.Public.JsonHelp.DataSetToJson(ds);
                baseResultDataValue.ResultDataValue = "{count:" + ds.Tables[0].Rows.Count.ToString() + ",list:" + jsonData + "}";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue TempletBatNoGroupData(long templetID, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ETemplet templet = IBETemplet.Get(templetID);
            if (templet != null && (!string.IsNullOrWhiteSpace(templet.ShowFillItem)))
            {
                StringBuilder strSQL = new StringBuilder();
                StringBuilder strWhere = new StringBuilder();
                IList<string> listItem = templet.ShowFillItem.Split('|').ToList();
                if (listItem != null && listItem.Count >= 2)
                {
                    foreach (string item in listItem)
                    {
                        IList<string> listTemp = item.Split('/').ToList();
                        if (listTemp != null && listTemp.Count > 1)
                        {
                            if (listTemp[0].ToUpper() == "EI")
                                continue;
                            strSQL.Append(",Max(case when TempletItemCode=\'" + listTemp[0] + "\' then ItemResult else null end) as \'" + listTemp[1] + "\'");
                            if (string.IsNullOrEmpty(strWhere.ToString()))
                                strWhere.Append(" TempletItemCode=\'" + listTemp[0] + "\'");
                            else
                                strWhere.Append(" or TempletItemCode=\'" + listTemp[0] + "\'"); ;
                        }
                    }
                    string sqlField = strSQL.ToString();
                    string sqlWhere = strWhere.ToString();
                    if ((!string.IsNullOrEmpty(sqlField)) && (!string.IsNullOrEmpty(sqlWhere)))
                    {
                        IDataParameter[] parameters = new SqlParameter[5];
                        parameters[0] = new SqlParameter("@TempletID", templetID.ToString());
                        parameters[1] = new SqlParameter("@SQLField", sqlField);
                        parameters[2] = new SqlParameter("@SQLWhere", " and (" + sqlWhere + ")");
                        parameters[3] = new SqlParameter("@BeginDate", beginDate);
                        parameters[4] = new SqlParameter("@EndDate", endDate);
                        DataSet ds = ZhiFang.DBUpdate.PM.SqlServerHelper.ExecuteProcedure("P_GetTempletGroupData", parameters, ZhiFang.DBUpdate.PM.DBUpdate.ADOConnectStr);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string jsonData = ZhiFang.Common.Public.JsonHelp.DataSetToJson(ds);
                            baseResultDataValue.ResultDataValue = "{count:" + ds.Tables[0].Rows.Count.ToString() + ",list:" + jsonData + "}";
                        }
                    }
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QueryReportGroupData(long reportDataID, long templetID, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ETemplet templet = IBETemplet.Get(templetID);
            if (templet != null && (!string.IsNullOrWhiteSpace(templet.ShowFillItem)))
            {
                StringBuilder strSQL = new StringBuilder();
                StringBuilder strWhere = new StringBuilder();
                IList<string> listItem = templet.ShowFillItem.Split('|').ToList();
                if (listItem != null && listItem.Count >= 2)
                {
                    foreach (string item in listItem)
                    {
                        IList<string> listTemp = item.Split('/').ToList();
                        if (listTemp != null && listTemp.Count > 1)
                        {
                            if (listTemp[0].ToUpper() == "EI")
                                continue;
                            strSQL.Append(",Max(case when TempletItemCode=\'" + listTemp[0] + "\' then ItemResult else null end) as \'" + listTemp[1] + "\'");
                            if (string.IsNullOrEmpty(strWhere.ToString()))
                                strWhere.Append(" TempletItemCode=\'" + listTemp[0] + "\'");
                            else
                                strWhere.Append(" or TempletItemCode=\'" + listTemp[0] + "\'"); ;
                        }
                    }
                    string sqlField = strSQL.ToString();
                    string sqlWhere = strWhere.ToString();
                    if ((!string.IsNullOrEmpty(sqlField)) && (!string.IsNullOrEmpty(sqlWhere)))
                    {
                        IDataParameter[] parameters = new SqlParameter[6];
                        parameters[0] = new SqlParameter("@ReportDataID", reportDataID.ToString());
                        parameters[0] = new SqlParameter("@TempletID", templetID.ToString());
                        parameters[1] = new SqlParameter("@SQLField", sqlField);
                        parameters[2] = new SqlParameter("@SQLWhere", " and (" + sqlWhere + ")");
                        parameters[3] = new SqlParameter("@BeginDate", beginDate);
                        parameters[4] = new SqlParameter("@EndDate", endDate);
                        DataSet ds = ZhiFang.DBUpdate.PM.SqlServerHelper.ExecuteProcedure("P_GetTempletGroupData", parameters, ZhiFang.DBUpdate.PM.DBUpdate.ADOConnectStr);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string jsonData = ZhiFang.Common.Public.JsonHelp.DataSetToJson(ds);
                            baseResultDataValue.ResultDataValue = "{count:" + ds.Tables[0].Rows.Count.ToString() + ",list:" + jsonData + "}";
                        }
                    }
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteMaintenanceDataTB(long templetID, string typeCode, string itemDate, string batchNumber)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            itemDate = DateTime.Parse(itemDate).ToString("yyyy-MM-dd");
            int intDelCount = this.DeleteByHql(" from EMaintenanceData emaintenancedata where emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                              " and emaintenancedata.ItemDate=\'" + itemDate + "\'" + " and emaintenancedata.TempletTypeCode=\'" + typeCode + "\'" +
                              " and emaintenancedata.BatchNumber=\'" + batchNumber + "\'");
            int dataCount = this.DBDao.GetListCountByHQL(" emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                              " and emaintenancedata.ItemDate=\'" + itemDate + "\'" + " and emaintenancedata.TempletTypeCode=\'" + typeCode + "\'" +
                              " and emaintenancedata.TempletDataType=2" + " and emaintenancedata.BatchNumber<>\'" + batchNumber + "\'");
            if (dataCount == 0)
            {
                this.DeleteByHql(" from EMaintenanceData emaintenancedata where emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                                              " and emaintenancedata.ItemDate=\'" + itemDate + "\'" + " and emaintenancedata.TempletTypeCode=\'" + typeCode + "\'" +
                                              " and emaintenancedata.TempletDataType=1");
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 模板删除及模板相关数据删除
        /// </summary>
        /// <param name="templetID">模板ID</param>
        /// /// <param name="isDelTempletData">是否删除模板相关数据</param>
        /// <returns></returns>
        public BaseResultDataValue TempletDataDelete(long templetID, bool isDelTempletData)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<EReportData> listEReportData = IBEReportData.SearchListByHQL(" ereportdata.IsCheck=1 and ereportdata.ETemplet.Id=" + templetID.ToString(), 1, 1);
            if (listEReportData != null && listEReportData.count > 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "该模板存在已审核的记录，不能删除！";
                return baseResultDataValue;
            }
            List<string> listSQL = new List<string>();
            if (isDelTempletData)
            {
                listSQL.Add(" delete from E_MaintenanceData where TempletID=" + templetID.ToString());
                listSQL.Add(" delete from E_TempletEmp where TempletID=" + templetID.ToString());
                listSQL.Add(" delete from E_TempletRes where TempletID=" + templetID.ToString());
                listSQL.Add(" delete from E_Attachment where TempletID=" + templetID.ToString());
                listSQL.Add(" delete from E_ReportData where IsCheck=0 and TempletID=" + templetID.ToString());
            }
            listSQL.Add(" delete from E_Templet where TempletID=" + templetID.ToString());
            baseResultDataValue.success = ZhiFang.DBUpdate.PM.SqlServerHelper.ExecuteSqlList(listSQL, ZhiFang.DBUpdate.PM.DBUpdate.ADOConnectStr);
            if (!baseResultDataValue.success)
                baseResultDataValue.ErrorInfo = "模板及其相关数据删除失败，请查看日志信息！";
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteMaintenanceData(long templetID, string templetBatNo, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            beginDate = DateTime.Parse(beginDate).ToString("yyyy-MM-dd");
            endDate = DateTime.Parse(endDate).ToString("yyyy-MM-dd");
            string whereHQL = "";
            if (!string.IsNullOrWhiteSpace(templetBatNo))
                whereHQL = whereHQL + " and emaintenancedata.TempletBatNo=\'" + templetBatNo + "\'";
            int intDelCount = this.DeleteByHql(" from EMaintenanceData emaintenancedata " +
                " where emaintenancedata.ETemplet.Id=" + templetID.ToString() + whereHQL +
                " and emaintenancedata.ItemDate>=\'" + beginDate + "\' and emaintenancedata.ItemDate<=\'" + endDate + "\'");
            return baseResultDataValue;
        }

        public BaseResultDataValue FillMaintenanceDataToExcel(long templetID, long employeeID, string beginDate, string endDate, string templetBatNo, string checkView)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ETemplet templet = IBETemplet.Get(templetID);
            beginDate = DateTime.Parse(beginDate).ToString("yyyy-MM-dd");
            endDate = DateTime.Parse(endDate).ToString("yyyy-MM-dd");
            if (templet == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法根据模板ID获取模板信息";
                ZhiFang.Common.Log.Log.Info("无法根据模板ID获取模板信息,模板ID：" + templetID.ToString());
                return baseResultDataValue;
            }
            templet.CheckView = checkView;
            bool isWrapText = (IBEParameter.QueryPara("QualityRecord", "CellIsWrapText") == "1");
            string para = IBEParameter.QueryPara("QualityRecord", "FillDataType");
            string TempletFillStruct = templet.TempletFillStruct;
            if (para == "1")
            {
                TempletFillStruct = templet.FillStruct;
                if (string.IsNullOrEmpty(TempletFillStruct))
                {
                    BaseResultDataValue brdv = IBETemplet.EditETempletFillStruct(templet.Id);
                    if (brdv.success)
                        TempletFillStruct = brdv.ResultDataValue;
                }
            }
            if (string.IsNullOrEmpty(TempletFillStruct))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "模板的填充结构信息为空";
                ZhiFang.Common.Log.Log.Info("模板的填充结构信息为空,模板ID：" + templetID.ToString());
                return baseResultDataValue;
            }

            string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
            string extName = Path.GetExtension(parentPath + templet.TempletPath);
            string dicFile = parentPath + "\\" + templet.LabID.ToString() + "\\QMS\\TempEquipExcelFile";
            string excelFile = dicFile + "\\" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + extName;
            if (!Directory.Exists(dicFile))
            {
                Directory.CreateDirectory(dicFile);
            }
            Dictionary<string, string> dicCellValue = null;
            //string isFillEmptyCell = ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsFillEmptyCell");
            string isFillEmptyCell = IBEParameter.QueryPara("QualityRecord", "FillEmptyCellType");
            string strHQL = "emaintenancedata.ETemplet.Id=" + templetID.ToString() +
                " and emaintenancedata.ItemDate>=\'" + beginDate + "\' and emaintenancedata.ItemDate<=\'" + endDate + "\'";
            if (!string.IsNullOrEmpty(templetBatNo))
                strHQL = strHQL + " and emaintenancedata.TempletBatNo=\'" + templetBatNo + "\'";
            IList<EMaintenanceData> listEMaintenanceData = this.SearchListByHQL(strHQL);
            //if ((listEMaintenanceData != null) && (listEMaintenanceData.Count > 0))
            if (para == "1")
                dicCellValue = _FillExcelCellNew(templet, DateTime.Parse(beginDate), TempletFillStruct, listEMaintenanceData, employeeID);
            else
                dicCellValue = _FillExcelCell(templet, DateTime.Parse(beginDate), TempletFillStruct, listEMaintenanceData, employeeID);
            baseResultDataValue.success = MyNPOIHelper.FillExcelMoudleSheet(parentPath + templet.TempletPath, excelFile, dicCellValue, isFillEmptyCell, isWrapText);
            if (baseResultDataValue.success)
                baseResultDataValue.ResultDataValue = excelFile;
            return baseResultDataValue;
        }

        public BaseResultDataValue PreviewExcelTemplet(long templetID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ETemplet templet = IBETemplet.Get(templetID);
            if (templet == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法根据模板ID获取模板信息";
                ZhiFang.Common.Log.Log.Info("无法根据模板ID获取模板信息,模板ID：" + templetID.ToString());
                return baseResultDataValue;
            }
            bool isWrapText = (IBEParameter.QueryPara("QualityRecord", "CellIsWrapText") == "1");
            string TempletFillStruct = templet.TempletFillStruct;
            if (string.IsNullOrEmpty(TempletFillStruct))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "模板的填充结构信息为空";
                ZhiFang.Common.Log.Log.Info("模板的填充结构信息为空,模板ID：" + templetID.ToString());
                return baseResultDataValue;
            }
            string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
            string extName = Path.GetExtension(parentPath + templet.TempletPath);
            string dicFile = parentPath + "\\" + templet.LabID.ToString() + "\\QMS\\TempEquipExcelFile";
            string excelFile = dicFile + "\\" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + extName;
            string isFillEmptyCell = ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsFillEmptyCell");
            if (!Directory.Exists(dicFile))
            {
                Directory.CreateDirectory(dicFile);
            }
            baseResultDataValue.success = MyNPOIHelper.FillExcelMoudleSheet(parentPath + templet.TempletPath, excelFile, null, isFillEmptyCell, isWrapText);
            if (baseResultDataValue.success)
            {
                parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath") + "\\" + templet.LabID.ToString() + "\\QMS\\TempEquipPDFFile";
                string pdfFile = parentPath + "\\" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + ".pdf";
                if (!Directory.Exists(parentPath))
                {
                    Directory.CreateDirectory(parentPath);
                }
                baseResultDataValue.success = ExcelHelp.ExcelToPDF(excelFile, pdfFile);
                if (baseResultDataValue.success)
                    baseResultDataValue.ResultDataValue = pdfFile;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ExcelToPdfFile(long templetID, long employeeID, string beginDate, string endDate, string templetBatNo, bool isPreview, string checkView)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //BaseResultDataValue brdv = IBEReportData.QueryReportIsChecked(templetID, DateTime.Parse(beginDate));
            string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
            //if (!brdv.success)
            //{
            //    baseResultDataValue.success = true;
            //    baseResultDataValue.ResultDataValue = parentPath + brdv.ResultDataValue;
            //    return baseResultDataValue;
            //}
            baseResultDataValue = FillMaintenanceDataToExcel(templetID, employeeID, beginDate, endDate, templetBatNo, checkView);
            if (baseResultDataValue.success)
            {
                try
                {
                    ETemplet templet = IBETemplet.Get(templetID);
                    string equipName = IBETemplet.QueryEquipTempletNameByID(templetID, true);
                    if (isPreview)
                        parentPath = parentPath + "\\" + templet.LabID.ToString() + "\\QMS\\TempEquipPDFFile";
                    else
                        parentPath = parentPath + "\\" + templet.LabID.ToString() + "\\QMS\\EquipPDFFile\\" + equipName;
                    string pdfFile = parentPath + "\\" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + ".pdf";
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    baseResultDataValue.success = ExcelHelp.ExcelToPDF(baseResultDataValue.ResultDataValue, pdfFile);
                    if (baseResultDataValue.success)
                        baseResultDataValue.ResultDataValue = pdfFile;
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.ErrorInfo = ex.Message;
                    ZhiFang.Common.Log.Log.Info("ExcelToPdfFile：" + ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return baseResultDataValue;
        }

        #region
        private Dictionary<string, string> _FillExcelCellNew(ETemplet templet, DateTime reportDate, string fillFormat, IList<EMaintenanceData> listEMaintenanceData, long employeeID)
        {
            //fillFormat Excel填充格式字符串
            Dictionary<string, string> dicCellValue = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(fillFormat))
                return dicCellValue;
            string[] arrayFormat = fillFormat.Split('&');
            if (fillFormat == null || fillFormat.Length == 0)
                return dicCellValue;
            HREmployee emp = IBHREmployee.Get(employeeID);
            foreach (string format in arrayFormat)
            {
                string[] arrayCell = format.Split(',');
                if (arrayCell != null && arrayCell.Length >= 3)
                {
                    string cellFormatValue = arrayCell[2];
                    if (string.IsNullOrWhiteSpace(cellFormatValue))
                        continue;
                    IList<string> listMoudleCell = IBETemplet.GetNeedContentByExpression(cellFormatValue, "(?<={)[^{}]+(?=})");

                    if (listMoudleCell == null || listMoudleCell.Count == 0)
                        continue;
                    string tempFormat = cellFormatValue;
                    foreach (string strRule in listMoudleCell)
                    {
                        string[] arrayChild = null;
                        bool isGetFormat = _GetFillExcelCellFormat(arrayCell, strRule, ref arrayChild);
                        arrayChild[22] = tempFormat;
                        if (isGetFormat && arrayChild != null && arrayChild.Length >= 15)
                        {

                            string templetType = arrayChild[2].ToUpper();
                            if (templetType == "MD")
                            {
                                if (arrayChild[14].ToUpper() != "ML")
                                    _FillMDExcelCell(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                                else
                                    _FillMDExcelCell_MemoList(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                            }
                            else if (templetType == "MW")
                            {
                                _FillMWExcelCell(templet.Id, reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                            }
                            else if (templetType == "MM")
                            {
                                _FillMMExcelCell(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                            }
                            else if (templetType == "SP")
                            {
                                _FillSPExcelCell(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                            }
                            else if (templetType.Length == 2 && templetType.Substring(0, 1).ToUpper() == "T")
                            {
                                _FillTBExcelCell(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                            }
                            else if (templetType == "ES")
                            {
                                _FillESExcelCell(templet, reportDate, arrayChild, emp, dicCellValue);
                            }
                            else if (templetType == "RD")
                            {
                                _FillRDExcelCell(reportDate, arrayChild, dicCellValue);
                            }
                            else if (templetType == "EQ")
                            {
                                _FillEQExcelCell(templet, reportDate, arrayChild, dicCellValue);
                            }
                            else if (templetType == "PG")
                            {
                                _FillPGExcelCell(templet, reportDate, arrayChild, dicCellValue);
                            }
                            else if (templetType == "FC")
                            {
                                _FillFCExcelCell(templet, reportDate, arrayChild, dicCellValue);
                            }
                            else
                            {
                                _FillCommonExcelCell(templet, reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                            }
                            tempFormat = arrayChild[22];
                        }
                    }//foreach Rule
                }//if
            } //foreach cell
            return dicCellValue;
        }

        #endregion

        private Dictionary<string, string> _FillExcelCell(ETemplet templet, DateTime reportDate, string fillFormat, IList<EMaintenanceData> listEMaintenanceData, long employeeID)
        {
            Dictionary<string, string> dicCellValue = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(fillFormat))
                return dicCellValue;
            string[] arrayFormat = fillFormat.Split(',');
            if (fillFormat == null || fillFormat.Length == 0)
                return dicCellValue;
            HREmployee emp = IBHREmployee.Get(employeeID);
            foreach (string format in arrayFormat)
            {
                //3|1&MD|每日保养&MD1|检查各管路是否泄漏&e|d1|h
                string[] arrayCell = format.Split('&');
                if (arrayCell != null && arrayCell.Length >= 4)
                {
                    string[] arrayChild = null;
                    bool isGetFormat = _GetFillExcelCellFormat(arrayCell, ref arrayChild);
                    if (isGetFormat && arrayChild != null && arrayChild.Length >= 15)
                    {
                        string templetType = arrayChild[2].ToUpper();
                        if (templetType == "MD")
                        {
                            if (arrayChild[14].ToUpper() != "ML")
                                _FillMDExcelCell(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                            else
                                _FillMDExcelCell_MemoList(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                        }
                        else if (templetType == "MW")
                        {
                            _FillMWExcelCell(templet.Id, reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                        }
                        else if (templetType == "MM")
                        {
                            _FillMMExcelCell(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                        }
                        else if (templetType == "SP")
                        {
                            _FillSPExcelCell(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                        }
                        else if (templetType.Length == 2 && templetType.Substring(0, 1).ToUpper() == "T")
                        {
                            _FillTBExcelCell(reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                        }
                        else if (templetType == "ES")
                        {
                            _FillESExcelCell(templet, reportDate, arrayChild, emp, dicCellValue);
                        }
                        else if (templetType == "RD")
                        {
                            _FillRDExcelCell(reportDate, arrayChild, dicCellValue);
                        }
                        else if (templetType == "EQ")
                        {
                            _FillEQExcelCell(templet, reportDate, arrayChild, dicCellValue);
                        }
                        else if (templetType == "PG")
                        {
                            _FillPGExcelCell(templet, reportDate, arrayChild, dicCellValue);
                        }
                        else if (templetType == "FC")
                        {
                            _FillFCExcelCell(templet, reportDate, arrayChild, dicCellValue);
                        }
                        else
                        {
                            _FillCommonExcelCell(templet, reportDate, arrayChild, listEMaintenanceData, dicCellValue);
                        }
                    }
                }
            }
            return dicCellValue;
        }

        private void _FillCommonExcelCell(ETemplet templet, DateTime reportDate, string[] arrayCell, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        {
            try
            {
                string[] arrayValue = _GetItemArrayValue(arrayCell);
                if (!string.IsNullOrEmpty(arrayCell[8]) && arrayCell[8].Length >= 2)//arrayCell[8]值为：MD1,MD2,SP1......
                {
                    string typeCode = arrayCell[8].Substring(0, 2);
                    string itemCode = arrayCell[8];
                    int intNum = 0;
                    if ((!string.IsNullOrEmpty(arrayCell[15])) && arrayCell[15].Length >= 2) //arrayCell[15]值为：d1,w1......
                    {
                        string num = arrayCell[15].Substring(1, arrayCell[15].Length - 1);
                        try
                        {
                            intNum = int.Parse(num) - 1;
                        }
                        catch
                        {
                            intNum = 0;
                        }
                        if (intNum > 0 && templet.CheckType == 0)
                        {
                            DateTime firstDay = new DateTime(reportDate.Year, reportDate.Month, 1);
                            reportDate = firstDay.AddDays(intNum);
                        }
                    }
                    IList<EMaintenanceData> listValue = null;
                    if (listEMaintenanceData != null && listEMaintenanceData.Count > 0)
                        if (itemCode.Length == 2)
                            listValue = listEMaintenanceData.Where(p => p.TempletDataType == 1 && p.TempletTypeCode.ToUpper() == typeCode.ToUpper() && p.ItemDate == reportDate).ToList();
                        else
                            listValue = listEMaintenanceData.Where(p => p.TempletDataType == 2 && p.TempletTypeCode.ToUpper() == typeCode.ToUpper() && p.TempletItemCode.ToUpper() == itemCode.ToUpper() && p.ItemDate == reportDate).ToList();
                    string cellValue = "";
                    if (listValue != null && listValue.Count > 0)
                    {
                        listValue = listValue.OrderByDescending(p => p.DataAddTime).ToList();
                        cellValue = _GetCellValue(listValue[0], arrayCell, arrayValue);
                    }
                    else
                        cellValue = _GetCellValue(null, arrayCell, arrayValue);
                    cellValue = _GetCellValueByRule(cellValue, arrayCell);
                    string key = arrayCell[0] + "," + arrayCell[1];
                    //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                    if (!dicCellValue.ContainsKey(key))
                        dicCellValue.Add(key, cellValue);
                    else
                        dicCellValue[key] = cellValue;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillCommonExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 填充每月数据-以天为单位
        /// </summary>
        /// <param name="reportDate">报表的月份（取该月份的任何一天）</param>
        /// <param name="arrayCell">格式数组</param>
        /// <param name="listEMaintenanceData">该月份的数据</param>
        /// <param name="dicCellValue">需要填充的单元格数据</param>
        private void _FillMDExcelCell(DateTime reportDate, string[] arrayCell, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        {
            try
            {
                string[] arrayValue = _GetItemArrayValue(arrayCell);
                DateTime itemDate = DateTime.Now;
                DateTime firstDay = new DateTime(reportDate.Year, reportDate.Month, 1);
                if (!string.IsNullOrEmpty(arrayCell[16]))//arrayCell[16]取值：空字符、H、V，代表是否横向或竖向填充
                {
                    bool isH = false;//日期水平方向递增
                    int rowIndex = int.Parse(arrayCell[0]);
                    int colIndex = int.Parse(arrayCell[1]);
                    int days = DateTime.DaysInMonth(reportDate.Year, reportDate.Month);
                    isH = (arrayCell[16].ToUpper() == "H");
                    if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                    {
                        int row = rowIndex;
                        int col = colIndex;
                        for (int i = 0; i < days; i++)
                        {
                            itemDate = firstDay.AddDays(i);
                            if (isH)
                                col = colIndex + i;
                            else
                                row = rowIndex + i;
                            arrayCell[0] = row.ToString();
                            arrayCell[1] = col.ToString();
                            _FillExcelOneCell(itemDate, arrayCell, arrayValue, listEMaintenanceData, dicCellValue);
                        }//for 
                    }
                }//if
                else
                {
                    int intNum = 0;
                    if ((!string.IsNullOrEmpty(arrayCell[15])) && arrayCell[15].Length >= 2)
                        intNum = int.Parse(arrayCell[15].Substring(1, arrayCell[15].Length - 1));
                    if (intNum > 0)
                        itemDate = firstDay.AddDays(intNum - 1);
                    else
                        itemDate = reportDate;
                    if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                        _FillExcelOneCell(itemDate, arrayCell, arrayValue, listEMaintenanceData, dicCellValue);
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillMDExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        private void _FillMDExcelCell_MemoList(DateTime reportDate, string[] arrayCell, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        {
            try
            {
                string[] arrayValue = _GetItemArrayValue(arrayCell);
                IList<EMaintenanceData> listValue = null;

                if (listEMaintenanceData != null && listEMaintenanceData.Count > 0)
                {
                    if (arrayCell[8].Length == 2)
                        listValue = listEMaintenanceData.Where(p => p.TempletDataType == 1 && p.TempletTypeCode == arrayCell[2]).OrderBy(p => p.ItemDate).ToList();
                }
                string cellValue = "";
                if (listValue != null && listValue.Count > 0)
                {
                    int i = 1;
                    foreach (EMaintenanceData emd in listValue)
                    {
                        if (string.IsNullOrEmpty(emd.ItemMemo))
                            continue;
                        string memo = i.ToString() + "." + ((DateTime)emd.ItemDate).ToString("yyyy-MM-dd") + " " + emd.ItemMemo + "。";
                        if (cellValue == "")
                            cellValue = memo;
                        else
                            cellValue += memo;
                        i++;
                    }
                }
                else
                    cellValue = "";
                cellValue = _GetCellValueByRule(cellValue, arrayCell);
                string key = arrayCell[0] + "," + arrayCell[1];
                if (!dicCellValue.ContainsKey(key))
                    dicCellValue.Add(key, cellValue);
                else
                    dicCellValue[key] = cellValue;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillMDExcelCell_MemoList：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 填充每月数据-以周为单位
        /// </summary>
        /// <param name="reportDate"></param>
        /// <param name="arrayCell"></param>
        /// <param name="listEMaintenanceData"></param>
        /// <param name="dicCellValue"></param>
        //private void _FillMWExcelCell(long templetID, DateTime reportDate, string[] arrayCell, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        //{
        //    try
        //    {
        //        string[] arrayValue = _GetItemArrayValue(arrayCell);
        //        int intMonday = 1;//星期几 1-代表星期一
        //        if (!string.IsNullOrEmpty(arrayCell[4]))
        //            intMonday = int.Parse(arrayCell[4]);
        //        bool isH = false;//日期水平方向递增
        //        int rowIndex = int.Parse(arrayCell[0]);
        //        int colIndex = int.Parse(arrayCell[1]);
        //        DateTime firstDay = new DateTime(reportDate.Year, reportDate.Month, 1);
        //        int dayMonth = DateTime.DaysInMonth(reportDate.Year, reportDate.Month);
        //        DateTime lastDay = new DateTime(reportDate.Year, reportDate.Month, dayMonth);
        //        DateTime itemDate = DateTime.Now;
        //        GregorianCalendar gc = new GregorianCalendar();
        //        DateTime dtMonday = DateTime.Now;
        //        for (int i = 0; i < 7; i++)
        //        {
        //            DateTime dtDay = firstDay.AddDays(i);
        //            DayOfWeek dof = gc.GetDayOfWeek(dtDay);
        //            if (((int)dof) == intMonday)
        //            {
        //                dtMonday = dtDay;
        //                break;
        //            }
        //        }
        //        DateTime beginDate = DateTime.Now;
        //        DateTime endDate = DateTime.Now;
        //        if (!string.IsNullOrEmpty(arrayCell[16]))
        //        {
        //            isH = (arrayCell[16].ToUpper() == "H");
        //            if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
        //            {
        //                int row = rowIndex;
        //                int col = colIndex;

        //                for (int i = 0; i < 6; i++)
        //                {
        //                    itemDate = dtMonday.AddDays(i * 7);
        //                    if (itemDate > lastDay)
        //                        break;
        //                    if (isH)
        //                        col = colIndex + i;
        //                    else
        //                        row = rowIndex + i;
        //                    arrayCell[0] = row.ToString();
        //                    arrayCell[1] = col.ToString();
        //                    _GetWeekBeginAndEndDate(itemDate, intMonday, ref beginDate, ref endDate);
        //                    if (endDate > lastDay)
        //                    {
        //                        IList<EMaintenanceData> listED = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
        //                             " and emaintenancedata.ItemDate>=\'" + beginDate + "\' and emaintenancedata.ItemDate<=\'" + endDate + "\'");
        //                        _FillExcelOneCell(beginDate, endDate, arrayCell, arrayValue, listED, dicCellValue);
        //                    }
        //                    else
        //                        _FillExcelOneCell(beginDate, endDate, arrayCell, arrayValue, listEMaintenanceData, dicCellValue);
        //                }//for 
        //            }
        //        }
        //        else if ((!string.IsNullOrEmpty(arrayCell[15])) && arrayCell[15].Length >= 2)
        //        {
        //            string num = arrayCell[15].Substring(1, arrayCell[15].Length - 1);
        //            int intNum = int.Parse(num);
        //            itemDate = dtMonday.AddDays((intNum - 1) * 7);
        //            if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
        //            {
        //                _GetWeekBeginAndEndDate(itemDate, intMonday, ref beginDate, ref endDate);
        //                if (endDate > lastDay)
        //                {
        //                    IList<EMaintenanceData> listED = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
        //                         " and emaintenancedata.ItemDate>=\'" + beginDate + "\' and emaintenancedata.ItemDate<=\'" + endDate + "\'");
        //                    _FillExcelOneCell(beginDate, endDate, arrayCell, arrayValue, listED, dicCellValue);
        //                }
        //                else
        //                    _FillExcelOneCell(beginDate, endDate, arrayCell, arrayValue, listEMaintenanceData, dicCellValue);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ZhiFang.Common.Log.Log.Info("_FillMWExcelCell：" + ex.Message);
        //        throw new Exception(ex.Message);
        //    }
        //}
        //private void _FillMWExcelCell(long templetID, DateTime reportDate, string[] arrayCell, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        //{
        //    try
        //    {
        //        string[] arrayValue = _GetItemArrayValue(arrayCell);
        //        int intMonday = 1;//星期几 1-代表星期一
        //        if (!string.IsNullOrEmpty(arrayCell[4]))
        //            intMonday = int.Parse(arrayCell[4]);
        //        bool isH = false;//日期水平方向递增
        //        int rowIndex = int.Parse(arrayCell[0]);
        //        int colIndex = int.Parse(arrayCell[1]);
        //        DateTime firstDay = new DateTime(reportDate.Year, reportDate.Month, 1);
        //        int dayMonth = DateTime.DaysInMonth(reportDate.Year, reportDate.Month);
        //        DateTime lastDay = new DateTime(reportDate.Year, reportDate.Month, dayMonth);
        //        DateTime itemDate = DateTime.Now;
        //        DateTime wBeginDate = DateTime.Now;//所在周的第一天
        //        DateTime wEndDate = DateTime.Now;//所在周的最后一天
        //        if (!string.IsNullOrEmpty(arrayCell[16]))
        //        {
        //            isH = (arrayCell[16].ToUpper() == "H");
        //            if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
        //            {
        //                int row = rowIndex;
        //                int col = colIndex;

        //                for (int i = 0; i < 6; i++)
        //                {
        //                    itemDate = firstDay.AddDays(i * 7);
        //                    if (itemDate > lastDay)
        //                        break;
        //                    if (isH)
        //                        col = colIndex + i;
        //                    else
        //                        row = rowIndex + i;
        //                    arrayCell[0] = row.ToString();
        //                    arrayCell[1] = col.ToString();
        //                    _GetWeekBeginAndEndDate(itemDate, ref wBeginDate, ref wEndDate);
        //                    if (wEndDate > lastDay || wBeginDate < firstDay)
        //                    {
        //                        IList<EMaintenanceData> listED = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
        //                             " and emaintenancedata.ItemDate>=\'" + wBeginDate + "\' and emaintenancedata.ItemDate<=\'" + wEndDate + "\'");
        //                        _FillExcelOneCell(wBeginDate, wEndDate, arrayCell, arrayValue, listED, dicCellValue);
        //                    }
        //                    else
        //                        _FillExcelOneCell(wBeginDate, wEndDate, arrayCell, arrayValue, listEMaintenanceData, dicCellValue);
        //                }//for 
        //            }
        //        }
        //        else if ((!string.IsNullOrEmpty(arrayCell[15])) && arrayCell[15].Length >= 2)
        //        {
        //            string num = arrayCell[15].Substring(1, arrayCell[15].Length - 1);
        //            int intNum = int.Parse(num);
        //            itemDate = firstDay.AddDays((intNum - 1) * 7);
        //            if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
        //            {
        //                _GetWeekBeginAndEndDate(itemDate, ref wBeginDate, ref wEndDate);
        //                if (wEndDate > lastDay || wBeginDate< firstDay)
        //                {
        //                    IList<EMaintenanceData> listED = this.SearchListByHQL("emaintenancedata.ETemplet.Id=" + templetID.ToString() +
        //                         " and emaintenancedata.ItemDate>=\'" + wBeginDate + "\' and emaintenancedata.ItemDate<=\'" + wEndDate + "\'");
        //                    _FillExcelOneCell(wBeginDate, wEndDate, arrayCell, arrayValue, listED, dicCellValue);
        //                }
        //                else
        //                    _FillExcelOneCell(wBeginDate, wEndDate, arrayCell, arrayValue, listEMaintenanceData, dicCellValue);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ZhiFang.Common.Log.Log.Info("_FillMWExcelCell：" + ex.Message);
        //        throw new Exception(ex.Message);
        //    }
        //}

        private void _FillMWExcelCell(long templetID, DateTime reportDate, string[] arrayCell, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        {
            try
            {
                string[] arrayValue = _GetItemArrayValue(arrayCell);
                int intMonday = 1;//星期几 1-代表星期一
                if (!string.IsNullOrEmpty(arrayCell[4]))
                    intMonday = int.Parse(arrayCell[4]);
                bool isH = false;//日期水平方向递增
                int rowIndex = int.Parse(arrayCell[0]);
                int colIndex = int.Parse(arrayCell[1]);
                DateTime firstDay = new DateTime(reportDate.Year, reportDate.Month, 1);
                int dayMonth = DateTime.DaysInMonth(reportDate.Year, reportDate.Month);
                DateTime lastDay = new DateTime(reportDate.Year, reportDate.Month, dayMonth);
                DateTime wBeginDate = DateTime.Now;//所在周的第一天
                DateTime wEndDate = DateTime.Now;//所在周的最后一天
                if (!string.IsNullOrEmpty(arrayCell[16]))
                {
                    isH = (arrayCell[16].ToUpper() == "H");
                    if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                    {
                        int row = rowIndex;
                        int col = colIndex;
                        bool isBraek = false;
                        for (int i = 0; i < 6; i++)
                        {
                            wBeginDate = firstDay.AddDays(i * 7);
                            wEndDate = firstDay.AddDays((i + 1) * 7).AddDays(-1);
                            if (wBeginDate >= lastDay || wEndDate >= lastDay)
                            {
                                isBraek = true;
                                wEndDate = lastDay;
                            }
                            if (isH)
                                col = colIndex + i;
                            else
                                row = rowIndex + i;
                            arrayCell[0] = row.ToString();
                            arrayCell[1] = col.ToString();
                            _FillExcelOneCell(wBeginDate, wEndDate, arrayCell, arrayValue, listEMaintenanceData, dicCellValue);
                            if (isBraek)
                                break;
                        }//for 
                    }
                }
                else if ((!string.IsNullOrEmpty(arrayCell[15])) && arrayCell[15].Length >= 2)
                {

                    string num = arrayCell[15].Substring(1, arrayCell[15].Length - 1);
                    int intNum = int.Parse(num);
                    if (intNum < 1 || intNum > 5)
                        throw new Exception("周保养模板维护规则中的周序号必须为1-5之间的数字！");
                    wBeginDate = firstDay.AddDays((intNum - 1) * 7);
                    wEndDate = firstDay.AddDays(intNum * 7).AddDays(-1);
                    if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                        _FillExcelOneCell(wBeginDate, wEndDate, arrayCell, arrayValue, listEMaintenanceData, dicCellValue);
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillMWExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private void _FillMMExcelCell(DateTime reportDate, string[] arrayCell, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        {
            try
            {
                string[] arrayValue = _GetItemArrayValue(arrayCell);
                DateTime itemDate = DateTime.Now;
                DateTime firstDay = new DateTime(reportDate.Year, reportDate.Month, 1);
                int dayMonth = DateTime.DaysInMonth(reportDate.Year, reportDate.Month);
                DateTime lastDay = new DateTime(reportDate.Year, reportDate.Month, dayMonth);
                if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                {
                    if ((!string.IsNullOrEmpty(arrayCell[15])) && arrayCell[15].Length >= 2)
                    {
                        string num = arrayCell[15].Substring(1, arrayCell[15].Length - 1);
                        int intNum = int.Parse(num);
                        itemDate = firstDay.AddDays(intNum - 1);
                        if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                            _FillExcelOneCell(itemDate, arrayCell, arrayValue, listEMaintenanceData, dicCellValue);
                    }
                    else
                    {
                        lastDay = lastDay.AddDays(1);
                        IList<EMaintenanceData> listValue = null;
                        if (listEMaintenanceData != null && listEMaintenanceData.Count > 0)
                        {
                            if (arrayCell[8].Length == 2)
                                listValue = listEMaintenanceData.Where(p => p.TempletDataType == 1 && p.TempletTypeCode == arrayCell[2] && p.ItemDate >= firstDay && p.ItemDate < lastDay).OrderByDescending(p => p.ItemDate).ToList();
                            else if (arrayCell[8].Length >= 3)
                                listValue = listEMaintenanceData.Where(p => p.TempletDataType == 2 && p.TempletTypeCode == arrayCell[2] && p.TempletItemCode == arrayCell[8] && p.ItemDate >= firstDay && p.ItemDate < lastDay).OrderByDescending(p => p.ItemDate).ToList();
                        }
                        string cellValue = "";
                        if (listValue != null && listValue.Count > 0)
                            cellValue = _GetCellValue(listValue[0], arrayCell, arrayValue);
                        else
                            cellValue = _GetCellValue(null, arrayCell, arrayValue);
                        cellValue = _GetCellValueByRule(cellValue, arrayCell);
                        string key = arrayCell[0] + "," + arrayCell[1];
                        //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                        if (!dicCellValue.ContainsKey(key))
                            dicCellValue.Add(key, cellValue);
                        else
                            dicCellValue[key] = cellValue;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillMMExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private void _FillTBExcelCell(DateTime reportDate, string[] arrayCell, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        {
            try
            {
                string[] arrayValue = _GetItemArrayValue(arrayCell);
                if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                {
                    IList<EMaintenanceData> listValue = null;
                    if (listEMaintenanceData == null || listEMaintenanceData.Count == 0)
                        return;
                    IList<EMaintenanceData> listTBValue = listEMaintenanceData.Where(p => p.TempletTypeCode == arrayCell[2]).OrderBy(p => p.DataAddTime).ToList();
                    if (listTBValue == null || listTBValue.Count == 0)
                        return;
                    List<DateTime> listItemDate = new List<DateTime>();
                    var lisItem = from EMaintenanceData in listTBValue
                                  group EMaintenanceData by
                                      new
                                      {
                                          TempletTypeCode = EMaintenanceData.TempletTypeCode,
                                          ItemDate = EMaintenanceData.ItemDate,
                                          BatchNumber = EMaintenanceData.BatchNumber
                                      } into g
                                  select new
                                  {
                                      ItemDate = g.Key.ItemDate,
                                      BatchNumber = g.Key.BatchNumber
                                  };
                    if (lisItem == null)
                        return;
                    //IList<TBMaintenanceData> listTBData = (IList<TBMaintenanceData>)lisItem;
                    bool isH = (arrayCell[16].ToUpper() == "H");//日期水平方向递
                    int row = int.Parse(arrayCell[0]);
                    int col = int.Parse(arrayCell[1]);
                    int startIndex = 0;
                    int endIndex = lisItem.ToList().Count;
                    if (string.IsNullOrWhiteSpace(arrayCell[16]))
                    {
                        int intNum = 0;
                        if ((!string.IsNullOrEmpty(arrayCell[15])) && arrayCell[15].Length >= 2)
                            intNum = int.Parse(arrayCell[15].Substring(1, arrayCell[15].Length - 1));
                        if (intNum > 0)
                        {
                            startIndex = intNum - 1;
                            endIndex = startIndex + 1;
                        }
                        else
                        {
                            startIndex = 0;
                            endIndex = 1;
                        }
                    }
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (i >= lisItem.ToList().Count)
                            break;
                        var item = lisItem.ToList()[i];
                        //}
                        //foreach (var item in lisItem)
                        //{
                        if (arrayCell[8].Length == 2)
                        {
                            listValue = listTBValue.Where(p => p.TempletDataType == 1 && p.TempletTypeCode == arrayCell[2] && p.ItemDate == item.ItemDate && p.BatchNumber == item.BatchNumber).ToList();
                            if (listValue == null || listValue.Count == 0)
                                listValue = listTBValue.Where(p => p.TempletDataType == 1 && p.TempletTypeCode == arrayCell[2] && p.ItemDate == item.ItemDate).ToList();
                        }
                        else if (arrayCell[8].Length >= 3)
                            listValue = listTBValue.Where(p => p.TempletDataType == 2 && p.TempletTypeCode == arrayCell[2] && p.TempletItemCode == arrayCell[8] && p.ItemDate == item.ItemDate && p.BatchNumber == item.BatchNumber).ToList();

                        string cellValue = "";
                        if (listValue != null && listValue.Count > 0)
                            cellValue = _GetCellValue(listValue[0], arrayCell, arrayValue);
                        else
                            cellValue = _GetCellValue(null, arrayCell, arrayValue);
                        cellValue = _GetCellValueByRule(cellValue, arrayCell);
                        string key = row.ToString() + "," + col.ToString();
                        //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                        if (!dicCellValue.ContainsKey(key))
                            dicCellValue.Add(key, cellValue);
                        else
                            dicCellValue[key] = cellValue;
                        if (isH)
                            col++;
                        else
                            row++;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillTBExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private void _FillSPExcelCell(DateTime reportDate, string[] arrayCell, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        {
            try
            {
                string[] arrayValue = _GetItemArrayValue(arrayCell);
                bool isLastDate = false;
                if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                {
                    int intNum = 0;
                    if ((!string.IsNullOrEmpty(arrayCell[15])) && arrayCell[15].Length >= 2)
                    {
                        string num = arrayCell[15].Substring(1, arrayCell[15].Length - 1);
                        try
                        {
                            intNum = int.Parse(num) - 1;
                        }
                        catch
                        {
                            intNum = 0;
                        }
                    }
                    else//没有为D1、D2...规则,如：{SP1|e}
                        isLastDate = true;
                    IList<EMaintenanceData> listValue = null;
                    if (listEMaintenanceData != null && listEMaintenanceData.Count > 0)
                    {
                        IList<EMaintenanceData> listSPValue = listEMaintenanceData.Where(p => p.TempletTypeCode.ToUpper() == arrayCell[2].ToUpper()).ToList();
                        if (listSPValue == null || listSPValue.Count == 0)
                            return;
                        List<DateTime> listItemDate = new List<DateTime>();
                        var lisItem = from EMaintenanceData in listSPValue
                                      group EMaintenanceData by
                                          new
                                          {
                                              TempletTypeCode = EMaintenanceData.TempletTypeCode,
                                              ItemDate = EMaintenanceData.ItemDate
                                          } into g
                                      select new
                                      {
                                          ItemDate = g.Key.ItemDate
                                      };
                        if (lisItem != null)
                        {
                            foreach (var item in lisItem)
                                listItemDate.Add((DateTime)item.ItemDate);
                            listItemDate = listItemDate.OrderBy(p => p).ToList();
                        }
                        if (intNum < listItemDate.Count)
                        {
                            if (isLastDate) //取最后一次维护的数据
                                intNum = listItemDate.Count - 1;
                            if (arrayCell[8].Length == 2)
                                listValue = listSPValue.Where(p => p.TempletDataType == 1 && p.TempletTypeCode == arrayCell[2] && p.ItemDate == listItemDate[intNum]).ToList();
                            else if (arrayCell[8].Length >= 3)
                                listValue = listSPValue.Where(p => p.TempletDataType == 2 && p.TempletTypeCode == arrayCell[2] && p.TempletItemCode == arrayCell[8] && p.ItemDate == listItemDate[intNum]).ToList();
                        }
                    }
                    string cellValue = "";
                    if (listValue != null && listValue.Count > 0)
                        cellValue = _GetCellValue(listValue[0], arrayCell, arrayValue);
                    else
                        cellValue = _GetCellValue(null, arrayCell, arrayValue);
                    cellValue = _GetCellValueByRule(cellValue, arrayCell);
                    string key = arrayCell[0] + "," + arrayCell[1];
                    //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                    if (!dicCellValue.ContainsKey(key))
                        dicCellValue.Add(key, cellValue);
                    else
                        dicCellValue[key] = cellValue;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillSPExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //ES报表审核人
        private void _FillESExcelCell(ETemplet templet, DateTime reportDate, string[] arrayCell, HREmployee emp, Dictionary<string, string> dicCellValue)
        {
            try
            {
                if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                {
                    if (emp != null)
                    {
                        string key = arrayCell[0] + "," + arrayCell[1];
                        string cellValue = "";
                        if (arrayCell[14].ToUpper() == "N")
                            cellValue = emp.CName;
                        else if (arrayCell[14].ToUpper() == "P")
                        {
                            string filePath = (string)IBBParameter.GetCache(BParameterParaNo.UploadEmpSignPath.ToString());
                            if (!string.IsNullOrEmpty(filePath))
                            {
                                string filename = emp.Id.ToString() + ".png";
                                key += "," + "P" + "," + emp.CName;
                                cellValue = filePath + "\\" + filename;
                            }
                        }
                        else if (arrayCell[14].ToUpper() == "D")
                        {
                            string[] arrayValue = _GetItemArrayValue(arrayCell);
                            string dateFormat = arrayValue[0];
                            if (!string.IsNullOrWhiteSpace(dateFormat))
                                dateFormat = dateFormat.ToUpper().Replace("Y", "y").Replace("D", "d").Replace("S", "s");
                            else
                                dateFormat = "yyyy-MM-dd";
                            cellValue = DateTime.Now.ToString(dateFormat);
                        }
                        else if (arrayCell[14].ToUpper() == "I")
                        {
                            cellValue = templet.CheckView;
                        }
                        cellValue = _GetCellValueByRule(cellValue, arrayCell);
                        //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                        if (!dicCellValue.ContainsKey(key))
                            dicCellValue.Add(key, cellValue);
                        else
                            dicCellValue[key] = cellValue;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillESExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //RD(Report Date)报表日期
        private void _FillRDExcelCell(DateTime reportDate, string[] arrayCell, Dictionary<string, string> dicCellValue)
        {
            try
            {
                string cellValue = "";
                if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])))
                {
                    string[] arrayValue = _GetItemArrayValue(arrayCell);
                    string dateFormat = arrayValue[0];
                    if (!string.IsNullOrWhiteSpace(dateFormat))
                        dateFormat = dateFormat.ToUpper().Replace("Y", "y").Replace("D", "d").Replace("S", "s");
                    else
                        dateFormat = "";

                    if (arrayCell[14].ToUpper() == "M")
                        cellValue = reportDate.ToString((dateFormat == "") ? "yyyy-MM" : dateFormat);
                    else if (arrayCell[14].ToUpper() == "D")
                        cellValue = reportDate.ToString((dateFormat == "") ? "yyyy-MM-dd" : dateFormat);
                    else if (arrayCell[14].ToUpper() == "Y")
                        cellValue = reportDate.ToString((dateFormat == "") ? "yyyy" : dateFormat);
                    else if (arrayCell[14].ToUpper() == "W")
                    {
                        int wIndex = (int)reportDate.DayOfWeek;
                        if (dateFormat == "R")
                        {
                            cellValue = wIndex == 0 ? "日" : NumberToChinese(wIndex.ToString());
                        }
                        else
                        {
                            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                            string[] weekdays1 = { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
                            if (dateFormat == "C1")
                                cellValue = weekdays1[wIndex];
                            else
                                cellValue = weekdays[wIndex];
                        }
                    }
                    cellValue = _GetCellValueByRule(cellValue, arrayCell);
                    string key = arrayCell[0] + "," + arrayCell[1];
                    //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                    if (!dicCellValue.ContainsKey(key))
                        dicCellValue.Add(key, cellValue);
                    else
                        dicCellValue[key] = cellValue;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillRDExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //EQ(Equip)仪器信息
        private void _FillEQExcelCell(ETemplet templet, DateTime reportDate, string[] arrayCell, Dictionary<string, string> dicCellValue)
        {
            try
            {
                if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])) && (templet.EEquip != null))
                {
                    string cellValue = "";
                    if (arrayCell[14].ToUpper() == "N")
                        cellValue = templet.EEquip.CName;
                    else if (arrayCell[14].ToUpper() == "E")
                        cellValue = templet.EEquip.EName;
                    else if (arrayCell[14].ToUpper() == "S")
                        cellValue = templet.EEquip.Shortcode;
                    else if (arrayCell[14].ToUpper() == "C")
                        cellValue = templet.EEquip.UseCode;
                    cellValue = _GetCellValueByRule(cellValue, arrayCell);
                    string key = arrayCell[0] + "," + arrayCell[1];
                    //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                    if (!dicCellValue.ContainsKey(key))
                        dicCellValue.Add(key, cellValue);
                    else
                        dicCellValue[key] = cellValue;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillRDExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //PG(PGroup)检验小组
        private void _FillPGExcelCell(ETemplet templet, DateTime reportDate, string[] arrayCell, Dictionary<string, string> dicCellValue)
        {
            try
            {
                if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])) && (templet.Section != null))
                {
                    string cellValue = "";
                    if (arrayCell[14].ToUpper() == "N")
                        cellValue = templet.Section.CName;
                    else if (arrayCell[14].ToUpper() == "E")
                        cellValue = templet.Section.EName;
                    else if (arrayCell[14].ToUpper() == "S")
                        cellValue = templet.Section.Shortcode;
                    else if (arrayCell[14].ToUpper() == "C")
                        cellValue = templet.Section.UseCode;
                    cellValue = _GetCellValueByRule(cellValue, arrayCell);
                    string key = arrayCell[0] + "," + arrayCell[1];
                    //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                    if (!dicCellValue.ContainsKey(key))
                        dicCellValue.Add(key, cellValue);
                    else
                        dicCellValue[key] = cellValue;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillRDExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //FC(Formula Cell)公式单元格
        private void _FillFCExcelCell(ETemplet templet, DateTime reportDate, string[] arrayCell, Dictionary<string, string> dicCellValue)
        {
            try
            {
                if ((!string.IsNullOrEmpty(arrayCell[2])) && (!string.IsNullOrEmpty(arrayCell[8])) && (!string.IsNullOrEmpty(arrayCell[14])))
                {
                    string cellValue = arrayCell[14];
                    if (cellValue.ToUpper() == "D")
                    {
                        string key = arrayCell[0] + "," + arrayCell[1] + "," + arrayCell[2];
                        if (!dicCellValue.ContainsKey(key))
                            dicCellValue.Add(key, cellValue);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(arrayCell[16]))//arrayCell[16]取值：空字符、H、V，代表是否横向或竖向填充
                        {
                            DateTime itemDate = DateTime.Now;
                            DateTime firstDay = new DateTime(reportDate.Year, reportDate.Month, 1);
                            bool isH = false;//日期水平方向递增
                            int rowIndex = int.Parse(arrayCell[0]);
                            int colIndex = int.Parse(arrayCell[1]);
                            int days = DateTime.DaysInMonth(reportDate.Year, reportDate.Month);
                            isH = (arrayCell[16].ToUpper() == "H");
                            int row = rowIndex;
                            int col = colIndex;
                            string colChar = cellValue.ToUpper();
                            for (int i = 0; i < days; i++)
                            {
                                string colValue = cellValue;
                                itemDate = firstDay.AddDays(i);
                                if (isH)
                                {
                                    col = colIndex + i;
                                    if (col < arrayExcelCol.Length)
                                    {
                                        colValue = colChar.Replace("COLNAME-", arrayExcelCol[col]);
                                    }
                                }
                                else
                                {
                                    row = rowIndex + i;
                                    colValue = colChar.Replace("-ROWINDEX", (row + 1).ToString());
                                }
                                arrayCell[0] = row.ToString();
                                arrayCell[1] = col.ToString();
                                string key = arrayCell[0] + "," + arrayCell[1] + "," + arrayCell[2];
                                if (!dicCellValue.ContainsKey(key))
                                    dicCellValue.Add(key, colValue);
                                else
                                    dicCellValue[key] = cellValue;
                            }//for 
                        }
                        else
                        {
                            string key = arrayCell[0] + "," + arrayCell[1] + "," + arrayCell[2];
                            if (!dicCellValue.ContainsKey(key))
                                dicCellValue.Add(key, cellValue);
                            else
                                dicCellValue[key] = cellValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillFCExcelCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 日保养填充
        /// </summary>
        /// <param name="itemDate"></param>
        /// <param name="arrayType"></param>
        /// <param name="arrayValue"></param>
        /// <param name="listEMaintenanceData"></param>
        /// <param name="dicCellValue"></param>
        private void _FillExcelOneCell(DateTime itemDate, string[] arrayType, string[] arrayValue, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        {
            //arrayCell[] 坐标说明
            //0：行，1：列，
            //2：大类型代码（例如：MD），3：大类型名称（例如：日保养），4：其他 
            //5：小类型代码（例如：MD1），6：小类型名称（例如：水管是否通畅），7：其他 
            //8：值类型，9：单元格，10：方向
            IList<EMaintenanceData> listValue = null;
            try
            {
                if (listEMaintenanceData != null && listEMaintenanceData.Count > 0)
                {
                    if (arrayType[8].Length == 2)
                        listValue = listEMaintenanceData.Where(p => p.TempletDataType == 1 && p.TempletTypeCode == arrayType[2] && p.ItemDate == itemDate).ToList();
                    else if (arrayType[8].Length >= 3)
                        listValue = listEMaintenanceData.Where(p => p.TempletDataType == 2 && p.TempletTypeCode == arrayType[2] && p.TempletItemCode == arrayType[8] && p.ItemDate == itemDate).ToList();
                }
                string cellValue = "";
                if (listValue != null && listValue.Count > 0)
                {
                    listValue = listValue.OrderByDescending(p => p.DataAddTime).ToList();
                    cellValue = _GetCellValue(listValue[0], arrayType, arrayValue);
                }
                else
                    cellValue = _GetCellValue(null, arrayType, arrayValue);
                cellValue = _GetCellValueByRule(cellValue, arrayType);
                string key = arrayType[0] + "," + arrayType[1];
                //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                if (!dicCellValue.ContainsKey(key))
                {
                    if (!string.IsNullOrEmpty(cellValue))
                        dicCellValue.Add(key, cellValue);
                    else if (itemDate < DateTime.Now)
                        dicCellValue.Add(key, cellValue);
                }
                else
                    dicCellValue[key] = cellValue;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillExcelOneCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 周保养填充
        /// </summary>
        /// <param name="itemBeginDate"></param>
        /// <param name="itemEndDate"></param>
        /// <param name="arrayType"></param>
        /// <param name="arrayValue"></param>
        /// <param name="listEMaintenanceData"></param>
        /// <param name="dicCellValue"></param>
        private void _FillExcelOneCell(DateTime itemBeginDate, DateTime itemEndDate, string[] arrayType, string[] arrayValue, IList<EMaintenanceData> listEMaintenanceData, Dictionary<string, string> dicCellValue)
        {
            IList<EMaintenanceData> listValue = null;
            try
            {
                if (listEMaintenanceData != null && listEMaintenanceData.Count > 0)
                {
                    if (arrayType[8].Length == 2)
                        listValue = listEMaintenanceData.Where(p => p.TempletDataType == 1 && p.TempletTypeCode == arrayType[2] && p.ItemDate >= itemBeginDate && p.ItemDate <= itemEndDate).OrderByDescending(p => p.ItemDate).ToList();
                    else if (arrayType[8].Length >= 3)
                        listValue = listEMaintenanceData.Where(p => p.TempletDataType == 2 && p.TempletTypeCode == arrayType[2] && p.TempletItemCode == arrayType[8] && p.ItemDate >= itemBeginDate && p.ItemDate <= itemEndDate).OrderByDescending(p => p.ItemDate).ToList();
                }
                string cellValue = "";
                if (listValue != null && listValue.Count > 0)
                {
                    listValue = listValue.OrderByDescending(p => p.DataAddTime).ToList();
                    cellValue = _GetCellValue(listValue[0], arrayType, arrayValue);
                }
                else
                    cellValue = _GetCellValue(null, arrayType, arrayValue);
                cellValue = _GetCellValueByRule(cellValue, arrayType);
                string key = arrayType[0] + "," + arrayType[1];
                //if ((!string.IsNullOrEmpty(cellValue)) && (!dicCellValue.ContainsKey(key)))
                if (!dicCellValue.ContainsKey(key))
                {
                    if (!string.IsNullOrEmpty(cellValue))
                        dicCellValue.Add(key, cellValue);
                    else if (itemEndDate < DateTime.Now)
                        dicCellValue.Add(key, cellValue);
                }
                else
                    dicCellValue[key] = cellValue;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("_FillExcelOneCell：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private string _GetCellValue(EMaintenanceData emaintenanceData, string[] arrayType, string[] arrayValue)
        {
            string cellValue = "";
            string itemDataType = arrayType[14].ToUpper();
            if (string.IsNullOrEmpty(itemDataType))
                itemDataType = "C";
            try
            {
                if (itemDataType == "E" || itemDataType == "E1")
                    cellValue = _GetCellValue_E(emaintenanceData, arrayValue);
                else if (itemDataType == "S" || itemDataType == "S1")
                    cellValue = _GetCellValue_S(emaintenanceData, arrayValue);
                else if (arrayNumber.Contains(itemDataType))
                    cellValue = _GetCellValue_Number(itemDataType, emaintenanceData, arrayValue);
                else if (arrayDate.Contains(itemDataType))
                    cellValue = _GetCellValue_Date(itemDataType, emaintenanceData, arrayValue);
                else if (itemDataType == "CL")
                    cellValue = _GetCellValue_CL(itemDataType, emaintenanceData, arrayValue);
                else if (arrayText.Contains(itemDataType))
                    cellValue = _GetCellValue_Text(itemDataType, emaintenanceData, arrayValue);
                else
                    cellValue = _GetCellValue_Text(itemDataType, emaintenanceData, arrayValue);

                if (itemDataType == "I" || itemDataType == "L")
                    itemDataType = "I";
                else if (itemDataType == "F" || itemDataType == "R")
                    itemDataType = "F";
                else if (arrayES.Contains(itemDataType) || arrayText.Contains(itemDataType))
                    itemDataType = "C";
                else if (arrayDate.Contains(itemDataType))
                    itemDataType = "D";

                if (!string.IsNullOrEmpty(cellValue))
                    cellValue = itemDataType + "|" + cellValue;
            }
            catch (Exception ex)
            {
                cellValue = "";
                ZhiFang.Common.Log.Log.Info("Excel模板【" + emaintenanceData.ETemplet.CName + "】数据格式错误，Error：" + ex.Message);
                //throw new Exception(ex.Message);
            }
            return cellValue;
        }

        private string _GetCellValue_E(EMaintenanceData emaintenanceData, string[] arrayValue)
        {
            string cellValue = "";
            //arrayValue[0] : "是";
            //arrayValue[1] : "否";
            //arrayValue[2] : "默认";
            string strY = string.IsNullOrEmpty(arrayValue[0]) ? "√" : arrayValue[0];
            string strN = string.IsNullOrEmpty(arrayValue[1]) ? "" : arrayValue[1];
            if (emaintenanceData != null)
            {
                string itemResult = (!string.IsNullOrWhiteSpace(emaintenanceData.ItemResult)) ? emaintenanceData.ItemResult : "";
                if (itemResult == "1")
                    cellValue = strY;
                else
                    cellValue = strN;
                cellValue = cellValue.Trim();
            }
            else
                cellValue = strN;
            return cellValue;
        }

        private string _GetCellValue_S(EMaintenanceData emaintenanceData, string[] arrayValue)
        {
            string cellValue = "";
            string strY = string.IsNullOrEmpty(arrayValue[0]) ? "√" : arrayValue[0];
            string strN = string.IsNullOrEmpty(arrayValue[1]) ? "×" : arrayValue[1];
            string strD = string.IsNullOrEmpty(arrayValue[2]) ? "" : arrayValue[2];
            if (emaintenanceData != null)
            {
                string itemResult = (!string.IsNullOrWhiteSpace(emaintenanceData.ItemResult)) ? emaintenanceData.ItemResult : "";

                if (itemResult == "1")
                    cellValue = strY;
                else if (itemResult == "2")
                    cellValue = strN;
                else
                    cellValue = strD;
                cellValue = cellValue.Trim();
            }
            else
                cellValue = strD;
            return cellValue;
        }

        private string _GetCellValue_Number(string itemDataType, EMaintenanceData emaintenanceData, string[] arrayValue)
        {
            string cellValue = "";
            if (emaintenanceData != null)
            {
                string itemResult = (!string.IsNullOrWhiteSpace(emaintenanceData.ItemResult)) ? emaintenanceData.ItemResult : "";
                switch (itemDataType)
                {
                    case "I":
                        if (!string.IsNullOrEmpty(itemResult))
                            cellValue = (int.Parse(itemResult)).ToString();
                        break;
                    case "L":
                        if (!string.IsNullOrEmpty(itemResult))
                            cellValue = (long.Parse(itemResult)).ToString();
                        break;
                    case "F":
                        if (!string.IsNullOrEmpty(itemResult))
                            cellValue = (double.Parse(itemResult)).ToString();
                        break;
                    case "R":
                        if (!string.IsNullOrEmpty(itemResult))
                            cellValue = (double.Parse(itemResult)).ToString();
                        break;
                }
                cellValue = cellValue.Trim();
            }
            return cellValue;
        }

        private string _GetCellValue_CL(string itemDataType, EMaintenanceData emaintenanceData, string[] arrayValue)
        {
            string cellValue = "";
            string itemResult = "";
            if (emaintenanceData != null)
                itemResult = (!string.IsNullOrWhiteSpace(emaintenanceData.ItemResult)) ? emaintenanceData.ItemResult : "";
            if (arrayValue[0].Trim() != "")
            {
                string strSplit = ",";
                if (arrayValue[3] != "")
                    strSplit = arrayValue[3];

                string[] arrayList = arrayValue[0].Split('#');
                string[] arrayChar = null;
                string fCharYes = "";
                string fCharNo = "";
                string bCharYes = "";
                string bCharNo = "";
                if (arrayValue[1].Trim() != "")
                {
                    arrayChar = arrayValue[1].Split('#');
                    if (arrayValue[2] == "" || arrayValue[2].ToUpper() == "F")
                    {
                        fCharYes = arrayChar[0];
                        fCharNo = arrayChar[1];
                    }
                    else
                    {
                        bCharYes = arrayChar[0];
                        bCharNo = arrayChar[1];
                    }
                }
                if (arrayChar != null && arrayChar.Length >= 2)
                {
                    string[] arrayResult = itemResult.Split(',');
                    for (int i = 0; i < arrayList.Length; i++)
                    {
                        if (arrayResult.Contains(arrayList[i]))
                            cellValue += strSplit + fCharYes + arrayList[i] + bCharYes;
                        else
                            cellValue += strSplit + fCharNo + arrayList[i] + bCharNo;
                    }
                }
                else
                {
                    for (int i = 0; i < arrayList.Length - 1; i++)
                        cellValue += strSplit + fCharNo + arrayList[i] + bCharNo;
                }
                if (strSplit.Length > 0)
                    cellValue = cellValue.Remove(0, strSplit.Length);
            }
            else
                cellValue = itemResult;

            return cellValue;
        }

        private string _GetCellValue_Text(string itemDataType, EMaintenanceData emaintenanceData, string[] arrayValue)
        {
            string cellValue = "";
            if (emaintenanceData != null)
            {
                string itemResult = (!string.IsNullOrWhiteSpace(emaintenanceData.ItemResult)) ? emaintenanceData.ItemResult : "";
                string itemMemo = (!string.IsNullOrWhiteSpace(emaintenanceData.ItemMemo)) ? emaintenanceData.ItemMemo : "";
                string operater = (!string.IsNullOrWhiteSpace(emaintenanceData.Operater)) ? emaintenanceData.Operater : "";
                switch (itemDataType)
                {
                    case "C"://
                        cellValue = itemResult;
                        break;
                    case "CL"://
                        cellValue = itemResult;
                        break;
                    case "M"://备注结果
                        cellValue = itemMemo;
                        break;
                    case "RP"://操作人
                        if (!string.IsNullOrEmpty(itemResult))
                            cellValue = operater;
                        break;
                    case "RM"://备注结果
                        if (!string.IsNullOrEmpty(itemResult))
                            cellValue = itemMemo;
                        break;
                    case "O"://操作人
                        cellValue = operater;
                        break;
                    case "OP"://操作人
                        cellValue = operater;
                        break;
                    case "OM"://备注结果
                        cellValue = itemMemo;
                        break;
                }
                cellValue = cellValue.Trim();
            }
            return cellValue;
        }

        private string _GetCellValue_Date(string itemDataType, EMaintenanceData emaintenanceData, string[] arrayValue)
        {
            string cellValue = "";
            if (emaintenanceData != null)
            {
                string itemResult = (!string.IsNullOrWhiteSpace(emaintenanceData.ItemResult)) ? emaintenanceData.ItemResult : "";
                string dateFormat = arrayValue[0];
                if (!string.IsNullOrWhiteSpace(dateFormat))
                    dateFormat = dateFormat.ToUpper().Replace("Y", "y").Replace("D", "d").Replace("S", "s");
                string dFormat = (dateFormat == "") ? "yyyy-MM-dd" : dateFormat;
                string tFormat = (dateFormat == "") ? "HH:mm:ss" : dateFormat;
                string dtFormat = (dateFormat == "") ? "yyyy-MM-dd HH:mm:ss" : dateFormat;
                DateTime? tempOperdateTime = emaintenanceData.DataAddTime;
                if (emaintenanceData.DataUpdateTime != null)
                    tempOperdateTime = emaintenanceData.DataUpdateTime;
                switch (itemDataType)
                {
                    case "D"://日期
                        if (!string.IsNullOrEmpty(itemResult))
                            cellValue = (DateTime.Parse(itemResult)).ToString(dFormat);
                        break;
                    case "T"://时间
                        if (!string.IsNullOrEmpty(itemResult))
                            cellValue = (DateTime.Parse(itemResult)).ToString(tFormat);
                        break;
                    case "DT"://日期时间
                        if (!string.IsNullOrEmpty(itemResult))
                            cellValue = (DateTime.Parse(itemResult)).ToString(dtFormat);
                        break;
                    case "RD"://操作日期
                        if ((emaintenanceData.OperateTime != null) && (!string.IsNullOrEmpty(itemResult)))
                            cellValue = ((DateTime)emaintenanceData.OperateTime).ToString(dFormat);
                        break;
                    case "RT"://操作时间
                        if ((emaintenanceData.OperateTime != null) && (!string.IsNullOrEmpty(itemResult)))
                            cellValue = ((DateTime)emaintenanceData.OperateTime).ToString(tFormat);
                        break;
                    case "RDT"://操作日期时间
                        if ((emaintenanceData.OperateTime != null) && (!string.IsNullOrEmpty(itemResult)))
                            cellValue = ((DateTime)emaintenanceData.OperateTime).ToString(dtFormat);
                        break;
                    case "OD"://操作日期
                        if (IBEParameter.QueryPara("QualityRecord", "IsQRDate") == "1")
                            tempOperdateTime = DateTime.Parse(((DateTime)emaintenanceData.ItemDate).ToString("yyyy-MM-dd") + " " + ((DateTime)emaintenanceData.DataUpdateTime).ToString("HH:mm:ss"));
                        cellValue = ((DateTime)tempOperdateTime).ToString(dFormat);
                        break;
                    case "OT"://操作时间
                        if (IBEParameter.QueryPara("QualityRecord", "IsQRDate") == "1")
                            tempOperdateTime = DateTime.Parse(((DateTime)emaintenanceData.ItemDate).ToString("yyyy-MM-dd") + " " + ((DateTime)emaintenanceData.DataUpdateTime).ToString("HH:mm:ss"));
                        cellValue = ((DateTime)tempOperdateTime).ToString(tFormat);
                        break;
                    case "ODT"://操作日期时间
                        if (IBEParameter.QueryPara("QualityRecord", "IsQRDate") == "1")
                            tempOperdateTime = DateTime.Parse(((DateTime)emaintenanceData.ItemDate).ToString("yyyy-MM-dd") + " " + ((DateTime)emaintenanceData.DataUpdateTime).ToString("HH:mm:ss"));
                        cellValue = ((DateTime)tempOperdateTime).ToString(dtFormat);
                        break;
                }
                cellValue = cellValue.Trim();
            }
            return cellValue;
        }

        private string _GetCellValueByRule(string cellValue, string[] arrayValue)
        {
            string strValue = cellValue;
            if ((!string.IsNullOrEmpty(arrayValue[21])) && (!string.IsNullOrEmpty(arrayValue[22])))
            {
                //组合规则：仪器名称:{EQ|N}，仪器编号：{EQ|S}
                //当值为空时，默认三个空格
                if (arrayValue[21].Trim() != arrayValue[22].Trim())
                {
                    string[] cellArray = cellValue.Split('|');
                    if (cellArray.Length > 1)
                        cellValue = cellArray[1];
                    else
                        cellValue = cellArray[0];
                    cellValue = string.IsNullOrEmpty(cellValue) ? "   " : cellValue;
                    strValue = arrayValue[22].Replace(arrayValue[21], cellValue);
                    arrayValue[22] = strValue;
                }
            }
            //去除值为空时候的空格
            strValue = string.IsNullOrEmpty(strValue) ? "" : strValue.Trim();
            return strValue;
        }

        private bool _GetFillExcelCellFormat(string[] arrayCell, ref string[] arrayFormat)
        {
            arrayFormat = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };//23个元素
            string[] tempArray = arrayCell[0].Split('|');
            if (tempArray != null && tempArray.Length >= 2)
            {
                arrayFormat[0] = tempArray[0];
                arrayFormat[1] = tempArray[1];
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("获取单元格位置出错：" + arrayCell[0]);
                return false;
            }
            //
            tempArray = arrayCell[1].Split('|');
            if (tempArray != null && tempArray.Length >= 2)
            {
                arrayFormat[2] = tempArray[0];
                arrayFormat[3] = tempArray[1];
                if (tempArray.Length >= 3)
                    arrayFormat[4] = tempArray[2];
                if (tempArray.Length >= 4)
                    arrayFormat[5] = tempArray[3];
                if (tempArray.Length >= 5)
                    arrayFormat[6] = tempArray[4];
                if (tempArray.Length >= 6)
                    arrayFormat[7] = tempArray[5];
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("获取大类型出错：" + arrayCell[1]);
                return false;
            }
            //
            tempArray = arrayCell[2].Split('|');
            if (tempArray != null && tempArray.Length >= 2)
            {
                arrayFormat[8] = tempArray[0];
                arrayFormat[9] = tempArray[1];
                if (tempArray.Length >= 3)
                    arrayFormat[10] = tempArray[2];
                if (tempArray.Length >= 4)
                    arrayFormat[11] = tempArray[3];
                if (tempArray.Length >= 5)
                    arrayFormat[12] = tempArray[4];
                if (tempArray.Length >= 6)
                    arrayFormat[13] = tempArray[5];
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("获取小类型出错：" + arrayCell[2]);
                return false;
            }
            //
            tempArray = arrayCell[3].Split('|');
            if (tempArray != null)
            {
                if (tempArray.Length >= 1 && tempArray[0] != "")
                {
                    arrayFormat[20] = tempArray[0];
                    arrayFormat[14] = tempArray[0].Split('/')[0];
                }
                if (tempArray.Length >= 2)
                    arrayFormat[15] = tempArray[1];
                if (tempArray.Length >= 3)
                    arrayFormat[16] = tempArray[2];
                if (tempArray.Length >= 4)
                    arrayFormat[17] = tempArray[3];
                if (tempArray.Length >= 5)
                    arrayFormat[18] = tempArray[4];
                if (tempArray.Length >= 6)
                    arrayFormat[19] = tempArray[5];
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("获取值类型出错：" + arrayCell[3]);
                return false;
            }
            return true;
        }

        private bool _GetFillExcelCellFormat(string[] arrayCell, string cellRule, ref string[] arrayFormat)
        {
            arrayFormat = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };//23个元素
            if (arrayCell != null && arrayCell.Length >= 2)
            {
                arrayFormat[0] = arrayCell[0];
                arrayFormat[1] = arrayCell[1];
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("获取单元格位置出错：" + arrayCell[0]);
                return false;
            }
            //
            string[] tempArray = cellRule.Split('|');
            if (tempArray != null && tempArray.Length >= 2)
            {
                #region 获取大类型名称，例如MD、MM
                if ((!string.IsNullOrWhiteSpace(tempArray[0])) && tempArray[0].Length > 2)
                    arrayFormat[2] = tempArray[0].Substring(0, 2);
                else
                    arrayFormat[2] = tempArray[0];
                //arrayFormat[3] = tempArray[1];
                //if (tempArray.Length >= 3)
                //    arrayFormat[4] = tempArray[2];
                //if (tempArray.Length >= 4)
                //    arrayFormat[5] = tempArray[3];
                //if (tempArray.Length >= 5)
                //    arrayFormat[6] = tempArray[4];
                //if (tempArray.Length >= 6)
                //    arrayFormat[7] = tempArray[5];
                #endregion
                #region 获取小类型名称，例如MD1、MM2
                arrayFormat[8] = tempArray[0];
                //arrayFormat[9] = tempArray[1];
                //if (tempArray.Length >= 3)
                //    arrayFormat[10] = tempArray[2];
                //if (tempArray.Length >= 4)
                //    arrayFormat[11] = tempArray[3];
                //if (tempArray.Length >= 5)
                //    arrayFormat[12] = tempArray[4];
                //if (tempArray.Length >= 6)
                //    arrayFormat[13] = tempArray[5];
                #endregion
                #region 获取值类型规则
                if (tempArray.Length > 1 && tempArray[1] != "")
                {
                    arrayFormat[20] = tempArray[1];
                    arrayFormat[14] = tempArray[1].Split('/')[0];
                }
                if (tempArray.Length > 2)
                    arrayFormat[15] = tempArray[2];
                if (tempArray.Length > 3)
                    arrayFormat[16] = tempArray[3];
                if (tempArray.Length > 4)
                    arrayFormat[17] = tempArray[4];
                if (tempArray.Length > 5)
                    arrayFormat[18] = tempArray[5];
                if (tempArray.Length > 6)
                    arrayFormat[19] = tempArray[6];
                #endregion
                arrayFormat[21] = "{" + cellRule + "}";
                arrayFormat[22] = arrayCell[2];
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("获取填充规则出错：" + arrayCell[3]);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrayCell"></param>
        /// <returns></returns>
        private string[] _GetItemArrayValue(string[] arrayCell)
        {
            string[] arrayValue = new string[] { "", "", "", "", "", "", "", "", "", "" };//10个元素
            if (arrayCell[14] != "" && arrayCell[20] != "")
            {
                if (arrayES.Contains(arrayCell[14].ToUpper()))
                {
                    string[] arrayInput = arrayCell[20].Split('/');
                    if (arrayInput != null && arrayInput.Length >= 2)
                    {
                        string[] tempArray = arrayInput[1].Split('#');
                        if (tempArray != null && tempArray.Length > 0)
                            //arrayValue[0] = tempArray[0].Replace("*", "☑").Replace("@", "☒");
                            arrayValue[0] = tempArray[0];
                        if (tempArray != null && tempArray.Length > 1)
                            //arrayValue[1] = tempArray[1].Replace("*", "☑").Replace("@", "☒");
                            arrayValue[1] = tempArray[1];
                        if (tempArray != null && tempArray.Length > 2)
                            //arrayValue[2] = tempArray[2].Replace("*", "☑").Replace("@", "☒");
                            arrayValue[2] = tempArray[2];
                        if (tempArray != null && tempArray.Length > 3)
                            arrayValue[3] = tempArray[3];
                        if (tempArray != null && tempArray.Length > 4)
                            arrayValue[4] = tempArray[4];
                        if (tempArray != null && tempArray.Length > 5)
                            arrayValue[5] = tempArray[5];
                    }
                }
                else if (arrayDate.Contains(arrayCell[14].ToUpper()) || (arrayCell[8].ToUpper() == "RD"))
                {
                    string[] arrayInput = arrayCell[20].Split('/');
                    if (arrayInput != null && arrayInput.Length >= 2)
                        arrayValue[0] = arrayInput[1];
                }
                else if (arrayCell[14].ToUpper() == "CL")
                {
                    string[] arrayInput = arrayCell[20].Split('/');
                    if (arrayInput != null && arrayInput.Length > 1)
                        arrayValue[0] = arrayInput[1];
                    if (arrayInput != null && arrayInput.Length > 2)
                        arrayValue[1] = arrayInput[2];
                    if (arrayInput != null && arrayInput.Length > 3)
                        arrayValue[2] = arrayInput[3];
                    if (arrayInput != null && arrayInput.Length > 4)
                        arrayValue[3] = arrayInput[4];
                }
            }
            return arrayValue;
        }

        /// <summary>
        /// 获取当前日期所在周的第一天和最后一天
        /// </summary>
        /// <param name="curDate">当前日期</param>
        /// <param name="beginDate">所在周的第一天</param>
        /// <param name="endDate">所在周的最后一天</param>
        //private void _GetWeekBeginAndEndDate(DateTime curDate, ref DateTime beginDate, ref DateTime endDate)
        //{
        //    int i = 0;//计算日期的基数，i=0时星期一为本周的第一天，i=-1星期天为本周的第一天
        //    GregorianCalendar gc = new GregorianCalendar();
        //    DayOfWeek dof = gc.GetDayOfWeek(curDate);
        //    int intMonday=(int)dof;
        //    switch (intMonday)
        //    {
        //        case 0:
        //            if (i == 0)
        //            {
        //                endDate = curDate;
        //                beginDate = curDate.AddDays(i - 6);
        //            }
        //            else
        //            {
        //                endDate = curDate.AddDays(6);
        //                beginDate = curDate;
        //            }
        //            break;
        //        case 1:
        //            endDate = curDate.AddDays(i + 6);
        //            beginDate = curDate.AddDays(i);
        //            break;
        //        case 2:
        //            endDate = curDate.AddDays(i + 5);
        //            beginDate = curDate.AddDays(i - 1);
        //            break;
        //        case 3:
        //            endDate = curDate.AddDays(i + 4);
        //            beginDate = curDate.AddDays(i - 2);
        //            break;
        //        case 4:
        //            endDate = curDate.AddDays(i + 3);
        //            beginDate = curDate.AddDays(i - 3);
        //            break;
        //        case 5:
        //            endDate = curDate.AddDays(i + 2);
        //            beginDate = curDate.AddDays(i - 4);
        //            break;
        //        case 6:
        //            endDate = curDate.AddDays(i + 1);
        //            beginDate = curDate.AddDays(i - 5);
        //            break;
        //    }
        //}

        private void _GetWeekBeginAndEndDate(DateTime curDate, ref DateTime beginDate, ref DateTime endDate)
        {
            int i = curDate.Day;
            DateTime firstDay = new DateTime(curDate.Year, curDate.Month, 1);
            int dayMonth = DateTime.DaysInMonth(curDate.Year, curDate.Month);
            DateTime lastDay = new DateTime(curDate.Year, curDate.Month, dayMonth);
            if (i >= 1 && i <= 7)
            {
                beginDate = firstDay;
                endDate = firstDay.AddDays(6);
            }
            else if (i >= 8 && i <= 14)
            {
                beginDate = firstDay.AddDays(7);
                endDate = firstDay.AddDays(13);
            }
            else if (i >= 15 && i <= 21)
            {
                beginDate = firstDay.AddDays(14);
                endDate = firstDay.AddDays(20);
            }
            else if (i >= 22 && i <= 28)
            {
                beginDate = firstDay.AddDays(21);
                endDate = firstDay.AddDays(27);
            }
            else if (i >= 29 && i <= 31)
            {
                beginDate = firstDay.AddDays(28);
                endDate = lastDay;
            }
        }

        public static string NumberToChinese(string numberStr)
        {
            string numStr = "0123456789";
            string chineseStr = "零一二三四五六七八九";
            char[] c = numberStr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int index = numStr.IndexOf(c[i]);
                if (index != -1)
                    c[i] = chineseStr.ToCharArray()[index];
            }
            numStr = null;
            chineseStr = null;
            return new string(c);
        }

    }
}