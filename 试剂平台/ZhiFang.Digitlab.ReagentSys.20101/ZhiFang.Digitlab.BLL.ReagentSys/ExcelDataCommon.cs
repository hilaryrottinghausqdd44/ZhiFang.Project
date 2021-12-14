using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using ZhiFang.Digitlab.IBLL;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;

namespace ZhiFang.Digitlab.BLL.ReagentSys
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
                        if (string.IsNullOrEmpty(dataRow[keyValuePair.Key].ToString()))
                            EmptyCount++;
                        System.Reflection.PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(dic[keyValuePair.Key]);
                        if (tempPropertyInfo != null && (!string.IsNullOrEmpty(dataRow[keyValuePair.Key].ToString())))
                            tempPropertyInfo.SetValue(tempObject, DataConvert(tempPropertyInfo, dataRow[keyValuePair.Key]), null);
                        else
                            ZhiFang.Common.Log.Log.Info(typeof(T).ToString() + "类属性" + keyValuePair.Key + "不存在！");
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

        public static T AddExcelDataToDataBase<T>(DataRow dataRow, Dictionary<string, string> dic) where T : BaseEntity
        {
            T resultEntity = null;
            int EmptyCount = 0;
            EmptyCount = 0;  
            T tempObject = (T)Activator.CreateInstance(typeof(T));
            foreach (KeyValuePair<string, string> keyValuePair in dic)
            {
                try
                {
                    if (string.IsNullOrEmpty(dataRow[keyValuePair.Key].ToString()))
                        EmptyCount++;
                    System.Reflection.PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(dic[keyValuePair.Key]);
                    if (tempPropertyInfo != null && (!string.IsNullOrEmpty(dataRow[keyValuePair.Key].ToString())))
                        tempPropertyInfo.SetValue(tempObject, DataConvert(tempPropertyInfo, dataRow[keyValuePair.Key]), null);
                    else
                        ZhiFang.Common.Log.Log.Info(typeof(T).ToString() + "类属性" + keyValuePair.Key + "不存在！");
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info(typeof(T).ToString() + "中的" + keyValuePair.Key + "属性列转换失败！");
                    throw new Exception(typeof(T).ToString() + "中的" + keyValuePair.Key + "属性列转换失败！Error:"+ex.Message);
                }
            }

            if (EmptyCount < dataRow.Table.Columns.Count)
            {
                resultEntity = tempObject;
            }
            return resultEntity;
        }

        public static DataSet GetDataSetByExcelFile(string excelFilePath)
        {
            ExcelHelper excelHelper = new ExcelHelper(excelFilePath, "YES");
            DataSet dataSet = excelHelper.GetExcelDataSet(null);
            return dataSet;
        }

        public static Dictionary<string, string> GetColumnNameByDataSet(DataSet dataSet, string fieldXmlPath, IList<string> listPrimaryKey)
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
            PropertyInfo[] listProperty  = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
   
	}
}