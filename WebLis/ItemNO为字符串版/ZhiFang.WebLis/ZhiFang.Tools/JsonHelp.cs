using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Data;

namespace ZhiFang.Tools
{
    public class JsonHelp
    {
        public static string JsonDotNetSerializer(object o)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //string result = JsonConvert.SerializeObject(o, Formatting.Indented, settings);
            string result = JsonConvert.SerializeObject(o, Formatting.None, settings);
            return result;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string row)
        {
            return JsonConvert.DeserializeObject<T>(row, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        /// <summary>
        /// 将调用wcf接口得到的resultDataValue转换成字符串数组
        /// </summary>
        /// <param name="resultDataValue"></param>
        /// <returns></returns>
        public static string[] GetRowsByResultDataValue(string resultDataValue)
        {

            int startIndex = resultDataValue.IndexOf("[");
            int length = resultDataValue.Length - startIndex - 3;
            string rows = resultDataValue.Substring(startIndex + 1, length);

            if (rows.Length <= 2)
                return new string[] { };

            string[] temps = rows.Split('}');
            string[] arrs = new string[temps.Length - 1];
            for (int i = 0; i < temps.Length - 1; i++)
            {
                if (temps[i] == "")
                    continue;
                string t = temps[i] + "}";
                if (i != 0)
                    arrs[i] = t.Substring(1, t.Length - 1);
                else
                    arrs[i] = t;
            }

            return arrs;
        }
        public static string ByteTypeToString(byte[] ByteField)
        {
            string tempByteStr = "";
            if (ByteField != null)
            {
                foreach (int tempByte in ByteField)
                {
                    tempByteStr = tempByteStr + "," + tempByte.ToString();
                }
                if (tempByteStr.Length > 0)
                {
                    tempByteStr = tempByteStr.Remove(0, 1);
                }
            }
            else
            {
                tempByteStr = "null";
            }
            return tempByteStr;
        }
        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder jsonsb = new StringBuilder();
            if (dt != null && dt.Columns.Count > 0 && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string jsonstr = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        jsonstr += "\"" + dt.Columns[j].ColumnName + "\":\"" + dt.Rows[i][j].ToString() + "\",";
                    }
                    jsonsb.Append("{" + jsonstr.Substring(0, jsonstr.Length - 1) + "},");
                }
                return "[" + jsonsb.ToString().Substring(0, jsonsb.Length - 1) + "]";
            }
            else
            {
                return "";
            }
        }
        public static string DataSetToJson(DataSet ds)
        {
            StringBuilder jsonsb = new StringBuilder();
            if (ds == null || ds.Tables.Count <= 0)
            {
                return "";
            }
            else
            {
                return DataTableToJson(ds.Tables[0]);
            }
        }
        /// <summary>
        /// List转JsonPaging
        /// </summary>
        /// <typeparam name="T">entity class</typeparam>
        /// <param name="Count">条数</param>
        /// <param name="ls">List</param>
        /// <returns>Json</returns>
        public static string JsonPaging<T>(int Count, List<T> ls)
        {
            return JsonConvert.SerializeObject(new { page = ls.Count, limit = ls });
        }
        /// <summary>
        /// List转Json
        /// </summary>
        /// <typeparam name="T">entity class</typeparam>
        /// <param name="Count">条数</param>
        /// <param name="ls">List</param>
        /// <returns>Json</returns>
        public static string Json<T>(List<T> ls)
        {
            return JsonConvert.SerializeObject(ls);
        }
    }
}
