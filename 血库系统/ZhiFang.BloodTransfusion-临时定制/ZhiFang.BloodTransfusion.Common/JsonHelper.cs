using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZhiFang.BloodTransfusion.Common
{
    public class JsonHelper
    {

        #region 序列化/反序列化 
        //使用JavaScriptSerializer方式需要引入的命名空间，这个在程序集System.Web.Extensions.dll.中 
        //using System.Web.Script.Serialization;
        //注：可用[ScriptIgnore] 标记不序列化的属性
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            return jsonSerialize.Serialize(obj);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jresult"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(JObject jresult, string propertyName)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;//忽略空值
            try
            {
                if (!string.IsNullOrEmpty(propertyName))
                {
                    T result = jresult[propertyName].ToObject<T>(Newtonsoft.Json.JsonSerializer.Create(setting));
                    return result;
                }
                else
                {
                    T result = jresult.ToObject<T>(Newtonsoft.Json.JsonSerializer.Create(setting));
                    return result;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("JsonToObject错误,propertyName:" + propertyName + ",ex:" + ex.Message);
                //return default<T>;
                throw ex;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jresult"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IList<T> JsonToObjectList<T>(JObject jresult, string propertyName)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;//忽略空值
            var jorderDtlList = jresult.SelectToken(propertyName).ToList();
            IList<T> result = jresult[propertyName].ToObject<IList<T>>();
            return result;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string strJson)
        {
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            //jsonSerialize.Deserialize<dynamic>(strJson);  //取值model["Name"]; 要使用索引取值，不能使用对象.属性
            return jsonSerialize.Deserialize<T>(strJson);
        }

        public static string JsonSerializeObject<T>(T o)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(o);//, Formatting.None, settings
            return result;
        }
        public static T JsonDeserializeObject<T>(string o)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            T orderDoc2 = JsonConvert.DeserializeObject<T>(o);//settings
            return orderDoc2;
        }
        #endregion

        #region 转码/解码
        /// <summary>
        /// Url转码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }
        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }
        /// <summary>
        /// Base64转码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBase64(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// Base64字符串解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FromBase64(string input)
        {
            byte[] bytes = Convert.FromBase64String(input);
            return Convert.ToBase64String(bytes);
        }
        #endregion

        public static JObject GetPropertyInfo<T>(T t)
        {
            JObject jPostData = new JObject();
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;//忽略空值
            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties())
            {
                var obj = p.GetValue(t, null);
                //if (obj == null) obj =DBNull.Value;
                if (obj == null)
                {
                    obj = DBNull.Value;
                }
                if (obj != null && obj != DBNull.Value && p.Name != "DataTimeStamp")
                {
                    JToken aToken = JToken.FromObject(obj, Newtonsoft.Json.JsonSerializer.Create(setting));//, 
                    jPostData.Add(p.Name, aToken);
                }
            }
            return jPostData;
        }
        public static JObject GetPropertyInfo<T>(T t, bool isToString)
        {
            JObject jPostData = new JObject();
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;//忽略空值
            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties())
            {
                var obj = p.GetValue(t, null);
                //if (obj == null) obj =DBNull.Value;
                if (obj == null)
                {
                    obj = DBNull.Value;
                }
                if (obj != null && obj != DBNull.Value && p.Name != "DataTimeStamp")
                {
                    if (isToString) obj = obj.ToString();
                    JToken aToken = JToken.FromObject(obj, Newtonsoft.Json.JsonSerializer.Create(setting));//, 
                    jPostData.Add(p.Name, aToken);
                }
            }
            return jPostData;
        }
        public static JObject GetPropertyInfo<T>(T t, IList<string> notContains)
        {
            JObject jPostData = new JObject();
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;//忽略空值
            if (!notContains.Contains("DataTimeStamp")) notContains.Add("DataTimeStamp");

            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties())
            {
                var obj = p.GetValue(t, null);
                if (obj == null)
                {
                    obj = DBNull.Value;
                }
                //p.Name != "DataTimeStamp"
                if (obj != null && obj != DBNull.Value && !notContains.Contains(p.Name))
                {
                    JToken aToken = JToken.FromObject(obj, Newtonsoft.Json.JsonSerializer.Create(setting));//, 
                    jPostData.Add(p.Name, aToken);
                }
            }
            return jPostData;
        }
        public static JObject GetPropertyInfo<T>(T t, IList<string> notContains, bool isToString)
        {
            JObject jPostData = new JObject();
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;//忽略空值
            if (!notContains.Contains("DataTimeStamp")) notContains.Add("DataTimeStamp");

            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties())
            {
                var obj = p.GetValue(t, null);
                if (obj == null)
                {
                    obj = DBNull.Value;
                }
                //p.Name != "DataTimeStamp"
                if (obj != null && obj != DBNull.Value && !notContains.Contains(p.Name))
                {
                    if (isToString) obj = obj.ToString();

                    JToken aToken = JToken.FromObject(obj, Newtonsoft.Json.JsonSerializer.Create(setting));
                    jPostData.Add(p.Name, aToken);
                }
            }
            return jPostData;
        }

    }
}
