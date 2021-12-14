using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.BLL.Common
{
    public class DataSetToJson
    {

        /// <summary>
        /// Datatable转换为Json
        /// </summary>
        /// <param name="table">Datatable对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            try
            {
                if (dt.Rows.Count == 0)
                {
                    jsonString.Append("");
                    return jsonString.ToString();
                }

                jsonString.Append("[");
                DataRowCollection drc = dt.Rows;
                for (int i = 0; i < drc.Count; i++)
                {
                    jsonString.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string strKey = dt.Columns[j].ColumnName;
                        string strValue = drc[i][j].ToString();
                        Type type = dt.Columns[j].DataType;
                        jsonString.Append("'" + strKey + "':");
                        if (strValue == null|| strValue.Trim()=="")
                        {
                            strValue = "''";
                        }
                        else
                        {
                            strValue = StringFormat(strValue, type);
                        }
                        if (j < dt.Columns.Count - 1)
                        {
                            jsonString.Append(strValue + ",");
                        }
                        else
                        {
                            jsonString.Append(strValue);
                        }
                    }
                    jsonString.Append("},");
                }
                jsonString.Remove(jsonString.Length - 1, 1);
                jsonString.Append("]");

            }
            catch (Exception ex)
            {
            }
            return jsonString.ToString();
        }
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string String2Json(String s)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];

                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string StringFormat(string str, Type type)
        {
            try
            {
                if (type == typeof(string))
                {
                    str = String2Json(str);
                    str = "'" + str + "'";
                }
                else if (type == typeof(DateTime))
                {
                    str = "" + Convert.ToDateTime(str).ToShortDateString() + "";
                }
                else if (type == typeof(bool))
                {
                    str = str.ToLower();
                }
                if (str.Length == 0)
                    str = "";
            }
            catch (Exception ex)
            {
            }
            return str;
        }

        public static string ToJson(DataView dv)
        {
            StringBuilder jsonString = new StringBuilder();
            try
            {
                if (dv.Count == 0)
                {
                    jsonString.Append("[{}]");
                    return jsonString.ToString();
                }

                jsonString.Append("[");
                for (int i = 0; i < dv.Count; i++)
                {
                    jsonString.Append("{");
                    for (int j = 0; j < dv.Table.Columns.Count; j++)
                    {
                        string strKey = dv.Table.Columns[j].ColumnName;
                        string strValue = dv[i][j].ToString();
                        Type type = dv.Table.Columns[j].DataType;
                        jsonString.Append("'" + strKey + "':");
                        strValue = StringFormat(strValue, type);
                        if (j < dv.Table.Columns.Count - 1)
                        {
                            jsonString.Append(strValue + ",");
                        }
                        else
                        {
                            jsonString.Append(strValue);
                        }
                    }
                    jsonString.Append("},");
                }
                jsonString.Remove(jsonString.Length - 1, 1);
                jsonString.Append("]");

            }
            catch (Exception ex)
            {
            }
            return jsonString.ToString();
        }
    }
}
