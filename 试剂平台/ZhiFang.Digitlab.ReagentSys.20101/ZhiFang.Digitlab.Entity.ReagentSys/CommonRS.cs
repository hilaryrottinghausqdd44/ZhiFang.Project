using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    public class CommonRS
    {
        public static DataSet GetListObjectToDataSet<T>(IList<T> listObject, string xmlpath)
        {
            DataSet dataset = new DataSet();
            if (listObject != null && listObject.Count > 0)
            {
                DataTable dt = new DataTable("TableName");
                DataSet ds = null;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                if (System.IO.File.Exists(xmlpath))
                {
                    ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(xmlpath);
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        dt.Columns.Add(new DataColumn(dataRow["FieldName"].ToString(), typeof(string)));
                        dic.Add(dataRow["FieldName"].ToString(), dataRow["ExcelFieldName"].ToString());
                    }
                }
                dataset.Tables.Add(dt.Clone());
                dataset = ObjectArrayToDataSet.FillDataSet<T>(dataset, listObject);
                if (dataset != null && ds != null && dic.Count > 0)
                {
                    foreach (DataColumn dataColumn in dataset.Tables[0].Columns)
                    {
                        dataColumn.ColumnName = dic[dataColumn.ColumnName];
                    }
                }
            }
            return dataset;
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
    }

}
