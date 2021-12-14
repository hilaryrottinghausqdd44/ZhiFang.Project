using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;

namespace ZhiFang.Tools
{
    public class JsonConvertClass : Newtonsoft.Json.JsonConverter
    {
        
        /// <summary>
        /// 是否允许转换
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            bool canConvert = false;
            switch (objectType.FullName)
            {
                case "System.Int64":
                    canConvert = true;
                    break;
                case "System.Single":
                    canConvert = true;
                    break;
                case "System.Double":
                    canConvert = true;
                    break;
                case "System.Decimal":
                    canConvert = true;
                    break;
                case "System.Byte[]":
                    canConvert = true;
                    break;
                case "System.DateTime":
                    canConvert = true;
                    break;
            }
            return canConvert;
        }

        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    return existingValue;
        //}

        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    if (value.GetType().FullName == "System.Int64" || value.GetType().FullName == "System.Single" || value.GetType().FullName == "System.Double" || value.GetType().FullName == "System.Decimal")
        //    {
        //        writer.WriteValue(value.ToString());
        //    }
        //    if (value.GetType().FullName == "System.Byte[]")
        //    {
        //        writer.WriteValue(JsonSerializer.ByteTypeToString((byte[])value));
        //    }
        //    if (value.GetType().FullName == "System.DateTime")
        //    {

        //        //writer.WriteValue(((System.DateTime)value).Ticks.ToString());
        //        writer.WriteValue(((System.DateTime)value).ToString(JsonSerializer.JsonDateTimeFormat, DateTimeFormatInfo.InvariantInfo));
        //    }
        //}

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 是否允许转换JSON字符串时调用
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
           if (value.GetType().FullName == "System.Int64" || value.GetType().FullName == "System.Single" || value.GetType().FullName == "System.Double" || value.GetType().FullName == "System.Decimal")
           {
               writer.WriteValue(value.ToString());
           }
           if (value.GetType().FullName == "System.Byte[]")
           {
               writer.WriteValue(JsonSerializer.ByteTypeToString((byte[])value));
           }
           if (value.GetType().FullName == "System.DateTime")
           {

               //writer.WriteValue(((System.DateTime)value).Ticks.ToString());
               writer.WriteValue(((System.DateTime)value).ToString(JsonSerializer.JsonDateTimeFormat, DateTimeFormatInfo.InvariantInfo));
           }
        }
    }

    public class JsonSerializer
    {
        public static string JsonDateTimeFormat = "yyyy/MM/dd HH:mm:ss";
        public static string JsonDotNetSerializer(object o)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(o, Formatting.Indented, settings);
            return result;
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
    }
}
