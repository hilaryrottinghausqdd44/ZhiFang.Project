using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;
using ZhiFang.LabStar.Common;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLisCommon : ZhiFang.IBLL.LabStar.IBLisCommon
    {
        public ZhiFang.IDAO.LabStar.IDLisCommonDao IDLisCommonDao { get; set; }

        public ZhiFang.IBLL.LabStar.IBLisTestItem IBLisTestItem { get; set; }

        public ZhiFang.IBLL.LabStar.IBLBEquipItem IBLBEquipItem { get; set; }


        public string GetMaxNoByFieldName(string entityName, string fieldName)
        {
            int maxNo = IDLisCommonDao.GetMaxNoByFieldNameDao(entityName, fieldName);
            return (maxNo + 1).ToString();
        }

        public int GetMaxNoByFieldName<T>(string fieldName)
        {
            int maxNo = IDLisCommonDao.GetMaxNoByFieldNameDao(typeof(T).Name, fieldName);
            return maxNo + 1;
        }

        public BaseResultDataValue AddCommonBaseRelationEntity<T>(IBGenericManager<T> commonIB, IList<T> addEntityList, bool isCheckEntityExist, bool isDelExist, Dictionary<string, object> propertyList, string delIDList) where T : BaseEntity
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string typeName = typeof(T).Name;
            try
            {
                if (addEntityList != null && addEntityList.Count > 0)
                {
                    foreach (T endtity in addEntityList)
                    {
                        IList<T> tempList = null;
                        string checkHQL = _getRelationEntityHQL(typeName, endtity);
                        if (isCheckEntityExist && (!string.IsNullOrEmpty(checkHQL)))
                        {
                            tempList = commonIB.SearchListByHQL(checkHQL);
                        }
                        if (tempList == null || tempList.Count == 0)
                        {
                            baseResultDataValue = AddCommonEntity(commonIB, propertyList, typeName, endtity);
                        }
                        else if (isDelExist) //删除原来的项目重新添加
                        {
                            foreach (var tempEntity in tempList)
                            {
                                if (commonIB.RemoveByHQL(tempEntity.Id))
                                    baseResultDataValue = AddCommonEntity(commonIB, propertyList, typeName, endtity);
                            }
                        }
                    }
                }
                baseResultDataValue = DeleteCommonBaseRelationEntity(commonIB, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = typeName + " AddCommonBaseRelationEntity Error：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        private BaseResultDataValue AddCommonEntity<T>(IBGenericManager<T> commonIB, Dictionary<string, object> propertyList, string typeName, T endtity) where T : BaseEntity
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            #region 属性反射赋值
            if (propertyList != null && propertyList.Count > 0)
            {
                foreach (KeyValuePair<string, object> kv in propertyList)
                {
                    try
                    {
                        System.Reflection.PropertyInfo propertyInfo = endtity.GetType().GetProperty(kv.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (propertyInfo != null && kv.Value != null)
                            propertyInfo.SetValue(endtity, kv.Value, null);
                    }
                    catch (Exception ex)
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info(typeName + " AddCommonBaseRelationEntity方法属性赋值失败：" + kv.Key + "---" + kv.Value.ToString() + "。 Error：" + ex.Message);
                    }
                }//foreach
            }
            #endregion
            commonIB.Entity = endtity;
            baseResultDataValue.success = commonIB.Add();
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteCommonBaseRelationEntity<T>(IBGenericManager<T> commonIB, string entityIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrWhiteSpace(entityIDList))
            {
                IList<string> listID = entityIDList.Split(',').ToList();
                bool delFlag = true;
                string strDelInfo = "";
                foreach (string id in listID)
                {
                    baseResultDataValue = DeleteCommonBaseRelationEntity(commonIB, long.Parse(id));
                    if (!baseResultDataValue.success)
                    {
                        delFlag = false;
                        if (!string.IsNullOrEmpty(baseResultDataValue.ErrorInfo))
                        {
                            if (string.IsNullOrEmpty(strDelInfo))
                                strDelInfo = baseResultDataValue.ErrorInfo;
                            else
                                strDelInfo += "</br>" + baseResultDataValue.ErrorInfo;
                        }
                    }
                }
                baseResultDataValue.success = delFlag;
                if (!delFlag)
                    baseResultDataValue.ErrorInfo = strDelInfo;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteCommonBaseRelationEntity<T>(IBGenericManager<T> commonIB, long entityID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string typeName = typeof(T).Name;
            try
            {
                BaseResultDataValue brdv = _judgeIfDelRelationEntity(commonIB, entityID, typeName);
                if (brdv.success)
                    baseResultDataValue.success = commonIB.RemoveByHQL(entityID);
                else
                    return brdv;
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = typeName + " DeleteCommonBaseRelationEntity Error：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        private string _getRelationEntityHQL<T>(string entityType, T entity)
        {
            string result = "";
            if (entityType == "LBSectionItem")
            {
                LBSectionItem tempEntity = (entity as LBSectionItem);
                result = " lbsectionitem.LBSection.Id=" + tempEntity.LBSection.Id + " and lbsectionitem.LBItem.Id=" + tempEntity.LBItem.Id;
            }
            else if (entityType == "LBSamplingItem")
            {
                LBSamplingItem tempEntity = (entity as LBSamplingItem);
                result = " lbsamplingitem.LBSamplingGroup.Id=" + tempEntity.LBSamplingGroup.Id + " and lbsamplingitem.LBItem.Id=" + tempEntity.LBItem.Id;
            }
            else if (entityType == "LBQCItem")
            {
                LBQCItem tempEntity = (entity as LBQCItem);
                result = " lbqcitem.LBQCMaterial.Id=" + tempEntity.LBQCMaterial.Id + " and lbqcitem.LBItem.Id=" + tempEntity.LBItem.Id;
            }
            else if (entityType == "LBQCRulesCon")
            {
                LBQCRulesCon tempEntity = (entity as LBQCRulesCon);
                result = " lbqcrulescon.LBQCRule.Id=" + tempEntity.LBQCRule.Id + " and lbqcrulescon.LBQCRuleBase.Id=" + tempEntity.LBQCRuleBase.Id;
            }
            else if (entityType == "LBQCItemRule")
            {
                LBQCItemRule tempEntity = (entity as LBQCItemRule);
                result = " lbqcitemrule.LBQCRule.Id=" + tempEntity.LBQCRule.Id + " and lbqcitemrule.LBQCItem.Id=" + tempEntity.LBQCItem.Id;
            }
            else if (entityType == "LBPhrase")
            {
                LBPhrase tempEntity = (entity as LBPhrase);
                result = " lbphrase.TypeName=\'" + tempEntity.TypeName + "\' and lbphrase.CName=\'" + tempEntity.CName + "\'" +
                    " and lbphrase.ObjectID=" + tempEntity.ObjectID.ToString();
            }
            else if (entityType == "LBSectionHisComp")
            {
                LBSectionHisComp tempEntity = (entity as LBSectionHisComp);
                result = " lbsectionhiscomp.LBSection.Id=" + tempEntity.LBSection.Id + " and lbsectionhiscomp.HisComp.Id=" + tempEntity.HisComp.Id;
            }
            else if (entityType == "LBRight")
            {
                long empID = 0;
                LBRight tempEntity = (entity as LBRight);
                if (tempEntity.EmpID != null)
                    empID = (long)tempEntity.EmpID;
                result = " lbright.LBSection.Id=" + tempEntity.LBSection.Id + " and lbright.EmpID=" + empID;
            }
            return result;
        }

        private BaseResultDataValue _judgeIfDelRelationEntity<T>(IBGenericManager<T> commonIB, long entityID, string entityType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entityType == "LBSectionItem")
            {
                T entity = commonIB.Get(entityID);
                LBSectionItem sectionItem = (entity as LBSectionItem);
                long sectionID = sectionItem.LBSection.Id;
                long itemID = sectionItem.LBItem.Id;
                bool tempBool = IBLisTestItem.QueryIsExistTestItemResult(sectionID, itemID);
                if (tempBool)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "项目【" + sectionItem.LBItem.CName + "】已经存在项目结果，不能删除或取消！";
                    return baseResultDataValue;
                }
                IList<LBEquipItem> listLBEquipItem = IBLBEquipItem.QueryIsExistSectionItem(sectionID, itemID);
                if (listLBEquipItem != null && listLBEquipItem.Count > 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "项目【" + sectionItem.LBItem.CName + "】已经设置为小组仪器项目，不能删除或取消！";
                    return baseResultDataValue;
                }
            }
            return baseResultDataValue;
        }

        #region SQL语句执行
        public BaseResultDataValue ExecSQL(string strSQL)
        {
            BaseResultDataValue brdv = IDLisCommonDao.ExecSQLDao(strSQL);
            return brdv;
        }

        public DataSet QuerySQL(string strSQL)
        {
            DataSet ds = IDLisCommonDao.QuerySQLDao(strSQL);
            return ds;
        }
        #endregion

        #region Excel文件导入

        public BaseResultDataValue AddEntityDataFormByExcelFile(string entityName, string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ////string typeName = typeof(T).Name;
            //try
            //{
            //    Type entityType = Assembly.Load("ZhiFang.Entity.LabStar").GetType("ZhiFang.Entity.LabStar." + entityName);
            //    Type bEntityType = Assembly.Load("ZhiFang.BLL.LabStar").GetType("ZhiFang.BLL.LabStar.B" + entityName);
            //    var tempObject = Activator.CreateInstance(bEntityType);
            //    string entityXMLConfig = "";
            //    //baseResultDataValue = CheckInputEntityExcelFormat(excelFilePath, serverPath, entityXMLConfig, "");
            //    if (baseResultDataValue.success)
            //    {

            //        if (baseResultDataValue.success)
            //        {
            //            baseResultDataValue = AddEntityDataFormExcel(tempObject, excelFilePath, serverPath, entityName);

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    baseResultDataValue.success = false;
            //    //baseResultDataValue.ErrorInfo = typeName + " AddCommonBaseRelationEntity Error：" + ex.Message;
            //    ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            //}
            return baseResultDataValue;
        }

        public BaseResultDataValue AddEntityDataFormByExcelFile<T>(string entityName, string entityCName, IBGenericManager<T> commonIB, string excelFilePath, string serverPath) where T : BaseEntity
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string xmlEntityPath = serverPath + "\\BaseTableXML\\" + entityName + ".xml";
                baseResultDataValue = CheckInputEntityExcelFormat<T>(excelFilePath, xmlEntityPath, entityCName);
                if (baseResultDataValue.success)
                {
                    baseResultDataValue = AddEntityDataFormExcel<T>(commonIB, excelFilePath, xmlEntityPath, entityCName);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = entityName + " AddCommonBaseRelationEntity Error：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue CheckInputEntityExcelFormat<T>(string excelFilePath, string xmlEntityPath, string entityCName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (System.IO.File.Exists(xmlEntityPath))
            {
                IList<string> dicColumn = ExcelDataCommon.GetRequiredFieldByXml(xmlEntityPath);
                Dictionary<string, Type> dicType = ExcelDataCommon.GetFieldTypeByXml<T>(xmlEntityPath);
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
                baseResultDataValue.ErrorInfo = entityCName + "导入配置信息不存在！";
                ZhiFang.Common.Log.Log.Info(entityCName + "导入配置信息不存在！");
            }
            return baseResultDataValue;
        }


        public BaseResultDataValue AddEntityDataFormExcel<T>(IBGenericManager<T> commonIB, string excelFilePath, string xmlEntityPath, string entityCName) where T : BaseEntity
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            DataTable dt = MyNPOIHelper.ImportExceltoDataTable(excelFilePath);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dataColumn.ColumnName = dataColumn.ColumnName.Trim();
                }

                if (System.IO.File.Exists(xmlEntityPath))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dt);
                    IList<string> listPrimaryKey = new List<string>();
                    Dictionary<string, string> dicDefaultValue = new Dictionary<string, string>();
                    Dictionary<string, string> dicColumn = ExcelDataCommon.GetColumnNameByDataSet(dataSet, xmlEntityPath, listPrimaryKey, dicDefaultValue);
                    baseResultDataValue = _AddEntityData<T>(commonIB, dt, dicColumn, listPrimaryKey, dicDefaultValue, entityCName);
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
                    baseResultDataValue.ErrorInfo = entityCName + "导入配置不存在！";
                    ZhiFang.Common.Log.Log.Info(entityCName + "入配置不存在！");
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = entityCName + "数据为空！";
                ZhiFang.Common.Log.Log.Info(entityCName + "数据为空！");
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddEntityData<T>(IBGenericManager<T> commonIB, DataTable dataTable, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue, string entityCName) where T : BaseEntity
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            int isExistCount = 0;
            int isErrorCount = 0;
            int isSuccCount = 0;
            Dictionary<string, long> dicMain = new Dictionary<string, long>();
            Type entityType = typeof(T);
            string lowerEntityName = entityType.Name.ToLower();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string keyValue = "";
                string keyHQL = "";
                dataRow["ExcelRowInputFlag"] = 0;
                dataRow["ExcelRowInputInfo"] = "导入成功";

                foreach (string strKey in listPrimaryKey)
                {
                    if (!string.IsNullOrWhiteSpace(dataRow[strKey].ToString()))
                    {
                        keyValue += "_" + dataRow[strKey].ToString().Trim();
                        if (string.IsNullOrEmpty(keyHQL))
                            keyHQL += lowerEntityName + "." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                        else
                            keyHQL += " and " + lowerEntityName + "." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                    }
                }

                if ((!string.IsNullOrWhiteSpace(keyValue)) && (!string.IsNullOrWhiteSpace(keyHQL)))
                {
                    if (dicMain.ContainsKey(keyValue))
                    {
                        //导入的信息中存在主键列相同的记录
                        ZhiFang.Common.Log.Log.Info("导入的信息中存在主键列相同的记录：" + keyValue);
                        isSuccCount++;
                        continue;
                    }
                    dicMain.Add(keyValue, 0);

                    IList<T> listEntity = commonIB.SearchListByHQL(keyHQL);
                    if (listEntity != null && listEntity.Count > 0)
                    {
                        dicMain[keyValue] = listEntity[0].Id;
                        isExistCount++;
                        dataRow["ExcelRowInputFlag"] = 1;
                        dataRow["ExcelRowInputInfo"] = "未导入：本条" + entityCName + "已经存在";
                        string cName = "";
                        PropertyInfo propertyInfo = entityType.GetProperty("CName");
                        if (propertyInfo != null)
                            cName = propertyInfo.GetValue(listEntity[0], null).ToString();
                        if (string.IsNullOrWhiteSpace(cName))
                        {
                            propertyInfo = entityType.GetProperty("Name");
                            if (propertyInfo != null)
                                cName = propertyInfo.GetValue(listEntity[0], null).ToString();
                        }
                        ZhiFang.Common.Log.Log.Info(string.Format("未导入：本条" + entityCName + "已经存在！名称为：{0}", cName));
                    }
                    else
                    {
                        T entity = ExcelDataCommon.AddExcelDataToDataBase<T>(dataRow, dicColumn, dicDefaultValue);
                        if (entity != null)
                        {
                            entity.DataAddTime = DateTime.Now;
                            commonIB.Entity = entity;
                            PropertyInfo propertyInfo = entityType.GetProperty("DataUpdateTime");
                            if (propertyInfo != null)
                                propertyInfo.SetValue(entity, DateTime.Now, null);
                            propertyInfo = entityType.GetProperty("IsUse");
                            if (propertyInfo != null)
                                propertyInfo.SetValue(entity, true, null);
                            bool add = commonIB.Add();
                            if (add)
                            {
                                isSuccCount++;
                            }
                            else
                            {
                                isErrorCount++;
                                dataRow["ExcelRowInputFlag"] = -2;
                                dataRow["ExcelRowInputInfo"] = "导入失败：保存失败";
                                ZhiFang.Common.Log.Log.Info("导入失败：保存失败！");
                            }
                        }
                    }
                }
            }
            baseResultDataValue.ResultDataValue = string.Format(entityCName + "共需导入{0}条，其中：导入成功{1}条，导入失败{3}条，未导入{2}条！", dataTable.Rows.Count, isSuccCount, isExistCount, isErrorCount);
            ZhiFang.Common.Log.Log.Info(baseResultDataValue.ResultDataValue);
            return baseResultDataValue;
        }

        #endregion

        #region Excel文件导入
        public DataSet QueryEntityDataInfo<T>(string entityName, IBGenericManager<T> commonIB, string idList, string where, string sort, string serverPath)
        {
            EntityList<T> entityList = null;
            string xmlEntityPath = serverPath + "\\BaseTableXML\\" + entityName + ".xml";
            if (string.IsNullOrEmpty(idList) && string.IsNullOrEmpty(where))
                return null;
            else
            {
                if (!string.IsNullOrEmpty(idList))
                    entityList = commonIB.SearchListByHQL(entityName.ToLower() + ".Id in (" + idList + ")", sort, 0, 0);
                else if (!string.IsNullOrEmpty(where))
                    entityList = commonIB.SearchListByHQL(where, sort, 0, 0);
                if (entityList != null && entityList.count > 0)
                    return ExcelDataCommon.GetListObjectToDataSet<T>(entityList.list, xmlEntityPath);
                else
                    return null;
            }
        }
        #endregion

    }
}