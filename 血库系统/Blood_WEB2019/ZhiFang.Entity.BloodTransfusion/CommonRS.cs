using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZhiFang.BloodTransfusion.Common;

namespace ZhiFang.Entity.BloodTransfusion
{
    public class CommonRS
    {
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
                dataset = ObjectArrayToDataSet.FillDataSet<T>(dataset, listObject, dicFormat);
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
