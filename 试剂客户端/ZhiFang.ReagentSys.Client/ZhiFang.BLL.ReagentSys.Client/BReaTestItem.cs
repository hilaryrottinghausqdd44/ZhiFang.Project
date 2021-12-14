
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.ReagentSys.Client.Common;
using System.Data;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaTestItem : BaseBLL<ReaTestItem>, ZhiFang.IBLL.ReagentSys.Client.IBReaTestItem
    {
        /// <summary>
        /// 客户端同步LIS的检验项目信息
        /// </summary>
        /// <returns></returns>
        public BaseResultBool SaveSyncReaTestItemInfo()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            IList<ReaTestItem> reaList = this.LoadAll();

            StringBuilder lisCode = new StringBuilder();
            foreach (ReaTestItem model in reaList)
            {
                if (!string.IsNullOrEmpty(model.LisCode))
                {
                    lisCode.Append(model.LisCode + ",");
                }
            }
            string hql = "";
            if (lisCode.Length > 0)
                hql = " LisCode not in(" + lisCode.ToString().TrimEnd(',') + ")";
            IList<ReaTestItem> lisList = DataAccess_SQL.CreateReaTestItemDao_SQL().GetListByHQL(hql);
            if (lisList != null && lisList.Count > 0)
            {
                try
                {
                    foreach (var entity in lisList)
                    {
                        this.Entity = entity;
                        baseResultBool.success = this.Add();
                        if (baseResultBool.success == false)
                        {
                            baseResultBool.ErrorInfo = "同步LIS检验项目信息保存时出错!";
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo + ex.Message);
                }
            }
            if (baseResultBool.success == true)
            {
                baseResultBool.BoolInfo = "同步LIS检验项目信息成功!";
                baseResultBool.ErrorInfo = baseResultBool.BoolInfo;
            }
            return baseResultBool;
        }
        public override bool Add()
        {
            bool result = EditVerification();
            if (result == false)
            {
                return false;
            }
            result = DBDao.Save(this.Entity);
            return result;
        }
        public bool EditVerification()
        {
            bool result = true;
            if (this.Entity != null)
            {
                //Lis项目编码不能重复
                if (!string.IsNullOrEmpty(this.Entity.LisCode))
                {
                    IList<ReaTestItem> tempList = this.SearchListByHQL(string.Format("reatestitem.Visible=1 and reatestitem.LisCode='{0}' and reatestitem.Id!={1}", this.Entity.LisCode, this.Entity.Id));
                    if (tempList != null && tempList.Count > 0)
                    {
                        result = false;
                        ZhiFang.Common.Log.Log.Error(string.Format("Lis项目编码为{0},已存在,请不要重复维护!", this.Entity.LisCode));
                        return result;
                    }
                }
            }
            return result;
        }
        public BaseResultDataValue AddReaTestItemByExcel(string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            DataTable dt = MyNPOIHelper.ImportExceltoDataTable(excelFilePath);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dataColumn.ColumnName = dataColumn.ColumnName.Trim();
                }
                string xmlHREmployee = serverPath + "\\BaseTableXML\\TestItem.xml";
                if (System.IO.File.Exists(xmlHREmployee))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dt);
                    IList<string> listPrimaryKey = new List<string>();
                    Dictionary<string, string> dicDefaultValue = new Dictionary<string, string>();
                    Dictionary<string, string> dicColumn = ExcelDataCommon.GetColumnNameByDataSet(dataSet, xmlHREmployee, listPrimaryKey, dicDefaultValue);
                    if (listPrimaryKey.Count > 0)
                    {
                        baseResultDataValue = _AddTestItemDataTable(dt, dicColumn, listPrimaryKey, dicDefaultValue);
                        baseResultDataValue.success = MyNPOIHelper.GetInputExcelFileState(excelFilePath, dt);
                        if (baseResultDataValue.success)
                        {
                            baseResultDataValue.ErrorInfo = baseResultDataValue.ResultDataValue;
                            baseResultDataValue.ResultDataValue = System.IO.Path.GetFileName(excelFilePath);
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "检验项目导入对照表没有设置唯一键！";
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "检验项目导入配置信息不存在！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "检验项目数据信息为空！";
            }
            if (!baseResultDataValue.success)
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddTestItemDataTable(DataTable dataTable, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            string deptColumn = "";
            if (dicColumn.Values.Contains("Id"))
                deptColumn = dicColumn.FirstOrDefault(q => q.Value == "Id").Key;
            if (string.IsNullOrWhiteSpace(deptColumn))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "检验项目没有配置项目编码信息！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            dicColumn.Remove(deptColumn);
            baseResultDataValue = _AddTestItemData(dataTable, deptColumn, dicColumn, listPrimaryKey, dicDefaultValue);
            return baseResultDataValue;
        }
        public BaseResultDataValue _AddTestItemData(DataTable dataTable, string deptColumn, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            int isExistCount = 0;
            int isErrorCount = 0;
            int isSuccCount = 0;
            Dictionary<string, long> dicMain = new Dictionary<string, long>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["ExcelRowInputFlag"] = 0;
                dataRow["ExcelRowInputInfo"] = "导入成功";

                string keyValue = "";
                string keyHQL = "";

                foreach (string strKey in listPrimaryKey)
                {
                    if (!string.IsNullOrEmpty(dataRow[strKey].ToString()))
                    {
                        keyValue += "_" + dataRow[strKey].ToString().Trim();
                        //keyHQL = " reatestitem.Id=" + dataRow["Id"].ToString().Trim();
                        if (string.IsNullOrEmpty(keyHQL))
                            keyHQL += " reatestitem.Id" + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                        else
                            keyHQL += " and " + " reatestitem." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                    }
                }

                if ((!string.IsNullOrEmpty(keyValue)) && (!string.IsNullOrEmpty(keyHQL)))
                {
                    if (dicMain.ContainsKey(keyValue))
                    {//导入的信息中存在主键列相同的记录
                        isSuccCount++;
                        continue;
                    }

                    IList<ReaTestItem> listHREmployee = null;
                    dicMain.Add(keyValue, 0);
                    listHREmployee = this.SearchListByHQL(keyHQL);
                    if (listHREmployee != null && listHREmployee.Count > 0)
                    {
                        dicMain[keyValue] = listHREmployee[0].Id;
                        isExistCount++;
                        dataRow["ExcelRowInputFlag"] = 1;
                        dataRow["ExcelRowInputInfo"] = "未导入：该检验项目信息已经存在";
                        ZhiFang.Common.Log.Log.Info(string.Format("未导入：检验项目信息已经存在！项目名称为：{0} 编码为：{1}", listHREmployee[0].CName, listHREmployee[0].ShortCode));
                    }
                    else
                    {
                        ReaTestItem emp = ExcelDataCommon.AddExcelDataToDataBase<ReaTestItem>(dataRow, dicColumn, dicDefaultValue);
                        if (emp != null)
                        {
                            long TestItemID = 0;
                            foreach (string strKey in listPrimaryKey)
                            {
                                if (!string.IsNullOrEmpty(dataRow[strKey].ToString()))
                                {
                                    TestItemID = long.Parse(dataRow[strKey].ToString().Trim());
                                }
                            }

                            long labid = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));

                            emp.Visible = 1;
                            emp.DataAddTime = DateTime.Now;
                            emp.DataUpdateTime = DateTime.Now;
                            emp.LabID = labid;
                            emp.Id = TestItemID;
                            //  emp.TestItemID = [dataRow[strKey].ToString().Trim()];
                            this.Entity = emp;
                            if (this.Add())
                            {
                                isSuccCount++;
                            }
                            else
                            {
                                isErrorCount++;
                                dataRow["ExcelRowInputFlag"] = -2;
                                dataRow["ExcelRowInputInfo"] = "导入失败：检验项目信息保存失败";
                                ZhiFang.Common.Log.Log.Info("导入失败：检验项目信息保存失败！");
                            }
                        }
                    }
                }
            }
            baseResultDataValue.ResultDataValue = string.Format("共需导入检验项目信息{0}条，其中：导入成功{1}条，导入失败{3}条，未导入{2}条！", dataTable.Rows.Count, isSuccCount, isExistCount, isErrorCount);
            return baseResultDataValue;
        }
        public BaseResultDataValue CheckTestItemExcelFormat(string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string xmlTestItem = serverPath + "\\BaseTableXML\\TestItem.xml";
            if (System.IO.File.Exists(xmlTestItem))
            {
                IList<string> dicColumn = ExcelDataCommon.GetRequiredFieldByXml(xmlTestItem);
                Dictionary<string, Type> dicType = ExcelDataCommon.GetFieldTypeByXml<ReaTestItem>(xmlTestItem);
                baseResultDataValue.success = MyNPOIHelper.CheckExcelFile(excelFilePath, dicColumn, dicType);
                if (!baseResultDataValue.success)
                {
                    baseResultDataValue.ErrorInfo = "Error001";
                    baseResultDataValue.ResultDataValue = System.IO.Path.GetFileName(excelFilePath);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "检验项目导入配置信息不存在！";
                ZhiFang.Common.Log.Log.Info("检验项目导入配置信息不存在！");
            }
            return baseResultDataValue;
        }//
         /// <summary>
         /// 获取导出检验项目信息列表
         /// </summary>
         /// <param name="idList">检验项目ID字符串列表</param>
         /// <param name="where">查询条件</param>
         /// <param name="sort">排序</param>
         /// <param name="xmlPath">导出信息配置文件路径</param>
         /// <returns></returns>
        public DataSet GetReaTestItemInfoByID(string idList, string where, string sort, string xmlPath)
        {
            EntityList<ReaTestItem> entityList = null;
            if (string.IsNullOrEmpty(idList) && string.IsNullOrEmpty(where))
                return null;
            else
            {
                if (!string.IsNullOrEmpty(idList))
                    entityList = this.SearchListByHQL(" reatestitem.Id in (" + idList + ")", sort, 0, 0);
                else if (!string.IsNullOrEmpty(where))
                    entityList = this.SearchListByHQL(where, sort, 0, 0);
                if (entityList != null && entityList.count > 0)
                    return CommonRS.GetListObjectToDataSet<ReaTestItem>(entityList.list, xmlPath);
                else
                    return null;
            }
        }

    }
}