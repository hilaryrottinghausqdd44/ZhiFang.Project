using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReagentSys.Client.Common
{
    public class JsonSerializer:ZhiFang.Common.Public.JsonSerializer
    {
        public static string JsonDotNetSerializerNoEnterSpace(object o)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(o, Formatting.None, settings);
            return result;
        }
    }
}
