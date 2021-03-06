using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace ZhiFang.WebAssist.Common
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
                string configF = ZhiFang.Common.Public.ConfigHelper.GetConfigString("SysConfig").Trim();
                if (string.IsNullOrEmpty(configF)) configF = "../SysConfig/Config.json";
                return configF;
            }
        }

        private static JObject BuildItems(string configF)
        {
            if (string.IsNullOrEmpty(configF))
                configF = JSONConfigHelp.ConfigJson;
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath(configF));

            var obj = JObject.Parse(json);

            return obj;
        }

        public static JObject Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems("");
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
        //public static String GetString(string key)
        //{
        //    return GetValue<String>(key);
        //}
        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }

        public static T GetValue<T>(string sysKye, string key)
        {
            var sysObj = Items[sysKye];
           
            return sysObj[key].Value<T>();
        }
        public static String GetString(string sysKye, string key)
        {
            return GetValue<String>(sysKye, key);
        }
    }

}