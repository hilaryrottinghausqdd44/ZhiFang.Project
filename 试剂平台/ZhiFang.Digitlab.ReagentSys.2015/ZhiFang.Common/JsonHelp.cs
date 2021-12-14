using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace ZhiFang.Common.Public
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

        public static string DataTableToJson(DataTable dt, bool isTitle)
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
                if (isTitle)
                    return "{count:" + dt.Rows.Count.ToString() + ",list:" + "[" + jsonsb.ToString().Substring(0, jsonsb.Length - 1) + "]}";
                else
                    return "[" + jsonsb.ToString().Substring(0, jsonsb.Length - 1) + "]";
            }
            else
            {
                return "";
            }
        }

        public static string DataTableToJson(DataTable dt, string TableName, bool isTitle)
        {
            StringBuilder jsonsb = new StringBuilder();
            if (dt != null && dt.Columns.Count > 0 && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string jsonstr = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        jsonstr += "\"" + TableName + "_" + dt.Columns[j].ColumnName + "\":\"" + dt.Rows[i][j].ToString() + "\",";
                    }
                    jsonsb.Append("{" + jsonstr.Substring(0, jsonstr.Length - 1) + "},");
                }
                if (isTitle)
                    return "{count:" + dt.Rows.Count.ToString() + ",list:" + "[" + jsonsb.ToString().Substring(0, jsonsb.Length - 1) + "]}";
                else
                    return "[" + jsonsb.ToString().Substring(0, jsonsb.Length - 1) + "]";
            }
            else
            {
                return "";
            }
        }

        public static string DataSetToJson(DataSet ds)
        {
            if (ds == null || ds.Tables.Count <= 0)
            {
                return "";
            }
            else
            {
                return DataTableToJson(ds.Tables[0]);
            }
        }

        public static DataTable JsonConfigToDataTable(string strJson)
        {
            JObject jsonObject = JObject.Parse(strJson);
            JArray jsonArray = (JArray)jsonObject["list"];
            return JsonConfigToDataTable(jsonArray);
        }

        private static DataTable JsonConfigToDataTable(JArray jsonArray)
        {
            DataTable dt = new DataTable();
            if (jsonArray != null && jsonArray.Count > 0)
            {
                JObject josnObject = (JObject)jsonArray[0];
                if (josnObject != null)
                {
                    foreach (KeyValuePair<string, JToken> kp in josnObject)
                    {
                        dt.Columns.Add(kp.Key, typeof(string));
                    }
                    foreach (JObject jo in jsonArray)
                    {
                        DataRow dr = dt.NewRow();
                        foreach (DataColumn dc in dt.Columns)
                            dr[dc.ColumnName] = jo[dc.ColumnName];
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
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
