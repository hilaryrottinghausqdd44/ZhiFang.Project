using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Base;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ZhiFang.ProjectProgressMonitorManage.Common
{
    /// <summary>
    ///
    /// </summary>
    public class ExcelDataCommon
    {
        public static BaseResultDataValue AddExcelDataToDataBase<T>(DataTable dataTable, Dictionary<string, string> dic, IBGenericManager<T> baseBLL)
        {
            BaseResultDataValue baseresultdatavalue = new BaseResultDataValue();
            int EmptyCount = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                EmptyCount = 0;
                T tempObject = (T)Activator.CreateInstance(typeof(T));
                foreach (KeyValuePair<string, string> keyValuePair in dic)
                {
                    try
                    {
                        if (dataTable.Columns.Contains(keyValuePair.Key))
                        {
                            if (string.IsNullOrEmpty(dataRow[keyValuePair.Key].ToString()))
                                EmptyCount++;

                            System.Reflection.PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(dic[keyValuePair.Key]);
                            if (tempPropertyInfo != null && (!string.IsNullOrEmpty(dataRow[keyValuePair.Key].ToString())))
                                tempPropertyInfo.SetValue(tempObject, DataConvert(tempPropertyInfo, dataRow[keyValuePair.Key]), null);
                            else
                                ZhiFang.Common.Log.Log.Info(typeof(T).ToString() + "类属性" + keyValuePair.Key + "不存在！");
                        }
                        else
                            ZhiFang.Common.Log.Log.Info("导入文件中不存在【" + keyValuePair.Key + "】信息！");
                    }
                    catch (Exception ex)
                    {
                        ZhiFang.Common.Log.Log.Info(typeof(T).ToString() + "中的" + keyValuePair.Key + "属性列转换失败！");
                        throw new Exception(ex.Message);
                    }
                }

                if (EmptyCount < dataTable.Columns.Count)
                {
                    baseBLL.Entity = tempObject;
                    if (!baseBLL.Add())
                        ZhiFang.Common.Log.Log.Info(typeof(T).ToString() + "保存失败！");
                }
            }
            return baseresultdatavalue;
        }

        public static T AddExcelDataToDataBase<T>(DataRow dataRow, Dictionary<string, string> dic, Dictionary<string, string> dicDefaultValue) where T : BaseEntity
        {
            T resultEntity = null;
            int EmptyCount = 0;
            EmptyCount = 0;
            T tempObject = (T)Activator.CreateInstance(typeof(T));
            foreach (KeyValuePair<string, string> keyValuePair in dic)
            {
                try
                {
                    if (dataRow.Table.Columns.Contains(keyValuePair.Key))
                    {
                        if (string.IsNullOrEmpty(dataRow[keyValuePair.Key].ToString()))
                            EmptyCount++;
                        System.Reflection.PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(dic[keyValuePair.Key]);
                        if (tempPropertyInfo != null && (!string.IsNullOrEmpty(dataRow[keyValuePair.Key].ToString())))
                            tempPropertyInfo.SetValue(tempObject, DataConvert(tempPropertyInfo, dataRow[keyValuePair.Key]), null);
                        else if (tempPropertyInfo == null)
                            ZhiFang.Common.Log.Log.Info(typeof(T).ToString() + "类属性" + keyValuePair.Key + "不存在！");
                        else
                        {
                            if (dicDefaultValue != null && dicDefaultValue.ContainsKey(keyValuePair.Key))
                                tempPropertyInfo.SetValue(tempObject, DataConvert(tempPropertyInfo, dicDefaultValue[keyValuePair.Key]), null); ;
                        }
                    }
                    else
                        ZhiFang.Common.Log.Log.Info("导入文件中不存在【" + keyValuePair.Key + "】信息！");
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info(typeof(T).ToString() + "中的" + keyValuePair.Key + "属性列转换失败！");
                    throw new Exception(typeof(T).ToString() + "中的" + keyValuePair.Key + "属性列转换失败！Error:" + ex.Message);
                }
            }
            if (EmptyCount < dataRow.Table.Columns.Count)
            {
                resultEntity = tempObject;
            }
            return resultEntity;
        }

        public static Dictionary<string, string> GetColumnNameByDataSet(DataSet dataSet, string fieldXmlPath, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            Dictionary<string, string> dicColumn = new Dictionary<string, string>();
            DataTable dataTable = dataSet.Tables[0];
            DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(dataRow["FieldName"].ToString()))
                    dicColumn.Add(dataRow["ExcelFieldName"].ToString().Trim(), dataRow["FieldName"].ToString().Trim());
                if (dataRow["IsPrimaryKey"].ToString() == "1")
                    listPrimaryKey.Add(dataRow["ExcelFieldName"].ToString().Trim());
                if (dicDefaultValue != null && (ds.Tables[0].Columns.Contains("DefaultValue")) && (!string.IsNullOrEmpty(dataRow["DefaultValue"].ToString())))
                    dicDefaultValue.Add(dataRow["ExcelFieldName"].ToString().Trim(), dataRow["DefaultValue"].ToString());
            }
            return dicColumn;
        }

        public static void GetColumnNameBySaleDocXMLFile(string fieldXmlPath, Dictionary<string, string> dic)
        {
            DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(dataRow["FieldName"].ToString()))
                {
                    dic.Add(dataRow["FieldName"].ToString().Trim(), dataRow["CompareFieldName"].ToString().Trim());
                }
            }
        }

        public static IList<string> GetRequiredFieldByXml(string fieldXmlPath)
        {
            IList<string> list = new List<string>();
            DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                if (dataRow["IsRequiredField"].ToString() == "1")
                    list.Add(dataRow["ExcelFieldName"].ToString().Trim());
            }
            return list;
        }

        public static Dictionary<string, Type> GetFieldTypeByXml<T>(string fieldXmlPath)
        {
            Dictionary<string, string> dicColumn = new Dictionary<string, string>();
            DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(dataRow["FieldName"].ToString()))
                    dicColumn.Add(dataRow["FieldName"].ToString().Trim(), dataRow["ExcelFieldName"].ToString().Trim());
            }

            Type type = typeof(T);
            T tempObject = (T)Activator.CreateInstance(typeof(T));
            PropertyInfo[] listProperty = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Dictionary<string, Type> dicType = new Dictionary<string, Type>();
            if (listProperty != null && listProperty.Length > 0)
            {
                foreach (PropertyInfo property in listProperty)
                {
                    if (dicColumn.ContainsKey(property.Name))
                        dicType.Add(dicColumn[property.Name], property.PropertyType);
                }
            }
            return dicType;
        }

        public static object DataConvert(System.Reflection.PropertyInfo propertyInfo, object dataColumnValue)
        {
            object resultStr = null;
            Type type = propertyInfo.PropertyType;
            string columnValue = dataColumnValue.ToString();
            if (!string.IsNullOrEmpty(columnValue))
            {
                columnValue = columnValue.Trim();
                if (type == typeof(int))
                {
                    resultStr = int.Parse(columnValue);
                }
                else if (type == typeof(Int64))
                {
                    resultStr = Int64.Parse(columnValue);
                }
                else if (type == typeof(double))
                {
                    resultStr = double.Parse(columnValue);
                }
                else if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
                {
                    resultStr = DateTime.Parse(columnValue);
                }
                else
                    resultStr = columnValue;
            }

            return resultStr;
        }

        public static DataSet GetListObjectToDataSet<T>(IList<T> listObject, string xmlpath)
        {
            DataSet dataset = new DataSet();
            if (System.IO.File.Exists(xmlpath))
            {
                DataSet dsField = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(xmlpath);
                if (dsField != null && dsField.Tables.Count > 0)
                {
                    dataset = GetListObjectToDataSet<T>(listObject, dsField);
                }
            }
            return dataset;
        }

        public static DataSet GetListObjectToDataSet<T>(IList<T> listObject, string jsonField, string version)
        {
            DataSet dataset = new DataSet();
            if (!string.IsNullOrEmpty(jsonField))
            {
                DataSet dsField = new DataSet();
                DataTable dt = new DataTable("TableName");

                JObject jsonObject = JObject.Parse(jsonField);
                JArray jsonArray = (JArray)jsonObject["FieldList"];
                if (jsonArray != null && jsonArray.Count > 0)
                {
                    foreach (JProperty jp in ((JObject)jsonArray[0]).Properties())
                    {
                        dt.Columns.Add(new DataColumn(jp.Name));
                    }
                    dsField.Tables.Add(dt.Clone());
                    foreach (JObject jo in jsonArray)
                    {
                        DataRow dr = dsField.Tables[0].NewRow();
                        foreach (JProperty jp in ((JObject)jsonArray[0]).Properties())
                        {
                            dr[jp.Name] = jo[jp.Name];
                        }
                        dsField.Tables[0].Rows.Add(dr);
                    }
                }
                if (dsField != null && dsField.Tables.Count > 0)
                {
                    dataset = GetListObjectToDataSet<T>(listObject, dsField);
                }
            }
            return dataset;
        }

        public static DataSet GetListObjectToDataSet<T>(IList<T> listObject, DataSet dsFields)
        {
            DataSet dataset = new DataSet();
            if (listObject != null && listObject.Count > 0)
            {
                DataTable dt = new DataTable("TableName");
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Dictionary<string, string> dicFormat = new Dictionary<string, string>();

                if (dsFields != null && dsFields.Tables.Count > 0)
                {
                    bool isContainCol = dsFields.Tables[0].Columns.Contains("DataFormat");
                    foreach (DataRow dataRow in dsFields.Tables[0].Rows)
                    {
                        dt.Columns.Add(new DataColumn(dataRow["FieldName"].ToString(), GetFieldType(dataRow)));
                        dic.Add(dataRow["FieldName"].ToString(), dataRow["ExcelFieldName"].ToString());
                        if (isContainCol && (!string.IsNullOrEmpty(dataRow["DataFormat"].ToString())))
                            dicFormat.Add(dataRow["FieldName"].ToString(), dataRow["DataFormat"].ToString());
                        string fieldCalcExp = GetCalcColumnExpression(dataRow);
                        if (fieldCalcExp != "")
                            dt.Columns[dataRow["FieldName"].ToString()].Expression = fieldCalcExp;
                    }
                }

                dataset.Tables.Add(dt.Clone());
                dataset = FillDataSet<T>(dataset, listObject, dicFormat);
                if (dataset != null && dsFields != null && dic.Count > 0)
                {
                    foreach (DataColumn dataColumn in dataset.Tables[0].Columns)
                    {
                        dataColumn.ColumnName = dic[dataColumn.ColumnName];
                    }
                }
            }
            return dataset;
        }

        public static DataSet GetListObjectToDataSet<T>(IList<T> listObject, string fields, string fieldsName, string version)
        {
            DataSet dataset = new DataSet();
            if (listObject != null && listObject.Count > 0 && (!string.IsNullOrEmpty(fields)) && (!string.IsNullOrEmpty(fieldsName)))
            {
                DataTable dt = new DataTable("TableName");
                string[] fieldsArray = fields.Split(',');
                string[] fieldsNameArray = fieldsName.Split(',');
                Dictionary<string, string> dicFields = new Dictionary<string, string>();
                if (fieldsArray.Length == fieldsNameArray.Length)
                {
                    for (int i = 0; i < fieldsArray.Length; i++)
                    {
                        dicFields.Add(fieldsArray[i], fieldsNameArray[i]);
                    }
                }

                foreach (KeyValuePair<string, string> kv in dicFields)
                {
                    dt.Columns.Add(new DataColumn(kv.Key));
                }

                dataset.Tables.Add(dt.Clone());
                dataset = ObjectArrayToDataSet.FillDataSet<T>(dataset, listObject);
                if (dataset != null && dicFields.Count > 0)
                {
                    foreach (DataColumn dataColumn in dataset.Tables[0].Columns)
                    {
                        dataColumn.ColumnName = dicFields[dataColumn.ColumnName];
                    }
                }

            }
            return dataset;
        }

        /// <summary>
        /// 根据对象数组填充数据集
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="objArr"></param>
        /// <returns></returns>
        public static DataSet FillDataSet<T>(DataSet dataSet, IList<T> objList, Dictionary<string, string> dicFormat)
        {
            DataColumnCollection dcs = dataSet.Tables[0].Columns;
            foreach (object obj in objList)
            {
                DataRow dr = dataSet.Tables[0].NewRow();
                for (int i = 0; i < dcs.Count; i++)
                {
                    int tempInt = 0;
                    object tempObject = obj;
                    string[] tempArray = dcs[i].ColumnName.Split('_');
                    for (int k = 1; k < tempArray.Length; k++)
                    {
                        tempInt++;
                        PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(tempArray[k].Trim());
                        if (tempPropertyInfo != null)
                        {
                            tempObject = tempPropertyInfo.GetValue(tempObject, null);
                            if (tempObject != null)
                            {
                                if (tempInt == tempArray.Length - 1)
                                {
                                    string format = "";
                                    if (dicFormat.ContainsKey(dcs[i].ColumnName))
                                        format = dicFormat[dcs[i].ColumnName];
                                    if ((!string.IsNullOrEmpty(format)) && (tempObject.GetType() == typeof(DateTime) || tempObject.GetType() == typeof(DateTime?)))
                                    {
                                        dr[i] = ((DateTime)tempObject).ToString(format);
                                    }
                                    else
                                        dr[i] = tempObject;
                                }
                            }
                            else
                            {
                                dr[i] = DBNull.Value;
                                break;
                            }
                        }
                    }
                }
                dataSet.Tables[0].Rows.Add(dr);
            }
            return dataSet;
        }

        public static Type GetFieldType(DataRow dr)
        {
            Type ty = typeof(string);
            if (dr.Table.Columns.Contains("FieldType") && (dr["FieldType"] != null))
            {
                string fieldType = dr["FieldType"].ToString().Trim();
                if (fieldType.ToLower() == "int")
                    ty = typeof(int);
                else if (fieldType.ToLower() == "long")
                    ty = typeof(long);
                else if (fieldType.ToLower() == "double")
                    ty = typeof(double);
                else if (fieldType.ToLower() == "float")
                    ty = typeof(float);
                else if (fieldType.ToLower() == "decimal")
                    ty = typeof(decimal);
            }
            return ty;
        }

        public static string GetCalcColumnExpression(DataRow dr)
        {
            string expression = "";
            if (dr.Table.Columns.Contains("FieldCalcExp") && (dr["FieldCalcExp"] != null))
                expression = dr["FieldCalcExp"].ToString().Trim();
            return expression;
        }

        public static string GetExcelExtName()
        {
            string excelExtName = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ExcelExtName");
            if (string.IsNullOrEmpty(excelExtName))
                excelExtName = "xlsx";
            else
                excelExtName = excelExtName.Replace(".", "");
            return excelExtName;
        }

        public static T EntityCopy<T>(T entity, string[] noCopyProperty)
        {
            T newEntity = Activator.CreateInstance<T>();
            try
            {
                foreach (System.Reflection.PropertyInfo pi in entity.GetType().GetProperties())
                {
                    if (noCopyProperty == null || (!noCopyProperty.Contains(pi.Name)))
                    {
                        System.Reflection.PropertyInfo newpi = newEntity.GetType().GetProperty(pi.Name);
                        newpi.SetValue(newEntity, pi.GetValue(entity, null), null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newEntity;
        }
    }

}