using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;

namespace ZhiFang.WeiXin.Common
{
    public class JsonChart
    {
        public static string GetPropertyValue (PropertyInfo tempPropertyInfo,object tempObject)
        {
            object tempFieldValue = tempPropertyInfo.GetValue(tempObject, null);
            if (tempFieldValue != null)
            {
                return tempFieldValue.ToString();
            }
            else
            {
                return "";
            }
        }

        public static string GetJsonStr(Dictionary<string, List<string>> tempDictionary)
        {
            string ResultStr = "";
            if ((tempDictionary != null) && (tempDictionary.Count > 0))
            {
                string[] tempStrArray = new string[tempDictionary.Count];
                StringBuilder tempStringBuilder = new StringBuilder();
                for (int index = 0; index < tempDictionary.Count; index++)
                {
                    var item = tempDictionary.ElementAt(index);
                    tempStringBuilder.Append(","+"{" + string.Join(",", item.Value.ToArray()) + "}");
                }

                if (tempStringBuilder.Length > 0)
                {
                    ResultStr = "data:[" + tempStringBuilder.ToString().Remove(0, 1) + "]";
                }
                //JsonSerializerSettings settings = new JsonSerializerSettings();
                //settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //return JsonConvert.SerializeObject(tempStrArray, Formatting.Indented, settings);
            }
            return ResultStr;
        }
        /// <summary>
        /// 折线图、面积图、雷达图和散点图的数据格式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="EntityList">源数据列表</param>
        /// <param name="XField">X轴字段名</param>
        /// <param name="YField">Y轴字段名</param>
        /// <returns>固定格式的Json字符串</returns>
        public static string JsonChartLine<T>(IList<T> EntityList, string XField, string YField)
        {
            if ((EntityList != null) && (EntityList.Count > 0))
            {
                const string ValueSeparator = ":";//属性和属性值分隔符
                const string CommonSeparator = "\""; //转义字符
                PropertyInfo temp_X_PropertyInfo = null;
                PropertyInfo temp_Y_PropertyInfo = null;
                Dictionary<string, List<string>> tempDictionary = new Dictionary<string, List<string>>();
                foreach (T tempEntity in EntityList)
                {
                    temp_X_PropertyInfo = tempEntity.GetType().GetProperty(XField.Trim());
                    temp_Y_PropertyInfo = tempEntity.GetType().GetProperty(YField.Trim());
                    if ((temp_X_PropertyInfo != null) && (temp_Y_PropertyInfo != null))
                    {
                        string XValue = GetPropertyValue(temp_X_PropertyInfo,tempEntity);
                        string YValue = GetPropertyValue(temp_Y_PropertyInfo,tempEntity);
                        if ((XValue.Length > 0) && (YValue.Length > 0))
                        {
                            string tempValueStr = "";
                            if (!tempDictionary.ContainsKey(XValue))
                            {
                                tempValueStr = CommonSeparator + "name" + CommonSeparator + ValueSeparator + CommonSeparator + XValue + CommonSeparator;
                                tempDictionary.Add(XValue, new List<string> { tempValueStr });
                            }
                            tempValueStr = CommonSeparator + "data" + (tempDictionary[XValue].Count).ToString() + CommonSeparator + ValueSeparator + YValue;
                            tempDictionary[XValue].Add(tempValueStr);
                        }
                    }
                }
                return GetJsonStr(tempDictionary);
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 柱状图和饼状图的数据格式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="EntityList">源数据列表</param>
        /// <param name="XField">X轴字段名</param>
        /// <param name="YField">Y轴字段名</param>
        /// <returns>固定格式的Json字符串</returns>
        public static string JsonChartHistogram<T>(IList<T> EntityList, string XField, string YField)
        {
            string ResultStr = "";
            if ((EntityList != null) && (EntityList.Count > 0))
            {
                const string ValueSeparator = ":";//属性和属性值分隔符
                const string CommonSeparator = "\""; //转义字符
                PropertyInfo temp_X_PropertyInfo = null;
                PropertyInfo temp_Y_PropertyInfo = null;
                StringBuilder tempStringBuilder = new StringBuilder();
                foreach (T tempEntity in EntityList)
                {
                    temp_X_PropertyInfo = tempEntity.GetType().GetProperty(XField.Trim());
                    temp_Y_PropertyInfo = tempEntity.GetType().GetProperty(YField.Trim());
                    if ((temp_X_PropertyInfo != null) && (temp_Y_PropertyInfo != null))
                    {
                        string XValue = GetPropertyValue(temp_X_PropertyInfo, tempEntity);
                        string YValue = GetPropertyValue(temp_Y_PropertyInfo, tempEntity);
                        if ((XValue.Length > 0) && (YValue.Length > 0))
                        {
                            tempStringBuilder.Append("," + "{" + CommonSeparator + "name" + CommonSeparator + ValueSeparator + CommonSeparator + XValue + CommonSeparator +
                                                     "," + CommonSeparator + "data" + CommonSeparator + ValueSeparator + YValue + "}");
                        }
                        
                    }
                }
                if (tempStringBuilder.Length > 0)
                {
                    ResultStr = "data:[" + tempStringBuilder.ToString().Remove(0, 1) + "]";
                }
            }
            return ResultStr; 
        }
    }
}
