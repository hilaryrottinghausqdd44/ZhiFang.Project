using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace ZhiFang.BloodTransfusion.Common
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public static class JSONConfigHelp
    {
        private static bool noCache = true;
        /// <summary>
        /// 获取在线编辑器的配置文件
        /// </summary>
        public static string ConfigJson
        {
            get
            {
                string uploadJson = ZhiFang.Common.Public.ConfigHelper.GetConfigString("JSONConfig").Trim();
                if (string.IsNullOrEmpty(uploadJson)) uploadJson = "../ui/layui/config/JSONConfig.js";
                return uploadJson;
            }
        }

        private static JObject BuildItems()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath(JSONConfigHelp.ConfigJson));

            var obj = JObject.Parse(json);

            return obj;
        }

        public static JObject Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }
        private static JObject _Items;


        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }

}