using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Common
{
    public class DtToJson
    {
        /// <summary>
        /// ListPaging
        /// </summary>
        /// <typeparam name="T">entity class</typeparam>
        /// <param name="page">home page</param>
        /// <param name="limit">条数</param>
        /// <param name="ls">List</param>
        /// <returns>List</returns>
        public static List<T> Pagination<T>(int page, int limit, List<T> ls)
        {
            if (ls == null || ls.Count == 0)
            { }
            List<T> Result = new List<T>();
            int rowCount = page * limit;
            if (ls.Count >= rowCount)
            {
                for (int i = rowCount - limit; i < rowCount; i++)
                {
                    Result.Add(ls[i]);
                }
            }
            else
            {
                if (ls.Count > rowCount - limit)
                {
                    for (int i = rowCount - limit; i < ls.Count; i++)
                    {
                        Result.Add(ls[i]);
                    }
                }
            }
            return Result;
        }

        /// <summary>
        /// DataSet转List
        /// </summary>
        /// <typeparam name="T">entity class</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List</returns>
        public static List<T> GetListColumns<T>(DataTable table)
        {
            List<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (value.GetType() == typeof(System.DBNull))
                        {
                            value = null;
                        }
                        else
                        {
                            if (pro.PropertyType == typeof(long?))
                            {
                                value = long.Parse(value.ToString());
                            }
                            if (pro.PropertyType == typeof(string))
                            {
                                value = value.ToString();
                            }
                        }
                        pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
    }

    public class ToJson
    {

        #region DataSet转换成Json格式
        /// <summary>
        /// DataSet转换成Json格式  
        /// </summary>  
        /// <param name="ds">DataSet</param> 
        /// <returns></returns>  
        public static string Dataset2Json(DataSet ds, int total = -1)
        {
            StringBuilder json = new StringBuilder();

            foreach (DataTable dt in ds.Tables)
            {
                json.Append("[");
                json.Append(DataTable2Json(dt));
                json.Append("]");
            } return json.ToString();
        }
        #endregion

        #region dataTable转换成Json格式
        /// <summary>  
        /// dataTable转换成Json格式  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                if (dt.Columns.Count > 0)
                {
                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                }
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }

            return jsonBuilder.ToString();
        }
        #endregion
    }
}
